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
        Game1 game;
        int[,] block;
        Color color;

        public void Draw(SpriteBatch spritebatch, Texture2D texture)
        {
            BlockDraw();
            for (int i = 0; i < 3; i++)
                for (int x = 0; x < 3; x++)
                    if (block[x, i] == 1)
                        spritebatch.Draw(texture, new Vector2((460 + i) * texture.Width, (30 + x) * texture.Height), color);
        }

        public void BlockDraw()
        {
            //game.NextBlock = 1
            if (game.NextBlock == 1)
            {
                color = Color.Purple;

                    block = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,1,0,} ,
                                    };
            }

            if (game.NextBlock == 2)
            {
                color = Color.Orange;
                    block = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                    };
            }
            //game.NextBlock = 3
            if (game.NextBlock == 3)
            {
                color = Color.Blue;
                    block = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {1,1,0,} ,
                                    };
            }
            //game.NextBlock = 4
            if (game.NextBlock == 4)
            {
                color = Color.Red;
                    block = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,0,} ,
                                        {0,1,1,} ,
                                    };
            }
            //game.NextBlock = 5
            if (game.NextBlock == 5)
            {
                color = Color.Green;
                    block = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {1,1,0,} ,
                                    };
            }
            //game.NextBlock = 6
            if (game.NextBlock == 6)
            {
                color = Color.Pink;
                    block = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
            }
            //game.NextBlock = 7
            if (game.NextBlock == 7)
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
