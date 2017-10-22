using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Background;
        static public KeyboardState currentkeyboardstate;
        static public KeyboardState previouskeyboardstate;
        //groote speelveld word hier bepaald
        static public int tablewidth = 12;
        static public int tableheight = 20;
        static public int createnewblock;
        //randomgenerator
        static public Random r;
        static public int GameOver = 0;

        //variabelen nodig voor bijhouden scores en andere gameplay waarden
        public int NextBlock;
        public double score1 = 0;
        public int spawned = 0;
        public int level = 1;

        //ints nodig voor het grid
        int RowCheck;
        int ColumnCheck;
        int SkipWhile;
        int BlockCounter;
        int SpawnBlock;

        SpriteFont font;
        Texture2D blocktexture;
        public Color color;

        static public int[,] TetrisTable;
        Color[] colorlist;

        NextBlock nextblock;

        //sounds
        SoundEffect RowClear;
        Song TetrisSong;

        //Score score;

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
            graphics.PreferredBackBufferHeight = 740;
            graphics.PreferredBackBufferWidth = 1250;
            graphics.ApplyChanges();
            //graphics.ToggleFullScreen();

            //lijst kleuren die de blokjes kunnen hebben
            colorlist = new Color[]
            {
                Color.Purple, Color.Orange, Color.Blue, Color.Red, Color.Green, Color.Pink, Color.Yellow
            };

            r = new Random();

            TetrisTable = new int[tablewidth+1, tableheight];

            //block = new Content.Block(Content.Load<Texture2D>("block2"), 1, this);

            //initializen van het blokje
            blocktexture = Content.Load<Texture2D>("block2");
            blocklist.Add(new Content.Block(Content.Load<Texture2D>("block2"), r.Next(1,8), this));

            nextblock = new NextBlock();
            NextBlock = r.Next(1, 8);

            //initializen speelveld
            for (int i = 0; i < tablewidth; i++)
            {
                TetrisTable[i, tableheight-1] = 1;
            }

            MediaPlayer.Play(TetrisSong);

            base.Initialize();
        }

        //initializen van achtergrond en een font
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Background = Content.Load<Texture2D>("Background");
            font = Content.Load<SpriteFont>("font");

            //sounds
            TetrisSong = Content.Load<Song>("tetrismusic");
            MediaPlayer.Play(TetrisSong);
            RowClear = Content.Load<SoundEffect>("rowclear2");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //enkele inputs voor debuggen en het spel pauzeren
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.P)) Debugger.Break();
            if (Keyboard.GetState().IsKeyDown(Keys.F)) graphics.ToggleFullScreen();
            if (Keyboard.GetState().IsKeyDown(Keys.O)) ResetTable();

            previouskeyboardstate = currentkeyboardstate;
            currentkeyboardstate = Keyboard.GetState();

            //initializen blokjes
            nextblock.Update();

            if (createnewblock == 0)
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

            //zorgen dat hey spel door kan lopen en niet stop
            while (TetrisTable[ColumnCheck, RowCheck] >= 1 && SkipWhile < tablewidth)
            {
                SkipWhile++;
                BlockCounter++;
                ColumnCheck = MathHelper.Min(ColumnCheck+1,tablewidth-1);
            }
            if (BlockCounter == tablewidth)
            {
                RemoveRow(RowCheck);
                BlockCounter = 0;
            }
            BlockCounter = 0;
            SkipWhile = 0;
            ColumnCheck = 0;
            RowCheck = (RowCheck + 1) % (tableheight - 1);

            //de moelijkheid dus het level van het spel verhogen als de speler het goed doet
            if(GameOver == 1 && currentkeyboardstate.IsKeyDown(Keys.Space) && previouskeyboardstate.IsKeyUp(Keys.Space))
                {
                    ResetTable();
                    GameOver = 0;
                    blocklist.Add(new Content.Block(Content.Load<Texture2D>("block2"), r.Next(1, 8), this));
                    NextBlock = r.Next(1, 8);
                }

            base.Update(gameTime);
        }

        //het tekenen van het grid en de blokjes
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Background, new Vector2(),Color.White);

            foreach(Content.Block block in blocklist)
                block.Draw(spriteBatch);
            
            //tekenen spelgrid
            for (int i = 0; i < tableheight; i++)
                for (int x = 0; x < tablewidth; x++)
                    if (TetrisTable[x, i] >= 1)
                    {
                        spriteBatch.Draw(blocktexture, new Vector2(x * blocktexture.Width, i * blocktexture.Height), colorlist[TetrisTable[x,i]-1]);
                    }
            
            //zorgen dat de speler verliest als de blokjes de top van het speelveld raken            
            if (GameOver == 1)
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2(200, 350), Color.White);
                spriteBatch.DrawString(font, "Press Space to play again", new Vector2(70, 400), Color.White);
            }
            spriteBatch.DrawString(font,"Score: " + score1, new Vector2(490,260), Color.White);
            spriteBatch.DrawString(font, "Level: " + level, new Vector2(490, 300), Color.White);
            nextblock.Draw(spriteBatch, blocktexture, NextBlock);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //zorgen dat er een nieuw blokje spawnt als de oude geplaatst is
        public void NewBlock()
        {
            SpawnBlock = NextBlock;
            NextBlock = r.Next(1,8);
            Console.WriteLine(NextBlock);
            blocklist.Add(new Content.Block(Content.Load<Texture2D>("block2"), SpawnBlock, this));
            score1 += 10;
            spawned += 1;
            if (spawned >= 10)
            {
                level += 1;
                spawned = 0;
            }
        }

        //deze methode maakt het mogelijk een rij weg te halen
        private void RemoveRow(int x)
        {
            for (int i = x; i > 0; i--)
                for (int p = 0; p < tablewidth; p++)
                    TetrisTable[p, i] = TetrisTable[p, i-1];
            score1 += 100;
            //RowClear.Play();
        }
        
        //deze methode rest het hele spel als je verloren hebt
        private void ResetTable()
        {
            Array.Clear(TetrisTable, 0, TetrisTable.Length);
            for (int i = 0; i < tablewidth; i++)
            {
                TetrisTable[i, tableheight - 1] = 1;
            }
            level = 0;
            spawned = 0;
            score1 = 0;
        }
    }
}
