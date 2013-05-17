using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;

namespace Arcana
{
    class Card : Rect
    {
        //private Vector2 position;
        //private Texture2D cardTexture;
        private static int width = 25;
        private static int height = 50;
        private bool inPlay = false;
        private string cardName;
        private int power;

        public Card(Vector2 position)
        {
            this.position = position;
            cardName = "bear";
            power = 2;
        }

        public Card(Vector2 position, string cardName, int power)
        {
            this.position = position;
            this.cardName = cardName;
            this.power = power;
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
            courierNew = cm.Load<SpriteFont>("Courier New");
            fontPos = position;
        }

        public void clicked()
        {
            if (!inPlay)
                inPlay = true;
            Console.Out.WriteLine(cardName + " " + power);
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        //courtesy http://msdn.microsoft.com/en-us/library/bb447673(v=xnagamestudio.31).aspx
        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(rectTexture, position, Color.White);
            base.Draw(sb);
            Vector2 fontOrigin = courierNew.MeasureString(cardName) / 2;
            sb.DrawString(courierNew, cardName, fontPos, Color.LightGreen,
        0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

    }
}
