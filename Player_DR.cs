using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;


namespace DragonRacer
{
    class Player
    {
        Vector2 facing;
        float topSpeed;
        Vector2 cSpeed;
        Vector2 pos, txtOriginD, sTxtOrigin, sPos, sTxtOriginF;
        Vector2 friction;
        PlayerIndex playerNum;
        GamePadState controller;
        Vector2 gamePadLeft;
        Texture2D txtDragon, swordText;
        float speedTime;
        float rotationFactor;
        float rotation, sRotation;
        float triggerButton;
        public Rectangle bb, bbS;
        bool[] bounds = new bool[4];
        bool[] hits = new bool[4];
        AnimatedTexture2D atxt;

        public Player() { }
        public Player(PlayerIndex pNum, Texture2D cTxt, Vector2 startPos, float topSpeedI, Vector2 playerAccel, float rF, Texture2D sText)
        {
            pos = startPos;
            sPos.X = pos.X - 5;
            sPos.Y = pos.Y;
            playerNum = pNum;
            txtDragon = cTxt;
            topSpeed = topSpeedI;
            swordText = sText;
            //pAccel = playerAccel;
            friction = new Vector2(.03f, .03f);
            //accel = playerAccel;
            cSpeed = new Vector2(0,0);
            rotationFactor = rF;
            sTxtOrigin = new Vector2(0, 25);
            sTxtOriginF = new Vector2(0, -5);
            hits[0] = false;
            hits[1] = false;
            hits[2] = false;
            hits[3] = false;
            atxt = new AnimatedTexture2D(cTxt, 64, 10, 0,true,false);
            
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public Vector2 getCSpeed()
        {
            return cSpeed;
        }

        public Vector2 getFacing()
        {
            return facing;
        }

        public float getRotation()
        {
            return rotation;
        }

        public Vector2 getTxtOrigin()
        {
            return txtOriginD;
        }

        public void movement(List<Player> pSwords)
        {
            KeyboardState kb = Keyboard.GetState();
            Vector2 zero = Vector2.Zero;
            triggerButton = controller.Triggers.Right;
            double wPressed;
            gamePadLeft = controller.ThumbSticks.Left;
           //hit = false;
            hits[0] = false;
            hits[1] = false;
            bool checker = false;
           

            if (triggerButton > 0 || kb.IsKeyDown(Keys.W))
            {
                //speedTime += .1f;
                if (speedTime < topSpeed)
                {
                    speedTime += .01f;
                    if (kb.IsKeyDown(Keys.W))
                    {
                        facing.Y = (float)Math.Sin(rotation) * 1;
                        facing.X = (float)Math.Cos(rotation) * 1;
                    }
                    else
                    {
                        facing.Y = (float)Math.Sin(rotation) * triggerButton;
                        facing.X = (float)Math.Cos(rotation) * triggerButton;
                    }
                    //cSpeed = accel * speedTime;
                   if (bounds[2] && (float)Math.Sin(rotation) * triggerButton < 0)
                    {
                        cSpeed.Y = 0;
                        hits[0] = true;
                        speedTime -= .01f;
                    }
                    if (bounds[0] && (float)Math.Sin(rotation) * triggerButton > 0)
                    {
                        cSpeed.Y = 0;
                        hits[0] = true;
                        speedTime -= .01f;
                    }
                    if (!hits[0])
                    {
                        cSpeed.Y = facing.Y * speedTime;
                        hits[0] = false;
                    }
                    if (bounds[3] && (float)Math.Cos(rotation) * triggerButton < 0)
                    {
                        cSpeed.X = 0;
                        hits[1] = true;
                        speedTime -= .01f;
                    }
                    if (bounds[1] && (float)Math.Cos(rotation) * triggerButton > 0)
                    {
                        cSpeed.X = 0;
                        hits[1] = true;
                        speedTime -= .01f;
                    }
                    if (!hits[1])
                    {
                        cSpeed.X = facing.X * speedTime;
                        hits[1] = false;
                    }
                }

                else
                {
                    speedTime = topSpeed;
                    facing.Y = (float)Math.Sin(rotation) * triggerButton;
                    facing.X = (float)Math.Cos(rotation) * triggerButton;
                    if (bounds[2] && (float)Math.Sin(rotation) * triggerButton < 0)
                    {
                        cSpeed.Y = 0;
                        hits[0] = true;
                        speedTime -= .01f;
                    }
                    if (bounds[0] && (float)Math.Sin(rotation) * triggerButton > 0)
                    {
                        cSpeed.Y = 0;
                        hits[0] = true;
                        speedTime -= .01f;
                    }
                    if (!hits[0])
                    {
                        cSpeed.Y = facing.Y * speedTime;
                        hits[0] = false;
                    }
                    if (bounds[3] && (float)Math.Cos(rotation) * triggerButton < 0)
                    {
                        cSpeed.X = 0;
                        hits[1] = true;
                        speedTime -= .01f;
                    }
                    if (bounds[1] && (float)Math.Cos(rotation) * triggerButton > 0)
                    {
                        cSpeed.X = 0;
                        hits[1] = true;
                        speedTime -= .01f;
                    }
                    if (!hits[1])
                    {
                        cSpeed.X = facing.X * speedTime;
                        hits[1] = false;
                    }
                }


            }

            else if (speedTime >= 0)
            {
                speedTime -= 0.01f;
                facing.Y = (float)Math.Sin(rotation);
                facing.X = (float)Math.Cos(rotation);
                //cSpeed = accel * speedTime;
                if (bounds[2] && (float)Math.Sin(rotation)  < 0)
                {
                    cSpeed.Y = 0;
                    hits[0] = true;
                    speedTime -= .01f;
                }
                if (bounds[0] && (float)Math.Sin(rotation)  > 0)
                {
                    cSpeed.Y = 0;
                    hits[0] = true;
                    speedTime -= .01f;
                }
                if (!hits[0])
                {
                    cSpeed.Y = facing.Y * speedTime;
                    hits[0] = false;
                }
                if (bounds[3] && (float)Math.Cos(rotation)  < 0)
                {
                    cSpeed.X = 0;
                    hits[1] = true;
                    speedTime -= .01f;
                }
                if (bounds[1] && (float)Math.Cos(rotation)  > 0)
                {
                    cSpeed.X = 0;
                    speedTime -= .01f;
                    hits[1] = true;
                }
                if (!hits[1])
                {
                    cSpeed.X = facing.X * speedTime;
                    hits[1] = false;
                }
            }

            else
            {
                speedTime = 0;
                cSpeed = zero;
            }
            
            if (controller.ThumbSticks.Left.X != 0)
            {
                if(kb.IsKeyDown(Keys.A))
                {
                    rotation += 1;
                }
                if(kb.IsKeyDown(Keys.D))
                {
                    rotation -= 1;
                }
                if (controller.IsConnected)
                {
                    rotation += (controller.ThumbSticks.Left.X * rotationFactor);
                }
            }

            foreach (Player p in pSwords)
            {
                if (!bounds[0] && !bounds[1] && !bounds[2] && !bounds[3])
                {
                    if (p != this)
                    {
                        if (bbS.Intersects(p.bbS))
                        {
                            cSpeed = -cSpeed;
                        }
                        if (bbS.Intersects(p.bb))
                        {
                            cSpeed = -cSpeed;
                        }
                        if (bb.Top.Equals(p.bb.Bottom + .01))
                        {
                            cSpeed = -cSpeed;
                        }
                        if (bb.Bottom.Equals(p.bb.Top - .01))
                        {
                            cSpeed = -cSpeed;
                        }
                        if (bb.Right.Equals(p.bb.Left - .01))
                        {
                            cSpeed = -cSpeed;
                        }
                        if (bb.Left.Equals(p.bb.Right + .01))
                        {
                            cSpeed = -cSpeed;
                        }
                    }
                }
            }

            pos += cSpeed;
        }

        public void collision(bool[,] boundries)
        {
            bounds[0] = false;
            bounds[1] = false;
            bounds[2] = false;
            bounds[3] = false;

            for(int y = ((int)pos.Y - (atxt.Height/3)); y < pos.Y + (atxt.Height/3);y++)
            {
                if (boundries[(int)pos.X - (atxt.Width / 2), y])
                {
                    bounds[3] = true;
                   
                }
               
                if(boundries[(int)pos.X + (atxt.Width/2),y])
                {
                    bounds[1] = true;
                   
                }
               
            }

            for(int x = ((int)pos.X - (atxt.Width/3)); x < pos.X + (atxt.Width/3);x++)
            {
                if(boundries[x, (int)pos.Y - (atxt.Height/2)])
                {
                    bounds[2] = true;
                    
                }
                
                if(boundries[x, (int)pos.Y + (atxt.Height/2)])
                {
                    bounds[0] = true;

                }
                
            }

            bool hit = false;
        
        }

        public void draw(SpriteBatch sb)
        {
            atxt.draw(sb, pos, Color.White, rotation, txtOriginD, 1f, SpriteEffects.None, 0.5f);
            if(controller.IsButtonDown(Buttons.A))
            {
                sb.Draw(swordText, sPos, null, Color.White, 0, sTxtOrigin, 2f, SpriteEffects.None, 0.4f);
            }
            if (controller.IsButtonDown(Buttons.B))
            {
                sb.Draw(swordText, sPos, null, Color.White, 0, sTxtOriginF, 2f, SpriteEffects.FlipVertically, 0.4f);
            }
        }

        public void Update(Level cLevel, List<Player> player)
        {
            atxt.update();
            sPos.X = pos.X - 5;
            sPos.Y = pos.Y;
            controller = GamePad.GetState(playerNum);
            bb = new Rectangle((int)pos.X, (int)pos.Y,atxt.Width,atxt.Height);
            bbS = new Rectangle((int)sPos.X, (int)sPos.Y, swordText.Width, swordText.Height);
            movement(player);
            collision(cLevel.getSolids());
        }

        
        public Texture2D getPlayerTxt()
        {
            return txtDragon;
        }
    }
}
