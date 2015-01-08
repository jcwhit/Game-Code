using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace SpaceJam
{
    class Projectile
    {
        Vector2 pos, vel, origin;
        Texture2D projectileTxt;
        readonly float rot;
        bool fired = false;

        public Projectile(Vector2 pos, Texture2D proj, float rotation, Vector2 vel)
        {
            this.pos = pos;
            projectileTxt = proj;
            rot = rotation;
            origin = new Vector2(projectileTxt.Width / 2, projectileTxt.Height / 2);
            this.vel = vel;
        }

        public void setFired()
        {
            fired = true;
        }

        public bool getFired()
        {
            return fired;
        }

        public void fire()
        {
            if (rot == (((float)Math.PI) / 2))
            {
                pos.Y += vel.Y;
            }
            if (rot == (((float)Math.PI * 3) / 2))
            {
                pos.Y -= vel.Y;
            }
            if (rot == 0)
            {
                pos.X += vel.X;
            }
            if (rot == ((float)Math.PI))
            {
                pos.X -= vel.X;
            }
            if (rot == (((float)Math.PI * 5) / 4))
            {
                pos.X -= vel.X;
                pos.Y -= vel.Y;
            }
            if (rot == (((float)Math.PI) / 4))
            {
                pos.X += vel.X;
                pos.Y += vel.Y;
            }
            if (rot == (((float)Math.PI * 3) / 4))
            {
                pos.X -= vel.X;
                pos.Y += vel.Y;
            }
            if (rot == (((float)Math.PI * 7) / 4))
            {
                pos.X += vel.X;
                pos.Y -= vel.Y;
            }
            //pos += projectVel;
        }

        public void Update()
        {
            fire();
        }

        public void draw(SpriteBatch sB)
        {
            sB.Draw(projectileTxt,pos,null,Color.White,rot,origin,1f,SpriteEffects.None,0.5f);
        }
    }
}
