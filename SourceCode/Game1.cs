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

namespace Amazon
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum State
        {
            MENU,
            RUN,
            GAMEOVER
        }
        public Texture2D MenuImage;
        public Texture2D GameOverImage;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        //Tao BackGround
        Background bg = new Background();

        //Tao Player
        Player player = new Player();
             
        List<Chim> chimlist = new List<Chim>();
        List<Soi> soilist = new List<Soi>();
        List<Khi> khilist = new List<Khi>();
        List<Mau> Maulist = new List<Mau>();

         GhiDiem HUD = new GhiDiem();

        //Sound Manager
        SoundManager sm = new SoundManager();
        //Game State
        State gameState = State.MENU;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuImage = Content.Load<Texture2D>("MenuGame");
            GameOverImage = Content.Load<Texture2D>("gameover");
            bg.LoadContent(Content);
            player.LoadContent(Content);
            HUD.LoadContent(Content);
            sm.LoadContent(Content);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            switch (gameState)
            {
                case State.RUN:
                    {
                     
                        foreach (Soi soi in soilist)
                        {
                            if (soi.boundingBox.Intersects(player.boundingBox))
                            {
                                player.Health -= 50;
                                soi.isVisible = false;

                            }

                            for (int i = 0; i < player.bulletList.Count; i++)
                            {
                                if (soi.boundingBox.Intersects(player.bulletList[i].boundingBox))
                                {
                                    HUD.playerSource += 20;
                                    soi.isVisible = false;
                                    player.bulletList.ElementAt(i).isVisible = false;
                                }
                            }

                            soi.Update(gameTime);
                        }
                     
                        foreach (Khi khi in khilist)
                        {
                            if (khi.boundingBox.Intersects(player.boundingBox))
                            {
                                player.Health -= 50;
                                khi.isVisible = false;

                            }

                            for (int i = 0; i < player.bulletList.Count; i++)
                            {
                                if (khi.boundingBox.Intersects(player.bulletList[i].boundingBox))
                                {
                                    HUD.playerSource += 20;
                                    khi.isVisible = false;
                                    player.bulletList.ElementAt(i).isVisible = false;
                                }
                            }

                            khi.Update(gameTime);
                        }

                        foreach (Chim chim in chimlist)
                        {
                            if (chim.boundingBox.Intersects(player.boundingBox))
                            {
                                player.Health -= 50;
                                chim.isVisible = false;

                            }
                            for (int i = 0; i < player.bulletList.Count; i++)
                            {
                                if (chim.boundingBox.Intersects(player.bulletList[i].boundingBox))
                                {
                                    HUD.playerSource += 50;
                                    chim.isVisible = false;
                                    player.bulletList.ElementAt(i).isVisible = false;
                                }
                            }
                            chim.Update(gameTime);
                        }

                        foreach (Mau mau in Maulist)
                        {
                            if (mau.boundingBox.Intersects(player.boundingBox))
                            {
                                player.Health += 50;
                                mau.isVisible = false;

                            }
                            for (int i = 0; i < player.bulletList.Count; i++)
                            {
                                if (mau.boundingBox.Intersects(player.bulletList[i].boundingBox))
                                {
                                    player.Health += 50;
                                    mau.isVisible = false;
                                    player.bulletList.ElementAt(i).isVisible = false;
                                }
                            }

                            mau.Update(gameTime);
                        }
                                               
                        if (player.Health <= 0)
                        {
                            gameState = State.GAMEOVER;
                        }
                        HUD.Update(gameTime);
                        player.Update(gameTime);
                        bg.Update(gameTime);
                        LoadAmazon();
                        break;
                    }

                case State.MENU:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        if (keyState.IsKeyDown(Keys.NumPad1))
                        {
                            gameState = State.RUN;
                            MediaPlayer.Play(sm.bgMusic);

                        }
                        if (keyState.IsKeyDown(Keys.D5))
                        {
                            this.Exit();
                        }
                        break;
                    }
                case State.GAMEOVER:
                    {
                        KeyboardState keyState = Keyboard.GetState();
                        if (keyState.IsKeyDown(Keys.Escape))
                        {
                            soilist.Clear();
                            player.Health = 200;
                            HUD.playerSource = 0;
                            gameState = State.MENU;

                        }
                        MediaPlayer.Stop();
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (gameState)
            {   //Drawing Game RUNING
                case State.RUN:
                    {
                        bg.Draw(spriteBatch);
                        player.Draw(spriteBatch);
                        foreach (Soi a in soilist)
                        {

                            a.Draw(spriteBatch);
                        }
                        foreach (Khi b in khilist)
                        {

                            b.Draw(spriteBatch);
                        }
                        foreach (Chim c in chimlist)
                        {

                            c.Draw(spriteBatch);
                        }
                        foreach (Mau d in Maulist)
                        {

                            d.Draw(spriteBatch);
                        }
                        HUD.Draw(spriteBatch);
                        break;
                    }
                //Drawing Game Menu
                case State.MENU:
                    {
                        bg.Draw(spriteBatch);
                        spriteBatch.Draw(MenuImage, new Vector2(150, 50), Color.White);

                        break;
                    }
                //Drawing Game Over
                case State.GAMEOVER:
                    {
                        spriteBatch.Draw(GameOverImage, new Vector2(0, -80), Color.White);
                        spriteBatch.DrawString(HUD.playerScoreFont, "Your Source: " + HUD.playerSource.ToString(), new Vector2(320, 350), Color.Red);
                        break;
                    }
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void LoadAmazon()
        {
            int ranX = random.Next(1000, 1500);
            int ranY = random.Next(130, 300);
            int ranKhi = random.Next(1500, 2500);
            int ranChim = random.Next(2000, 3500);
            int ranMau = random.Next(1000, 5000);
                        
            if (soilist.Count() <= 2)
            {
                soilist.Add(new Soi(Content.Load<Texture2D>("Soi"), new Vector2(ranX, 400)));
            }

            for (int i = 0; i < soilist.Count; i++)
            {
                if (!soilist[i].isVisible)
                {
                    soilist.RemoveAt(i);
                    i--;
                    sm.explodeSound.Play();
                }
            }
                       
            if (khilist.Count() <= 2)
            {
                khilist.Add(new Khi(Content.Load<Texture2D>("Khi"), new Vector2(ranKhi, 400)));
            }

            for (int i = 0; i < khilist.Count; i++)
            {
                if (!khilist[i].isVisible)
                {
                    khilist.RemoveAt(i);
                    i--;
                    sm.explodeSound.Play();
                }
            }
          
            if (chimlist.Count() <= 2)
            {
                chimlist.Add(new Chim(Content.Load<Texture2D>("Chim"), new Vector2(ranChim, ranY)));
            }

            for (int i = 0; i < chimlist.Count; i++)
            {
                if (!chimlist[i].isVisible)
                {
                    chimlist.RemoveAt(i);
                    i--;
                    sm.explodeSound.Play();
                }
            }

            //Load Vat Pham An Mau
            if (Maulist.Count() <= 1)
            {
                Maulist.Add(new Mau(Content.Load<Texture2D>("TraiTim"), new Vector2(ranMau, 350)));
            }

            for (int i = 0; i < Maulist.Count; i++)
            {
                if (!Maulist[i].isVisible)
                {
                    Maulist.RemoveAt(i);
                    i--;
                    sm.explodeSound.Play();
                }
            }
        }
    }
}
