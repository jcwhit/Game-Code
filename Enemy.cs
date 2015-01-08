using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;

namespace Spaces
{
    class Enemy
    {
        Vector2 pos;
        Texture2D texture;
        Color drawColor, color;
        Rectangle bb;

        public Enemy(Vector2 posn, Texture2D eTexture)
        {
            pos = posn;
            texture = eTexture;
            bb = new Rectangle((int)pos.X, (int)pos.Y, eTexture.Height, eTexture.Width);
            color = Color.White;
            drawColor = Color.Blue;
        }

        public Rectangle getbb()
        {
            return bb;
        }

        public Vector2 getPos()
        {
            return pos;
        }
        public void update()
        {
            bb = new Rectangle((int)pos.X, (int)pos.Y, texture.Height, texture.Width);
        }

        public void enemyDraw(SpriteBatch sB)
        {
            sB.Draw(texture, pos, drawColor);
        }


    }
}
