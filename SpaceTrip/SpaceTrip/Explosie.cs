using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrip
{
    // idee genomen van http://www.dreamincode.net/forums/topic/194878-xna-animated-sprite/
    class Explosie
    {
        public Texture2D texture;
        public Vector2 positie;
        public float timer;
        public float interval;
        public Vector2 origin;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;


        //Constructor
        public Explosie(Texture2D newTexture, Vector2 newPositie)
        {
            positie = newPositie;
            texture = newTexture;
            timer = 0f;
            interval = 20f;
            spriteWidth = 134;
            spriteHeight = 160;
            isVisible = true;
        }

     
        public void Update(GameTime gameTime)
        {
            //Increase the time by the number of milliseconds since update was called
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //check the time si more than the chosen interval

            if (timer > interval)
            {
                //show next Frame
                currentFrame++;
                //Reset Timer
                timer = 0f;
            }
            //if were on the last frame, reset back to the one before the first frame(because currentFrame is called next as the next frame will be 1)
            if (currentFrame == 12)
            {
                isVisible = false;
                currentFrame = 0;
            }
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (isVisible==true)
            {
                
                spritebatch.Draw(texture, positie, sourceRect, Color.White,0f,origin,1.0f,SpriteEffects.None,0);
                
            }
        }
    }
}
