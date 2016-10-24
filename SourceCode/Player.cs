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
    class Player
    {
        public bool isNhay;
        public int speed, Health;
        public float bulletDelay;
        public Rectangle boundingBox;
        public Rectangle sourceRect;
        public Rectangle destRect;
        public Rectangle healthRectangle;
        public Texture2D ThoSan, ThoSanNhay, textureDanBay, TextureHealth;
        public Vector2 ViTriDan, ViTriCayMau, ViTriNhay;
        public List<VienDan> bulletList;

        public float elapsed;
        public float delay = 200f;
        public int frames = 0;

        SoundManager sm = new SoundManager();

        //Check Nhảy
        Vector2 charPos;
        bool jumping; //Is the character jumping?
        float startY, jumpspeed = 0; //startY to tell us //where it lands, jumpspeed to see how fast it jumps

        public Player()
        {
            bulletList = new List<VienDan>();
            ThoSan = null;
            ViTriDan = new Vector2(170, 410);
            bulletDelay = 20;
            speed = 10;
            isNhay = false;
            Health = 200;
            ViTriCayMau = new Vector2(50, 50);
            //Jump
            charPos = new Vector2(70, 400);//Char loc, X/Y
            startY = charPos.Y;//Starting position
            jumping = false;//Init jumping to false
            jumpspeed = 0;//Default no speed


            //Dest Rectangle Player
            destRect = new Rectangle((int)charPos.X, (int)charPos.Y, 85, 61);

        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {
            ThoSan = Content.Load<Texture2D>("Player");
            ThoSanNhay = Content.Load<Texture2D>("PlayerNhay");

            textureDanBay = Content.Load<Texture2D>("Dan");
            TextureHealth = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        //Vẽ
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isNhay)
            {
                spriteBatch.Draw(ThoSan, destRect, sourceRect, Color.White);
            }
            if (isNhay)
            {
                spriteBatch.Draw(ThoSanNhay, charPos, Color.White);
            }
            spriteBatch.Draw(TextureHealth, healthRectangle, Color.White);

            foreach (VienDan b in bulletList)
                b.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {

            //Xử lý Cắt Frame Player
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 5)
                {
                    frames = 0;

                }
                else
                {
                    frames++;
                }
                elapsed = 100;

            }
            sourceRect = new Rectangle(87 * frames, 0, 87, 61);

            //Xu Ly Ban Phim
            KeyboardState keyState = Keyboard.GetState();
            if (jumping)
            {
                charPos.Y += jumpspeed;//Making it go up
                jumpspeed += 1;//Some math (explained later)
                if (charPos.Y >= startY)
                //If it's farther than ground
                {
                    charPos.Y = startY;//Then set it on
                    jumping = false;
                    isNhay = false;

                }
            }
            else
            {
                if (keyState.IsKeyDown(Keys.Up))
                {
                    jumping = true;
                    jumpspeed = -14;//Give it upward thrust
                    isNhay = true;
                }
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();

            }
            
            //Set Recctangle Máu
            healthRectangle = new Rectangle((int)ViTriCayMau.X, (int)ViTriCayMau.Y, Health, 25);

            //Tạo BoudingBox để check colision
            boundingBox = new Rectangle(60, 400, 85, 61);
            
            //Cập Nhập Đạn Bắn
            UpdateBullet();

        }

        public void Shoot()
        {
            if (bulletDelay >= 0)
                bulletDelay--;


            if (bulletDelay >= 0)
            {
                //Khai Báo thuộc Tính viên Đạn
                VienDan newBullet = new VienDan(textureDanBay);
                newBullet.position = new Vector2(ViTriDan.X - newBullet.DanBay.Width / 2, charPos.Y);
                newBullet.isVisible = true;
                //Sound Bắn
                sm.playerShootSound.Play();
                //Thêm Đạn Vào List
                if (bulletList.Count() < 1)
                {
                    bulletList.Add(newBullet);
                    sm.playerShootSound.Play();

                }

            }
            ////reset bullet
            if (bulletDelay == 0)
            {
                bulletDelay = 20;
            }

        }

        public void UpdateBullet()
        {
            foreach (VienDan b in bulletList)
            {

                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.DanBay.Width, b.DanBay.Height);

                b.position.X = b.position.X + b.speed;
                if (b.position.X >= 1000)
                    b.isVisible = false;

            }
                      
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }

        }
    }
}
