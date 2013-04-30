using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arcana
{
    class PlayerBox : Rect
    {
        //private Texture2D rectangleTexture;
        private static int width = 100;
        private static int height = 50;
        //private Vector2 position;
        private int playerNumber;

        public PlayerBox()
        {
            playerNumber = 1;
            position = new Vector2(0, 0);
        }

        public PlayerBox(int playerNumber)
        {
            this.playerNumber = playerNumber;
            position = (playerNumber == 1) ? new Vector2(0, 0) : new Vector2(700, 550);
        }

        private Texture2D makeRectangle(GraphicsDevice gd)
        {
            rectTexture = new Texture2D
                (gd, width, height, 1, TextureUsage.None, SurfaceFormat.Color);
            Color[] color = new Color[width * height];
            for (int i = 0; i < color.Length; ++i)
                color[i] = new Color(0, 0, 255, 255);
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
