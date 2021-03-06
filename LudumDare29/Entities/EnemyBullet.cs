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

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

#endif
#endregion

namespace LudumDare29.Entities
{
	public partial class EnemyBullet
	{
        public bool rightDirection;
        private float movementSpeed = 450;
        private void CustomInitialize()
        {

        }

        private void CustomActivity()
        {
            if (rightDirection)
            {
                this.Velocity = RotationMatrix.Left * movementSpeed;
            }
            else
            {
                this.Velocity = RotationMatrix.Right * movementSpeed;
            }
        }

        private void CustomDestroy()
        {
            Collision.RemoveSelfFromListsBelongingTo();

        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
