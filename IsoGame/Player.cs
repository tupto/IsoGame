using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsoGame
{
	public class Player
	{
		Vector2 position;
		Vector2 velocity;
		float speed;
		Map map;

		static Texture2D playerTex;

		public Player(Map map, Vector2 position, float speed)
		{
			this.map = map;
			this.position = position;
			this.velocity = Vector2.Zero;
			this.speed = speed;
		}

		public Rectangle GetCollisionRect()
		{
			return new Rectangle((int)position.X, (int)position.Y, playerTex?.Width ?? 1, playerTex?.Height ?? 1);
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState ks = Keyboard.GetState();

			if (ks.IsKeyDown(Keys.W))
			{
				velocity.Y = -speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
			}
			else if (ks.IsKeyDown(Keys.S))
			{
				velocity.Y = speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
			}
			else
			{
				velocity.Y = 0;
			}

			if (ks.IsKeyDown(Keys.A))
			{
				velocity.X = -speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
			}
			else if (ks.IsKeyDown(Keys.D))
			{
				velocity.X = speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
			}
			else
			{
				velocity.X = 0;
			}

			position += velocity;

			HandleMapCollisions();
		}

		public void HandleMapCollisions()
		{
			int dY = 0;
			int dX = 0;
			Rectangle myRect = GetCollisionRect();
			foreach (Rectangle rect in map.GetCollisions(myRect))
			{
				int diff;
				if (myRect.Top < rect.Bottom)
				{
					diff = rect.Bottom - myRect.Top;

					if (dY < diff)
						dY = diff;
				}
				else if (myRect.Bottom > rect.Top)
				{
					diff = rect.Top - myRect.Bottom;

					if (dY > diff)
						dY = diff;
				}
			}

			myRect.Y += dY;
			position.Y += dY;

			foreach (Rectangle rect in map.GetCollisions(myRect))
			{
				int diff;
				if (myRect.Left < rect.Right)
				{
					diff = rect.Right - myRect.Left;

					if (dX < diff)
						dX = diff;
				}
				else if (myRect.Right > rect.Left)
				{
					diff = rect.Left - myRect.Right;

					if (dX > diff)
						dX = diff;
				}
			}

			position.X += dX;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (playerTex == null)
			{
				playerTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, 20, 40);
				Color[] data = new Color[20 * 40];
				for (int i = 0; i < 20 * 40; i++)
				{
					data[i] = new Color(0, 0, 0);
				}
				playerTex.SetData<Color>(data);
			}

			spriteBatch.Draw(playerTex, position, Color.White);
		}
	}
}
