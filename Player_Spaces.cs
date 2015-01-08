using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;

namespace Spaces
{
    class Player
    {
        Vector2 pos, drawpos, attackPos;
        PlayerIndex playerNum;
        GamePadState controller;
        Texture2D txt, attackTxt;
        Color color, healthColor, attackColor;
        float speed;
        Boolean healthCheck = false, healthCheckp2 = false;
        public bool[,]allspaces;
        Rectangle bb;
        Level playerLevel;


        public Player(PlayerIndex playernum, Vector2 startingpos, Texture2D txtN, float spd, Texture2D aTxt,bool[,]spaces, Level lvl)
        {
            playerNum = playernum;
            pos = startingpos;
            drawpos = new Vector2((int)pos.X, (int)pos.Y);
            txt = txtN;
            color = Color.White;
            healthColor = Color.Red;
            attackColor = Color.White;
            speed = spd;
            attackTxt = aTxt;
            attackPos = pos;
            allspaces = spaces;
            bb = new Rectangle((int)pos.X, (int)pos.Y, txt.Height, txt.Width);
            playerLevel = lvl;
        }

        private void movement()
        {
            KeyboardState kb = Keyboard.GetState();
             
            Vector2 poso = new Vector2(pos.X + txt.Width/2, pos.Y + txt.Height/2);
            Vector2 velocity = Vector2.Zero;
          
            if (playerNum.Equals(PlayerIndex.One))
            {
                if (kb.IsKeyDown(Keys.W) && allspaces[(int)poso.X/playerLevel.getTileSize(),((int)poso.Y - (int)speed-playerLevel.getTileSize()-txt.Height/2)
                    / playerLevel.getTileSize()] && allspaces[((int)poso.X / playerLevel.getTileSize()) - 1, ((int)poso.Y - (int)speed - playerLevel.getTileSize() - txt.Height / 2)
                    / playerLevel.getTileSize()] && allspaces[((int)poso.X / playerLevel.getTileSize()) + 1, ((int)poso.Y - (int)speed - playerLevel.getTileSize() - txt.Height / 2)
                    / playerLevel.getTileSize()])
                    velocity.Y -= speed;
                if (kb.IsKeyDown(Keys.S) && allspaces[(int)poso.X/playerLevel.getTileSize(),((int)poso.Y + (int)speed)/playerLevel.getTileSize()])
                    velocity.Y += speed;
                if (kb.IsKeyDown(Keys.A) && allspaces[((int)poso.X - (int)speed - playerLevel.getTileSize() - txt.Width / 2)
                    / playerLevel.getTileSize(), (int)poso.Y / playerLevel.getTileSize()])
                    velocity.X -= speed;
                if (kb.IsKeyDown(Keys.D) && allspaces[((int)poso.X + (int)speed) / playerLevel.getTileSize(), (int)poso.Y / playerLevel.getTileSize()])
                    velocity.X += speed;
                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }
                //player moves
                pos.X += velocity.X * speed;
                pos.Y += velocity.Y * speed;   
            }

            if (playerNum.Equals(PlayerIndex.Two))
            {
                if (kb.IsKeyDown(Keys.Up) && allspaces[(int)poso.X/playerLevel.getTileSize(),((int)poso.Y - (int)speed-playerLevel.getTileSize()-txt.Height/2)
                    /playerLevel.getTileSize()])
                    velocity.Y -= speed;
                if (kb.IsKeyDown(Keys.Down) && allspaces[(int)poso.X/playerLevel.getTileSize(),((int)poso.Y + (int)speed)/playerLevel.getTileSize()])
                    velocity.Y += speed;
                if (kb.IsKeyDown(Keys.Left) && allspaces[((int)poso.X - (int)speed - playerLevel.getTileSize() - txt.Width / 2)
                    / playerLevel.getTileSize(), (int)poso.Y / playerLevel.getTileSize()])
                    velocity.X -= speed;
                if (kb.IsKeyDown(Keys.Right) && allspaces[((int)poso.X + (int)speed) / playerLevel.getTileSize(), (int)poso.Y / playerLevel.getTileSize()])
                    velocity.X += speed;
                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }
                //player moves
                pos.X += velocity.X * speed;
                pos.Y += velocity.Y * speed;   
            }

            pos.X += controller.ThumbSticks.Left.X * 3;
            pos.Y -= controller.ThumbSticks.Left.Y * 3;
        }

        public void playerAttacks(SpriteBatch sB)
        {

        }

        public void update(SpriteBatch sB)
        {
            movement();
            playerAttacks(sB);
            controller = GamePad.GetState(playerNum);
        }

        public void draw(SpriteBatch sB)
        {
            sB.Draw(txt, pos, null, color, 0, new Vector2(8, 8), 1f, SpriteEffects.None, .1f);
        }

        public void updateHealth(Hud health)
        {
            if (healthCheck == true)
            {
                health.decreaseHelth();
            }
        }

        public void updateHealthp2(Hud health)
        {
            if (healthCheckp2 == true)
            {
                health.decreaseHelthp2();
            }
        }

        public void drawAttacks(SpriteBatch sB, double health)
        {
            KeyboardState kb = Keyboard.GetState();

            if (playerNum.Equals(PlayerIndex.One))
            {
                attackPos = pos + new Vector2(9, 5);
                if (kb.IsKeyDown(Keys.C))
                {
                    sB.Draw(attackTxt, attackPos, null, attackColor, 1.55f, new Vector2(8, 8), 1f, SpriteEffects.None, .1f);
                    healthCheck = true;
                }
                else
                {
                    healthCheck = false;
                }
            }

            if (playerNum.Equals(PlayerIndex.Two))
            {
                attackPos = pos + new Vector2(9, 5);
                if (kb.IsKeyDown(Keys.Space))
                {
                    sB.Draw(attackTxt, attackPos, null, attackColor, 1.55f, new Vector2(8, 8), 1f, SpriteEffects.None, .1f);
                    healthCheckp2 = true;
                }
                else
                {
                    healthCheckp2 = false;
                }
            }
        }

        public Vector2 getPos()
        {
            return pos;
        }
        public void newlvlchanger(Level lvl)
        {
            playerLevel = lvl;
        }
    }
}
