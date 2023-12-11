using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace TiledTest
{
    public class TileMapManager
    {
        private SpriteBatch spriteBatch;
        TmxMap map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;
        public bool Visible;

        public List<Rectangle> collisionObjects;

        public TileMapManager(SpriteBatch spriteBatch, TmxMap map, Texture2D tileset, int tilesetTilesWide, int tileWidth, int tileHeight)
        {
            this.spriteBatch = spriteBatch;
            this.map = map;
            this.tileset = tileset;
            this.tilesetTilesWide = tilesetTilesWide;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            collisionObjects = new List<Rectangle>();
        }

        public void LoadMap()
        {
            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }
        }

        public void Draw(Matrix matrix, Rectangle playerRect)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              samplerState: SamplerState.PointClamp,
                              effect: null,
                              blendState: null,
                              rasterizerState: null,
                              depthStencilState: null,
                              transformMatrix: matrix);

            for (var i = 0; i < map.TileLayers.Count; i++)
            {
                for (var j = 0; j < map.TileLayers[i].Tiles.Count; j++)
                {
                    int gid = map.TileLayers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);

                        if (i == 0)
                        {
                            spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                        }

                        if (i == 1 && Visible)
                        {
                            foreach (var o in map.ObjectGroups["Collisions"].Objects)
                            {
                                Rectangle collisionRect = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);

                                if (playerRect.Intersects(collisionRect) && Visible)
                                {
                                    spriteBatch.DrawRectangle(collisionRect, Color.Black);

                                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                                }   
                            }
                        }
                    }
                }
            }

            spriteBatch.End();
        }

        //public void Draw(Matrix matrix, Rectangle playerRect)
        //{
        //    spriteBatch.Begin(SpriteSortMode.Deferred,
        //                      samplerState: SamplerState.PointClamp,
        //                      effect: null,
        //                      blendState: null,
        //                      rasterizerState: null,
        //                      depthStencilState: null,
        //                      transformMatrix: matrix);

        //    for (var i = 0; i < map.TileLayers.Count; i++)
        //    {
        //        for (var j = 0; j < map.TileLayers[i].Tiles.Count; j++)
        //        {
        //            int gid = map.TileLayers[i].Tiles[j].Gid;
        //            if (gid == 0)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                int tileFrame = gid - 1;
        //                int column = tileFrame % tilesetTilesWide;
        //                int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
        //                float x = (j % map.Width) * map.TileWidth;
        //                float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
        //                Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);

        //                // Only draw tiles in layer 2 when visible is true
        //                if (i == 1)
        //                {
        //                    // Check if the tile intersects with the player's field of view
        //                    foreach (var o in map.ObjectGroups["Collisions"].Objects)
        //                    {       
        //                        foreach (Rectangle r in collisionObjects)
        //                        {
        //                            spriteBatch.DrawRectangle(r, Color.Black);

        //                            if (playerRect.Intersects(r))
        //                            {
        //                                spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (i != 1)
        //                {
        //                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
        //                }
        //            }
        //        }
        //    }

        //    spriteBatch.End();
        //}
    }
}
