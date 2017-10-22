using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2
{
    class NextBlock
    {
        int[,] block;
        Color color;
        int nextblock = 1;

        public void Update()
        {
            //nextblock = game.NextBlock;
            BlockDraw();
        }

        public void Draw(SpriteBatch spritebatch, Texture2D texture, int next)
        {
            nextblock = next;
            BlockDraw();
            for (int i = 0; i < 3; i++)
                for (int x = 0; x < 3; x++)
                    if (block[x, i] == 1)
                        spritebatch.Draw(texture, new Vector2(i * texture.Width +500, x * texture.Height+ 60), color);
        }

        public void BlockDraw()
        {
            //nextblock = 1
            if (nextblock == 1)
            {
                color = Color.Purple;

                    block = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,1,0,} ,
                                    };
            }

            if (nextblock == 2)
            {
                color = Color.Orange;
                    block = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                    };
            }
            //nextblock = 3
            if (nextblock == 3)
            {
                color = Color.Blue;
                    block = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {1,1,0,} ,
                                    };
            }
            //nextblock = 4
            if (nextblock == 4)
            {
                color = Color.Red;
                    block = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,0,} ,
                                        {0,1,1,} ,
                                    };
            }
            //nextblock = 5
            if (nextblock == 5)
            {
                color = Color.Green;
                    block = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {1,1,0,} ,
                                    };
            }
            //nextblock = 6
            if (nextblock == 6)
            {
                color = Color.Pink;
                    block = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
            }
            //nextblock = 7
            if (nextblock == 7)
            {
                color = Color.Yellow;
                    block = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {0,1,1,} ,
                                    };
            }
        }
    }
}


































/// Made by Timo Lapre (5919622) & Niels Visser