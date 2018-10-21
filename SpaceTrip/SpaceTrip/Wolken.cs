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
    public class Wolken
    {
        Texture2D wolkenTexure1, wolkenTexure2, wolkenTexure3;
        public Vector2 Positie1, Positie2,Positie3;
        int speed;

        public Wolken()
        {
            wolkenTexure1 = null;
            wolkenTexure2 = null;
            wolkenTexure3 = null;
            Positie1 = new Vector2(1200, 250);
            Positie2 = new Vector2(1900, 100);
            Positie3 = new Vector2(3000, 300);

            speed = 1;
        }
        public void LoadContent(ContentManager content)
        {
            wolkenTexure1 = content.Load<Texture2D>("wolken1");
            wolkenTexure2 = content.Load<Texture2D>("wolken2");
            //wolkenTexure3 = content.Load<Texture2D>("wolken3");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(wolkenTexure1, Positie2, Color.White);
            spriteBatch.Draw(wolkenTexure2, Positie2, Color.White);
            //spriteBatch.Draw(wolkenTexure3, Positie3, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            Positie1.X = Positie1.X - speed;
            Positie2.X = Positie2.X - speed;
            Positie3.X = Positie3.X - speed;

            if (Positie1.X <= -3000)
            {
                Positie1.X = 1400;
                Positie2.X = 1900;
                Positie3.X = 3300;
            }
        }
    }
}
