using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledTest
{
    internal class Shadow
    {
        GraphicsDeviceManager graphicsDevice;

        private Texture2D shadowTex;
        protected Vector2 shadowPos;
        protected Rectangle shadowRect;
        public void Load(ContentManager content)
        {
            shadowTex = content.Load<Texture2D>("blackrectangle");
            shadowRect = new Rectangle((int)shadowPos.X, (int)shadowPos.Y, shadowTex.Width, shadowTex.Height);
        }

        public void Update(Player player)
        {
            shadowRect.Location = player.Pos.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 playerPos, Matrix matrix)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                                samplerState: SamplerState.PointClamp,
                                effect: null, blendState: null,
                                rasterizerState: null,
                                depthStencilState: null,
                                transformMatrix: matrix);

            Vector2 shadowOrigin = new Vector2(shadowTex.Width / 2, shadowTex.Height / 2);

            spriteBatch.Draw(shadowTex, playerPos, null, Color.White, 0f, shadowOrigin, 2.0f, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
