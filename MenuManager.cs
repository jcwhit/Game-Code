//This was for creating a reuseable menu system for one of the GCGC game. We scrapped the idea but some of this was good.
//Basically it creates panels that can be edited to add other panels and text boxes as well as buttons.


﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Text;

namespace LongestRun
{
    class MenuManager
    {
        Texture2D bG;
        Vector2 wS, pos;
        Rectangle window;
        

        public MenuManager(Texture2D backGround, Vector2 windowSize, Vector2 position)
        {
            bG = backGround;
            wS = windowSize;
            pos = position;
            window = new Rectangle((int)pos.X, (int)pos.Y, (int)wS.X, (int)wS.Y);
        }

        public void update()
        {

        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(bG, window, Color.Orange);
        }
    }

    class Panel
    {
        Texture2D bG;
        Rectangle panel;
        Vector2 pSize, pos;
        Rectangle[] buttonLocations;
        bool[] buttonNumberCheck;

        public Panel(Texture2D backGround, Vector2 windowSize, Vector2 windowPos, Vector2 position, Vector2 panelSize, int numButtons)
        {
            bG = backGround;
            buttonLocations = new Rectangle[numButtons];
            buttonNumberCheck = new bool[numButtons];
            
            //Check if panel fits within window
            if(((position.X > windowPos.X) && (position.Y > windowPos.Y))&&((panelSize.X < windowSize.X)&&(panelSize.Y < windowSize.Y)))
            {
                pos = position;
                pSize = panelSize;
            }

            
            panel = new Rectangle((int)pos.X, (int)pos.Y, (int)pSize.X, (int)pSize.Y);
        }

        
        //This creates buttons for panels buttonTxt is the writting on the button
        public void ButtonBuilder(Vector2 buttonPos, String buttonTxt, SpriteFont sF, SpriteBatch sBTxt, int buttonSize, int buttonNum)
        {
            SpriteFont font = sF;
            Vector2 bPos = new Vector2(0f,0f);
            int rectLength = 0;// = buttonTxt.Length;
            

            if ((buttonPos.X > pos.X) && (buttonPos.Y > pos.Y))
            {
                bPos = buttonPos;
            }

            for(int i = 0; i < buttonTxt.Length; i++)
            {
                rectLength += 12;
            }

            Rectangle button = new Rectangle((int)buttonPos.X, (int)buttonPos.Y, rectLength, buttonSize);

            buttonLocations[buttonNum] = button;

            //(this).draw(sBTxt);
            sBTxt.Draw(bG, button, Color.Violet);
            sBTxt.DrawString(sF, buttonTxt, new Vector2(bPos.X + 1, bPos.Y + 1), Color.White, 0f, new Vector2(0,0),1f, SpriteEffects.None, 1f);
        }

        //this returns the true or false index of the button that has been clicked
        public void mouseChecker(int button)
        {
            if (buttonLocations[button].Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                buttonNumberCheck[button] = true;
            }
            else
            {
                buttonNumberCheck[button] = false;
            }
        }

        public void update()
        {
            for (int i = 0; i < buttonNumberCheck.Length; i++)
            {
                mouseChecker(i);
            }
        }

        public void draw(SpriteBatch sB)
        {
            sB.Draw(bG, panel, Color.Navy);

            if (buttonNumberCheck[0] == true)
            {
                sB.Draw(bG, new Rectangle(0,0, 50, 50), Color.Black);
            }
        }

        public void draw(SpriteBatch sB, Texture2D customTxture)
        {
            sB.Draw(customTxture, pos, Color.White);
        }
    }

}
