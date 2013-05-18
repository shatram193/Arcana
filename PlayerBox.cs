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
    //The Player Box carries the representation
    //of the player's starting health.
    class PlayerBox : Rect
    {
        private static int width = 100;
        private static int height = 50;
        private int playerNumber;
        private int healthDisplay;

        //Default constructor.
        public PlayerBox()
        {
            playerNumber = 1;
            position = new Vector2(0, 0);
            fontPos = position;
        }

        //Parameter: int playerNumber - associates a box with a player
        //Parameter: int health - the player's health
        public PlayerBox(int playerNumber, int health)
        {
            this.playerNumber = playerNumber;
            healthDisplay = health;
            position = (playerNumber == 1) ? new Vector2(0, 0) : new Vector2(700, 550);
            fontPos = new Vector2(position.X + 20, position.Y + 20);
        }

        //Make a custom texture of the appropriate dimensions,
        //filling in each pixel and setting its alpha value individually.
        //courtesy http://roosterproduction.wordpress.com/2008/12/20/drawing-a-filled-rectangle-in-xna-20-or-30-without-a-existing-saved-texture/
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

        //Draws the player box, plus the player's health.
        public void Draw(SpriteBatch sb)
        {
            string stringHealth = healthDisplay.ToString();
            //In order to draw text, we need a StringBuilder instead
            //of a string.
            StringBuilder stb = new StringBuilder(stringHealth);
            base.Draw(sb);
            Vector2 fontOrigin = courierNew.MeasureString(stb) / 2;
            sb.DrawString(courierNew, stringHealth, fontPos, Color.LightGreen,
        0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        //The usual loading of content, but fonts must be loaded as well.
        public void LoadContent(ContentManager cm, GraphicsDevice gd)
        {
            rectTexture = makeRectangle(gd);
            courierNew = cm.Load<SpriteFont>("Courier New");
        }

    }
}
