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
		
		
		static Microsoft.Xna.Framework.Graphics.Texture2D menemyTextures;
		//Blocks the thread on request of enemyTextures until it has been loaded
		static ManualResetEvent menemyTexturesMre = new ManualResetEvent(false);
		// Used to lock getter and setter so that enemyTextures can be set on any thread even if its load is in progrss
		static object menemyTextures_Lock = new object();
		public static Microsoft.Xna.Framework.Graphics.Texture2D enemyTextures
		{
			get
			{
				lock (menemyTextures_Lock)
				{
					bool isBlocking = !menemyTexturesMre.WaitOne(0);
					if (isBlocking)
					{
						RequestContentLoad("GlobalContent/Textures/enemyTextures.png");
					}
					menemyTexturesMre.WaitOne();
					return menemyTextures;
				}
			}
			set
			{
				lock (menemyTextures_Lock)
				{
					menemyTextures = value;
					menemyTexturesMre.Set();
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "enemyTextures":
					return enemyTextures;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "enemyTextures":
					return enemyTextures;
			}
			return null;
		}
		public static bool IsInitialized { get; private set; }
		public static bool ShouldStopLoading { get; set; }
		static string ContentManagerName = "Global";
		public static void Initialize ()
		{
			
			NamedDelegate namedDelegate = new NamedDelegate();
			namedDelegate.Name = "GlobalContent/Textures/enemyTextures.png";
			namedDelegate.LoadMethod = LoadGlobalContent_Textures_enemyTextures_png;
			LoadMethodList.Add( namedDelegate );
			
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
		static void LoadGlobalContent_Textures_enemyTextures_png ()
		{
			menemyTextures = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/textures/enemytextures.png", ContentManagerName);
			menemyTexturesMre.Set();
		}
		public static void Reload (object whatToReload)
		{
		}
		
		
	}
}
