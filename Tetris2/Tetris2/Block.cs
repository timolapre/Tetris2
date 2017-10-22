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
        //variabelen nodig om het blokje te initialiseren
        Texture2D texture;
        int blockID;
        int[,] table;
        int posX = Game1.tablewidth/2-2;
        int posY = 0;
        int rotation = 0;
        int timer;
        bool falling = true;
        Game1 game;
        //Score score;
        Color color;
        int TimerMoveLeft;
        int TimerMoveRight;

        //zorgen dat het blokje een texture heeft
        public Block(Texture2D newtexture, int block, Game1 game)
        {
            texture = newtexture;
            blockID = block;
            BlockDraw();
            this.game = game;
        }

        //het blokje daadwerkelijk in een grid tekenen
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
            //zorgen dat de speler het vallen van het blokje kan versnellen
            timer++;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                timer += 10;

            //ervoor zorgen dat het blokje kan vallen
            if (timer > 40 - (game.level-1)*1.5 && falling && CanFall() == true)
            {
                posY++;
                timer = 0;
            }
            //zorgen dat het blokje stopt met valllen
            if (CanFall() == false && timer > 40)
                PlaceBlok();


            //ervoor zorgen dat het blokje rond kan draaien
            if (Game1.currentkeyboardstate.IsKeyDown(Keys.Up) && Game1.previouskeyboardstate.IsKeyUp(Keys.Up))
                rotation = (rotation + 1) % 4;

            //De beweging en de collision van het blokje mogelijk maken
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if ((Game1.TetrisTable[MathHelper.Max(posX - 1, 0), posY] >= 1 && table[0, 0] == 1) || (Game1.TetrisTable[MathHelper.Max(posX - 1, 0), posY + 1] >= 1 && table[1, 0] == 1) || (Game1.TetrisTable[MathHelper.Max(posX - 1, 0), posY + 2] >= 1 && table[2, 0] == 1) || (Game1.TetrisTable[MathHelper.Max(posX, 0), posY] >= 1 && table[0, 1] == 1) || (Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 1] >= 1 && table[1, 1] == 1) || (Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 2] >= 1 && table[2, 1] == 1)) { }
                else
                {
                    TimerMoveLeft++;
                    if (TimerMoveLeft % 8 == 0 || TimerMoveLeft == 1) posX -= 1;
                }
            }
            else TimerMoveLeft = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if ((Game1.TetrisTable[MathHelper.Min(posX + 3, Game1.tablewidth - 3), posY] >= 1 && table[0, 2] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 3, Game1.tablewidth - 3), posY + 1] >= 1 && table[1, 2] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 3, Game1.tablewidth - 3), posY + 2] >= 1 && table[2, 2] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY] >= 1 && table[0, 1] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY + 1] >= 1 && table[1, 1] == 1) || (Game1.TetrisTable[MathHelper.Min(posX + 2, Game1.tablewidth - 3), posY + 2] >= 1 && table[2, 1] == 1)) { }
                else
                {
                    TimerMoveRight++;
                    if (TimerMoveRight % 8 == 0 || TimerMoveRight == 1) posX += 1;
                }
            }
            else TimerMoveRight = 0;

            //mog wat helpende collision code
            if (table[0, 0] == 0 && table[1, 0] == 0 && table[2, 0] == 0 && table[0, 2] == 0 && table[1, 2] == 0 && table[2, 2] == 0)
            {
                posX = MathHelper.Min(MathHelper.Max(posX, -1), Game1.tablewidth - 2);
            }
            else if (table[0,0] == 0 && table[1,0] == 0 && table[2,0] == 0)
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

            /*if (falling)
            {
                if (table[2, 0] == 1 || table[2, 1] == 1 || table[2, 2] == 1)
                {
                    if ((Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 3] >= 1 && table[2, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 3] >= 1 && table[2, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 3] >= 1 && table[2, 2] == 1))
                    {
                        if (timer > 30)
                        {
                            PlaceBlok();
                        }
                    }
                }
                if ((Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 2] >= 1 && table[1, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 2] >= 1 && table[1, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 2] >= 1 && table[1, 2] == 1))
                {
                    if (timer > 30)
                    {
                        PlaceBlok();
                    }
                }
                if ((Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 1] >= 1 && table[0, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 1] >= 1 && table[0, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 1] >= 1 && table[0, 2] == 1))
                {
                    if (timer > 30)
                    {
                        PlaceBlok();
                    }
                }
            }*/

            //GameOver
            if (falling == false && posY <= 2)
                Game1.GameOver = 1;

            if(Game1.GameOver == 1)
            {
                posY = 3;
            }
        }

        //zorgen dat het blokje geplaatst kan worden en er een nieuw blokje ontstaat
        private void PlaceBlok()
        {
            falling = false;
            for (int i = 0; i < 3; i++)
                for (int x = 0; x < 3; x++)
                    if (table[i, x] == 1)
                        Game1.TetrisTable[posX + x, posY + i] = blockID;
            game.color = color;
            Game1.createnewblock = 1;
        }

        //het tekenen van de verschillende blokjes in het grid (we weten dat we ze ook wiskundig konden draaien alleen dit begrepen we beter)
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
                color = Color.Pink;
                if(rotation == 0)
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
            /*//BlockID = 8
            if (blockID == 8)
            {
                color = Color.LightBlue;
                if (rotation == 0)
                {
                    table = new int[,] {
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 1)
                {
                    table = new int[,] {
                                        {0,0,1,} ,
                                        {1,1,1,} ,
                                        {1,0,0,} ,
                                    };
                }
                else if (rotation == 2)
                {
                    table = new int[,] {
                                        {1,1,0,} ,
                                        {0,1,0,} ,
                                        {0,1,1,} ,
                                    };
                }
                else if (rotation == 3)
                {
                    table = new int[,] {
                                        {0,0,1,} ,
                                        {1,1,1,} ,
                                        {1,0,0,} ,
                                    };
                }
            }*/
        }

        //bepalen wanneer het voor een blokje mogelijk is om te vallen en wanneer niet
        public bool CanFall()
        {
            if (falling)
            {
                if (table[2, 0] == 1 || table[2, 1] == 1 || table[2, 2] == 1)
                {
                    if ((Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 3] >= 1 && table[2, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 3] >= 1 && table[2, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 3] >= 1 && table[2, 2] == 1))
                    {
                        return false;
                    }
                }
                if ((Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 2] >= 1 && table[1, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 2] >= 1 && table[1, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 2] >= 1 && table[1, 2] == 1))
                {
                    return false;
                }
                if ((Game1.TetrisTable[MathHelper.Max(posX, 0), posY + 1] >= 1 && table[0, 0] == 1) || (Game1.TetrisTable[posX + 1, posY + 1] >= 1 && table[0, 1] == 1) || (Game1.TetrisTable[posX + 2, posY + 1] >= 1 && table[0, 2] == 1))
                {
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
