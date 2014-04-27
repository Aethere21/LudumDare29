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
		void CustomInitialize()
		{
            TileCollisionShapes.Visible = true;
            GlobalData.PlayerData.currentLevel = "StartLevel";
            GlobalData.PlayerData.gunDamage = 1;
            GlobalData.PlayerData.health = 100;
            GlobalData.PlayerData.playerDefense = 0;
            GlobalData.PlayerData.score = 0;
            SetLevelByName(GlobalData.PlayerData.currentLevel);
		}

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();
            CameraActivity();

            PlayerInstance.DetermineMovementValues();
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void SetLevelByName(string name)
        {
            TiledMap.RemoveFromManagersOneWay();
            RemoveShapes(TileCollisionShapes);
            ClearList();
            TiledMap = (LayeredTileMap)GetMember(name);
            TiledMap.AddToManagers();
            SetUpTiles();
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
            for (int z = ActionEntityList.Count - 1; z >= 0; z--)
            {
                BulletList[z].Destroy();
            }
            for (int y = ActionEntityList.Count - 1; y >= 0; y--)
            {
                SignEntityList[y].Destroy();
            }
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
                        AddShapes(TileCollisionShapes, tileSize, tileSize, new Vector3(centerX, centerY, 0), true, Color.Green);
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

                    if(entry.Key.StartsWith("Sign_"))
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
            rect.Visible = Visible;
            rect.Color = color;
            shapeCol.AxisAlignedRectangles.Add(rect);
        }

        private void CollisionActivity()
        {
            //Player
            PlayerInstance.CollideAgainst(TileCollisionShapes, false);
            //Bullets
            for (int i = BulletList.Count - 1; i >= 0; i--)
            {
                if(BulletList[i].Collision.CollideAgainst(TileCollisionShapes))
                {
                    BulletList[i].Destroy();
                }
            }
            //Next Level
            for (int x = NextLevelEntityList.Count - 1; x >= 0; x--)
            {
                if(PlayerInstance.Collision.CollideAgainst(NextLevelEntityList[x].Collision))
                {
                    SetLevelByName(NextLevelEntityList[x].nextLevelName);
                }
            }
            //Signs
            for (int z = SignEntityList.Count - 1; z >= 0; z--)
            {
                if(PlayerInstance.Collision.CollideAgainst(SignEntityList[z].Collision))
                {
                    if(CanOpenMessage())
                    {
                        SignEntityList[z].OpenMessage();
                    }
                }
            }

            for (int a = 0; a < ActionEntityList.Count; a++)
            {
                if(PlayerInstance.Collision.CollideAgainst(ActionEntityList[a].Collision))
                {
                    if(ActionEntityList[a].actionString == "takeOffFedora")
                    {
                        PlayerInstance.FedoraSprite.Visible = false;
                    }
                }
            }
        }

        private void CameraActivity()
        {
            Camera.Main.Velocity.Y = PlayerInstance.Position.Y - Camera.Main.Position.Y;
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
