using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsoGame
{
    public class Tile
    {
        public TileType tileType;

        public static int TileWidth 
        {
            get { return Renderer.IsoRendering ? 64 : 32; }
        }
		public static int TileHeight
        {
            get { return Renderer.IsoRendering ? 32 : 32; }
        }

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
                    if (Renderer.IsoRendering)
                    {
                        return IsoGame.CurrentInstance.isoGrassTex;
                    }
                    if (grassTex == null)
					{
                        grassTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, TileWidth, TileHeight);

                        Color[] data = new Color[TileWidth * TileHeight];
                        for (int i = 0; i < TileWidth * TileHeight; i++)
                        {
                            data[i] = new Color(70, 200, 70);
                        }
                        grassTex.SetData<Color>(data);
					}
					return grassTex;
				case TileType.Water:
                    if (Renderer.IsoRendering)
                    {
                        return IsoGame.CurrentInstance.isoWaterTex;
                    }
                    if (waterTex == null)
					{
                        waterTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, TileWidth, TileHeight);

                        Color[] data = new Color[TileWidth * TileHeight];
                        for (int i = 0; i < TileWidth * TileHeight; i++)
                        {
                            data[i] = new Color(70, 70, 200);
                        }
                        waterTex.SetData<Color>(data);
					}
					return waterTex;
				case TileType.Wall:
                    if (Renderer.IsoRendering)
                    {
                        return IsoGame.CurrentInstance.isoWallTex;
                    }
                    if (wallTex == null)
                    {
                        wallTex = new Texture2D(IsoGame.CurrentInstance.GraphicsDevice, TileWidth, TileHeight);

                        Color[] data = new Color[TileWidth * TileHeight];
                        for (int i = 0; i < TileWidth * TileHeight; i++)
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
