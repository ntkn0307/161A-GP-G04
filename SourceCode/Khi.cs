﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Amazon
{
    class Khi
    {
        public Rectangle boundingBox;
        public Texture2D KhiTexture;
        public Vector2 position;

        //Bootlean Ẩn hiện
        public bool isVisible;

        //Detect tốc Độ Chạy Ghép Hình
        float elapsed;
        float delay = 200f;
        int frames = 0;
        public int speed;

        //Rectangle Của Nhân Vật
        Rectangle sourceRect;
        public Khi(Texture2D newTexture, Vector2 newposition)
        {
            position = newposition;
            KhiTexture = newTexture;
            speed = 4;
            isVisible = true;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(KhiTexture, position, sourceRect, Color.White);
            }

        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, KhiTexture.Width, KhiTexture.Height);

            //Update Movement
            position.X = position.X - speed;
            if (position.X <= -200)
                position.X = 1200;

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
            sourceRect = new Rectangle(76 * frames, 0, 76, 44);

        }

    }
}