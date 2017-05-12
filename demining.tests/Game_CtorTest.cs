namespace demining.tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using demining.bl;

    [TestClass]
    public class Game_CtorTest
    {
        [TestMethod]
        public void Game_Ctor_X9Y9M10()
        {
            var spiel = new Game(9, 9, 10);

            Assert.AreEqual(9, spiel.Fields.GetLength(0), "y ist abweichend");
            Assert.AreEqual(9, spiel.Fields.GetLength(1), "x ist abweichend");
            Assert.AreEqual(10, spiel.NumberOfMines, "Minen sind abweichend");

            Assert.AreEqual(Status.Running, spiel.Status, "Status ist nicht running");
        }

        [TestMethod]
        public void Game_Ctor_X5Y5M5()
        {
            var spiel = new Game(5, 5, 5);

            Assert.AreEqual(5, spiel.Fields.GetLength(0), "y ist abweichend");
            Assert.AreEqual(5, spiel.Fields.GetLength(1), "x ist abweichend");
            Assert.AreEqual(5, spiel.NumberOfMines, "Minen sind abweichend");

            Assert.AreEqual(Status.Running, spiel.Status, "Status ist nicht running");
        }

        [TestMethod]
        public void Game_Ctor_X1Y2M3()
        {
            var spiel = new Game(1, 2, 3);

            Assert.AreEqual(2, spiel.Fields.GetLength(0), "y ist abweichend");
            Assert.AreEqual(1, spiel.Fields.GetLength(1), "x ist abweichend");
            Assert.AreEqual(3, spiel.NumberOfMines, "Minen sind abweichend");

            Assert.AreEqual(Status.Running, spiel.Status, "Status ist nicht running");
        }
    }
}
