#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using FlatRedBall.TileCollisions;
using FlatRedBall.TileGraphics;
using Microsoft.Xna.Framework;
#endif
#endregion

namespace LudumDare29.Screens
{
	public partial class GameScreen
	{

        bool switchingLevels = false;

        bool TheEnd = false;

		void CustomInitialize()
		{
            TileCollisionShapes.Visible = true;
            GlobalData.PlayerData.currentLevel = "StartLevel";
            GlobalData.PlayerData.gunDamage = 1;
            GlobalData.PlayerData.health = 100;
            GlobalData.PlayerData.playerDefense = 0;
            GlobalData.PlayerData.score = 0;
            SetLevelByName(GlobalData.PlayerData.currentLevel);

            //FlatRedBallServices.Game.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 25);
		}

		void CustomActivity(bool firstTimeCalled)
		{
            if(!switchingLevels)
            {
                CollisionActivity();
            }
            CameraActivity();

            PlayerInstance.DetermineMovementValues();

            GlobalData.PlayerData.playerPos = new Vector2(PlayerInstance.Position.X, PlayerInstance.Position.Y);

            FlatRedBall.Debugging.Debugger.Write("Player Health: " + GlobalData.PlayerData.health);

            HealthActivity();

            if(TheEnd)
            {
                TheEndText.Visible = true;
                if(InputManager.Keyboard.KeyDown(Keys.Enter))
                {
                    MoveToScreen(typeof(MenuScreen));
                }
            }
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void HealthActivity()
        {
            if(GlobalData.PlayerData.health <= 0)
            {
                GlobalData.PlayerData.health = 0;
                PlayerInstance.Destroy();
                TheEnd = true;
            }
        }

        private void SetLevelByName(string name)
        {
            if(name == "StartLevel")
            {
                FlatRedBallServices.GraphicsOptions.BackgroundColor = Color.CornflowerBlue;
            }
            else if(name == "EndLevel")
            {
                FlatRedBallServices.GraphicsOptions.BackgroundColor = Color.Gray;
            }
            else
            {
                FlatRedBallServices.GraphicsOptions.BackgroundColor = Color.Black;
            }
            switchingLevels = true;
            TiledMap.RemoveFromManagersOneWay();
            TiledMap.RemoveSelfFromListsBelongingTo();
            TiledMap.Destroy();
            RemoveShapes(TileCollisionShapes);
            RemoveShapes(EntityCollisionShapes);
            RemoveShapes(EnemyCollisionGround);
            ClearList();
            TiledMap = (LayeredTileMap)GetMember(name);
            TiledMap.AddToManagers();
            SetUpTiles();
            switchingLevels = false;
        }

        public void ClearList()
        {
            for (int i = NextLevelEntityList.Count - 1; i >= 0; i--)
            {
                NextLevelEntityList[i].Destroy();
            }
            for (int x = ActionEntityList.Count - 1; x >= 0; x--)
            {
                ActionEntityList[x].Destroy();
            }
            for (int z = BulletList.Count - 1; z >= 0; z--)
            {
                BulletList[z].Destroy();
            }
            for (int y = SignEntityList.Count - 1; y >= 0; y--)
            {
                SignEntityList[y].Destroy();
            }
            for (int g = EnemyCornerList.Count - 1; g >= 0; g--)
            {
                EnemyCornerList[g].Destroy();
            }
            for (int h = GroundEnemyList.Count - 1; h >= 0; h--)
            {
                GroundEnemyList[h].Destroy();
            }
            for (int o = EnemyBulletList.Count - 1; o >= 0; o--)
            {
                EnemyBulletList[o].Destroy();
            }
            NextLevelEntityList.Clear();
            ActionEntityList.Clear();
            BulletList.Clear();
            EnemyBulletList.Clear();
            GroundEnemyList.Clear();
            SignEntityList.Clear();
            EnemyCornerList.Clear();
        }

        private void SetUpTiles()
        {
            const float tileSize = 16;

            string[] splitters = { "_" };

            foreach(MapDrawableBatch layer in TiledMap.MapLayers)
            {
                if(layer.NamedTileOrderedIndexes.ContainsKey("Collision"))
                {
                    List<int> indexes = layer.NamedTileOrderedIndexes["Collision"];
                    foreach(int index in indexes)
                    {
                        float left;
                        float bottom;
                        layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                        float centerX = left + tileSize;
                        float centerY = bottom + tileSize;
                        AddShapes(TileCollisionShapes, tileSize, tileSize, new Vector3(centerX, centerY, 0), false, Color.Green);
                    }
                }
                if (layer.NamedTileOrderedIndexes.ContainsKey("StartPos"))
                {
                    List<int> indexes = layer.NamedTileOrderedIndexes["StartPos"];
                    foreach (int index in indexes)
                    {
                        float left;
                        float bottom;
                        layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                        float centerX = left + tileSize / 2.0f;
                        float centerY = bottom + tileSize / 2.0f;
                        PlayerInstance.Position = new Vector3(centerX, centerY, 10);
                        layer.Visible = false;
                    }
                }

                foreach (KeyValuePair<string, List<int>> entry in layer.NamedTileOrderedIndexes)
                {
                    if (entry.Key.StartsWith("NextLevel_"))
                    {
                        string[] levelName = entry.Key.Split(splitters, StringSplitOptions.None);

                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            Entities.NextLevelEntity nextLevel = new Entities.NextLevelEntity();
                            nextLevel.Position = new Vector3(centerX, centerY, 15);
                            nextLevel.nextLevelName = levelName[1];
                            NextLevelEntityList.Add(nextLevel);
                        }
                    }

                    if (entry.Key.StartsWith("Sign_"))
                    {
                        string[] signMessage = entry.Key.Split(splitters, StringSplitOptions.None);

                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            Entities.SignEntity sign = new Entities.SignEntity();
                            sign.Position = new Vector3(centerX, centerY, 20);
                            sign.signMessage = signMessage[1];
                            SignEntityList.Add(sign);
                        }
                    }

                    if (entry.Key.StartsWith("Action_"))
                    {
                        string[] actionString = entry.Key.Split(splitters, StringSplitOptions.None);

                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            Entities.ActionEntity action = new Entities.ActionEntity();
                            action.Position = new Vector3(centerX, centerY, 0);
                            action.actionString = actionString[1];
                            ActionEntityList.Add(action);

                            layer.Visible = false;
                        }
                    }

                    if (entry.Key.StartsWith("EnemyCorner_"))
                    {
                        string[] rotationString = entry.Key.Split(splitters, StringSplitOptions.None);

                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            Entities.EnemyCorner enemy = new Entities.EnemyCorner();
                            enemy.Position = new Vector3(centerX, centerY, 10);
                            if(rotationString[1] == "Right")
                            {
                                enemy.rightDirection = true;
                            }
                            else
                            {
                                enemy.rightDirection = false;
                            }
                            EnemyCornerList.Add(enemy);
                            layer.Visible = false;
                        }
                    }

                    if(entry.Key.StartsWith("EnemyGround"))
                    {
                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            Entities.GroundEnemy enemy = new Entities.GroundEnemy();
                            enemy.Position = new Vector3(centerX, centerY, 12);
                            GroundEnemyList.Add(enemy);
                            layer.Visible = false;
                        }
                    }

                    if (entry.Key == "EnemyCollision")
                    {
                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            AddShapes(EntityCollisionShapes, 16, 16, new Vector3(centerX, centerY, 0), true, Color.Red);
                            layer.Visible = false;
                        }
                    }

                    if (entry.Key == "EnemyCollisionGround")
                    {
                        List<int> indexes = entry.Value;
                        foreach (int index in indexes)
                        {
                            float left;
                            float bottom;
                            layer.GetBottomLeftWorldCoordinateForOrderedTile(index, out left, out bottom);
                            float centerX = left + tileSize;
                            float centerY = bottom + tileSize;

                            AddShapes(EnemyCollisionGround, 16, 16, new Vector3(centerX, centerY, 0), false, Color.Red);
                            layer.Visible = false;
                        }
                    }
                }
            }
        }

        private void RemoveShapes(ShapeCollection shapes)
        {
            for (int i = shapes.AxisAlignedRectangles.Count - 1; i >= 0; i--)
            {
                ShapeManager.Remove(shapes.AxisAlignedRectangles[i]);
            }
        }

        private void AddShapes(ShapeCollection shapeCol, float ScaleX, float ScaleY, Vector3 position, bool Visible, Color color)
        {
            AxisAlignedRectangle rect = new AxisAlignedRectangle(ScaleX, ScaleY);
            rect.Position = position;
            rect.Visible = false;
            rect.Color = color;
            shapeCol.AxisAlignedRectangles.Add(rect);
        }

        private void CollisionActivity()
        {

            //Player Against Tiles
            PlayerInstance.CollideAgainst(TileCollisionShapes, false);

            //Player Bullets
            for (int bullet = BulletList.Count - 1; bullet >= 0; bullet--)
            {
                //Against Tiles
                if(BulletList[bullet].Collision.CollideAgainst(TileCollisionShapes))
                {
                    BulletList[bullet].Destroy();
                }
                else
                {
                    //Against Corner Enemy
                    for (int corner = EnemyCornerList.Count - 1; corner >= 0; corner--)
                    {
                        if(BulletList[bullet].Collision.CollideAgainst(EnemyCornerList[corner].Collision))
                        {
                            BulletList[bullet].Destroy();
                            EnemyCornerList[corner].Health -= 10;
                        }
                    }
                    try
                    {
                        //Against ground enemy
                        for (int ground = GroundEnemyList.Count - 1; ground >= 0; ground--)
                        {
                            if (BulletList[bullet].Collision.CollideAgainst(GroundEnemyList[ground].Collision))
                            {
                                BulletList[bullet].Destroy();
                                GroundEnemyList[ground].Health -= 15;
                            }
                        }
                    }
                    catch { }
                }
            }

            //Player Against NextLevelEntity
            for (int next = NextLevelEntityList.Count - 1; next >= 0; next--)
            {
                if(PlayerInstance.Collision.CollideAgainst(NextLevelEntityList[next].Collision))
                {
                    SetLevelByName(NextLevelEntityList[next].nextLevelName);
                }
            }

            //Player Against Action Entity
            for (int Action = ActionEntityList.Count - 1; Action >= 0; Action--)
            {
                if(PlayerInstance.Collision.CollideAgainst(ActionEntityList[Action].Collision))
                {
                    if (ActionEntityList[Action].actionString == "takeOffFedora")
                    {
                        if (CanOpenMessage())
                        {
                            PlayerInstance.FedoraSprite.Visible = false;
                        }
                        else if (PlayerInstance.FedoraSprite.Visible)
                        {
                            PlayerInstance.Collision.CollideAgainstMove(ActionEntityList[Action].Collision, 0, 1);
                        }
                    }
                    else if(ActionEntityList[Action].actionString == "Kill")
                    {
                        if(PlayerInstance.Collision.CollideAgainst(ActionEntityList[Action].Collision))
                        {
                            GlobalData.PlayerData.health = 0;
                        }
                    }
                    else if (ActionEntityList[Action].actionString == "PutOnFedora")
                    {
                        if (PlayerInstance.Collision.CollideAgainst(ActionEntityList[Action].Collision))
                        {
                            PlayerInstance.FedoraSprite.Visible = true;
                        }
                    }
                    else if (ActionEntityList[Action].actionString == "TheEnd")
                    {
                        TheEnd = true;
                    }
                }
            }

            //Player Against Signs
            for (int sign = SignEntityList.Count - 1; sign >= 0; sign--)
            {
                if(PlayerInstance.Collision.CollideAgainst(SignEntityList[sign].Collision))
                {
                    if (CanOpenMessage())
                    {
                        SignEntityList[sign].OpenMessage();
                    }
                }
            }

            //Enemy Bullets
            for (int ebullet = EnemyBulletList.Count - 1; ebullet >= 0; ebullet--)
            {
                //Against Player
                if(PlayerInstance.Collision.CollideAgainst(EnemyBulletList[ebullet].Collision))
                {
                    EnemyBulletList[ebullet].Destroy();
                    GlobalData.PlayerData.health -= 1;
                }
                else
                {
                    //Against Tiles
                    if(EnemyBulletList[ebullet].Collision.CollideAgainst(TileCollisionShapes))
                    {
                        EnemyBulletList[ebullet].Destroy();
                    }
                }
            }

            //Ground Enemy
            for (int ground = GroundEnemyList.Count - 1; ground >= 0; ground--)
            {
                //Against Tiles
                GroundEnemyList[ground].Collision.CollideAgainstBounce(EnemyCollisionGround, 0, 1, 0.25f);
                if (GroundEnemyList[ground].Collision.CollideAgainstBounce(EntityCollisionShapes, 0, 1, 1))
                {
                    GroundEnemyList[ground].movementRight = !GroundEnemyList[ground].movementRight;
                }
                //Against Player
                else
                {
                    if (GroundEnemyList[ground].Collision.CollideAgainstBounce(PlayerInstance.Collision, 0, 1, 5))
                    {
                        GlobalData.PlayerData.health -= 2;
                    }   
                }
            }
        }

        private void CameraActivity()
        {
            Camera.Main.Velocity.Y = PlayerInstance.Position.Y - Camera.Main.Position.Y + 100;
            Camera.Main.Velocity.X = PlayerInstance.Position.X - Camera.Main.Position.X;
        }

        private bool CanOpenMessage()
        {
            for (int i = 0; i < SignEntityList.Count; i++)
            {
                if(SignEntityList[i].messageOpen)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
