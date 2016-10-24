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
    class Background
    {
        public Texture2D texture;
        public Vector2 bgpos1, bgpos2;
        public int speed;

        public Background()
        {
            texture = null;
            bgpos1 = new Vector2(0, 0);
            bgpos2 = new Vector2(-2048, 0);
            speed = 5;
        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {           
            texture = Content.Load<Texture2D>("BackGroundNew");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgpos1, Color.White);
            spriteBatch.Draw(texture, bgpos2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            bgpos1.X = bgpos1.X - speed;
            bgpos2.X = bgpos2.X - speed;

            if (bgpos1.X <= -1024)
            {
                bgpos1.X = 0;
                bgpos2.X = 1024;

            }
        }
    }
}
