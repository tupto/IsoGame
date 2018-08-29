using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsoGame
{
	public class Map
	{
		int mapWidth;
		int mapHeight;
		Tile[,] map;
		Player player;

		public Map(int mapWidth, int mapHeight)
		{
			this.mapWidth = mapWidth;
			this.mapHeight = mapHeight;
			map = new Tile[mapWidth, mapHeight];

			for (int x = 0; x < mapWidth; x++)
			{
				for (int y = 0; y < mapHeight; y++)
				{
					if (x == 0 || y == 0 || x == mapWidth - 1 || y == mapHeight - 1)
					{
						map[x, y] = new Tile();
						map[x, y].tileType = Tile.TileType.Wall;
					}
					else if (x >= 2 && x <= 4 && y >= 2 && y <= 6)
					{
						map[x, y] = new Tile();
						map[x, y].tileType = Tile.TileType.Water;
					}
					else
					{
						map[x, y] = new Tile();
						map[x, y].tileType = Tile.TileType.Grass;
					}
				}
			}

			player = new Player(this, new Vector2(2, 2), 8);
		}

		public Rectangle[] GetCollisions(Rectangle colRect)
		{
			List<Rectangle> collisions = new List<Rectangle>();

			for (int x = 0; x < mapWidth; x++)
			{
				for (int y = 0; y < mapHeight; y++)
				{
					Rectangle rect = new Rectangle(x * Tile.TileWidth, y * Tile.TileHeight, Tile.TileWidth, Tile.TileHeight);
					if (rect.Intersects(colRect) && map[x, y].tileType == Tile.TileType.Wall)
					{
						collisions.Add(rect);
					}
				}
			}

			return collisions.ToArray();
		}

		public void Update(GameTime gameTime)
		{
			player.Update(gameTime);
		}

		public void Render(Renderer renderer)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				for (int y = 0; y < mapHeight; y++)
				{
					Texture2D mapTex = Tile.GetTextureFromType(map[x, y].tileType);
                    RenderObj obj = new RenderObj { tex = mapTex, position = new Vector2(x, y) };
                    renderer.AddObject(obj);
                }
			}

			player.Render(renderer);
		}
	}
}
