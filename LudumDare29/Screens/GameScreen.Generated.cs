using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

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
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using LudumDare29.Entities;
using LudumDare29.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math;
using FlatRedBall.TileGraphics;

namespace LudumDare29.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		protected static FlatRedBall.TileGraphics.LayeredTileMap StartLevel;
		protected static FlatRedBall.TileGraphics.LayeredTileMap Level2;
		
		private FlatRedBall.TileGraphics.LayeredTileMap TiledMap;
		private FlatRedBall.Math.Geometry.ShapeCollection TileCollisionShapes;
		private PositionedObjectList<LudumDare29.Entities.Bullet> BulletList;
		private PositionedObjectList<LudumDare29.Entities.NextLevelEntity> NextLevelEntityList;
		private LudumDare29.Entities.Player PlayerInstance;
		private PositionedObjectList<LudumDare29.Entities.SignEntity> SignEntityList;
		private PositionedObjectList<LudumDare29.Entities.ActionEntity> ActionEntityList;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			TiledMap = new FlatRedBall.TileGraphics.LayeredTileMap();
			TiledMap.Name = "TiledMap";
			TileCollisionShapes = new FlatRedBall.Math.Geometry.ShapeCollection();
			TileCollisionShapes.Name = "TileCollisionShapes";
			BulletList = new PositionedObjectList<LudumDare29.Entities.Bullet>();
			BulletList.Name = "BulletList";
			NextLevelEntityList = new PositionedObjectList<LudumDare29.Entities.NextLevelEntity>();
			NextLevelEntityList.Name = "NextLevelEntityList";
			PlayerInstance = new LudumDare29.Entities.Player(ContentManagerName, false);
			PlayerInstance.Name = "PlayerInstance";
			SignEntityList = new PositionedObjectList<LudumDare29.Entities.SignEntity>();
			SignEntityList.Name = "SignEntityList";
			ActionEntityList = new PositionedObjectList<LudumDare29.Entities.ActionEntity>();
			ActionEntityList.Name = "ActionEntityList";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			BulletFactory.Initialize(BulletList, ContentManagerName);
			TileCollisionShapes.AddToManagers();
			PlayerInstance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = BulletList.Count - 1; i > -1; i--)
				{
					if (i < BulletList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						BulletList[i].Activity();
					}
				}
				for (int i = NextLevelEntityList.Count - 1; i > -1; i--)
				{
					if (i < NextLevelEntityList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						NextLevelEntityList[i].Activity();
					}
				}
				PlayerInstance.Activity();
				for (int i = SignEntityList.Count - 1; i > -1; i--)
				{
					if (i < SignEntityList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						SignEntityList[i].Activity();
					}
				}
				for (int i = ActionEntityList.Count - 1; i > -1; i--)
				{
					if (i < ActionEntityList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						ActionEntityList[i].Activity();
					}
				}
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			BulletFactory.Destroy();
			StartLevel.Destroy();
			StartLevel = null;
			Level2.Destroy();
			Level2 = null;
			
			BulletList.MakeOneWay();
			NextLevelEntityList.MakeOneWay();
			SignEntityList.MakeOneWay();
			ActionEntityList.MakeOneWay();
			if (TileCollisionShapes != null)
			{
				TileCollisionShapes.RemoveFromManagers(ContentManagerName != "Global");
			}
			for (int i = BulletList.Count - 1; i > -1; i--)
			{
				BulletList[i].Destroy();
			}
			for (int i = NextLevelEntityList.Count - 1; i > -1; i--)
			{
				NextLevelEntityList[i].Destroy();
			}
			if (PlayerInstance != null)
			{
				PlayerInstance.Destroy();
				PlayerInstance.Detach();
			}
			for (int i = SignEntityList.Count - 1; i > -1; i--)
			{
				SignEntityList[i].Destroy();
			}
			for (int i = ActionEntityList.Count - 1; i > -1; i--)
			{
				ActionEntityList[i].Destroy();
			}
			BulletList.MakeTwoWay();
			NextLevelEntityList.MakeTwoWay();
			SignEntityList.MakeTwoWay();
			ActionEntityList.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			if (TileCollisionShapes != null)
			{
				TileCollisionShapes.RemoveFromManagers(false);
			}
			for (int i = BulletList.Count - 1; i > -1; i--)
			{
				BulletList[i].Destroy();
			}
			for (int i = NextLevelEntityList.Count - 1; i > -1; i--)
			{
				NextLevelEntityList[i].Destroy();
			}
			PlayerInstance.RemoveFromManagers();
			for (int i = SignEntityList.Count - 1; i > -1; i--)
			{
				SignEntityList[i].Destroy();
			}
			for (int i = ActionEntityList.Count - 1; i > -1; i--)
			{
				ActionEntityList[i].Destroy();
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				PlayerInstance.AssignCustomVariables(true);
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			for (int i = 0; i < BulletList.Count; i++)
			{
				BulletList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < NextLevelEntityList.Count; i++)
			{
				NextLevelEntityList[i].ConvertToManuallyUpdated();
			}
			PlayerInstance.ConvertToManuallyUpdated();
			for (int i = 0; i < SignEntityList.Count; i++)
			{
				SignEntityList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < ActionEntityList.Count; i++)
			{
				ActionEntityList[i].ConvertToManuallyUpdated();
			}
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
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
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.TileGraphics.LayeredTileMap>(@"content/screens/gamescreen/startlevel.tilb", contentManagerName))
			{
			}
			StartLevel = LayeredTileMap.FromReducedTileMapInfo("content/screens/gamescreen/startlevel.tilb", contentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.TileGraphics.LayeredTileMap>(@"content/screens/gamescreen/level2.tilb", contentManagerName))
			{
			}
			Level2 = LayeredTileMap.FromReducedTileMapInfo("content/screens/gamescreen/level2.tilb", contentManagerName);
			LudumDare29.Entities.Player.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "StartLevel":
					return StartLevel;
				case  "Level2":
					return Level2;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "StartLevel":
					return StartLevel;
				case  "Level2":
					return Level2;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "StartLevel":
					return StartLevel;
				case  "Level2":
					return Level2;
			}
			return null;
		}


	}
}
