using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceTrip
{
    public class Sound
    {
        public SoundEffect shotingSound, explosieSound, raketSound, zombieSound;
        public Song BackgroundSound;
        
        public Sound()
        {
            shotingSound = null;
            raketSound = null;
            zombieSound = null;
            explosieSound=null;
            BackgroundSound = null;
        }
        public void LoadContent(ContentManager Content)
        {
            shotingSound = Content.Load<SoundEffect>("fireSound");
            raketSound = Content.Load<SoundEffect>("rocket_sound");
            explosieSound = Content.Load<SoundEffect>("explodSound");
            BackgroundSound = Content.Load<Song>("backgroundSound2");
            zombieSound = Content.Load<SoundEffect>("zombieSound");
        }
    }

}
