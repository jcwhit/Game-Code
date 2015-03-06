//This was the player class for a game that got scrapped but was a rougelike bullet hell game.

﻿using System;
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
    class Player
    {
        PlayerIndex player;
        Vector2 pos, vel;
        Texture2D  projText;
        //Animations
        AnimatedTexture2D pText, cTxt, backDiagIdleTxt, backDiagRunTxt, backIdleTxt, backRunTxt, diagFrontIdleTxt, diagFrontRunTxt, frontRunTxt, idleFrontTxt, sideRunTxt, sideIdleTxt ;
        float rot, rf, rotT;
        GamePadState controller;
        List<Projectile> projs = new List<Projectile>(50);
        bool pushed = true;
        int coolDown = 0;
        int soundLength;
        SpriteEffects effect = SpriteEffects.None;
        SoundEffect sEffect;
        SoundEffectInstance soundEffectInstance;
        
        //Constructor intializes the player at the specified orgin. Was going to add an orgin animation but never got around to it
        public Player(PlayerIndex pNum, Vector2 startPos, float rotationFactor)
        {
            player = pNum;
            pos = startPos;
            //originText = new Vector2(pText.Width / 2, pText.Height / 2);
            rf = rotationFactor;
            rot = 0;
            vel = new Vector2(5, 5);
            soundEffectInstance = sEffect.CreateInstance();
            soundLength = 0;
            //rotT = (((float)Math.PI) / 2);
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public float getRot()
        {
            return rotT;
        }
        
        //Contoller based movement
        public void movementController()
        {

            pos.Y -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * 5;
            pos.X += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * 5;
            
            //Moves the player and updates the sprite for the animation
            if (GamePad.GetState(player).ThumbSticks.Left.Y < 0 || GamePad.GetState(player).ThumbSticks.Left.Y > 0 || GamePad.GetState(player).ThumbSticks.Left.X > 0 || GamePad.GetState(player).ThumbSticks.Left.X < 0)
            {
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                {
                    cTxt = frontRunTxt;
                    effect = SpriteEffects.None;
                    rotT = (((float)Math.PI) / 2);
                    //Dont remember adding sounds
                    if(soundLength > 0 )
                    {
                        soundEffectInstance.Play();
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                {
                    cTxt = backRunTxt;
                    effect = SpriteEffects.None;
                    rotT = (((float)Math.PI * 3) / 2);
                    sEffect.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                {
                    cTxt = sideRunTxt;
                    effect = SpriteEffects.FlipHorizontally;
                    rotT = 0;
                    sEffect.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                {
                    cTxt = sideRunTxt;
                    effect = SpriteEffects.None;
                    rotT = ((float)Math.PI);
                    sEffect.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0 && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                {
                    cTxt = backDiagRunTxt;
                    effect = SpriteEffects.None;
                    rotT = (((float)Math.PI * 5) / 4);
                    sEffect.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0 && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                {
                    cTxt = diagFrontRunTxt;
                    effect = SpriteEffects.FlipHorizontally;
                    rotT = (((float)Math.PI) / 4);
                    sEffect.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0 && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                {
                    cTxt = diagFrontRunTxt;
                    effect = SpriteEffects.None;
                    rotT = (((float)Math.PI * 3) / 4);
                    sEffect.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0 && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                {
                    cTxt = backDiagRunTxt;
                    //flips the character to face the other way
                    effect = SpriteEffects.FlipHorizontally;
                    rotT = (((float)Math.PI * 7) / 4);
                    sEffect.Play();
                }
            }
            
            //If no inputs, this would run the idle animations
            else
            {
                if (rotT == (((float)Math.PI) / 2))
                {
                    cTxt = idleFrontTxt;
                    effect = SpriteEffects.None;
                }
                if (rotT == (((float)Math.PI * 3) / 2))
                {
                    cTxt = backIdleTxt;
                    effect = SpriteEffects.None;
                }
                if (rotT == 0)
                {
                    cTxt = sideIdleTxt;
                    effect = SpriteEffects.FlipHorizontally;
                }
                if (rotT == ((float)Math.PI))
                {
                    cTxt = sideIdleTxt;
                    effect = SpriteEffects.None;
                }
                if (rotT == (((float)Math.PI * 5) / 4))
                {
                    cTxt = backDiagIdleTxt;
                    effect = SpriteEffects.None;
                }
                if (rotT == (((float)Math.PI) / 4))
                {
                    cTxt = diagFrontIdleTxt;
                    effect = SpriteEffects.FlipHorizontally;
                }
                if (rotT == (((float)Math.PI * 3) / 4))
                {
                    cTxt = diagFrontIdleTxt;
                    effect = SpriteEffects.None;
                }
                if (rotT == (((float)Math.PI * 7) / 4))
                {
                    cTxt = backDiagIdleTxt;
                    effect = SpriteEffects.FlipHorizontally;
                }
                //rot = 0;
            }
        }
        
        //Same as above except for keyboard controlls
        public void movementWASD()
        {
            KeyboardState kb = Keyboard.GetState();
            Vector2 velocity = Vector2.Zero;

            if (kb.IsKeyDown(Keys.W))
            {
                velocity.Y -= vel.Y;
                cTxt = backRunTxt;
                effect = SpriteEffects.None;
                rotT = (((float)Math.PI * 3) / 2);
                sEffect.Play();
            }
            if (kb.IsKeyDown(Keys.S))
            {
                velocity.Y += vel.Y;
                cTxt = frontRunTxt;
                effect = SpriteEffects.None;
                rotT = (((float)Math.PI) / 2);
                sEffect.Play();
            }
            if (kb.IsKeyDown(Keys.A))
            {
                velocity.X -= vel.X;
                cTxt = sideRunTxt;
                effect = SpriteEffects.None;
                rotT = ((float)Math.PI);
                sEffect.Play();
            }
            if (kb.IsKeyDown(Keys.D))
            {
                velocity.X += vel.X;
                cTxt = sideRunTxt;
                effect = SpriteEffects.FlipHorizontally;
                rotT = 0;
                sEffect.Play();
            }

            //Normalize for diag movement
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            
            //This section is for diagonal movement animations and force
            if (kb.IsKeyDown(Keys.W) && kb.IsKeyDown(Keys.D))
            {
                cTxt = backDiagRunTxt;
                effect = SpriteEffects.FlipHorizontally;
                rotT = (((float)Math.PI * 7) / 4);
                sEffect.Play();
            }

            if (kb.IsKeyDown(Keys.W) && kb.IsKeyDown(Keys.A))
            {
                cTxt = backDiagRunTxt;
                effect = SpriteEffects.None;
                rotT = (((float)Math.PI) / 4);
                sEffect.Play();
            }

            if (kb.IsKeyDown(Keys.S) && kb.IsKeyDown(Keys.D))
            {
                cTxt = diagFrontRunTxt;
                effect = SpriteEffects.FlipHorizontally;
                 rotT = (((float)Math.PI) / 4);
                 sEffect.Play();
            }

            if (kb.IsKeyDown(Keys.S) && kb.IsKeyDown(Keys.A))
            {
                cTxt = diagFrontRunTxt;
                effect = SpriteEffects.None;
                rotT = (((float)Math.PI * 3) / 4);
                sEffect.Play();
            }
            //player moves
            pos.X += velocity.X * 5;
            pos.Y += velocity.Y * 5;   
        }
        
        //Held all the skills for the hero, never got to add more than a simple projectile
        public void skills()
        {
            KeyboardState kb = Keyboard.GetState();
            if ((controller.IsButtonDown(Buttons.RightTrigger) || kb.IsKeyDown(Keys.Space)) && pushed)
            {
                pushed = false;
                coolDown++;
                Projectile p = new Projectile(pos, projText, getRot(), new Vector2(10,10));
                projs.Add(p);
                p.setFired();
                sEffect.Play();
            }
        }

        public void Update()
        {
            controller = GamePad.GetState(player);
            movementController();

            //WASD Controls
            movementWASD();

            skills();
            cTxt.update();

            //Cool Down for porjectile-----------------------
            if (coolDown == 100)
            {
                pushed = true;
                coolDown = 0;
            }
            if (coolDown > 0)
            {
                coolDown++;
            }
            //-----------------------------------------------

            //Update Projectiles
            foreach (Projectile p in projs)
            {
                p.Update();
            }
        }
        
        //Loads all the textures for the character and animations
        public void playerLoad(ContentManager content)
        {
            //Animations
            backDiagIdleTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Back Diagonal Idle"), 20, 10, 0, false, false);
            backDiagRunTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Back Diagonal Run"), 20, 10, 0, false, false);
            backIdleTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Back Idle"), 20, 10, 0, false, false);
            backRunTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Back Run"), 20, 10, 0, false, false);
            diagFrontIdleTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Diagonal Front Idle"), 20, 10, 0, false, false);
            diagFrontRunTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Diagonal Front Run"), 20, 10, 0, false, false);
            frontRunTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Front Run"), 20, 10, 0, false, false);
            idleFrontTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Idle Front"), 20, 10, 0, false, false);
            sideRunTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Side Run"), 20, 10, 0, false, false);
            sideIdleTxt = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Side Idle"), 20, 10, 0, false, false);
            cTxt = pText = new AnimatedTexture2D(content.Load<Texture2D>("Boxing Bro Idle Front"), 20, 10, 0, false, false);

            //Sounds
            sEffect = content.Load<SoundEffect>("clap");
            projText = content.Load<Texture2D>("projectileTest");
        }
        
        //Standard draw function in XNA
        public void draw(SpriteBatch sB)
        {
            cTxt.draw(sB,pos,Color.White,rot, new Vector2(0,0),2,effect,0.5f);

            foreach (Projectile p in projs)
            {
                if (p.getFired() == true)
                {
                    p.draw(sB);
                }
            }
        }
    }
}
