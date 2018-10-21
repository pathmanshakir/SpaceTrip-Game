using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceTrip
{
    public class Game1 : Game
    {
#region Variables,Objects,Initialisaties 
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Speler speler = new Speler();
        Piloot piloot;
        Camera camera;
        List<Vijand> vijandList = new List<Vijand>();
        List<VijandLucht> vijandLuchtList = new List<VijandLucht>();
        List<RuimteSteen> ruimteSteenList = new List<RuimteSteen>();
        List<Explosie> explosieList = new List<Explosie>();
        Background background = new Background();
        Wolken wolken = new Wolken();
        Score score = new Score();
        Sound sound = new Sound();
        Blok blokje;
        Level level;
        Level2 level2 = new Level2();
         bool level1;
         bool startAgain;
         bool landing = false;
         bool level1Accomplished;
         bool misshionEnd;
         int VijandKogelDamage;
        Vector2 camPos = new Vector2();
        List<ICollide> collideObjecten;
        Random randNew = new Random();
        public enum State
        {
            Menu,
            Play,
            Gameover

        }
        State gameState = State.Menu;

        //Menu Textures
        Texture2D startMenu;
        Texture2D EndMenu;
        Texture2D jumpMenu;
        Texture2D levelUPTexture;
        Texture2D level2Entry;
        Texture2D missionEnded;
        Rectangle levelTextureRec;

#endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "SpaceTrip game";
            VijandKogelDamage = 10;

            startMenu = null;
            jumpMenu = null;

            level1 = true;
            level1Accomplished = false;
            startAgain = false;
            misshionEnd = false;


        



        }
        protected override void Initialize()
        {
            //FullScreen Code
            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = false;
            //graphics.ApplyChanges();

            //Camera en Screensize
            camera = new Camera(GraphicsDevice.Viewport);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1100;
            graphics.PreferredBackBufferHeight = 650;
            graphics.ApplyChanges();
            piloot = new Piloot();


            base.Initialize();
        }
        protected override void LoadContent()
        {
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            speler.LoadContent(Content);
            piloot.LoadContent(Content);
           
          
            background.LoadContent(Content);
            wolken.LoadContent(Content);
            score.LoadContent(Content);
            sound.LoadContent(Content);
            startMenu = Content.Load<Texture2D>("startMenu");
            EndMenu = Content.Load<Texture2D>("endMenu");
            jumpMenu = Content.Load<Texture2D>("planet");
            levelUPTexture = Content.Load<Texture2D>("level_up");
            missionEnded = Content.Load<Texture2D>("rsz_mission");
            level2Entry = Content.Load<Texture2D>("level1_Accomplished");
            //Level One Plateform
            Texture2D blokText = Content.Load<Texture2D>("blok");
            //Level Twee platform


            Texture2D Tile1 = Content.Load<Texture2D>("Tile_1");
            Texture2D Tile2 = Content.Load<Texture2D>("Tile_2");
            Texture2D Tile3 = Content.Load<Texture2D>("Tile3");
            Texture2D Tile4 = Content.Load<Texture2D>("Tile_4");
            Texture2D Fire = Content.Load<Texture2D>("rsz_fire");



            blokje = new Blok(blokText, new Vector2(30, 30));
            level = new Level();
            level.texture = blokText;
            level.CreateWorld();
            level2 = new Level2();
            level2.Tile1 = Tile1;
            level2.Tile2 = Tile2;
            level2.Tile3 = Tile3;
            level2.Tile4 = Tile4;
            level2.Fire = Fire;


            level2.CreateWorld();
            collideObjecten = new List<ICollide>();
           
           //Loading zombie in level 2
            foreach (var zombie in level2.zombieList)
            {
                zombie.LoadContent(Content);
            }

            //Collison plateform Level 1
            for (int i = 0; i < level.blokArray.GetLength(0); i++)
            {

                if (level.blokArray[i] != null)
                    collideObjecten.Add(level.blokArray[i]);
            }
            //Collision plateform level 2
            for (int i = 0; i < level2.blokArray.GetLength(0); i++)
            {
                for (int j = 0; j < level2.blokArray.GetLength(1); j++)
                {
                    if (level2.blokArray[i, j] != null)
                        collideObjecten.Add(level2.blokArray[i, j]);
                }
            }

        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //If Players dies, jumping to Gameover menu
            if (piloot.alive == false)
            {
                level1 = true;
                landing = false;
                startAgain = false;
                gameState = State.Gameover;



            }
            //Level one Updates
            if (level1 == true)
            {


                //StartMenu
                switch (gameState)
                {
                    //First Level Different Menu's and Playing Codes
                    case State.Play:
                        {

                            background.speed = 5;

                            wolken.Update(gameTime);
                            KeyboardState keyState = Keyboard.GetState();
                            if (keyState.IsKeyDown(Keys.L))
                            {

                                landing = true;
                            }
                            //After landing the piloot
                            if (landing == true)
                            {
                                CheckCollision();
                                levelTextureRec = new Rectangle(1500, 400, (int)levelUPTexture.Width, (int)levelUPTexture.Height);

                                piloot.Update(gameTime);
                                

                                if (piloot.pilootRec.Intersects(levelTextureRec))
                                {
                                    level1Accomplished = true;
                                    piloot.velocity.X = 0;


                                    if (keyState.IsKeyDown(Keys.Enter))
                                    {
                                        level1 = false;
                                        piloot.velocity.X = 0;
                                        graphics.GraphicsDevice.Clear(Color.Black);
                                    }


                                }
                            }
                            if (landing == false)
                            {

                                background.Update(gameTime);
                                speler.Update(gameTime);
                                if (speler.health <= 0) { gameState = State.Gameover; }
                                if (score.playerScore < 500)
                                {
                                    LoadRuimteStenen();
                                    LoadVijand();
                                    //Collisions
                                    foreach (Vijand v in vijandList)
                                    {
                                        v.Update(gameTime);
                                        if (v.vijandRec.Intersects(speler.SpelersRec))
                                        {
                                            speler.health -= 20;
                                            v.alive = false;
                                            explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(v.Positie.X, v.Positie.Y)));
                                        }
                                        for (int i = 0; i < speler.bulletList.Count; i++)
                                        {
                                            if (v.vijandRec.Intersects(speler.bulletList[i].kogelRec))
                                            {
                                                sound.explosieSound.Play();
                                                speler.bulletList[i].isVisible = false;
                                                v.alive = false;
                                                speler.bulletList.ElementAt(i).isVisible = false;
                                                explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(v.Positie.X + 100, v.Positie.Y + 80)));
                                                score.playerScore += 20;
                                            }
                                        }
                                        for (int i = 0; i < v.bulletList.Count; i++)
                                        {
                                            if (speler.SpelersRec.Intersects(v.bulletList[i].kogelRec))
                                            {
                                                speler.health -= 4;
                                                speler.health -= VijandKogelDamage;
                                                v.bulletList[i].isVisible = false;
                                                explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(speler.Positie.X + 120, speler.Positie.Y + 50)));
                                            }
                                        }


                                    }
                                    foreach (Explosie ex in explosieList)
                                    {
                                        ex.Update(gameTime);
                                    }
                                    foreach (RuimteSteen r in ruimteSteenList)
                                    {
                                        r.Update(gameTime);
                                        //Check if any of ruimtesteen is colliding the player, if yeah remove them from ruimtelist
                                        if (r.RuimteSteenRec.Intersects(speler.SpelersRec))
                                        {
                                            speler.health -= 10;
                                            r.isVisible = false;
                                            explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(r.positie.X, r.positie.Y)));
                                        }
                                        //Check the bullet class for collision between bullets and ruimtesteenen
                                        for (int i = 0; i < speler.bulletList.Count; i++)
                                        {
                                            if (r.RuimteSteenRec.Intersects(speler.bulletList[i].kogelRec))
                                            {
                                                sound.explosieSound.Play();
                                                explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(r.positie.X, r.positie.Y)));
                                                r.isVisible = false;
                                                speler.bulletList.ElementAt(i).isVisible = false;
                                                score.playerScore += 5;
                                            }

                                        }


                                    }

                                    ManangeExplosie();
                                }
                                //health.Update(gameTime);
                            }
                            break;
                        }
                    case State.Menu:
                        {

                            KeyboardState keyState = Keyboard.GetState();
                            if (keyState.IsKeyDown(Keys.Enter))
                            {
                                gameState = State.Play;
                                MediaPlayer.Play(sound.BackgroundSound);
                            }
                            background.Update(gameTime);
                            background.speed = 1;

                            break;
                        }
                    case State.Gameover:
                        {
                            KeyboardState keyState = Keyboard.GetState();
                            if (keyState.IsKeyDown(Keys.R))
                            {
                                vijandList.Clear();
                             
                      

                                ruimteSteenList.Clear();
                                startAgain = true;
                                gameState = State.Menu;
                                speler.health = 200;
                                score.playerScore = 0;
                                piloot.alive = true;
                                piloot.positie = new Vector2(100, 0);
                                piloot.isJumping = true;
                                piloot.parachuteWeg = false;
                                piloot.velocity = new Vector2(0, 0);
                                camera.Position = Vector2.Zero;
                                camPos = Vector2.Zero;
                                level1Accomplished = false;


                            }
                            MediaPlayer.Stop();
                            break;

                        }
                }
            }
            //Level Two Updates
            if (level1 == false)
            {
                graphics.GraphicsDevice.Clear(Color.Black);
                piloot.alive = true;

       

                piloot.Update(gameTime);

              
                foreach (var zombie in level2.zombieList)
                {
                    zombie.Update(gameTime);
                    if (zombie.zombieRec.Contains (new Point((int)piloot.positie.X+60, (int)piloot.positie.Y-60)))
                    {
                   
                        Console.WriteLine("Geraakt ");
                        piloot.alive = false;
                        zombie.alive = false;
                    }
                    for (int i = 0; i < piloot.bulletList.Count; i++)
                    {
                        if (piloot.bulletList[i].kogelRec.Intersects(zombie.zombieRec))
                        {
                            sound.zombieSound.Play();
                            piloot.bulletList[i].isVisible = false;
                            zombie.zombieDamage++;
                            if (zombie.zombieDamage >= 6) {
                                     zombie.alive = false;                
                               }
                            
                            piloot.bulletList.ElementAt(i).isVisible = false;
                           
                            explosieList.Add(new Explosie(Content.Load<Texture2D>("Blood"), new Vector2(zombie.positie.X, zombie.positie.Y-70)));
                        }
                    }
               

                }
                RemoveZombies();
                if (piloot.positie.X < background.BG2posities[1].X + 50)
                {
                    piloot.positie.X = background.BG2posities[1].X + 50;
                }
                CheckCollision();
                LoadLuchtVijand();
             
                foreach (VijandLucht v in vijandLuchtList)
                {
                    v.Update(gameTime);
                    if (v.vijandRec.Intersects(piloot.pilootRec))
                    {

                        v.alive = false;
                        piloot.alive = false;
                        explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(v.Positie.X, v.Positie.Y)));
                    }
                    for (int i = 0; i < v.bulletList.Count; i++)
                    {
                        if (v.bulletList[i].kogelRec.Intersects(piloot.pilootRec))
                        {
                            piloot.alive = false;
                            v.bulletList[i].isVisible = false;
                            explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(piloot.positie.X, piloot.positie.Y - 100)));
                        }
                        //if (v.bulletList[i].kogelRec.Intersects(piloot.bulletUpList[i].kogelRec))
                        //{
                        //    piloot.bulletUpList[i].isVisible = false;
                        //    piloot.bulletUpList.ElementAt(i).isVisible = false;
                        //    explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(v.Positie.X + 100, v.Positie.Y + 80)));
                        //}

                    }
                    for (int i = 0; i < piloot.bulletList.Count; i++)
                    {
                        if (v.vijandRec.Intersects(piloot.bulletList[i].kogelRec))
                        {
                            sound.explosieSound.Play();
                            piloot.bulletList[i].isVisible = false;
                            v.alive = false;
                            piloot.bulletList.ElementAt(i).isVisible = false;
                            explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(v.Positie.X + 100, v.Positie.Y + 80)));

                        }
                 
                    }
                    for (int i = 0; i < piloot.bulletUpList.Count; i++)
                    {
                        if (v.vijandRec.Intersects(piloot.bulletUpList[i].kogelRec))
                        {
                            sound.explosieSound.Play();
                            piloot.bulletUpList[i].isVisible = false;
                            v.alive = false;
                            piloot.bulletUpList.ElementAt(i).isVisible = false;
                            explosieList.Add(new Explosie(Content.Load<Texture2D>("explosion_sheet"), new Vector2(v.Positie.X + 100, v.Positie.Y + 80)));

                        }
                       
                    }
                   

                }
            
                foreach (Explosie ex in explosieList)
                {
                    ex.Update(gameTime);
                }
                if (piloot.positie.X > 14400)
                {
                    misshionEnd = true;
                }

            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Cyan);
            var viewMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);
            //If players dies, Reseting and drawing things
            if (!piloot.alive && startAgain == false)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(EndMenu, new Vector2(camPos.X, 0), Color.White);
                gameState = State.Gameover;




            }

            //Level one Draw
            if (level1 == true)
            {
                switch (gameState)
                {
                    case State.Play:
                        {

                            background.Draw(spriteBatch);

                            if (landing == false)
                            {

                                speler.Draw(spriteBatch);
                                foreach (Vijand v in vijandList)
                                {
                                    v.Draw(spriteBatch);

                                }
                                foreach (RuimteSteen r in ruimteSteenList)
                                {
                                    r.Draw(spriteBatch);
                                }
                                foreach (Explosie ex in explosieList)
                                {
                                    ex.Draw(spriteBatch);
                                }
                            }
                            score.Draw(spriteBatch);
                            wolken.Draw(spriteBatch);
                            if (score.playerScore >= 500 && landing == false)
                            {
                                spriteBatch.Draw(jumpMenu, new Vector2(0, 0), Color.White);
                                // level2.DrawLevel(spriteBatch);
                            }
                            if (score.playerScore >= 450)
                            {
                                level.DrawWorld(spriteBatch);

                            }
                            if (landing == true)
                            {
                                if (piloot.isMoving)
                                if (piloot.alive == true && piloot.positie.X > 300)
                                    {
                                        camPos.X += piloot.velocity.X;
                                        camera.Position = camPos;
                                    }
                                piloot.Draw(gameTime, spriteBatch);

                            
                                spriteBatch.Draw(levelUPTexture, levelTextureRec, Color.White);
                                if (level1Accomplished == true)
                                {
                                    spriteBatch.Draw(level2Entry, new Vector2(camPos.X, 0), Color.White);
                                }

                            }

                            break;
                        }
                    case State.Menu:
                        {
                            background.Draw(spriteBatch);

                            spriteBatch.Draw(startMenu, new Vector2(200, 100), Color.White);

                            break;
                        }
                    case State.Gameover:
                        {
                            spriteBatch.Draw(EndMenu, new Vector2(0, 0), Color.White);
                            spriteBatch.DrawString(score.scoreFont, "Your Final Score was " + score.playerScore.ToString(), new Vector2(900, 100), Color.White);
                            break;
                        }
                }
            }
            //Level two Draw
            if (level1 == false)
            {
                background.Draw_Level2(spriteBatch);
                level2.DrawLevel(spriteBatch);
             

                if (piloot.alive)
                {
                    //GraphicsDevice.Clear(Color.Cyan);
                 
                    if (piloot.alive == true && piloot.isMoving && piloot.positie.X > background.BG2posities[1].X + 300)
                    {

                        camPos.X += piloot.velocity.X;
                        camera.Position = camPos;
                    }

                   

                    foreach (VijandLucht v in vijandLuchtList)
                    {
                        v.Draw(spriteBatch);

                    }
                    piloot.Draw(gameTime, spriteBatch);
                    foreach (var zombie in level2.zombieList)
                    {
                        zombie.Draw(gameTime, spriteBatch);
                    }
                    foreach (Explosie ex in explosieList)
                    {
                        ex.Draw(spriteBatch);
                    }
                    spriteBatch.Draw(speler._texture, new Vector2(14500, 300), Color.White);

                    if (misshionEnd)
                    {
                        GraphicsDevice.Clear(Color.Cyan);
                        spriteBatch.Draw(missionEnded, new Vector2(camPos.X, 0), Color.White);
                    }

                }
            }
          
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        //Loading ,Removing and managing methodes for different Objects
        public void LoadRuimteStenen() {
            int randX = randNew.Next(1100, 2000);
            int randY = randNew.Next(70, 580);

            if (ruimteSteenList.Count < 3)
            {
                ruimteSteenList.Add(new RuimteSteen(Content.Load<Texture2D>("ruimteSteen"), new Vector2(randX, randY)));

            }
            //Als de ruimtestenen vernietgd of buiten het scherm vliegt, worden ze gewoon uitgehaald uit list.
            for (int i = 0; i < ruimteSteenList.Count; i++)
            {
                if (!ruimteSteenList[i].isVisible)
                {
                    ruimteSteenList.RemoveAt(i);
                    i--;
                }
            }

        }
        public void ManangeExplosie()
        {
            for (int i = 0; i < explosieList.Count; i++)
            {
                if (!explosieList[i].isVisible)
                {
                    explosieList.RemoveAt(i);
                    i--;
                }
            }
        }
        public void LoadVijand()
        {
            int randX = randNew.Next(1200, 2500);
            int randY = randNew.Next(70, 580);

            if (vijandList.Count < 6)
            {
                vijandList.Add(new Vijand(Content.Load<Texture2D>("vijand"), new Vector2(randX, randY), Content.Load<Texture2D>("vijandKogel")));

            }
            //Als de Vijanden vernietgd of buiten het scherm vliegt, worden ze gewoon uitgehaald uit list.
            for (int i = 0; i < vijandList.Count; i++)
            {
                if (!vijandList[i].alive)
                {
                    vijandList.RemoveAt(i);
                    i--;
                }
            }


        }
        public void LoadLuchtVijand()
        {
            int randX = randNew.Next(4000, 16000);
            int randY = randNew.Next(70, 80);

            if (vijandLuchtList.Count < 8)
            {

                vijandLuchtList.Add(new VijandLucht(Content.Load<Texture2D>("VijandLucht"), new Vector2(randX, randY), Content.Load<Texture2D>("rsz_rocket")));

            }
            //Als de Vijanden vernietgd of buiten het scherm vliegt, worden ze gewoon uitgehaald uit list.
            for (int i = 0; i < vijandLuchtList.Count; i++)
            {
                if (!vijandLuchtList[i].alive || vijandLuchtList[i].Positie.X <1000 )
                {
                    vijandLuchtList.RemoveAt(i);
                    i--;
                }
            }


        }
        public void RemoveZombies()
        {
            for (int i = 0; i < level2.zombieList.Count; i++)
            {
                if (!level2.zombieList[i].alive)
                {
                    level2.zombieList.RemoveAt(i);
                    i--;
                }
            }
        }
        private bool CheckCollision()
        {
            for (int i = 0; i < collideObjecten.Count; i++)
            {


                if (piloot.GetCollisionRectangle().Intersects(collideObjecten[i].GetCollisionRectangle()))
                {

                    Console.WriteLine("CamPOS = " + camPos.X);
                    Console.WriteLine("pilot x = " + piloot.pilootRec.X);
                    Console.WriteLine("pilot y = " + piloot.pilootRec.Y);
                    Console.WriteLine("blok x= " + collideObjecten[i].GetCollisionRectangle().X);
                    Console.WriteLine("blok y= " + collideObjecten[i].GetCollisionRectangle().Y);


                    piloot.positie.Y = collideObjecten[i].GetCollisionRectangle().Top+10;
                    piloot.isJumping = false;
                    piloot.parachuteWeg = true;

                    return true;

                }

            }

            return false;

        }
       
        }
    }

    
