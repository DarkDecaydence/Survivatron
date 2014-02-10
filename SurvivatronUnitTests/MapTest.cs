using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survivatron.MapSpecs;
using Microsoft.Xna.Framework;


namespace SurvivatronUnitTests
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestMapGetZone()
        {
            Map testMap = new Map(10, 10);
            IMap testMap2 = testMap.GetZone(new Rectangle(0, 0, 10, 10));

            Assert.IsTrue(testMap.Equals(testMap2));
        }
    }
}
