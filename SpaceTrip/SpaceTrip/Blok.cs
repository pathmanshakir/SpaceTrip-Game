using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrip
{
    class Blok : ICollide
    {
        public Texture2D _texture;
        public Vector2 Positie;
        public Rectangle BlokRec;

        public Blok(Texture2D texture, Vector2 pos)
        {
            _texture = texture;
            Positie = pos;
            BlokRec = new Rectangle((int)Positie.X, (int)Positie.Y,_texture.Width,_texture.Height );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Positie, Color.AliceBlue);

        }

        public Rectangle GetCollisionRectangle()
        {
            return BlokRec;
        }
    }
}