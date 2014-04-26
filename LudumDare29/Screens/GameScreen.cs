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
#endif
#endregion

namespace LudumDare29.Screens
{
	public partial class GameScreen
	{

		void CustomInitialize()
		{
            TileCollisionShapes.GridSize = 32;
            TileCollisionShapes.Visible = true;
            SetLevelByName("StartLevel");
		}

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();
            CameraActivity();

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
                        float centerX = left + tileSize / 2.0f;
                        float centerY = bottom + tileSize / 2.0f;
                        TileCollisionShapes.AddCollisionAtWorld(centerX, centerY);
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
                    }
                }
            }
        }

        private void RemoveShapes(TileShapeCollection shapes)
        {
            for (int i = shapes.Rectangles.Count - 1; i >= 0; i--)
            {
                //shapes.Rectangles[i].RemoveSelfFromListsBelongingTo();
                ShapeManager.Remove(shapes.Rectangles[i]);
            }
        }

        private void CollisionActivity()
        {
            PlayerInstance.CollideAgainst(TileCollision, false);
        }

        public bool TileCollision()
        {
            if (TileCollisionShapes.CollideAgainstSolid(PlayerInstance.Collision))
            {
                return true;
            }
            return false;
        }

        private void CameraActivity()
        {
            Camera.Main.Velocity.Y = PlayerInstance.Position.Y - Camera.Main.Position.Y;
            Camera.Main.Velocity.X = PlayerInstance.Position.X - Camera.Main.Position.X;
            //Camera.Main.Position = PlayerInstance.Position;
        }
	}
}
