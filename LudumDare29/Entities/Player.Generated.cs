
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using LudumDare29.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using LudumDare29.Entities;
using LudumDare29.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;
using Microsoft.Xna.Framework.Graphics;

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
	public partial class Player : LudumDare29.Entities.PlatformerCharacterBase, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Entities.PlatformerCharacterBase.ContentManagerName;}
            set{ Entities.PlatformerCharacterBase.ContentManagerName = value;}
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimationChainListFile;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D PlayerTexture;
		
		private FlatRedBall.Sprite SpriteInstance;
		private FlatRedBall.Sprite HandSprite;
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mGunRectangle;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle GunRectangle
		{
			get
			{
				return mGunRectangle;
			}
			private set
			{
				mGunRectangle = value;
			}
		}
		public event EventHandler BeforeGroundMovementSet;
		public event EventHandler AfterGroundMovementSet;
		public override LudumDare29.DataTypes.MovementValues GroundMovement
		{
			set
			{
				if (BeforeGroundMovementSet != null)
				{
					BeforeGroundMovementSet(this, null);
				}
				base.GroundMovement = value;
				if (AfterGroundMovementSet != null)
				{
					AfterGroundMovementSet(this, null);
				}
			}
			get
			{
				return base.GroundMovement;
			}
		}
		public event EventHandler BeforeAirMovementSet;
		public event EventHandler AfterAirMovementSet;
		public override LudumDare29.DataTypes.MovementValues AirMovement
		{
			set
			{
				if (BeforeAirMovementSet != null)
				{
					BeforeAirMovementSet(this, null);
				}
				base.AirMovement = value;
				if (AfterAirMovementSet != null)
				{
					AfterAirMovementSet(this, null);
				}
			}
			get
			{
				return base.AirMovement;
			}
		}
		public event EventHandler BeforeAfterDoubleJumpSet;
		public event EventHandler AfterAfterDoubleJumpSet;
		public override LudumDare29.DataTypes.MovementValues AfterDoubleJump
		{
			set
			{
				if (BeforeAfterDoubleJumpSet != null)
				{
					BeforeAfterDoubleJumpSet(this, null);
				}
				base.AfterDoubleJump = value;
				if (AfterAfterDoubleJumpSet != null)
				{
					AfterAfterDoubleJumpSet(this, null);
				}
			}
			get
			{
				return base.AfterDoubleJump;
			}
		}

        public Player()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Player(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Player(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			SpriteInstance = new FlatRedBall.Sprite();
			SpriteInstance.Name = "SpriteInstance";
			HandSprite = new FlatRedBall.Sprite();
			HandSprite.Name = "HandSprite";
			mGunRectangle = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mGunRectangle.Name = "mGunRectangle";
			
			base.InitializeEntity(addToManagers);


		}

// Generated AddToManagers
		public override void ReAddToManagers (Layer layerToAddTo)
		{
			base.ReAddToManagers(layerToAddTo);
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
			SpriteManager.AddToLayer(HandSprite, LayerProvidedByContainer);
			ShapeManager.AddToLayer(mGunRectangle, LayerProvidedByContainer);
		}
		public override void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
			SpriteManager.AddToLayer(HandSprite, LayerProvidedByContainer);
			ShapeManager.AddToLayer(mGunRectangle, LayerProvidedByContainer);
			base.AddToManagers(layerToAddTo);
			CustomInitialize();
		}

		public override void Activity()
		{
			// Generated Activity
			base.Activity();
			
			CustomActivity();
			
			// After Custom Activity
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();
			
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSprite(SpriteInstance);
			}
			if (HandSprite != null)
			{
				SpriteManager.RemoveSprite(HandSprite);
			}
			if (GunRectangle != null)
			{
				ShapeManager.Remove(GunRectangle);
			}


			CustomDestroy();
		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			if (mCollision.Parent == null)
			{
				mCollision.CopyAbsoluteToRelative();
				mCollision.AttachTo(this, false);
			}
			Collision.Height = 48f;
			Collision.Width = 28f;
			if (SpriteInstance.Parent == null)
			{
				SpriteInstance.CopyAbsoluteToRelative();
				SpriteInstance.AttachTo(this, false);
			}
			SpriteInstance.AnimationChains = AnimationChainListFile;
			SpriteInstance.TextureScale = 1f;
			SpriteInstance.PixelSize = 1.5f;
			SpriteInstance.CurrentChainName = "StandRight";
			if (HandSprite.Parent == null)
			{
				HandSprite.CopyAbsoluteToRelative();
				HandSprite.AttachTo(this, false);
			}
			HandSprite.AnimationChains = AnimationChainListFile;
			HandSprite.TextureScale = 1f;
			HandSprite.PixelSize = 1.5f;
			HandSprite.CurrentChainName = "Hand";
			if (mGunRectangle.Parent == null)
			{
				mGunRectangle.CopyAbsoluteToRelative();
				mGunRectangle.AttachTo(this, false);
			}
			GunRectangle.Width = 5f;
			GunRectangle.Height = 5f;
			GunRectangle.Visible = false;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp (Layer layerToAddTo)
		{
			base.AddToManagersBottomUp(layerToAddTo);
		}
		public override void RemoveFromManagers ()
		{
			base.RemoveFromManagers();
			base.RemoveFromManagers();
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSpriteOneWay(SpriteInstance);
			}
			if (HandSprite != null)
			{
				SpriteManager.RemoveSpriteOneWay(HandSprite);
			}
			if (GunRectangle != null)
			{
				ShapeManager.RemoveOneWay(GunRectangle);
			}
		}
		public override void AssignCustomVariables (bool callOnContainedElements)
		{
			base.AssignCustomVariables(callOnContainedElements);
			if (callOnContainedElements)
			{
			}
			mCollision.Height = 48f;
			mCollision.Width = 28f;
			SpriteInstance.AnimationChains = AnimationChainListFile;
			SpriteInstance.TextureScale = 1f;
			SpriteInstance.PixelSize = 1.5f;
			SpriteInstance.CurrentChainName = "StandRight";
			HandSprite.AnimationChains = AnimationChainListFile;
			HandSprite.TextureScale = 1f;
			HandSprite.PixelSize = 1.5f;
			HandSprite.CurrentChainName = "Hand";
			mGunRectangle.Width = 5f;
			mGunRectangle.Height = 5f;
			mGunRectangle.Visible = false;
			GroundMovement = Player.MovementValues["ImmediateVelocityOnGround"];
			AirMovement = Player.MovementValues["ImmediateVelocityBeforeDoubleJump"];
			AfterDoubleJump = Player.MovementValues["ImmediateVelocityInAir"];
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
			SpriteManager.ConvertToManuallyUpdated(HandSprite);
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			PlatformerCharacterBase.LoadStaticContent(contentManagerName);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/player/animationchainlistfile.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimationChainListFile = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/player/animationchainlistfile.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/player/playertexture.png", ContentManagerName))
				{
					registerUnload = true;
				}
				PlayerTexture = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/player/playertexture.png", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static new void UnloadStaticContent ()
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
				if (PlayerTexture != null)
				{
					PlayerTexture= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
				case  "PlayerTexture":
					return PlayerTexture;
			}
			return null;
		}
		public static new object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
				case  "PlayerTexture":
					return PlayerTexture;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimationChainListFile":
					return AnimationChainListFile;
				case  "PlayerTexture":
					return PlayerTexture;
			}
			return null;
		}
		public override void SetToIgnorePausing ()
		{
			base.SetToIgnorePausing();
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Collision);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(HandSprite);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(GunRectangle);
		}
		public override void MoveToLayer (Layer layerToMoveTo)
		{
			base.MoveToLayer(layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(SpriteInstance);
			}
			SpriteManager.AddToLayer(SpriteInstance, layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(HandSprite);
			}
			SpriteManager.AddToLayer(HandSprite, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
