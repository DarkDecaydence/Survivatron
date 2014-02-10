using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survivatron.MapSpecs
{
    public class Column
    {
        public Row[] rows;

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

        public override bool Equals(object obj)
        {
            Column cCast = (Column)obj;
            int i = 0;
            if (rows.Length != cCast.rows.Length) { return false; }
            for (Row r = rows[i]; i < rows.Length; i++)
                if (!r.Equals(cCast.rows[i]))
                    return false;
            return true;
        }
    }
}
