using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Client
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tileImage;
        A_Star a_star;

        Agent agent;
        MouseState mClick;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            this.IsMouseVisible = true;
        }

        public static TileInfo[,] tileInfo = new TileInfo[10, 10];
        public Point startLoc = new Point(3, 4);
        public Point endLoc = new Point(9, 9);
        protected override void Initialize()
        {
            // Create the tile objects in the array
            for (int x = 0; x < tileInfo.GetLength(0); x++)
            {
                for (int y = 0; y < tileInfo.GetLength(0); y++)
                {
                    tileInfo[x, y] = new TileInfo();
                }
            }
            // Change some tiles to walls
            tileInfo[4, 0].tileType = TileInfo.TileType.Wall;
            tileInfo[4, 1].tileType = TileInfo.TileType.Wall;
            tileInfo[4, 2].tileType = TileInfo.TileType.Wall;
            tileInfo[4, 3].tileType = TileInfo.TileType.Wall;
            tileInfo[4, 4].tileType = TileInfo.TileType.Wall;
            tileInfo[4, 5].tileType = TileInfo.TileType.Wall;
            tileInfo[3, 5].tileType = TileInfo.TileType.Wall;
            tileInfo[2, 5].tileType = TileInfo.TileType.Wall;
            tileInfo[1, 5].tileType = TileInfo.TileType.Wall;
            tileInfo[1, 4].tileType = TileInfo.TileType.Wall;
            tileInfo[1, 3].tileType = TileInfo.TileType.Wall;
            tileInfo[1, 2].tileType = TileInfo.TileType.Wall;
            tileInfo[7, 6].tileType = TileInfo.TileType.Wall;
            tileInfo[7, 7].tileType = TileInfo.TileType.Wall;
            tileInfo[7, 8].tileType = TileInfo.TileType.Wall;
            tileInfo[7, 9].tileType = TileInfo.TileType.Wall;
            // Pass the tile information and a weight for the H.
            // The lower the H weight value shorter the path
            // the higher it is the less number of checks it take to determine
            // a path.  Less checks might be useful for a very large number of agents.
            int hWeight = 2;
            a_star = new A_Star(tileInfo, hWeight);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileImage = Content.Load<Texture2D>("tile");
            agent = new Agent(Content.Load<Texture2D>("agent"), startLoc, a_star);
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            mClick = Mouse.GetState();
            if (mClick.X > this.GraphicsDevice.PresentationParameters.Bounds.Left &&
                mClick.Y > this.GraphicsDevice.PresentationParameters.Bounds.Top &&
                mClick.X < this.GraphicsDevice.PresentationParameters.Bounds.Right &&
                mClick.Y < this.GraphicsDevice.PresentationParameters.Bounds.Bottom
                )
            {
                if (mClick.LeftButton == ButtonState.Pressed)
                {
                    startLoc.X = (int)mClick.X / 50;
                    startLoc.Y = (int)mClick.Y / 50;
                }
                if (mClick.RightButton == ButtonState.Pressed)
                {
                    endLoc.X = (int)mClick.X / 50;
                    endLoc.Y = (int)mClick.Y / 50;
                }
            }
            
            agent.setDestination(startLoc.X, startLoc.Y, endLoc.X, endLoc.Y);
            base.Update(gameTime);
            agent.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for (int x = 0; x < tileInfo.GetLength(0); x++)
            {
                for (int y = 0; y < tileInfo.GetLength(0); y++)
                {
                    if (tileInfo[x, y].tileType == TileInfo.TileType.Floor)
                        spriteBatch.Draw(tileImage, new Rectangle(x * 50, y * 50,50,50), Color.White);
                    else if (tileInfo[x, y].tileType == TileInfo.TileType.Wall)
                        spriteBatch.Draw(tileImage, new Rectangle(x * 50, y * 50,50,50), Color.DarkGray);
                }
            }
            for (int i = 0; i < agent.a_star.Path.Count; i++)
            {
                spriteBatch.Draw(tileImage, new Vector2(agent.a_star.Path[i].X * 50, agent.a_star.Path[i].Y * 50), Color.Yellow);
            }
            spriteBatch.Draw(tileImage, new Vector2(startLoc.X * 50, startLoc.Y * 50), Color.Green);
            spriteBatch.Draw(tileImage, new Vector2(endLoc.X * 50, endLoc.Y * 50), Color.Red);
            agent.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
