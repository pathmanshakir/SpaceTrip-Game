using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrip
{
    class Kogel
    {
        public  Texture2D kogel;
        public Rectangle kogelRec;
        public bool isVisible;
        public Vector2 Positie;
        public float speed;
      
        public Kogel(Texture2D texture)
        {

            speed = 15;
            kogel = texture;
            isVisible = false ;
            Positie = new Vector2(100, 100);
            //kogelRec = new Rectangle(0,0, 70, 35);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           if (isVisible)
            {
                spriteBatch.Draw(kogel, Positie, Color.White);
            }
        }
    }
}
