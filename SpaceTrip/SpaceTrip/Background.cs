using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrip
{
    class Background
    {
        Texture2D background;
        Texture2D background_level2;
        public Vector2 Positie1,Positie2, Positie3;
     
        public Vector2[] BG2posities;
        public int speed;

        public Background() {
            BG2posities = new Vector2[15];
            background = null;
            Positie1 = new Vector2(0, 0);
            Positie2 = new Vector2(1100, 0);
            Positie3 = new Vector2(2200, 0);
            speed = 5;
            for (int i = 0; i < 15; i++)
            {
                BG2posities[i] = new Vector2(i*1100, 0);
                
            }
        

        }
        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("background3");
            background_level2 = content.Load<Texture2D>("rsz_bg");
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, Positie1, Color.White);
            spriteBatch.Draw(background, Positie2, Color.White);
            spriteBatch.Draw(background, Positie3, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            Positie1.X = Positie1.X - speed;
            Positie2.X = Positie2.X - speed;
            Positie3.X = Positie3.X - speed;

            if (Positie1.X<=-1100)
            {
                Positie1.X = 0;
                Positie2.X = 1100;
                Positie3.X = 2200;
            }
        }
        public void Draw_Level2(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 15; i++)
            {
                spriteBatch.Draw(background_level2, BG2posities[i], Color.White);
            }
     
           
        }
        }
}
