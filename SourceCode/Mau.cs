using System;
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
    class Mau
    {
        public Rectangle boundingBox;
        public Texture2D MauTexture;
        public Vector2 position;

        //Bootlean Ẩn hiện
        public bool isVisible;

        public int speed;

        //Rectangle Của Nhân Vật
        Rectangle sourceRect;
        public Mau(Texture2D newTexture, Vector2 newposition)
        {
            position = newposition;
            MauTexture = newTexture;
            speed = 4;
            isVisible = true;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(MauTexture, position, Color.White);
            }

        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, MauTexture.Width, MauTexture.Height);

            //Update Movement
            position.X = position.X - speed;
            if (position.X <= -200)
                position.X = 1200;

        }

    }
}