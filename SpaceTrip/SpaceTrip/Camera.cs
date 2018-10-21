using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTrip
{
    class Camera
    {
        private readonly Viewport _viewport;

        public Camera(Viewport viewport)
        {
            _viewport = viewport;

        
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }
        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);
            }
        }
        public Vector2 Position;
        public Vector2 Origin { get; set; }
        public Matrix GetViewMatrix()
        {
            // Position = new Vector2(100, 200);
            Matrix m =
                Matrix.CreateTranslation(new Vector3(-Position, 0));
                
   
            return m;
        }
    }
}
