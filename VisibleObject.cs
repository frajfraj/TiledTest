

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TiledTest
{
    public class VisibleObject
    {
        private Vector2 u, d, l, r;

        //public void VisibleRadius(Player player, map.ObjectGroups["Collisions"].Objects)
        //{
        //    Rectangle pBound = player.PlayerBounds;
        //    Vector2 pPos = player.Pos;

        //    u = new Vector2(0, pPos.Bounds.Top - bounds.Bottom);
        //    d = new Vector2(0, platform.Bounds.Bottom - bounds.Top);
        //    l = new Vector2(platform.Bounds.Left - bounds.Right, 0);
        //    r = new Vector2(platform.Bounds.Right - bounds.Left, 0);

        //    if (u.Length() < d.Length() && u.Length() < l.Length() && u.Length() < r.Length())
        //    {
        //        pos.Y -= platform.Bounds.Bottom - bounds.Height + 1;
        //        pos.Y = bounds.Y;
        //    }
        //}
    }
}
