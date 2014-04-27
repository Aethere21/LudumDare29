using System.Collections.Generic;
using System.Threading;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using FlatRedBall.Localization;

namespace LudumDare29
{
	public static partial class GlobalContent
	{
		struct NamedDelegate
		{
			public string Name;
			public System.Action LoadMethod;
		}
		
		static List<NamedDelegate> LoadMethodList = new List<NamedDelegate>();
		
		
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		public static bool IsInitialized { get; private set; }
		public static bool ShouldStopLoading { get; set; }
		static string ContentManagerName = "Global";
		public static void Initialize ()
		{
			
			NamedDelegate namedDelegate = new NamedDelegate();
			
			#if WINDOWS_8
			System.Threading.Tasks.Task.Run((System.Action)AsyncInitialize);
			#else
			ThreadStart threadStart = new ThreadStart(AsyncInitialize);
			Thread thread = new Thread(threadStart);
			thread.Name = "GlobalContent Async load";
			thread.Start();
			#endif
		}
		
		static void RequestContentLoad (string contentName)
		{
			lock (LoadMethodList)
			{
				int index = -1;
				for (int i = 0; i < LoadMethodList.Count; i++)
				{
					if (LoadMethodList[i].Name == contentName)
					{
						index = i;
						break;
					}
				}
				if (index != -1)
				{
					NamedDelegate delegateToShuffle = LoadMethodList[index];
					LoadMethodList.RemoveAt(index);
					LoadMethodList.Insert(0, delegateToShuffle);
				}
			}
		}
		
		static void AsyncInitialize ()
		{
			#if XBOX360
			// We can not use threads 0 or 2
			// Async screen loading uses thread 4, so we'll use 3 here
			Thread.CurrentThread.SetProcessorAffinity(3);
			#endif
			bool shouldLoop = LoadMethodList.Count != 0;
			while (shouldLoop)
			{
				System.Action action = null;
				lock (LoadMethodList)
				{
					action = LoadMethodList[0].LoadMethod;
					LoadMethodList.RemoveAt(0);
					shouldLoop = LoadMethodList.Count != 0 && !ShouldStopLoading;
				}
				action();
			}
			IsInitialized = true;
			
		}
		public static void Reload (object whatToReload)
		{
		}
		
		
	}
}
