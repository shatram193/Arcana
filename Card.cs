using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arcana
{
    class Card : Rect
    {
        //private Vector2 position;
        //private Texture2D cardTexture;
        private static int width = 25;
        private static int height = 50;

        public Card(Vector2 position)
        {
            this.position = position;
        }

        private Texture2D makeRectangle(GraphicsDevice gd)
        {
            rectTexture = new Texture2D
                (gd, width, height, 1, TextureUsage.None, SurfaceFormat.Color);
            Color[] color = new Color[width * height];
            for (int i = 0; i < color.Length; ++i)
                color[i] = new Color(255, 0, 0, 255);
            rectTexture.SetData(color);
            return rectTexture;
        }

        //public void Draw(SpriteBatch sb)
        //{
        //    sb.Draw(rectTexture, position, Color.White);
        //}

        public void LoadContent(ContentManager cm, GraphicsDevice gd)
        {
            rectTexture = makeRectangle(gd);

        }
    }
}
