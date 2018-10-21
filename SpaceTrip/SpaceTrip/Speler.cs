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
    class Speler:ICollide
    {
        public Texture2D _texture;
        private Texture2D _textureKogel;
        public Texture2D healthTexture;
        public float bulletDelay;
        public Vector2 Positie;
        public Vector2 healthPositie;
        public List<Kogel> bulletList;
        public int health;
        public Rectangle healthRec;
        Sound sound = new Sound();
      
        KeyboardState state;
        int speed;
        //Collision variable 
        public Rectangle SpelersRec;
        
        public Speler()
        {
            _texture = null;
            bulletDelay = 10;
            speed = 5;
            Positie = new Vector2(300,400);
            healthPositie = new Vector2(50, 50);
            bulletList = new List<Kogel>();
            health = 200;
            

        }
        public void LoadContent(ContentManager content)
        {
            _texture= content.Load<Texture2D>("speler");
            _textureKogel = content.Load<Texture2D>("kogel");
            healthTexture = content.Load<Texture2D>("health");
            sound.LoadContent(content);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthTexture, healthRec, Color.White);
            spriteBatch.Draw(_texture, Positie, Color.White);
            foreach (Kogel b in bulletList)
            {

                b.Draw(spriteBatch);
            }
        }
        public void Update(GameTime gameTime)
        {
            //Collision Rectangle for player
            SpelersRec = new Rectangle((int)Positie.X, (int)Positie.Y, _texture.Width, _texture.Height);
            healthRec = new Rectangle((int)healthPositie.X, (int)healthPositie.Y, health, 30);

            state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Right))
            {
                Positie.X += speed;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                Positie.X -= speed;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                Positie.Y -= speed;
            }
            if (state.IsKeyDown(Keys.Down))
            { Positie.Y += speed; }
            if (state.IsKeyDown(Keys.Space))
            {
                Shoot();
                
            }
            if (Positie.X <= 0) { Positie.X = 0; }
            if (Positie.X >= 1100-_texture.Width )
            {
                Positie.X = 1100- _texture.Width;
            }

            if (Positie.Y <= 0)
            { Positie.Y = 0; }
            if (Positie.Y >= 650 - _texture.Height)
            {
                Positie.Y = 650 - _texture.Height;
            }
            UpdateBullets();
        }
      
        public void Shoot()
        {
         if(bulletDelay >= 0)
            {
                bulletDelay--; 
            }
            if (bulletDelay <=0)
            {
                sound.shotingSound.Play();
                Kogel newKogel = new Kogel(_textureKogel);
                newKogel.Positie = new Vector2(Positie.X + (_texture.Width / 2 + 120) - ( _textureKogel.Width/ 2), Positie.Y + (_texture.Height / 2) - (_textureKogel.Height / 2));
                newKogel.isVisible = true;
                
                if (bulletList.Count<1000) { bulletList.Add(newKogel); }
               
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
                b.Positie.X=b.Positie.X+b.speed;
                if (b.Positie.X >=1100)
                {
                    b.isVisible = false;
                }
                

            }
          
        }

        public Rectangle GetCollisionRectangle()
        {
            return SpelersRec;
        }
    }
}
