using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace TiledTest
{
    public class Player
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet sheet;
        private float moveSpeed = 1.5f;
        public Rectangle playerBounds, fieldOfView;
        public bool isIdle = false;

        //public Rectangle PlayerBounds {  get { return playerBounds; } }
        //public Rectangle FieldOfView { get { return fieldOfView; } }
        public Vector2 Pos { get { return pos; } set { pos = value; } }

        public Player()
        {
            playerSprite = new AnimatedSprite[10];
            pos = new Vector2(100, 50);
            playerBounds = new Rectangle((int)pos.X - 8/*centered at centre*/, (int)pos.Y - 8, 16, 17);
            fieldOfView = new Rectangle((int)pos.X - 50, (int)pos.Y - 50, 100, 100);
        }

        public void Load(SpriteSheet[] spriteSheets)
        {
            for (int i = 0; i<spriteSheets.Length; i++)
            {
                sheet = spriteSheets[i];
                playerSprite[i] = new AnimatedSprite(sheet);
            }
        }

        public void Update(GameTime gameTime) 
        {             
            
            
            isIdle = true;
            playerSprite[0].Play("idleDown");

            string animation = "";
            var keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.D))
            {
                animation = "walkRight";
                pos.X += moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.A))
            {
                animation = "walkLeft";
                pos.X -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.W))
            {
                animation = "walkUp";
                pos.Y -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.S))
            {
                animation = "walkDown";
                pos.Y += moveSpeed;
                isIdle = false;
            }
            if (!isIdle)
            {
                playerSprite[1].Play(animation);
                playerSprite[1].Update(gameTime);
            }
            playerBounds.X = (int)pos.X-8;
            playerBounds.Y = (int)pos.Y-8;
            playerSprite[0].Update(gameTime);

            fieldOfView.X = (int)pos.X - 50;
            fieldOfView.Y = (int)pos.Y - 50;
        }

        public void Draw(SpriteBatch spriteBatch, Matrix matrix)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, 
                                samplerState:SamplerState.PointClamp, 
                                effect:null, blendState:null, 
                                rasterizerState:null,
                                depthStencilState: null,  
                                transformMatrix: matrix);

            spriteBatch.DrawRectangle(fieldOfView, Color.Black);

            if (isIdle)
                spriteBatch.Draw(playerSprite[0], pos);
            else
                spriteBatch.Draw(playerSprite[1], pos);

            spriteBatch.End();
        }
    }
}
