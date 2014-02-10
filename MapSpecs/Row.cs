﻿using System;
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
    }
}