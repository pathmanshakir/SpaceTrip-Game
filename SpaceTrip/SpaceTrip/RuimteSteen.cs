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
    public class RuimteSteen
    {
        public Texture2D texture;
        public Rectangle RuimteSteenRec;
        public Vector2 positie;
        Random rnd = new Random();
        public Vector2 origin;
        public float rotatieAngle;
        public int speed;
        public bool isVisible;
        public float randX,randY;
        

        public RuimteSteen(Texture2D NewTexture, Vector2 newPositie)
        {

            positie = new Vector2(0, rnd.Next(5, 500 + 1));
            texture = NewTexture;
            speed = 4;
            isVisible = true;
            randX = rnd.Next(1100, 2000);
            randY = rnd.Next(70, 580);
        }
          
        public void LoadContent(ContentManager Content)
        {
           
        }

        public void Update(GameTime gameTime)
        {
           
            //Updating origin for rotation
             origin.X = (float)texture.Width / 2;
            origin.Y = (float)texture.Height / 2;
             //set boundingbox for collision
            RuimteSteenRec = new Rectangle((int)positie.X, (int)positie.Y-(int)origin.Y, 70, 69);

            //Rotatie
            //https://msdn.microsoft.com/nl-be/library/bb203869.aspx
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotatieAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotatieAngle = rotatieAngle % circle;

            positie.X = positie.X - speed;
            if (positie.X <= texture.Width-positie.X)
            {
                
                positie.X = randX;
                positie.Y = randY;
            }

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                
                spriteBatch.Draw(texture, positie, null, Color.White, rotatieAngle, origin, 1.0f, SpriteEffects.None, 0f);
              
            }
        }
    }
    
}
