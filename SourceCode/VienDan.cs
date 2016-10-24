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
    class VienDan
    {
        public Rectangle boundingBox;
        public Texture2D DanBay;
        public Vector2 position;
        public float speed;
        public bool isVisible;

        public VienDan(Texture2D newTexture)
        {

            speed = 10;
            DanBay = newTexture;
            isVisible = false;

        }
        public void LoadContent(ContentManager Content)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DanBay, position, Color.White);
        }

    }
}