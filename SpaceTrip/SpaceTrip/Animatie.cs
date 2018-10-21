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
    class Animatie
    {

        public Texture2D texture;
    

        public float timer;
        public float interval;
        public int spriteWidth, spriteHeight;

        public int frameCount;
        public bool isVisible;
        public bool isLooping;
      
      


        //Constructor
        public Animatie(Texture2D newTexture, int newFrameWidth, float newTimer, bool newIsLooping)
        {

            texture = newTexture;
            spriteWidth = newFrameWidth;
            spriteHeight = newTexture.Height;
            timer = newTimer;
            interval = 20f;
            frameCount = texture.Width / spriteWidth;
            isLooping = newIsLooping;
            isVisible = true;
        }
    }
}



