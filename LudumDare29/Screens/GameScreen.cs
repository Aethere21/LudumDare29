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
            TiledMap = (LayeredTileMap)GetMember(name);
            TiledMap.AddToManagers();
            SetUpTiles();
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
            PlayerInstance.CollideAgainst(TileCollisionShapes, false);
            for (int i = BulletList.Count - 1; i >= 0; i--)
            {
                if(BulletList[i].Collision.CollideAgainst(TileCollisionShapes))
                {
                    BulletList[i].Destroy();
                }
            }

            for (int x = NextLevelEntityList.Count - 1; x >= 0; x--)
            {
                if(PlayerInstance.Collision.CollideAgainst(NextLevelEntityList[x].Collision))
                {
                    SetLevelByName(NextLevelEntityList[x].nextLevelName);
                    //Console.WriteLine("Collided!");
                }
            }
        }

        private void CameraActivity()
        {
            Camera.Main.Velocity.Y = PlayerInstance.Position.Y - Camera.Main.Position.Y;
            Camera.Main.Velocity.X = PlayerInstance.Position.X - Camera.Main.Position.X;
        }
	}
}
