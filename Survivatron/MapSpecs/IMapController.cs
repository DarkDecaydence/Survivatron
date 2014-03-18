using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;
using Microsoft.Xna.Framework;

namespace Survivatron.MapSpecs
{
    interface IMapController
    {
        GameObject GetGameObject(GOID goid);
        bool SetGameObject(GameObject gameObject);
    }
}
