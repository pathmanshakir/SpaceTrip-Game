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
    class Piloot : ICollide
    {
        public AnimatiePiloot animatiePiloot;
        Animatie RunAnimatie;
        Animatie idleAnimatie;
        Animatie shootAnimatie;
        public Animatie springAnimatie;
        Animatie springParachute;
        public Vector2 positie = new Vector2(100, 0);
        public Vector2 velocity;
        public bool alive;
        public bool isJumping;
        public bool parachuteWeg;
        public bool isMoving;
     
        public Rectangle pilootRec;

        public float timer;
        public float interval;

        //Bullets
        private Texture2D _textureKogel;
        private Texture2D _textureKogelUp;
        public float bulletDelay;
        Sound sound = new Sound();
        public List<Kogel> bulletList;
        public List<Kogel> bulletUpList;
     
        public Piloot()
        {

            alive = true;
            isJumping = true;
            parachuteWeg = false;
            isMoving = false;
            bulletList = new List<Kogel>();
            bulletUpList = new List<Kogel>();
            bulletDelay = 10;

            timer = 0f;
            interval = 20f;


        }


        public void LoadContent(ContentManager Content)
        {
            _textureKogel = Content.Load<Texture2D>("piloot_kogel");
            _textureKogelUp = Content.Load<Texture2D>("piloot_rocket");
            sound.LoadContent(Content);
            RunAnimatie = new Animatie(Content.Load<Texture2D>("rsz_run"), 130, 0.1f, true);
            shootAnimatie = new Animatie(Content.Load<Texture2D>("rsz_shoot"), 130, 1.9f, true);
            idleAnimatie = new Animatie(Content.Load<Texture2D>("rsz_staan"), 130, 0.1f, true);
            springAnimatie = new Animatie(Content.Load<Texture2D>("rsz_jump"), 130, 0.01f, true);
            springParachute = new Animatie(Content.Load<Texture2D>("piloot"), 284, 0.00001f, true);
        
          

        }
        public void Update(GameTime gameTime)
        {
           
            pilootRec = new Rectangle((int)positie.X, (int)positie.Y, 130, 137);


         
            positie += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 5f;


            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -5f;

            }
            else { velocity.X = 0f; }
         
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && isJumping == false)
            {

                positie.Y -= 50f;
                velocity.Y = -6f;
                isJumping = true;

            


            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
          
                ShootUp();
             
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) )
            {
              
                Shoot();
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > interval)
                {
                    animatiePiloot.PlayAnimatie(shootAnimatie);
                }
            }


            UpdateUpBullets();
            UpdateBullets();
            
            if (isJumping == true)
            {

                float i = 1;
                velocity.Y += 0.15f * i;

            }
            if (isJumping == false) { velocity.Y = 5f; }
           
            if (velocity.X != 0)
            {
                animatiePiloot.PlayAnimatie(RunAnimatie);
                isMoving = true;

            }
            if (velocity.X == 0)
            {
                animatiePiloot.PlayAnimatie(idleAnimatie);
            }
            if (velocity.Y != 0 && isJumping == true)
            {
                animatiePiloot.PlayAnimatie(springAnimatie);
            
                    
                
            }
            if (positie.Y < 600 && parachuteWeg == false)
            {
                animatiePiloot.PlayAnimatie(springParachute);
            }
            if (positie.Y > 584) { alive = false; }

           


        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
            //kogel Draw
            spriteBatch.Draw(_textureKogel, new Vector2(positie.X-10, positie.Y - 75), Color.White);
            foreach (Kogel b in bulletList)
            {

                b.Draw(spriteBatch);
            }
            foreach (Kogel b in bulletUpList)
            {

                b.Draw(spriteBatch);
            }

            //Piloot en animaties
            SpriteEffects flip = SpriteEffects.None;
                if (velocity.X >= 0)
                {
                    flip = SpriteEffects.None;
                }
                else if (velocity.X < 0)
                {
                    flip = SpriteEffects.FlipHorizontally;

                }
            animatiePiloot.Draw(gameTime, spriteBatch, positie, flip);

        }
        public Rectangle GetCollisionRectangle()
        {
            pilootRec.X = (int)positie.X;
            pilootRec.Y = (int)positie.Y;
            pilootRec.Height = -4;
            pilootRec.Width = 20;


            return pilootRec;
        }

        public void Shoot()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
            if (bulletDelay <= 0)
            {
                sound.shotingSound.Play();
                Kogel newKogel = new Kogel(_textureKogel);
                newKogel.Positie = new Vector2(positie.X,positie.Y-70);
                newKogel.isVisible = true;

                if (bulletList.Count < 1000) { bulletList.Add(newKogel); }

            }
            if (bulletDelay == 0) { bulletDelay = 10; }
        }
        public void ShootUp()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
            if (bulletDelay <= 0)
            {
                sound.raketSound.Play();
                Kogel newKogel = new Kogel(_textureKogelUp);
                newKogel.Positie = new Vector2(positie.X, positie.Y - 70);
                newKogel.isVisible = true;

                if (bulletUpList.Count < 1000) { bulletUpList.Add(newKogel); }

            }
            if (bulletDelay == 0) { bulletDelay = 10; }
        }
        public void UpdateBullets()
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
            foreach (Kogel b in bulletList)
            {

                //Collision Rectangle for bullets
                b.kogelRec = new Rectangle((int)b.Positie.X, (int)b.Positie.Y, b.kogel.Width, b.kogel.Height);
                b.Positie.X = b.Positie.X + b.speed;
                if (b.Positie.X >= positie.X+1100)
                {
                    b.isVisible = false;
                }


            }

        }
        public void UpdateUpBullets()
        {
            for (int i = 0; i < bulletUpList.Count; i++)
            {
                if (!bulletUpList[i].isVisible)
                {
                    bulletUpList.RemoveAt(i);
                    i--;
                }
            }
            foreach (Kogel b in bulletUpList)
            {

                //Collision Rectangle for bullets
           
                
                    b.kogelRec = new Rectangle((int)b.Positie.X, (int)b.Positie.Y, b.kogel.Width, b.kogel.Height);
                    b.Positie.Y = b.Positie.Y - b.speed;
                if (b.Positie.Y <= 0)
                {
                    b.isVisible = false;
                   
                }
                }

            }
       

        }
    }
    


