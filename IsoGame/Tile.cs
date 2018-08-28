using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsoGame
{
	public class Tile
	{
		public TileType tileType;

		public const int TILE_WIDTH = 32;
		public const int TILE_HEIGHT = 32;

		static Texture2D grassTex;
		static Texture2D waterTex;
		static Texture2D wallTex;

		public Tile()
		{
		}

		public static Texture2D GetTextureFromType(TileType type)
		{
			switch (type)
			{
				case TileType.Grass:
					if (grassTex == null)
					{
						grassTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, TILE_WIDTH, TILE_HEIGHT);

						Color[] data = new Color[TILE_WIDTH * TILE_HEIGHT];
						for (int i = 0; i < TILE_WIDTH * TILE_HEIGHT; i++)
						{
							data[i] = new Color(70, 200, 70);
						}
						grassTex.SetData<Color>(data);
					}
					return grassTex;
				case TileType.Water:
					if (waterTex == null)
					{
						waterTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, TILE_WIDTH, TILE_HEIGHT);

						Color[] data = new Color[TILE_WIDTH * TILE_HEIGHT];
						for (int i = 0; i < TILE_WIDTH * TILE_HEIGHT; i++)
						{
							data[i] = new Color(70, 70, 200);
						}
						waterTex.SetData<Color>(data);
					}
					return waterTex;
				case TileType.Wall:
					if (wallTex == null)
					{
						wallTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, TILE_WIDTH, TILE_HEIGHT);

						Color[] data = new Color[TILE_WIDTH * TILE_HEIGHT];
						for (int i = 0; i < TILE_WIDTH * TILE_HEIGHT; i++)
						{
							data[i] = new Color(200, 70, 70);
						}
						wallTex.SetData<Color>(data);
					}
					return wallTex;
				default:
					return null;
			}
		}

		public enum TileType
		{
			Grass,
			Water,
			Wall
		}
	}
}
