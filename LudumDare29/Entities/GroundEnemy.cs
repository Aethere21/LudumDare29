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
	public partial class GroundEnemy
	{

        public bool movementRight = true;

        public int Health;

		private void CustomInitialize()
		{

            Health = 100;

		}

		private void CustomActivity()
		{
            this.YVelocity = -85;
            if (this.movementRight)
            {
                this.Velocity.X = 100;
            }
            else
            {
                this.Velocity.X = -100;
            }

            if(this.Health <= 0)
            {
                this.Destroy();
            }

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
