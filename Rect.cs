using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arcana
{
    //Programmer: Mark Shatraw
    //The Card and PlayerBox classes had enough in common
    //that it seemed reasonable to have them inherit from
    //the same abstract class. This class has certain things they
    //share in common, but because they are different 
    //dimensions and colors, their draw methods are in the
    //individual classes themselves.
    class Rect
    {
        protected Vector2 position;
        protected Texture2D rectTexture;
        protected static SpriteFont courierNew;
        protected Vector2 fontPos;

        //Default draw method. Not used
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(rectTexture, position, Color.White);
        }

        //Returns the position of the rectangle.
        public Vector2 getPosition()
        {
            return position;
        }

    }
}
