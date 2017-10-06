using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        static public int tablewidth = 11;
        static public int tableheight = 15;
        static public int createnewblock;

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
            graphics.PreferredBackBufferWidth = 600;
            graphics.ApplyChanges();

            TetrisTable = new int[tablewidth, tableheight];

            //block = new Content.Block(Content.Load<Texture2D>("block"), 1, this);

            blocktexture = Content.Load<Texture2D>("block");
            blocklist.Add(new Content.Block(Content.Load<Texture2D>("block"), 2, this));

            for (int i = 0; i < tablewidth; i++)
                TetrisTable[i, tableheight-1] = 1;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.P)) Debugger.Break();
            //if (Keyboard.GetState().IsKeyDown(Keys.F)) graphics.IsFullScreen = true;

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
                NewBlock();
                createnewblock = 0;
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
                        spriteBatch.Draw(blocktexture, new Vector2(x * blocktexture.Width, i * blocktexture.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void NewBlock()
        {
            blocklist.Add(new Content.Block(Content.Load<Texture2D>("block"), 1, this));
        }
    }
}
