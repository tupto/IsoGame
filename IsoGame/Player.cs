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
        Vector2 renderOffset;
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

        public Vector2 GetRenderOffset()
        {
            return new Vector2(-playerTex?.Width / 2 ?? 1, -playerTex?.Height / 2 ?? 1);
        }

		public Rectangle GetCollisionRect()
		{
			return new Rectangle((int)position.X * Tile.TileWidth, (int)position.Y * Tile.TileHeight, playerTex?.Width ?? 1, playerTex?.Height ?? 1);
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

            Renderer.IsoRendering = ks.CapsLock;

            Vector2 oldPosition = position;
            position += velocity;

			HandleMapCollisions(oldPosition);
		}

		public void HandleMapCollisions(Vector2 oldPosition)
		{
			Rectangle myRect = GetCollisionRect();
			foreach (Rectangle rect in map.GetCollisions(myRect))
			{
				if (myRect.Top < rect.Bottom || myRect.Bottom > rect.Top)
                {
                    myRect.Y = (int)oldPosition.Y;
                    position.Y = oldPosition.Y;
                }
			}

			foreach (Rectangle rect in map.GetCollisions(myRect))
			{
				if (myRect.Left < rect.Right || myRect.Right > rect.Left)
                {
                    myRect.X = (int)oldPosition.X;
                    position.X = oldPosition.X;
                }
			}
		}

		public void Render(Renderer renderer)
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
            
            renderer.AddObject(new RenderObj { tex = playerTex, position = position, renderOffset = GetRenderOffset() });
        }
	}
}
