using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsoGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class IsoGame : Game
	{
		public static IsoGame CurrentInstance;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        public Texture2D isoGrassTex;
        public Texture2D isoWallTex;
        public Texture2D isoWaterTex;

        Renderer renderer;
		Map map;

		public IsoGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			CurrentInstance = this;

            renderer = new Renderer(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            map = new Map(10, 10);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

            isoGrassTex = Content.Load<Texture2D>("grass");
            isoWallTex = Content.Load<Texture2D>("wall");
            isoWaterTex = Content.Load<Texture2D>("water");

        }

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			map.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

            map.Render(renderer);
            renderer.Draw(spriteBatch);

            //spriteBatch.Draw(Tile.GetTextureFromType(Tile.TileType.Grass), Vector2.Zero, Color.White);

            spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
