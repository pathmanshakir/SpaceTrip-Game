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
    class VijandLucht
    {
        public Texture2D texture, kogel_texture;
        public Rectangle vijandRec;
        public bool alive;
        public Vector2 Positie;
        Random rnd = new Random();
        public int speed, health, bulletDelay;
        public List<Kogel> bulletList;
        public float randX, randY;

       
        public VijandLucht(Texture2D newTexture, Vector2 newPositie, Texture2D newKogel_texture)
        {
            bulletList = new List<Kogel>();
            texture = newTexture;
            kogel_texture = newKogel_texture;
            health = 5;
            speed = 7;
            bulletDelay = 250;
            alive = true;
            Positie = newPositie;
            randX = rnd.Next(2000,3000);
            randY = rnd.Next(70, 80);

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("vijand");

        }
        public void Update(GameTime gameTime)
        {
            vijandRec = new Rectangle((int)Positie.X, (int)Positie.Y, texture.Width, texture.Height);
            //update vijand positie
            Positie.X = Positie.X - speed;
            if (Positie.X <= texture.Width - Positie.X)
            {

                Positie.X = randX;
                Positie.Y = randY;

            }
            VijandShot();
            UpdateBullets();
        }

        public void Draw(SpriteBatch spriteBatch)
        {


            if (alive)
            {
                spriteBatch.Draw(texture, Positie, Color.White);
                foreach (Kogel k in bulletList)
                {
                    k.Draw(spriteBatch);
                }
            }

        }

        public void UpdateBullets()
        {

            foreach (Kogel b in bulletList)
            {

                //Collision Rectangle for bullets
                b.kogelRec = new Rectangle((int)b.Positie.X, (int)b.Positie.Y, b.kogel.Width, b.kogel.Height);
                b.Positie.Y = b.Positie.Y + 8;
                if (b.Positie.Y >= 700)
                {
                    b.isVisible = false;
                }


            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
        public void VijandShot()
        {

            //shoot only bulletdelay resets
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

            if (bulletDelay <= 0)
            {
                //create new bullet and position it front center of enemy ship.
                Kogel newKogel = new Kogel(kogel_texture);
                newKogel.Positie = new Vector2(Positie.X+75 , Positie.Y+65 );
                newKogel.isVisible = true;

                if (bulletList.Count < 20) { bulletList.Add(newKogel); }

            }
            if (bulletDelay == 0) { bulletDelay = 30; }
        }
    }
}
