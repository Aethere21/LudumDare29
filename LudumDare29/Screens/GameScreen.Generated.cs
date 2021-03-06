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
using FlatRedBall.Graphics;
using FlatRedBall.TileGraphics;

namespace LudumDare29.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static FlatRedBall.TileGraphics.LayeredTileMap mStartLevel;
		static string mLastContentManagerForStartLevel;
		public static FlatRedBall.TileGraphics.LayeredTileMap StartLevel
		{
			get
			{
				if (mStartLevel == null || mLastContentManagerForStartLevel != "GameScreen")
				{
					mLastContentManagerForStartLevel = "GameScreen";
					mStartLevel = LayeredTileMap.FromReducedTileMapInfo("content/screens/gamescreen/startlevel.tilb", "GameScreen");
				}
				return mStartLevel;
			}
		}
		static FlatRedBall.TileGraphics.LayeredTileMap mLevel2;
		static string mLastContentManagerForLevel2;
		public static FlatRedBall.TileGraphics.LayeredTileMap Level2
		{
			get
			{
				if (mLevel2 == null || mLastContentManagerForLevel2 != "GameScreen")
				{
					mLastContentManagerForLevel2 = "GameScreen";
					mLevel2 = LayeredTileMap.FromReducedTileMapInfo("content/screens/gamescreen/level2.tilb", "GameScreen");
				}
				return mLevel2;
			}
		}
		static FlatRedBall.TileGraphics.LayeredTileMap mLevel3;
		static string mLastContentManagerForLevel3;
		public static FlatRedBall.TileGraphics.LayeredTileMap Level3
		{
			get
			{
				if (mLevel3 == null || mLastContentManagerForLevel3 != "GameScreen")
				{
					mLastContentManagerForLevel3 = "GameScreen";
					mLevel3 = LayeredTileMap.FromScene("content/screens/gamescreen/level3.scnx", "GameScreen");
				}
				return mLevel3;
			}
		}
		static FlatRedBall.TileGraphics.LayeredTileMap mLevel4;
		static string mLastContentManagerForLevel4;
		public static FlatRedBall.TileGraphics.LayeredTileMap Level4
		{
			get
			{
				if (mLevel4 == null || mLastContentManagerForLevel4 != "GameScreen")
				{
					mLastContentManagerForLevel4 = "GameScreen";
					mLevel4 = LayeredTileMap.FromReducedTileMapInfo("content/screens/gamescreen/level4.tilb", "GameScreen");
				}
				return mLevel4;
			}
		}
		static FlatRedBall.TileGraphics.LayeredTileMap mEndLevel;
		static string mLastContentManagerForEndLevel;
		public static FlatRedBall.TileGraphics.LayeredTileMap EndLevel
		{
			get
			{
				if (mEndLevel == null || mLastContentManagerForEndLevel != "GameScreen")
				{
					mLastContentManagerForEndLevel = "GameScreen";
					mEndLevel = LayeredTileMap.FromReducedTileMapInfo("content/screens/gamescreen/endlevel.tilb", "GameScreen");
				}
				return mEndLevel;
			}
		}
		
		private FlatRedBall.TileGraphics.LayeredTileMap TiledMap;
		private FlatRedBall.Math.Geometry.ShapeCollection TileCollisionShapes;
		private PositionedObjectList<LudumDare29.Entities.Bullet> BulletList;
		private PositionedObjectList<LudumDare29.Entities.NextLevelEntity> NextLevelEntityList;
		private LudumDare29.Entities.Player PlayerInstance;
		private PositionedObjectList<LudumDare29.Entities.SignEntity> SignEntityList;
		private PositionedObjectList<LudumDare29.Entities.ActionEntity> ActionEntityList;
		private PositionedObjectList<LudumDare29.Entities.EnemyBullet> EnemyBulletList;
		private PositionedObjectList<LudumDare29.Entities.EnemyCorner> EnemyCornerList;
		private PositionedObjectList<LudumDare29.Entities.GroundEnemy> GroundEnemyList;
		private FlatRedBall.Math.Geometry.ShapeCollection EntityCollisionShapes;
		private FlatRedBall.Math.Geometry.ShapeCollection EnemyCollisionGround;
		private FlatRedBall.Graphics.Text TheEndText;

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
			EnemyBulletList = new PositionedObjectList<LudumDare29.Entities.EnemyBullet>();
			EnemyBulletList.Name = "EnemyBulletList";
			EnemyCornerList = new PositionedObjectList<LudumDare29.Entities.EnemyCorner>();
			EnemyCornerList.Name = "EnemyCornerList";
			GroundEnemyList = new PositionedObjectList<LudumDare29.Entities.GroundEnemy>();
			GroundEnemyList.Name = "GroundEnemyList";
			EntityCollisionShapes = new FlatRedBall.Math.Geometry.ShapeCollection();
			EntityCollisionShapes.Name = "EntityCollisionShapes";
			EnemyCollisionGround = new FlatRedBall.Math.Geometry.ShapeCollection();
			EnemyCollisionGround.Name = "EnemyCollisionGround";
			TheEndText = new FlatRedBall.Graphics.Text();
			TheEndText.Name = "TheEndText";
			
			
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
			EnemyBulletFactory.Initialize(EnemyBulletList, ContentManagerName);
			TiledMap.AddToManagers();
			TileCollisionShapes.AddToManagers();
			PlayerInstance.AddToManagers(mLayer);
			EntityCollisionShapes.AddToManagers();
			EnemyCollisionGround.AddToManagers();
			TextManager.AddText(TheEndText); if(TheEndText.Font != null) TheEndText.SetPixelPerfectScale(SpriteManager.Camera);
			if (TheEndText.Font != null)
			{
				TheEndText.SetPixelPerfectScale(mLayer);
			}
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
				for (int i = EnemyBulletList.Count - 1; i > -1; i--)
				{
					if (i < EnemyBulletList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						EnemyBulletList[i].Activity();
					}
				}
				for (int i = EnemyCornerList.Count - 1; i > -1; i--)
				{
					if (i < EnemyCornerList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						EnemyCornerList[i].Activity();
					}
				}
				for (int i = GroundEnemyList.Count - 1; i > -1; i--)
				{
					if (i < GroundEnemyList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						GroundEnemyList[i].Activity();
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
			EnemyBulletFactory.Destroy();
			if (mStartLevel != null)
			{
				mStartLevel.Destroy();
				mStartLevel = null;
			}
			if (mLevel2 != null)
			{
				mLevel2.Destroy();
				mLevel2 = null;
			}
			if (mLevel3 != null)
			{
				mLevel3.Destroy();
				mLevel3 = null;
			}
			if (mLevel4 != null)
			{
				mLevel4.Destroy();
				mLevel4 = null;
			}
			if (mEndLevel != null)
			{
				mEndLevel.Destroy();
				mEndLevel = null;
			}
			
			BulletList.MakeOneWay();
			NextLevelEntityList.MakeOneWay();
			SignEntityList.MakeOneWay();
			ActionEntityList.MakeOneWay();
			EnemyBulletList.MakeOneWay();
			EnemyCornerList.MakeOneWay();
			GroundEnemyList.MakeOneWay();
			if (TiledMap != null)
			{
				TiledMap.Destroy();
			}
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
			for (int i = EnemyBulletList.Count - 1; i > -1; i--)
			{
				EnemyBulletList[i].Destroy();
			}
			for (int i = EnemyCornerList.Count - 1; i > -1; i--)
			{
				EnemyCornerList[i].Destroy();
			}
			for (int i = GroundEnemyList.Count - 1; i > -1; i--)
			{
				GroundEnemyList[i].Destroy();
			}
			if (EntityCollisionShapes != null)
			{
				EntityCollisionShapes.RemoveFromManagers(ContentManagerName != "Global");
			}
			if (EnemyCollisionGround != null)
			{
				EnemyCollisionGround.RemoveFromManagers(ContentManagerName != "Global");
			}
			if (TheEndText != null)
			{
				TextManager.RemoveText(TheEndText);
			}
			BulletList.MakeTwoWay();
			NextLevelEntityList.MakeTwoWay();
			SignEntityList.MakeTwoWay();
			ActionEntityList.MakeTwoWay();
			EnemyBulletList.MakeTwoWay();
			EnemyCornerList.MakeTwoWay();
			GroundEnemyList.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (TheEndText.Parent == null)
			{
				TheEndText.CopyAbsoluteToRelative();
				TheEndText.RelativeZ += -40;
				TheEndText.AttachTo(SpriteManager.Camera, false);
			}
			TheEndText.DisplayText = "The End.\nPress Enter To Go To Main Menu!";
			if (TheEndText.Parent == null)
			{
				TheEndText.X = 0f;
			}
			else
			{
				TheEndText.RelativeX = 0f;
			}
			if (TheEndText.Parent == null)
			{
				TheEndText.Y = 0f;
			}
			else
			{
				TheEndText.RelativeY = 0f;
			}
			if (TheEndText.Parent == null)
			{
				TheEndText.Z = 25f;
			}
			else
			{
				TheEndText.RelativeZ = 25f - 40.0f;
			}
			TheEndText.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Center;
			TheEndText.Visible = false;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			if (TiledMap != null)
			{
				TiledMap.Destroy();
			}
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
			for (int i = EnemyBulletList.Count - 1; i > -1; i--)
			{
				EnemyBulletList[i].Destroy();
			}
			for (int i = EnemyCornerList.Count - 1; i > -1; i--)
			{
				EnemyCornerList[i].Destroy();
			}
			for (int i = GroundEnemyList.Count - 1; i > -1; i--)
			{
				GroundEnemyList[i].Destroy();
			}
			if (EntityCollisionShapes != null)
			{
				EntityCollisionShapes.RemoveFromManagers(false);
			}
			if (EnemyCollisionGround != null)
			{
				EnemyCollisionGround.RemoveFromManagers(false);
			}
			if (TheEndText != null)
			{
				TextManager.RemoveTextOneWay(TheEndText);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				PlayerInstance.AssignCustomVariables(true);
			}
			TheEndText.DisplayText = "The End.\nPress Enter To Go To Main Menu!";
			if (TheEndText.Parent == null)
			{
				TheEndText.X = 0f;
			}
			else
			{
				TheEndText.RelativeX = 0f;
			}
			if (TheEndText.Parent == null)
			{
				TheEndText.Y = 0f;
			}
			else
			{
				TheEndText.RelativeY = 0f;
			}
			if (TheEndText.Parent == null)
			{
				TheEndText.Z = 25f;
			}
			else
			{
				TheEndText.RelativeZ = 25f - 40.0f;
			}
			TheEndText.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Center;
			TheEndText.Visible = false;
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
			for (int i = 0; i < EnemyBulletList.Count; i++)
			{
				EnemyBulletList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < EnemyCornerList.Count; i++)
			{
				EnemyCornerList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < GroundEnemyList.Count; i++)
			{
				GroundEnemyList[i].ConvertToManuallyUpdated();
			}
			TextManager.ConvertToManuallyUpdated(TheEndText);
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
				case  "Level3":
					return Level3;
				case  "Level4":
					return Level4;
				case  "EndLevel":
					return EndLevel;
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
				case  "Level3":
					return Level3;
				case  "Level4":
					return Level4;
				case  "EndLevel":
					return EndLevel;
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
				case  "Level3":
					return Level3;
				case  "Level4":
					return Level4;
				case  "EndLevel":
					return EndLevel;
			}
			return null;
		}


	}
}
