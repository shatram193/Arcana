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
    //This retains all of the game state associated
    //with an individual player: the player's hand,
    //health, player number, and player box.
    //Also present is a database link for the creation
    //of the cards in the player's hand.
    class Player
    {
        private int playerNumber;
        private PlayerBox playerBox;
        private Dictionary<string, Card> hand;
        private int health = 20;
        private DatabaseLink db;


        //Default constructor, for testing purposes.
        public Player()
        {
            playerNumber = 1;
            playerBox = new PlayerBox();
            db = null;
            hand = new Dictionary<string, Card>();
            hand.Add("card", new Card(new Vector2(375, 0)));
        }

        //Parameter: int playerNumber - the number assigned to the player
        //Parameter: DatabaseLink db - the class that possesses the
        //link to the database for card loading purposes.
        public Player(int playerNumber, DatabaseLink db)
        {
            this.playerNumber = playerNumber;
            playerBox = new PlayerBox(playerNumber, health);
            //Chooses the location of the player's hand.
            Vector2 position = (this.playerNumber == 1) ? new Vector2(375, 0) : new Vector2(375, 550);
            this.db = db;
            //"Select" in the DatabaseLink class returns an array of lists of strings.
            //We here parse the list of strings into its component database entries
            //and add those to the player's hand.
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

        //Draws the sprites loaded below in loadAssets.
        public void drawAssets(SpriteBatch sb)
        {
            playerBox.Draw(sb);
            foreach (string s in hand.Keys)
                hand[s].Draw(sb);
        }

        //As I rolled my own sprite textures instead of 
        //using baked-in pictures, this method
        //loads the assets for both the Player Box
        //and cards in the player's Hand.
        public void loadAssets(ContentManager cm, GraphicsDevice gd)
        {
            playerBox.LoadContent(cm, gd);
            foreach (string s in hand.Keys)
                hand[s].LoadContent(cm, gd);
        }

        //On click, returns the power of the card
        //clicked so it can be subtracted from
        //an opponent's life. If no card is clicked,
        //returns zero.
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

        //Updates health based on the damage dealt
        //by a card.
        public void dealDamage(int damage)
        {
            health -= damage;
        }

        //For state checks of health.
        public int getHealth()
        {
            return health;
        }

        //Gets the player number for adding cards
        //to the appropriate database.
        public int player()
        {
            return playerNumber;
        }
    }
}
