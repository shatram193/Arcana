using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Arcana
{
    /// <summary>
    /// Programmer: Mark Shatraw
    /// This is the main type for Arcana, where the GUI
    /// and the components of the state that are not
    /// the database get instantiated.
    /// </summary>
    public class ArcanaMain : Microsoft.Xna.Framework.Game
    {
        //The main graphics components of 2D XNA games.
        //The game asks them for sprites and drawing, and
        //they provide.
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //The principal components of the game state.
        //Players get a link to the database to get their
        //available cards.
        private Player player;
        private Player player2;
        MouseState mouseStateCurrent, mouseStatePrevious;
        DatabaseLink db;

        public ArcanaMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public ArcanaMain(DatabaseLink db)
        {
            this.db = db;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            player = new Player(1, db);
            player2 = new Player(2, db);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.loadAssets(this.Content, GraphicsDevice);
            player2.loadAssets(this.Content, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // nothing to unload
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Mouse logic
            mouseStateCurrent = Mouse.GetState();

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                Vector2 mousePos = new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y);
                int damage = player.onClick(mousePos);
                player2.dealDamage(damage);
            }
            if (player.getHealth() <= 0 || player2.getHealth() <= 0)
            {
                //Victory condition: When a player's health is reduced to zero,
                //they unlock a card in the database.
                int playerNumber = (player.getHealth() == 0) ? player2.player() : player.player();
                db.Insert(playerNumber);
                this.Exit();
            }
            mouseStatePrevious = mouseStateCurrent;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Green); //non-default color
            spriteBatch.Begin();
            //The SpriteBatch draws as it receives assets from the players,
            //like the playerBoxes and the players' cards.
            player.drawAssets(spriteBatch);
            player2.drawAssets(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
