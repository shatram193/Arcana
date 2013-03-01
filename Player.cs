using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arcana
{
    class Player
    {
        //private int playerNumber;
        private PlayerBox playerBox;

        public Player()
        {
            //playerNumber = 1;
            playerBox = new PlayerBox();
        }

        public void drawAssets(SpriteBatch sb)
        {
            playerBox.Draw(sb);
        }

        public void loadAssets(ContentManager cm, GraphicsDevice gd)
        {
            playerBox.LoadContent(cm, gd);
        }
    }
}
