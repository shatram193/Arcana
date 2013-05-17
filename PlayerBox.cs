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
        private int healthDisplay;

        public PlayerBox()
        {
            playerNumber = 1;
            position = new Vector2(0, 0);
            fontPos = position;
        }

        public PlayerBox(int playerNumber, int health)
        {
            this.playerNumber = playerNumber;
            healthDisplay = health;
            position = (playerNumber == 1) ? new Vector2(0, 0) : new Vector2(700, 550);
            fontPos = new Vector2(position.X + 20, position.Y + 20);
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

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(rectTexture, position, Color.White);
            string stringHealth = healthDisplay.ToString();
            StringBuilder stb = new StringBuilder(stringHealth);
            base.Draw(sb);
            Vector2 fontOrigin = courierNew.MeasureString(stb) / 2;
            sb.DrawString(courierNew, stringHealth, fontPos, Color.LightGreen,
        0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public void LoadContent(ContentManager cm, GraphicsDevice gd)
        {
            rectTexture = makeRectangle(gd);
            courierNew = cm.Load<SpriteFont>("Courier New");
        }

    }
}
