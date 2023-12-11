using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;
using TiledSharp;

namespace TiledTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private TmxMap map;
        private Shadow shadow;
        private TileMapManager mapManager;
        private List<Rectangle> collisionObjects;
        private Matrix matrix;
        private Vector2 shadowPos;
        private Vector2 u, d, r, l;
        private bool visible;

        private Texture2D shadowTex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            player = new Player();
            shadow = new Shadow();

            graphics.PreferredBackBufferWidth = 256 * 4;
            graphics.PreferredBackBufferHeight = 256 * 4;
            graphics.ApplyChanges();

            var Width = graphics.PreferredBackBufferWidth;
            var Height = graphics.PreferredBackBufferHeight;
            var WindowSize = new Vector2 (Width, Height);
            var mapSize = new Vector2(256, 256);//our tilemap size
            matrix = Matrix.CreateScale(new Vector3(WindowSize / mapSize, 1));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            shadow.Load(Content);
            

            map = new TmxMap("Content/map.tmx");
            var tileset = Content.Load<Texture2D>("Tiny Adventure Pack/" + map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;
            var tilesetTilesWide = tileset.Width / tileWidth;
            mapManager = new TileMapManager(spriteBatch, map, tileset, tilesetTilesWide, tileWidth, tileHeight);
            collisionObjects = new List<Rectangle>();

            mapManager.LoadMap();

            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            SpriteSheet[] sheets = { Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Idle/playerSheetIdle.sf", new JsonContentLoader()), 
                                     Content.Load<SpriteSheet>("Tiny Adventure Pack/Character/char_two/Walk/playerSheetWalk.sf", new JsonContentLoader()) };
            player.Load(sheets);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initpos = player.pos;
            player.Update(gameTime);
            foreach (var rects in collisionObjects)
            {
                if (rects.Intersects(player.playerBounds))
                {
                    player.pos = initpos;
                    player.isIdle = true;
                }
            }

            foreach (Rectangle rects in mapManager.collisionObjects)
            {
                if (player.fieldOfView.Intersects(rects))
                {
                    mapManager.Visible = true;
                }
            }
            


            shadow.Update(player);

            Window.Title = "field of view pos:" + player.fieldOfView.Location + "player pos: " + player.pos;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mapManager.Draw(matrix, player.fieldOfView);
            
            player.Draw(spriteBatch, matrix);

            shadow.Draw(spriteBatch, player.pos, matrix);

            base.Draw(gameTime);
        }
    }
}