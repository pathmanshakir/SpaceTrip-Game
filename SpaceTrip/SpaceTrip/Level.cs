using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrip
{
    class Level
    {
        public Texture2D texture;
        int num = 0;
        public Blok[] blokArray = new Blok[14];
        public bool tekenWorld = true;
        public Vector2 Positie = new Vector2(128, 650-64);

        public void CreateWorld()
        {
            for (int x = 0; x < 14; x++)
            {
                blokArray[x] = new Blok(texture, new Vector2(num*128, 520));
                num++;
               
            }
        }
        
        public void DrawWorld(SpriteBatch spritebatch)
        {
            for (int x = 0; x < 14; x++)
            {

                
                if (blokArray[x] != null)
                    {
                         blokArray[x].Draw(spritebatch);
                 


                }
            }

        }

        
    }
}
