//Really simple AI that trverses sequential nodes to form a patrol.

﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Text;

namespace Spaces
{
    class AI
    {
        //List of all enemies
        public List<Enemy> allEnemy = new List<Enemy>();
        public Texture2D baseText;

        public AI()
        {

        }

        public void Load(ContentManager content)
        {
            baseText = content.Load<Texture2D>("player");
        }
        
        //Spawns all of the enemies passed into AI
        public void spawnEnemies(List<Vector2> eAmount)
        {
            foreach (Vector2 enemy in eAmount)
            {
                Enemy test1 = new Enemy(enemy, baseText);
                allEnemy.Add(test1);
            }
        }

        public void draw(SpriteBatch sB)
        {
            foreach (Enemy enemy in allEnemy)
            {
                enemy.enemyDraw(sB);
            }
        }

        public void update()
        {
            foreach (Enemy enemy in allEnemy)
            {
                enemy.update();
            }
        }
    }
}
