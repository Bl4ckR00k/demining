namespace demining.tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using demining.bl;

    [TestClass]
    public class Game_CreateGameFieldTest
    {
        [TestMethod]
        public void CreateGameField_X9Y9M10()
        {
            var spiel = new Game(9, 9, 10);
            spiel.CreateGameField();

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(10, actualCountsOfMines);
            Assert.AreEqual(Status.Running, spiel.Status);
        }

        [TestMethod]
        public void CreateGameField_X5Y5M5()
        {
            var spiel = new Game(5, 5, 5);
            spiel.CreateGameField();

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(5, actualCountsOfMines);
            Assert.AreEqual(Status.Running, spiel.Status);
        }

        [TestMethod]
        public void CreateGameField_X1Y2M3()
        {
            var spiel = new Game(1, 2, 3);
            spiel.CreateGameField();

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(2, actualCountsOfMines);
            Assert.AreEqual(Status.Won, spiel.Status);
        }

        [TestMethod]
        public void CreateGameField_X4Y4M16()
        {
            var spiel = new Game(4, 4, 16);
            spiel.CreateGameField();

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(16, actualCountsOfMines);
            Assert.AreEqual(Status.Won, spiel.Status);
        }

        [TestMethod]
        public void CreateGameField_X3Y3M0()
        {
            var spiel = new Game(3, 3, 0);
            spiel.CreateGameField();

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(0, actualCountsOfMines);
            Assert.AreEqual(Status.Won, spiel.Status);
        }
    }
}
