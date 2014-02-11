using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survivatron.ViewFrames
{
    public interface IViewFrame
    {
        void Draw(SpriteBatch spriteBatch);
        IViewFrame Pan(Vector2 direction);
    }
}
