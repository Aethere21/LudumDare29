using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare29
{
    public class TextDrawableBatch : PositionedObject, IDrawableBatch
    {
        private SpriteBatch spriteBatch = new SpriteBatch(FlatRedBallServices.GraphicsDevice);

        private string stringToDraw;

        public bool UpdateEveryFrame
        {
            get { return true; }
        }

        public TextDrawableBatch(string _stringToDraw)
        {
            stringToDraw = _stringToDraw;
        }

        public void Update()
        {

        }

        public void Destroy()
        {


        }

        public void Draw(Camera camera)
        {

        }

    }
}
