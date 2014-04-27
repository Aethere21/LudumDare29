
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using LudumDare29.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using LudumDare29.Performance;
using LudumDare29.Entities;
using LudumDare29.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace LudumDare29.Entities
{
	public partial class EnemyBullet : PositionedObject, IDestroyable, IPoolable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimationChainListFile;
		
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mCollision;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle Collision
		{
			get
			{
				return mCollision;
			}
			private set
			{
				mCollision = value;
			}
		}
		private FlatRedBall.Sprite SpriteInstance;
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public EnemyBullet()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public EnemyBullet(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public EnemyBullet(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			mCollision = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mCollision.Name = "mCollision";
			SpriteInstance = new FlatRedBall.Sprite();
			SpriteInstance.Name = "SpriteInstance";
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void ReAddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			ShapeManager.AddToLayer(mCollision, LayerProvidedByContainer);
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			ShapeManager.AddToLayer(mCollision, LayerProvidedByContainer);
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			if (Used)
			{
				EnemyBulletFactory.MakeUnused(this, false);
			}
			
			if (Collision != null)
			{
				ShapeManager.RemoveOneWay(Collision);
			}
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSpriteOneWay(SpriteInstance);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (mCollision.Parent == null)
			{
				mCollision.CopyAbsoluteToRelative();
				mCollision.AttachTo(this, false);
			}
			Collision.Width = 9f;
			Collision.Height = 3f;
			Collision.Visible = false;
			if (SpriteInstance.Parent == null)
			{
				SpriteInstance.CopyAbsoluteToRelative();
				SpriteInstance.AttachTo(this, false);
			}
			SpriteInstance.TextureScale = 1.5f;
			SpriteInstance.AnimationChains = AnimationChainListFile;
			SpriteInstance.CurrentChainName = "Bullet";
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			if (Collision != null)
			{
				ShapeManager.RemoveOneWay(Collision);
			}
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSpriteOneWay(SpriteInstance);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			mCollision.Width = 9f;
			mCollision.Height = 3f;
			mCollision.Visible = false;
			SpriteInstance.TextureScale = 1.5f;
			SpriteInstance.AnimationChains = AnimationChainListFile;
			SpriteInstance.CurrentChainName = "Bullet";
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("EnemyBulletStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/enemybullet/animationchainlistfile.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimationChainListFile = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/enemybullet/animationchainlistfile.achx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("EnemyBulletStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (AnimationChainListFile != null)
				{
					AnimationChainListFile= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
			}
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Collision);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(SpriteInstance);
			}
			SpriteManager.AddToLayer(SpriteInstance, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
