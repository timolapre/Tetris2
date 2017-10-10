using Microsoft.Xna.Framework;
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
        int[,] table;
        int posX = 0;
        int posY = 0;
        int rotation = 0;
        int timer;
        bool falling = true;
        Game1 game;
        Color color;

        public Block(Texture2D newtexture, int block, Game1 game)
        {
            texture = newtexture;
            blockID = block;
            BlockDraw();
            this.game = game;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (falling == true)
                for (int i = 0; i < 3; i++)
                    for (int x = 0; x < 3; x++)
                        if (table[x, i] == 1)
                            spritebatch.Draw(texture, new Vector2((posX + i) * texture.Width, (posY + x) * texture.Height), color);
        }

        public void BlockUpdate()
        {
            timer++;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) timer += 7;
            //Falling
            if (timer > 40 && falling)
            {
                posY++;
                timer = 0;
            }


            //Rotation
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Up) && Game1.previouskeyboardstate.IsKeyUp(Keys.Up))
                rotation = (rotation + 1) % 4;

            //Moving
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Left) && Game1.previouskeyboardstate.IsKeyUp(Keys.Left))
                if ((Game1.TetrisTable[MathHelper.Max(posX - 1, 0), posY] == 1 && table[0, 0] == 1) || (Game1.TetrisTable[MathHelper.Max(posX - 1, 0), posY + 1] == 1 && table[1, 0] == 1) || (Game1.TetrisTable[MathHelper.Max(posX - 1, 0), posY + 2] == 1 && table[2, 0] == 1) || (Game1.TetrisTable[MathHelper.Max(posX, 0), posY] == 1 && table[0, 1] == 1) || (Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 1] == 1 && table[1, 1] == 1) || (Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 2] == 1 && table[2, 1] == 1)) { }
                    else posX -= 1;
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Right) && Game1.previouskeyboardstate.IsKeyUp(Keys.Right))
                if ((Game1.TetrisTable[MathHelper.Min(posX + 3, Game1.tablewidth-3), posY] == 1 && table[0, 2] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 3, Game1.tablewidth - 3), posY + 1] == 1 && table[1, 2] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 3, Game1.tablewidth - 3), posY + 2] == 1 && table[2, 2] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY] == 1 && table[0, 1] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY + 1] == 1 && table[1, 1] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY + 2] == 1 && table[2, 1] == 1)) { }
                    else posX += 1;
            
            if(table[0,0] == 0 && table[1,0] == 0 && table[2,0] == 0)
            {
                posX = MathHelper.Min(MathHelper.Max(posX, -1), Game1.tablewidth - 3);
            }
            else if (table[0, 2] == 0 && table[1, 2] == 0 && table[2, 2] == 0)
            {
                posX = MathHelper.Min(MathHelper.Max(posX, 0), Game1.tablewidth - 2);
            }
            else
            {
                posX = MathHelper.Min(MathHelper.Max(posX, 0), Game1.tablewidth - 3);
            }


            if (falling)
            {
                if (table[2, 0] == 1 || table[2, 1] == 1 || table[2, 2] == 1)
                {
                    if ((Game1.TetrisTable[MathHelper.Max(posX,0), posY + 3] == 1 && table[2, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 3] == 1 && table[2, 1] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth-3), posY + 3] == 1 && table[2, 2] == 1))
                    {
                        if (timer > 30)
                        {
                            falling = false;
                            for (int i = 0; i < 3; i++)
                                for (int x = 0; x < 3; x++)
                                    if (table[i, x] == 1)
                                        Game1.TetrisTable[posX + x, posY + i] = 1;
                            Game1.createnewblock = 1;
                        }
                    }
                }
                if ((Game1.TetrisTable[MathHelper.Max(posX,0), posY + 2] == 1 && table[1, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 2] == 1 && table[1, 1] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY + 2] == 1 && table[1, 2] == 1))
                {
                    if(timer > 30)
                    {
                        falling = false;
                        for (int i = 0; i < 3; i++)
                            for (int x = 0; x < 3; x++)
                                if (table[i, x] == 1)
                                    Game1.TetrisTable[posX + x, posY + i] = 1;
                        Game1.createnewblock = 1;
                    }
                }
            }
        }

        public void BlockDraw()
        {
            //BlockID = 1
            if (blockID == 1)
            {
                color = Color.Purple;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
            }


            //BlockID = 2
            if (blockID == 2)
            {
                color = Color.Orange;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,0,1,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {1,0,0,} ,
                                    };
                }
            }
            //BlockID = 3
            if (blockID == 3)
            {
                color = Color.Blue;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {1,1,0,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,0,1,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {0,1,1,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {1,0,0,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
            }
            //BlockID = 4
            if (blockID == 4)
            {
                color = Color.Red;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,0,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,0,1,} ,
                                        {0,1,1,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {1,1,0,} ,
                                        {0,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {1,1,0,} ,
                                        {1,0,0,} ,
                                    };
                }
            }
            //BlockID = 5
            if (blockID == 5)
            {
                color = Color.Green;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {1,1,0,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                        {0,0,1,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {0,1,1,} ,
                                        {1,1,0,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {1,0,0,} ,
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
            }
            //BlockID = 6
            if (blockID == 6)
            {
                color = Color.LightPink;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,0,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {1,1,1,} ,
                                        {0,0,0,} ,
                                    };
                }
            }
            //BlockID = 7
            if (blockID == 7)
            {
                color = Color.Yellow;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {0,0,0,} ,
                                        {0,1,1,} ,
                                        {0,1,1,} ,
                                    };
                }
            }
        }
    }
}
