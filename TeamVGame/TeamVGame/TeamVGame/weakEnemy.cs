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
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2
using Microsoft.Xna.Framework.Content;

namespace TeamVGame
{
    class weakEnemy
    {
        public static Texture2D texture { get; set; } //  Sprite texture, read-only property

        public Vector2 position { get; set; }  //  Sprite position on screen

        public Vector2 size { get; set; }      //  Sprite size in pixels

        public Vector2 velocity { get; set; }  //  Sprite velocity

        private Vector2 screenSize { get; set; } //  screen size

        public float time { get; set; }
        public int frames { get; set; }

        public bool attackState = false;

        public int health = 100;

        public const int E_POWER = 10;

        public bool E_Die = false;

        float delay = 500f;

       

        // public bool E_Shot = false;

        Random Rnd = new Random();

        public int E_GetPower()
        {
            return E_POWER;
        }

        public weakEnemy(Texture2D newTexture, Vector2 newSize, int ScreenWidth, int ScreenHeight)
        {

            texture = newTexture;   //Picture "name"
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);

        }
        public weakEnemy(Vector2 position)
        {
            this.position = position;
        }

        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("walkingEnemy");

        }
        
        public void E_Attacked()
        {
            health -= 10;
            if (health == 0)
            {
                position = new Vector2(1000, 0);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(28 * frames, 0, 27, 35), Color.White); //Adding the sprite to the batch with the rendured properties of the sprite

        }
        public void changeFrame(int frameWidth)
        {
            this.frames = frameWidth;
     
        }


        public void Update(float elapsedSeconds)
        {
            this.position += this.velocity * elapsedSeconds;
        }


        public void Populate()
        {

            position = new Vector2(200, Rnd.Next(1, 6) * 200);
            velocity = new Vector2(1, 0);
        }

        public bool tower_collision(Tower tower)
        {
            if ((this.position.X + this.size.X >= tower.position.X) && // right side
                (this.position.Y + this.size.Y >= tower.position.Y) && //top boundary
                (this.position.Y < tower.position.Y + tower.size.Y)) //bottom boundary
                return true;
            else
                return false;
           
        }
        
        public void move(Tower tower)
        {
            this.velocity = new Vector2(1, 0);
            this.position += this.velocity;

            if (this.tower_collision(tower) == true) {

                this.velocity = new Vector2(0, 0);
                this.attackState = true;
                this.position = new Vector2(tower.position.X, this.position.Y);
           
            }

        }
        


    }
    
    static class Shared
    {
        // "readonly" prevents anyone from writing to a field after it is initialised
        public static readonly Random Random = new Random();
    }

}
