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
    public class Score
    {
        public int playerScore, screenWidth, screenHeight;
        public SpriteFont scoreFont;
        public Vector2 scorePositie;
        public bool showScore;

        public Score()
        {
            playerScore = 0;
            screenHeight = 650;
            screenWidth = 1100;
            scoreFont = null;
            showScore = true;
            scorePositie = new Vector2(50,90);
        }
        public void LoadContent(ContentManager Content) {

            scoreFont = Content.Load<SpriteFont>("File");
        }
        public void Update(GameTime gametime)
        {
            KeyboardState keystate = Keyboard.GetState();
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (showScore)
            {
                spritebatch.DrawString(scoreFont,"Score - "+playerScore,scorePositie,Color.Red);
            }
        }

    }
}
