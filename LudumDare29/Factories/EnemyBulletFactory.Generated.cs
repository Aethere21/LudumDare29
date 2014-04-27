using LudumDare29.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using LudumDare29.Performance;

namespace LudumDare29.Factories
{
	public class EnemyBulletFactory : IEntityFactory
	{
		public static EnemyBullet CreateNew ()
		{
			return CreateNew(null);
		}
		public static EnemyBullet CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			EnemyBullet instance = null;
			instance = new EnemyBullet(mContentManagerName, false);
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
		
		public static void Initialize (PositionedObjectList<EnemyBullet> listFromScreen, string contentManager)
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
				EnemyBullet instance = new EnemyBullet(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (EnemyBullet objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (EnemyBullet objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<EnemyBullet> mScreenListReference;
			static PoolList<EnemyBullet> mPool = new PoolList<EnemyBullet>();
			public static Action<EnemyBullet> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return EnemyBulletFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return EnemyBulletFactory.CreateNew(layer);
			}
			public static PositionedObjectList<EnemyBullet> ScreenListReference
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
			static EnemyBulletFactory mSelf;
			public static EnemyBulletFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new EnemyBulletFactory();
					}
					return mSelf;
				}
			}
	}
}
