/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris2.Content
{
    class Block
    {
        Texture2D texture;
        int blockID;
        int[,] table2;
        int posX = 0;
        int posY = 0;
        int rotation = 0;
        int timer;
        bool falling = true;
        Game1 game;

        public Block(Texture2D newtexture, int block, Game1 game)
        {
            texture = newtexture;
            blockID = block;
            this.game = game;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if(falling == true)
                for (int i = 0; i < 3; i++)
                    for (int x = 0; x < 3; x++)
                        if(table2[x,i] == 1)
                            spritebatch.Draw(texture, new Vector2((posX+i)*texture.Width, (posY+x)*texture.Height), Color.White);
        }

        public void BlockUpdate()
        {
            timer++;
            //Falling
            if(timer > 50 && falling)
            {
                posY++;
                timer = 0;
            }

            //Rotation
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Down) && Game1.previouskeyboardstate.IsKeyUp(Keys.Down))
                rotation = (rotation + 1) % 4;

            //Moving
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Left) && Game1.previouskeyboardstate.IsKeyUp(Keys.Left))
                posX = MathHelper.Max(posX - 1, 0);
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Right) && Game1.previouskeyboardstate.IsKeyUp(Keys.Right))
                posX = MathHelper.Min(posX + 1, Game1.tablewidth-3);


            if (falling)
            {
                if ((Game1.TetrisTable[posX, posY + 3] == 1 && table2[0, 2] == 1) || (Game1.TetrisTable[posX + 1, posY + 3] == 1 && table2[1, 2] == 1) || (Game1.TetrisTable[posX + 2, posY + 3] == 1 && table2[2, 2] == 1))
                {
                    falling = false;
                    for (int i = 0; i < 3; i++)
                        for (int x = 0; x < 3; x++)
                            if (table2[i, x] == 1)
                                Game1.TetrisTable[posX + x, posY + i] = 1;
                    game.NewBlock();
                }
                if ((Game1.TetrisTable[posX, posY + 2] == 1 && table2[0, 1] == 1) || (Game1.TetrisTable[posX + 1, posY + 2] == 1 && table2[1, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 2] == 1 && table2[2, 1] == 1))
                {
                    falling = false;
                    for (int i = 0; i < 3; i++)
                        for (int x = 0; x < 3; x++)
                            if (table2[i, x] == 1)
                                Game1.TetrisTable[posX + x, posY + i] = 1;
                }
            }
        }

        public void BlockDraw()
        {
            //BlockID = 1
            if (blockID == 1)
            {
                if(rotation == 0)
                {
                    table2 = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if(rotation == 1)
                {
                    table2 = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table2 = new int[,] {
                                        {0,1,0,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table2 = new int[,] {
                                        {0,1,0,} ,
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
            }


            //BlockID = 2
            if (blockID == 2)
            {
                if (rotation == 0)
                {
                    table2 = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table2 = new int[,] {
                                        {0,0,1,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table2 = new int[,] {
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table2 = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {1,0,0,} ,
                                    };
                }
            }
        }
    }
}*/
