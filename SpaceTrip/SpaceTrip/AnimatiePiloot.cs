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
    // Idee genomen van https://github.com/Discosultan/penumbra
    struct AnimatiePiloot
    {
       
        public Animatie Animatie
        {
            get { return animatie; }
        }
        Animatie animatie;
        public int frameIndex;
        public float timer;
        public Vector2 Origin { get { return new Vector2(Animatie.spriteWidth / 2, Animatie.spriteHeight); } }
        public void PlayAnimatie(Animatie newAnimatie)
        {
            if (Animatie == newAnimatie)
            {
                return;
            }
            else
            {
                animatie = newAnimatie;
                frameIndex = 0;
                timer = 0;
            }
            
        }
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch, Vector2 positie,SpriteEffects spriteEffects)
        {
            if (Animatie == null)
            {
                throw new NotSupportedException("No animatie selected!");
            }
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= animatie.timer)
            {
                timer -= animatie.timer;
                if (animatie.isLooping)
                {
                    frameIndex = (frameIndex + 1) % animatie.frameCount;
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, animatie.frameCount - 1);
                }
            }
                    Rectangle rectangle = new Rectangle(frameIndex * animatie.spriteWidth, 0, animatie.spriteWidth, animatie.spriteHeight);
                    spriteBatch.Draw(Animatie.texture, positie, rectangle, Color.White, 0f, Origin, 1f, spriteEffects, 0f);

                
            }
        }
    }

   

