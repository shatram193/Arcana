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
        private DatabaseLink db;

        public Player()
        {
            playerNumber = 1;
            playerBox = new PlayerBox();
            db = null;
            hand = new Dictionary<string, Card>();
            hand.Add("card", new Card(new Vector2(375, 0)));
        }

        public Player(int playerNumber, DatabaseLink db)
        {
            this.playerNumber = playerNumber;
            playerBox = new PlayerBox(playerNumber, health);
            Vector2 position = (this.playerNumber == 1) ? new Vector2(375, 0) : new Vector2(375, 550);
            this.db = db;
            List<string>[] availableCards = db.Select(playerNumber);
            hand = new Dictionary<string, Card>();
            List<string> names = availableCards[0];
            List<string> powers = availableCards[1];
            int i = 0;
            foreach (string name in names)
            {
                string power = powers[i++];
                hand.Add(name, new Card(position, name, Int32.Parse(power)));
            }
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

        public int onClick(Vector2 pos)
        {
            foreach (string s in hand.Keys)
            {
                Card card = hand[s];
                Vector2 bounds = card.getPosition();
                if ((pos.X <= (bounds.X + card.getWidth())) && (pos.X >= bounds.X)
                    && (pos.Y <= bounds.Y + card.getHeight()) && (pos.Y >= bounds.Y))
                {
                    return card.clicked();
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        public void dealDamage(int damage)
        {
            health -= damage;
        }

        public int getHealth()
        {
            return health;
        }

        public int player()
        {
            return playerNumber;
        }
    }
}
