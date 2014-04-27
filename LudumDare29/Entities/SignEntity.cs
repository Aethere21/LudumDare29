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
	public partial class SignEntity
	{
        private bool messageShown;
        public string signMessage;

        public bool messageOpen;

        public string action;

		private void CustomInitialize()
		{
            SpriteInstance.RelativePosition.Y = 105;
            TextInstance.RelativePosition.Y = 105;

            SpriteInstance.Visible = false;
            TextInstance.Visible = false;
            messageOpen = false;
            TextInstance.MaxWidthBehavior = FlatRedBall.Graphics.MaxWidthBehavior.Wrap;
            TextInstance.MaxWidth = 220;
		}

		private void CustomActivity()
		{
            TextInstance.DisplayText = signMessage;

            if(messageShown)
            {
                if(InputManager.Keyboard.KeyDown(Keys.Enter))
                {
                    SpriteInstance.Visible = false;
                    TextInstance.Visible = false;
                    messageOpen = false;
                }
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void OpenMessage()
        {
            if (!messageShown)
            {
                messageShown = true;
                SpriteInstance.Visible = true;
                TextInstance.Visible = true;
                messageOpen = true;
            }
        }
	}
}