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
    //Programmer: Mark Shatraw
    //Where the "magic" happens.
    //The card draws itself (when called), displays its text,
    //and returns its damage to its caller.
    class Card : Rect
    {
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

        //Parameter: Vector2 position - where the card will be located
        //relative to its top left-hand coordinates.
        //Parameter: string cardName - the name of the card to be displayer.
        //Parameter: int power - the power of the card.
        public Card(Vector2 position, string cardName, int power)
        {
            this.position = position;
            this.cardName = cardName;
            this.power = power;
        }

        //Makes a custom texture of the appropriate dimensions,
        //filling in each pixel and setting its alpha value individually.
        //courtesy http://roosterproduction.wordpress.com/2008/12/20/drawing-a-filled-rectangle-in-xna-20-or-30-without-a-existing-saved-texture/
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

        //Usual loading of content, plus the font.
        public void LoadContent(ContentManager cm, GraphicsDevice gd)
        {
            rectTexture = makeRectangle(gd);
            courierNew = cm.Load<SpriteFont>("Courier New");
            fontPos = position;
        }

        //On click, returns the power of the creature to "damage"
        //the opponent.
        public int clicked()
        {
            if (!inPlay)
                inPlay = true;
            Console.Out.WriteLine(cardName + " " + power);
            return power;
        }

        //Dimensional getters. Not to be changed.

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        //Draws the font. Slightly less complicated than
        //the Player Box because no conversions are necessary.
        //courtesy http://msdn.microsoft.com/en-us/library/bb447673(v=xnagamestudio.31).aspx
        public void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            Vector2 fontOrigin = courierNew.MeasureString(cardName) / 2;
            sb.DrawString(courierNew, cardName, fontPos, Color.LightGreen,
        0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

    }
}
