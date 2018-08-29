using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsoGame
{
    public class Renderer
    {
        List<RenderObj> renderObjects;
        int screenWidth;
        int screenHeight;

        public static bool IsoRendering = true;

        public Renderer(int screenWidth, int screenHeight)
        {
            renderObjects = new List<RenderObj>();
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void AddObject(RenderObj obj)
        {
            renderObjects.Add(obj);
        }

        public Vector2 TranslateToIso(Vector2 orthoVec)
        {
            Vector2 isoVec = new Vector2();
            isoVec.X = (orthoVec.X * Tile.TileWidth / 2) - (orthoVec.Y * Tile.TileWidth / 2);
            isoVec.Y = (orthoVec.X * Tile.TileHeight / 2) + (orthoVec.Y * Tile.TileHeight / 2);

            return isoVec;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (RenderObj obj in renderObjects)
            {
                if (IsoRendering)
                {
                    Vector2 isoVec = TranslateToIso(obj.position);
                    isoVec += obj.renderOffset;

                    //Just so the thing is on screen
                    isoVec.X += screenWidth / 2;
                    //isoVec.Y += screenHeight / 2;

                    spriteBatch.Draw(obj.tex, isoVec, Color.White);
                }
                else
                {
                    spriteBatch.Draw(obj.tex, obj.position * new Vector2(Tile.TileWidth, Tile.TileHeight), Color.White);
                }
            }
            renderObjects.Clear();
        }
    }

    public struct RenderObj
    {
        public Texture2D tex;
        public Vector2 position;
        public Vector2 renderOffset;
    }
}
