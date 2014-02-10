using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survivatron.ViewFrames
{
    interface IViewFrame
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
