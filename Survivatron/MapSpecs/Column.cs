using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survivatron.MapSpecs
{
    public class Column
    {
        public Row[] rows { get; set; }

        public Column(int length)
        {
            rows = new Row[length];
            for (int i = 0; i < length; i++)
            { rows[i] = new Row(); }
        }

        public Column Crop(int start, int finish)
        {
            Column cropped = new Column(finish-start);

            int j = 0;
            for (int i = start; i < finish; i++)
            { cropped.rows[j++] = rows[i]; }

            return cropped;
        }

        public Column SetZone(int start, Column newColumn)
        {
            int j = 0;
            for (int i = start; i < (start + newColumn.rows.Length); i++)
                rows[i] = newColumn.rows[j++];

            return this;
        }

        public override bool Equals(object obj)
        {
            Column cCast = (Column)obj;
            // Checks if cast is null.
            if (cCast == null)
                return false;

            // Checks recusively if rows are equal.
            int i = 0;
            if (rows.Length != cCast.rows.Length) { return false; }
            for (Row r = rows[i]; i < rows.Length; i++)
                if (!r.Equals(cCast.rows[i]))
                    return false;

            return true;
        }
    }
}
