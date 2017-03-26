using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2

namespace TeamVGame
{
    class FireSprite
    {
        public Texture2D texture { get; set; } //  Sprite texture, read-only property

        public Vector2 position { get; set; }  //  Sprite position on screen

        public Vector2 size { get; set; }      //  Sprite size in pixels

        public Vector2 velocity { get; set; }  //  Sprite velocity

        private Vector2 screenSize { get; set; } //  screen size

        public FireSprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth, int ScreenHeight)
        {
            texture = newTexture;
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);
        }

        public void P_Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Move()
        {
            
        }
    }
}
