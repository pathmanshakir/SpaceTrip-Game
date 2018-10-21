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
    class Zombie 
    {
        public AnimatiePiloot animatieZombie;
        public Animatie zombieWalk, zombieDead, zombieGirl;
     
        public Vector2 positie = new Vector2();
        public bool alive;

        bool keerTerug;
        public int zombieDamage;
        int eindTile;
        int beginTile;
        public Rectangle zombieRec;


        public Zombie(Vector2 new_Positie)
        {

            alive = true;
             positie= new_Positie;
            eindTile = (int)positie.X + 384;
            beginTile = (int)positie.X;
            keerTerug = false;
            zombieDamage = 0;
            


        }


        public void LoadContent(ContentManager Content)
        {
           
            zombieWalk = new Animatie(Content.Load<Texture2D>("rsz_zombie"), 150, 0.1f, true);
            //zombieGirl = new Animatie(Content.Load<Texture2D>("rsz_female_zombie"), 180, 0.1f, true);
            zombieDead = new Animatie(Content.Load<Texture2D>("zombeDead"), 150, 0.1f, true);
            zombieWalk.spriteHeight = 181;
            
        }
        public void Update(GameTime gameTime)
        {

             zombieRec = new Rectangle((int)positie.X, (int)positie.Y-181, 150, 181);
            

            if (positie.X < eindTile && keerTerug ==false)
            {
                positie.X++;
                if(positie.X == eindTile)
                {
                    keerTerug = true;
                }
               
            }
            if (positie.X > beginTile == keerTerug == true)
            {
                positie.X--;
                if (positie.X == beginTile)
                {
                    keerTerug = false;
                }

            }
            animatieZombie.PlayAnimatie(zombieWalk);

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (alive)
            {
                //Piloot en animaties
                SpriteEffects flip = SpriteEffects.None;
                if (positie.X < eindTile && keerTerug == false)
                {
                    flip = SpriteEffects.None;
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;

                }
                animatieZombie.Draw(gameTime, spriteBatch, positie, flip);
            }
        }

    }
}



