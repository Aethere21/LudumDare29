using LudumDare29.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using LudumDare29.Performance;

namespace LudumDare29.Factories
{
	public class BulletFactory : IEntityFactory
	{
		public static Bullet CreateNew ()
		{
			return CreateNew(null);
		}
		public static Bullet CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			Bullet instance = null;
			instance = new Bullet(mContentManagerName, false);
			instance.AddToManagers(layer);
			if (mScreenListReference != null)
			{
				mScreenListReference.Add(instance);
			}
			if (EntitySpawned != null)
			{
				EntitySpawned(instance);
			}
			return instance;
		}
		
		public static void Initialize (PositionedObjectList<Bullet> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
		}
		
		public static void Destroy ()
		{
			mContentManagerName = null;
			mScreenListReference = null;
			mPool.Clear();
			EntitySpawned = null;
		}
		
		private static void FactoryInitialize ()
		{
			const int numberToPreAllocate = 20;
			for (int i = 0; i < numberToPreAllocate; i++)
			{
				Bullet instance = new Bullet(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (Bullet objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (Bullet objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<Bullet> mScreenListReference;
			static PoolList<Bullet> mPool = new PoolList<Bullet>();
			public static Action<Bullet> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return BulletFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return BulletFactory.CreateNew(layer);
			}
			public static PositionedObjectList<Bullet> ScreenListReference
			{
				get
				{
					return mScreenListReference;
				}
				set
				{
					mScreenListReference = value;
				}
			}
			static BulletFactory mSelf;
			public static BulletFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new BulletFactory();
					}
					return mSelf;
				}
			}
	}
}
