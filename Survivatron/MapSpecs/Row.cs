using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Statics;

namespace Survivatron.MapSpecs
{
    public class Row
    {
        public List<GameObject> objects;

        public Row()
        {
            objects = new List<GameObject>();
            objects.Add(new Floor());
        }

        public bool isFree()
        {
            foreach (GameObject g in objects)
                if (g.Solid) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            Row castR = (Row)obj;
            int i = 0;
            if (objects.Count != castR.objects.Count) { return false; }
            for (GameObject gObj = objects[i]; i < objects.Count; i++)
                if (!gObj.Equals(castR.objects[i]))
                    return false;
            return true;
        }
    }
}
