
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
	public partial class SignEntity : PositionedObject, IDestroyable
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
		protected static Microsoft.Xna.Framework.Graphics.Texture2D MazetechFont_1;
		protected static FlatRedBall.Graphics.BitmapFont MazetechFont;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D MazetechFont_0;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D MessageTexture;
		
		private FlatRedBall.Graphics.Text TextInstance;
		private FlatRedBall.Sprite SpriteInstance;
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
		protected Layer LayerProvidedByContainer = null;

        public SignEntity()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public SignEntity(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public SignEntity(string contentManagerName, bool addToManagers) :
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
			TextInstance = new FlatRedBall.Graphics.Text();
			TextInstance.Name = "TextInstance";
			SpriteInstance = new FlatRedBall.Sprite();
			SpriteInstance.Name = "SpriteInstance";
			mCollision = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mCollision.Name = "mCollision";
			
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
			TextManager.AddToLayer(TextInstance, LayerProvidedByContainer);
			if (TextInstance.Font != null)
			{
				TextInstance.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
			ShapeManager.AddToLayer(mCollision, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			TextManager.AddToLayer(TextInstance, LayerProvidedByContainer);
			if (TextInstance.Font != null)
			{
				TextInstance.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
			ShapeManager.AddToLayer(mCollision, LayerProvidedByContainer);
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
			
			if (TextInstance != null)
			{
				TextManager.RemoveText(TextInstance);
			}
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSprite(SpriteInstance);
			}
			if (Collision != null)
			{
				ShapeManager.Remove(Collision);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (TextInstance.Parent == null)
			{
				TextInstance.CopyAbsoluteToRelative();
				TextInstance.AttachTo(this, false);
			}
			TextInstance.DisplayText = "TEEEST";
			TextInstance.Font = MazetechFont;
			TextInstance.NewLineDistance = 9f;
			TextInstance.Scale = 6f;
			TextInstance.Spacing = 6f;
			if (TextInstance.Parent == null)
			{
				TextInstance.Z = 15f;
			}
			else
			{
				TextInstance.RelativeZ = 15f;
			}
			TextInstance.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Center;
			TextInstance.VerticalAlignment = FlatRedBall.Graphics.VerticalAlignment.Center;
			if (SpriteInstance.Parent == null)
			{
				SpriteInstance.CopyAbsoluteToRelative();
				SpriteInstance.AttachTo(this, false);
			}
			SpriteInstance.Texture = MessageTexture;
			SpriteInstance.TextureScale = 2.5f;
			if (SpriteInstance.Parent == null)
			{
				SpriteInstance.Z = -10f;
			}
			else
			{
				SpriteInstance.RelativeZ = -10f;
			}
			SpriteInstance.Visible = true;
			if (mCollision.Parent == null)
			{
				mCollision.CopyAbsoluteToRelative();
				mCollision.AttachTo(this, false);
			}
			Collision.Color = Color.Red;
			Collision.Height = 200f;
			Collision.Visible = true;
			Collision.Width = 32f;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			if (TextInstance != null)
			{
				TextManager.RemoveTextOneWay(TextInstance);
			}
			if (SpriteInstance != null)
			{
				SpriteManager.RemoveSpriteOneWay(SpriteInstance);
			}
			if (Collision != null)
			{
				ShapeManager.RemoveOneWay(Collision);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			TextInstance.DisplayText = "TEEEST";
			TextInstance.Font = MazetechFont;
			TextInstance.NewLineDistance = 9f;
			TextInstance.Scale = 6f;
			TextInstance.Spacing = 6f;
			if (TextInstance.Parent == null)
			{
				TextInstance.Z = 15f;
			}
			else
			{
				TextInstance.RelativeZ = 15f;
			}
			TextInstance.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Center;
			TextInstance.VerticalAlignment = FlatRedBall.Graphics.VerticalAlignment.Center;
			SpriteInstance.Texture = MessageTexture;
			SpriteInstance.TextureScale = 2.5f;
			if (SpriteInstance.Parent == null)
			{
				SpriteInstance.Z = -10f;
			}
			else
			{
				SpriteInstance.RelativeZ = -10f;
			}
			SpriteInstance.Visible = true;
			mCollision.Color = Color.Red;
			mCollision.Height = 200f;
			mCollision.Visible = true;
			mCollision.Width = 32f;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			TextManager.ConvertToManuallyUpdated(TextInstance);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("SignEntityStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/signentity/mazetechfont_1.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MazetechFont_1 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/signentity/mazetechfont_1.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.BitmapFont>(@"content/entities/signentity/mazetechfont.fnt", ContentManagerName))
				{
					registerUnload = true;
				}
				MazetechFont = FlatRedBallServices.Load<FlatRedBall.Graphics.BitmapFont>(@"content/entities/signentity/mazetechfont.fnt", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/signentity/mazetechfont_0.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MazetechFont_0 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/signentity/mazetechfont_0.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/signentity/messagetexture.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MessageTexture = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/signentity/messagetexture.png", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("SignEntityStaticUnload", UnloadStaticContent);
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
				if (MazetechFont_1 != null)
				{
					MazetechFont_1= null;
				}
				if (MazetechFont != null)
				{
					MazetechFont= null;
				}
				if (MazetechFont_0 != null)
				{
					MazetechFont_0= null;
				}
				if (MessageTexture != null)
				{
					MessageTexture= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "MazetechFont_1":
					return MazetechFont_1;
				case  "MazetechFont":
					return MazetechFont;
				case  "MazetechFont_0":
					return MazetechFont_0;
				case  "MessageTexture":
					return MessageTexture;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "MazetechFont_1":
					return MazetechFont_1;
				case  "MazetechFont":
					return MazetechFont;
				case  "MazetechFont_0":
					return MazetechFont_0;
				case  "MessageTexture":
					return MessageTexture;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "MazetechFont_1":
					return MazetechFont_1;
				case  "MazetechFont":
					return MazetechFont;
				case  "MazetechFont_0":
					return MazetechFont_0;
				case  "MessageTexture":
					return MessageTexture;
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
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(TextInstance);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Collision);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(TextInstance);
			}
			TextManager.AddToLayer(TextInstance, layerToMoveTo);
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
