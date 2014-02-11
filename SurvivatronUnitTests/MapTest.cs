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
        /* Test shows that copy of map is returned, and that copy is equal. */
        public void TestMapGetZone()
        {
            Map testMap = new Map(10, 10);
            Map testMap2 = (Map)testMap.GetZone(new Rectangle(0, 0, 10, 10));

            Assert.IsTrue(testMap.Equals(testMap2));
        }

        [TestMethod]
        /* Test persistant return of same map. */
        public void TestMapGetPersistance()
        {
            Map testMap = new Map(20, 20);
            Map testMap2 = (Map)testMap.GetZone(new Rectangle(0, 0, 10, 10));

            Assert.IsTrue(testMap2.Equals(testMap.GetZone(new Rectangle(0, 0, 10, 10))));
        }

        [TestMethod]
        /* Shows that SetZone replaces map parts. */
        public void TestMapSetZone()
        {
            Map testMap = new Map(20, 20);
            Map testMap2 = (Map)testMap.SetZone(new Vector2(1.0f,1.0f), new Map(2, 2));

            Assert.IsTrue(testMap2.Equals(testMap));

        }
    }
}
