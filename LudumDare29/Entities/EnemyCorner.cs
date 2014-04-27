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
	public partial class EnemyCorner
	{

        public bool rightDirection;

        public int Health;

		private void CustomInitialize()
		{

            Health = 100;

		}

		private void CustomActivity()
		{

            if (!rightDirection)
            {
                this.BulletPosRect.RelativePosition = new Vector3(5, -5, 0);
                this.SpriteInstance.FlipHorizontal = true;
            }
            else
            {
                this.BulletPosRect.RelativePosition = new Vector3(-5, -5, 0);
                this.SpriteInstance.FlipHorizontal = false;
            }

            BulletShootActivity();

            if (this.Health <= 0)
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

        double lastTimeShot;
        const double shootTime = 1.2;
        private void BulletShootActivity()
        {           
            if(FlatRedBall.Screens.ScreenManager.CurrentScreen.PauseAdjustedSecondsSince(lastTimeShot) > shootTime)
            {
                float distanceX = GlobalData.PlayerData.playerPos.X - BulletPosRect.Position.X;
                float distanceY = GlobalData.PlayerData.playerPos.Y - BulletPosRect.Position.Y;
                Entities.EnemyBullet bullet = new EnemyBullet();
                bullet.Position = BulletPosRect.Position;
                if(rightDirection)
                {
                    bullet.RotationZ = (float)Math.Atan2((double)distanceY, (double)distanceX);
                }
                else
                {
                    bullet.RotationZ = (float)Math.Atan2((double)distanceY, (double)distanceX);
                }
                Factories.EnemyBulletFactory.ScreenListReference.Add(bullet);
                lastTimeShot += shootTime;
            }
        }
	}
}
