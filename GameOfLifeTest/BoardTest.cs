using GameOfLife;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeTest
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void UpdateCells_BlinkerTest()
        {
            Board board = new Board(5,5);
            board.InsertShape(2, 1, ShapeFactory.MakeBlinker());
            board.UpdateCells();

            var data = board.Data;
            Assert.AreEqual(1, data[2, 1], "Blinker west cell test on second generation");
            Assert.AreEqual(1, data[2, 2], "Blinker center cell test on second generation");
            Assert.AreEqual(1, data[2, 3], "Blinker east cell test on second generation");

            board.UpdateCells();
            data = board.Data;
            Assert.AreEqual(1, data[1, 2], "Blinker north cell test on third generation");
            Assert.AreEqual(1, data[2, 2], "Blinker center cell test on third generation");
            Assert.AreEqual(1, data[3, 2], "Blinker south cell test on third generation");
        }

        [Test]
        public void UpdateCells_BeaconTest()
        {
            Board board = new Board(6,6);
            board.InsertShape(1, 1, ShapeFactory.MakeBeacon());
            board.UpdateCells();

            var data = board.Data;
            Assert.AreEqual(1, data[1, 1], "Beacon Upper Block, North West Cell - Second generation");
            Assert.AreEqual(1, data[1, 2], "Beacon Upper Block, North East Cell - Second generation");
            Assert.AreEqual(1, data[2, 1], "Beacon Upper Block, South West Cell - Second generation");
            Assert.AreEqual(0, data[2, 2], "Beacon Upper Block, South East Cell - Second generation");

            Assert.AreEqual(0, data[3, 3], "Beacon Lower Block, North West Cell - Second generation");
            Assert.AreEqual(1, data[3, 4], "Beacon Lower Block, North East Cell - Second generation");
            Assert.AreEqual(1, data[4, 3], "Beacon Lower Block, South West Cell - Second generation");
            Assert.AreEqual(1, data[4, 4], "Beacon Lower Block, South East Cell - Second generation");

            board.UpdateCells();
            data = board.Data;

            Assert.AreEqual(1, data[1, 1], "Beacon Upper Block, North West Cell - Third generation");
            Assert.AreEqual(1, data[1, 2], "Beacon Upper Block, North East Cell - Third generation");
            Assert.AreEqual(1, data[2, 1], "Beacon Upper Block, South West Cell - Third generation");
            Assert.AreEqual(1, data[2, 2], "Beacon Upper Block, South East Cell - Third generation");

            Assert.AreEqual(1, data[3, 3], "Beacon Lower Block, North West Cell - Third generation");
            Assert.AreEqual(1, data[3, 4], "Beacon Lower Block, North East Cell - Third generation");
            Assert.AreEqual(1, data[4, 3], "Beacon Lower Block, South West Cell - Third generation");
            Assert.AreEqual(1, data[4, 4], "Beacon Lower Block, South East Cell - Third generation");
        }
    }
}
