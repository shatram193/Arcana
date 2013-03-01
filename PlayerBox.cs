using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arcana
{
    class PlayerBox
    {
        Texture2D rectangleTexture;
        private static int width = 100;
        private static int height = 50;
        private Vector2 position;

        public PlayerBox()
        {
            position = new Vector2(0, 0);
        }

        private Texture2D makeRectangle(GraphicsDevice gd)
        {
            Texture2D rectTexture = new Texture2D
                (gd, width, height, 1, TextureUsage.None, SurfaceFormat.Color);
            Color[] color = new Color[width * height];
            for (int i = 0; i < color.Length; ++i)
                color[i] = new Color(0, 0, 0, 255);
            rectTexture.SetData(color);
            return rectTexture;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(rectangleTexture, position, Color.White); 
        }

        public void LoadContent(ContentManager cm, GraphicsDevice gd)
        {
            rectangleTexture = makeRectangle(gd);
            
        }
    }
}
