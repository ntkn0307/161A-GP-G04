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
    class GhiDiem
    {
        public int playerSource;
        public SpriteFont playerScoreFont;
        public Vector2 playerScropePos;
        public bool showHud;
        public GhiDiem()
        {
            playerSource = 0;
            showHud = true;
            playerScoreFont = null;
            playerScropePos = new Vector2(350, 50);
        }
        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("Georgia");

        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHud)
            {
                spriteBatch.DrawString(playerScoreFont, "Score : " + playerSource, playerScropePos, Color.Red);
            }
        }
    }
}
