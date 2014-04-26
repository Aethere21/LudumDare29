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
	public partial class Player
	{

        public enum LookDirection
        {
            Left,
            Right
        }

        public LookDirection DirectionFacing
        {
            get;
            private set;
        }

		private void CustomInitialize()
		{
            HandSprite.Detach();
            HandSprite.AttachTo(SpriteInstance, false);
            FlatRedBallServices.Game.IsMouseVisible = true;
            HandSprite.RelativePosition = new Vector3(1, 4, 0);
            GunRectangle.Detach();
            GunRectangle.AttachTo(HandSprite, false);
		}

		private void CustomActivity()
		{
            FlatRedBall.Debugging.Debugger.Write("Pos: " + GunRectangle.RelativePosition + "\nRotation: " + HandSprite.RelativeRotationZ);

            ArmActivity();
            MovementAnimations();

            ShootingActivity();

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void ArmActivity()
        {
            float distanceX = InputManager.Mouse.WorldXAt(0) - HandSprite.Position.X;
            float distanceY = InputManager.Mouse.WorldYAt(0) - HandSprite.Position.Y;

            if (DirectionFacing == LookDirection.Right)
            {
                HandSprite.RelativeRotationZ = (float)Math.Atan2((double)distanceY, (double)distanceX);
                GunRectangle.RelativePosition.X = 20;
                GunRectangle.RelativePosition.Y = 3;
                HandSprite.FlipHorizontal = false;
            }
            else
            {
                HandSprite.RelativeRotationZ = (float)Math.Atan2((double)distanceY, (double)distanceX) + (float)(Math.PI);
                GunRectangle.RelativePosition.X = -20;
                GunRectangle.RelativePosition.Y = 3;
                HandSprite.FlipHorizontal = true;
            }
        }

        private void MovementAnimations()
        {
            if(HorizontalRatio > 0)
            {
                this.DirectionFacing = LookDirection.Right;
            }
            else if(HorizontalRatio < 0)
            {
                this.DirectionFacing = LookDirection.Left;
            }

            if(!IsOnGround)
            {
                if(DirectionFacing == LookDirection.Left)
                {
                    this.SpriteInstance.CurrentChainName = "JumpLeft";
                }
                else
                {
                    this.SpriteInstance.CurrentChainName = "JumpRight";
                }
            }
            else
            {
                if(HorizontalRatio > 0)
                {
                    this.SpriteInstance.CurrentChainName = "WalkingRight";
                }
                else if(HorizontalRatio < 0)
                {
                    this.SpriteInstance.CurrentChainName = "WalkingLeft";
                }
                else
                {
                    if(DirectionFacing == LookDirection.Right)
                    {
                        this.SpriteInstance.CurrentChainName = "StandRight";
                    }
                    else
                    {
                        this.SpriteInstance.CurrentChainName = "StandLeft";
                    }
                }
            }
        }

        private void ShootingActivity()
        {
            if (InputManager.Mouse.ButtonReleased(Mouse.MouseButtons.LeftButton))
            {
                Entities.Bullet bullet = new Entities.Bullet();
                bullet.Position = GunRectangle.Position;
                bullet.RotationZ = HandSprite.RotationZ;
                bullet.rightDirection = HandSprite.FlipHorizontal;
                Factories.BulletFactory.ScreenListReference.Add(bullet);
            }
        }
	}
}
