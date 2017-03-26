using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2
namespace TeamVGame
{
    class ProtectSprite
    {
        public Texture2D texture { get; set; } //  Sprite texture, read-only property

        public Vector2 position { get; set; }  //  Sprite position on screen

        public Vector2 size { get; set; }      //  Sprite size in pixels

        public Vector2 velocity { get; set; }  //  Sprite velocity

        private Vector2 screenSize { get; set; } //  screen size
        public float time { get; set; }
        public int frames { get; set; }

        public const int P_HEALTH = 100;

        public const int P_POWER = 20;

        public bool P_Die = false;

        public int P_GetPower()
        {
            return P_POWER;
        }

        public ProtectSprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth, int ScreenHeight)
        {
            texture = newTexture;   //Picture "name"
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);

        }

        public void P_BeAttacked (EnemySprite EnemySprite, int CurrentHealth)
        {
            if (EnemySprite.position.X <= (this.position.X + this.size.X))
            {
                CurrentHealth -= EnemySprite.E_GetPower();
            }
           
            if (CurrentHealth == 0)
            {
                P_Die = true;
            }
        }
  
        public void P_Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(200 * frames, 0, 200, 200), Color.White); //Adding the sprite to the batch with the rendured properties of the sprite

        }
    }
}
