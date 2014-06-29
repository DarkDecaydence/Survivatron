using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survivatron
{
    public struct GOID
    {
        public int ID, subID;

        public GOID(int id, int sid)
        { ID = id; subID = sid; }

        public override bool Equals(object obj)
        {
            GOID tID = (GOID)obj;
            return (tID.ID == ID && tID.subID == subID);
        }
    }
}
