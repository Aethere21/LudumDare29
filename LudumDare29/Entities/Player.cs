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
		private void CustomInitialize()
		{
            HandSprite.Detach();
            HandSprite.AttachTo(SpriteInstance, false);
            FlatRedBallServices.Game.IsMouseVisible = true;
            HandSprite.RelativePosition = new Vector3(1, 4, 0);
		}

		private void CustomActivity()
		{
            FlatRedBall.Debugging.Debugger.Write("Pos: " + HandSprite.RelativePosition + "\nRotation: " + HandSprite.RelativeRotationZ);

            if (InputManager.Keyboard.KeyReleased(Keys.J))
            {
                HandSprite.RelativePosition.X += 1f;
            }
            if (InputManager.Keyboard.KeyReleased(Keys.H))
            {
                HandSprite.RelativePosition.X -= 1f;
            }
            if (InputManager.Keyboard.KeyReleased(Keys.K))
            {
                HandSprite.RelativeRotationZ += 0.01f;
            }
            if(InputManager.Keyboard.KeyReleased(Keys.N))
            {
                HandSprite.RelativePosition.Y += 1;
            }
            if (InputManager.Keyboard.KeyReleased(Keys.M))
            {
                HandSprite.RelativePosition.Y -= 1;
            }
            
            float distanceX = InputManager.Mouse.WorldXAt(0) - HandSprite.Position.X;
            float distanceY = InputManager.Mouse.WorldYAt(0) - HandSprite.Position.Y;
            HandSprite.RelativeRotationZ = (float)Math.Atan2((double)distanceY, (double)distanceX);
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
