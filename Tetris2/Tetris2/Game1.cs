using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static public KeyboardState currentkeyboardstate;
        static public KeyboardState previouskeyboardstate;
        static public int tablewidth = 12;
        static public int tableheight = 20;
        static public int createnewblock;
        static Random r;
        static public int GameOver = 0;

        public double score = 0;
        public int spawned = 0;
        public int level = 0;

        int RowCheck;
        int ColumnCheck;
        int SkipWhile;
        int BlockCounter;

        SpriteFont font;
        Texture2D blocktexture;

        static public int[,] TetrisTable;

        //Content.Block block;
        List<Content.Block> blocklist = new List<Content.Block>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1300;
            graphics.ApplyChanges();
            //graphics.ToggleFullScreen();

            r = new Random();

            TetrisTable = new int[tablewidth+1, tableheight];

            //block = new Content.Block(Content.Load<Texture2D>("block2"), 1, this);

            blocktexture = Content.Load<Texture2D>("block2");
            blocklist.Add(new Content.Block(Content.Load<Texture2D>("block2"), r.Next(1,8), this));

            for (int i = 0; i < tablewidth; i++)
            {
                TetrisTable[i, tableheight-1] = 1;
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.P)) Debugger.Break();
            if (Keyboard.GetState().IsKeyDown(Keys.F)) graphics.ToggleFullScreen();
            if (Keyboard.GetState().IsKeyDown(Keys.O)) ResetTable();

            previouskeyboardstate = currentkeyboardstate;
            currentkeyboardstate = Keyboard.GetState();

            if(createnewblock == 0)
            {
                foreach (Content.Block block in blocklist)
                {
                    block.BlockUpdate();
                    block.BlockDraw();
                }
            }
            else
            {
                if (GameOver == 0)
                    NewBlock();

                createnewblock = 0;
                
            }

            while (TetrisTable[ColumnCheck, RowCheck] == 1 && SkipWhile < tablewidth)
            {
                SkipWhile++;
                BlockCounter++;
                ColumnCheck = MathHelper.Min(ColumnCheck+1,tablewidth-1);
                Console.WriteLine(BlockCounter +" "+ RowCheck);
           }
            if (BlockCounter == tablewidth)
            {
                RemoveRow(RowCheck);
                BlockCounter = 0;
                Console.WriteLine(RowCheck + " " + ColumnCheck + " " + BlockCounter);
            }
            BlockCounter = 0;
            SkipWhile = 0;
            ColumnCheck = 0;
            RowCheck = (RowCheck + 1) % (tableheight - 1);

            if(GameOver == 1 && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    ResetTable();
                    GameOver = 0;
                    blocklist.Add(new Content.Block(Content.Load<Texture2D>("block2"), r.Next(1, 8), this));
                    spawned += 1;
                    if (spawned >= 10)
                    {
                    level += 1;
                    spawned = 0;
                    }
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach(Content.Block block in blocklist)
                block.Draw(spriteBatch);
            for (int i = 0; i < tableheight; i++)
                for (int x = 0; x < tablewidth; x++)
                    if (TetrisTable[x, i] == 1)
                        spriteBatch.Draw(blocktexture, new Vector2(x * blocktexture.Width, i * blocktexture.Height), Color.SeaGreen);
            if (GameOver == 1)
                spriteBatch.DrawString(font, "Press Space to play again", new Vector2(60, 400), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void NewBlock()
        {
            int RandomBlock = r.Next(1,8);
            blocklist.Add(new Content.Block(Content.Load<Texture2D>("block2"), RandomBlock, this));
        }

        private void RemoveRow(int x)
        {
            for (int i = x; i > 0; i--)
                for (int p = 0; p < tablewidth; p++)
                    TetrisTable[p, i] = TetrisTable[p, i-1];
                    Score.RemovedRow++;           
        }
        
        private void ResetTable()
        {
            Array.Clear(TetrisTable, 0, TetrisTable.Length);
            for (int i = 0; i < tablewidth; i++)
            {
                TetrisTable[i, tableheight - 1] = 1;
            }
            level = 0;
            spawned = 0;
        }
    }
}
