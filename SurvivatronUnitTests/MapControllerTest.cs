using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Survivatron.GameObjects;
using Survivatron.GameObjects.Dynamics;
using Survivatron.MapSpecs;
using Microsoft.Xna.Framework;

namespace SurvivatronUnitTests
{
    [TestClass]
    public class MapControllerTest
    {
        [TestMethod]
        /* Tests the GetZone method, and that the returned copy is equal. */
        public void TestMCGetZone()
        {
            Map newMap = new Map(20, 20);
            MapController mc = MapController.Construct(newMap);

            Map testMap = (Map)mc.GetZone(new Rectangle(0, 0, 20, 20));

            Assert.IsTrue(newMap.Equals(testMap));
        }

        [TestMethod]
        /* Tests the GetDimensions method to see it returns correct dimensions. */
        public void TestMCGetDimensions()
        {
            Map newMap = new Map(20, 20);
            MapController mc = MapController.Construct(newMap);

            Vector2 testDims = mc.GetDimensions();

            Assert.IsTrue(((int)testDims.X) == newMap.columns.Length &&
                ((int)testDims.Y) == newMap.columns[0].rows.Length);
        }

        [TestMethod]
        /* Tests the GetGameObject method, and that it returns correct object. */
        public void TestMCGetGameObject()
        {
            Map newMap = new Map(20, 20);
            MapController mc = MapController.Construct(newMap);

            Sheep testSheep = new Sheep(3);
            Dynamic dynaSheep = (Dynamic)testSheep;
            mc.AddDynamic(new Vector2(2, 2), ref dynaSheep);
            testSheep = (Sheep)dynaSheep;

            Sheep testSheep2 = (Sheep)mc.GetGameObject(testSheep.ID);

            Assert.IsTrue(testSheep.ID.Equals(testSheep2.ID));
        }

        [TestMethod]
        /* Tests the SetGameObject method, and that it returns the correct status. */
        public void TestMCSetGameObject()
        {
            Map newMap = new Map(20, 20);
            MapController mc = MapController.Construct(newMap);

            Sheep newSheep = new Sheep(4);

            bool status = false;
            status = mc.SetGameObject(newSheep);

            Sheep sheepCopy = (Sheep)mc.GetGameObject(newSheep.ID);

            Assert.IsTrue(sheepCopy.Equals(newSheep) && status);
        }
    }
}
