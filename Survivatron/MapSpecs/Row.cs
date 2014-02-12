using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Statics;
using Microsoft.Xna.Framework;

namespace Survivatron.MapSpecs
{
    public class Row
    {
        public List<GameObject> Objects { get; set; }

        public Row()
        {
            Objects = new List<GameObject>();
            Objects.Add(new Floor());
        }

        public bool isFree()
        {
            foreach (GameObject g in Objects)
                if (g.Solid) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            Row castR = (Row)obj;
            // Checks if cast is null.
            if (castR == null)
                return false;

            // Checks if objects are equal.
            int i = 0;
            if (Objects.Count != castR.Objects.Count) { return false; }
            for (GameObject gObj = Objects[i]; i < Objects.Count; i++)
                if (!gObj.Equals(castR.Objects[i]))
                    return false;

            return true;
        }

        public Row SetZone(Row newRow)
        {
            Objects = newRow.Objects;
            return this;
        }
    }
}
