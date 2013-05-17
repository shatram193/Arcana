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
        private int playerNumber;
        private PlayerBox playerBox;
        private Dictionary<string, Card> hand;
        private int health = 20;
        //private int handSize;

        public Player()
        {
            playerNumber = 1;
            playerBox = new PlayerBox();
            //handSize = 1;
            hand = new Dictionary<string, Card>();
            hand.Add("card", new Card(new Vector2(375, 0)));
        }

        public Player(int playerNumber)
        {
            this.playerNumber = playerNumber;
            playerBox = new PlayerBox(playerNumber, health);
            //handSize = 1;
            hand = new Dictionary<string, Card>();
            Vector2 position = (this.playerNumber == 1) ? new Vector2(375, 0) : new Vector2(375, 550);
            hand.Add("card", new Card(position));
        }

        public void drawAssets(SpriteBatch sb)
        {
            playerBox.Draw(sb);
            foreach (string s in hand.Keys)
                hand[s].Draw(sb);
        }

        public void loadAssets(ContentManager cm, GraphicsDevice gd)
        {
            playerBox.LoadContent(cm, gd);
            foreach (string s in hand.Keys)
                hand[s].LoadContent(cm, gd);
        }

        public void onClick(Vector2 pos)
        {
            foreach (string s in hand.Keys)
            {
                Card card = hand[s];
                Vector2 bounds = card.getPosition();
                if ((pos.X <= (bounds.X + card.getWidth())) && (pos.X >= bounds.X)
                    && (pos.Y <= bounds.Y + card.getHeight()) && (pos.Y >= bounds.Y))
                    card.clicked();
            }
        }
    }
}
