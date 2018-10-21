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
    class Level2
    {
        
        public Texture2D Tile3, Tile2, Tile1,Tile4,Fire;
        public int TilePos;

        
        public Zombie zom;



        public Level2()
        {
            TilePos = 1100;
           
      

        }

     
 
        public byte[,] tileArray = new byte[,]
        {                                           //0   
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,3,0,1,0,2,0,3,0,1,1,1,1,1,1,0,2,0,1,1,1,1,1,1,0,3,2,2,0,3,0,1,1,1,1,1,1,2,3,2,0,3,0,3,0,1,0,1,0,1,0,1,0,0,3,0,1,1,0,4,4,4,4,4,4,4},
            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
        };

        public Blok[,] blokArray = new Blok[2, 80];
        public List<Zombie> zombieList = new List<Zombie>();

        public void CreateWorld()
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 80; y++)
                {
                    if (tileArray[x, y] == 0)
                    {
                        TilePos += 200;
                    }
                    if (tileArray[x, y] == 1)
                    {
                        blokArray[x, y] = new Blok(Tile1, new Microsoft.Xna.Framework.Vector2(TilePos, 520));
                        TilePos += 64;

                    }
                    if (tileArray[x, y] == 2)
                    {
                      

                        blokArray[x, y] = new Blok(Tile2, new Microsoft.Xna.Framework.Vector2(TilePos , 470));
                        TilePos += 192;
                      
                    }
                    if (tileArray[x, y] == 3)
                    {
                        blokArray[x, y] = new Blok(Tile3, new Vector2(TilePos, 370));
                       
                        zombieList.Add(new Zombie(new Vector2(TilePos, 380)));

                        TilePos += 384;

                    }
                    if (tileArray[x, y] == 4)
                    {
                        blokArray[x, y] = new Blok(Tile3, new Vector2(TilePos, 370));

                        zombieList.Add(new Zombie(new Vector2(TilePos, 380)));

                        TilePos += 384;

                    }
                    if (tileArray[x, y] == 8)
                    {
                        blokArray[x, y] = new Blok(Fire, new Microsoft.Xna.Framework.Vector2(y*160, 650-66));
                    }


                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 80; y++)
                {
                    if (blokArray[x, y] != null)
                    {
                        blokArray[x, y].Draw(spritebatch);
                    
                    }

                }
            }
        }
    }
}
