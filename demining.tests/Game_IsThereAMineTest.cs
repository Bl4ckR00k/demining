namespace demining.tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using demining.bl;

    [TestClass]
    public class Game_IsThereAMineTest
    {
        [TestMethod]
        public void Game_IsThereAMine_AllMarked()
        {
            var Spiel = new Game(3, 3, 3);
            Spiel.CreateGameField();

            Spiel.Fields.Cast<Field>().ToList().Where(w => w.Type == Type.Mine).ToList().ForEach(f => f.Flag = Mark.yes);
            Assert.IsTrue(Spiel.IsThereAMine());

            var countsOfMarks = Spiel.Fields.Cast<Field>().ToList().Count(w => w.Flag == Mark.yes);
            Assert.AreEqual(3, countsOfMarks, "Anzahl der Markierungen stimmt nicht überein");
        }

        [TestMethod]
        public void Game_IsThereAMine_NoMarked()
        {
            var Spiel = new Game(3, 3, 3);
            Spiel.CreateGameField();

            Assert.IsFalse(Spiel.IsThereAMine());

            var countsOfMarks = Spiel.Fields.Cast<Field>().ToList().Count(w => w.Flag == Mark.yes);
            Assert.AreEqual(0, countsOfMarks, "Anzahl der Markierungen stimmt nicht überein");

        }

        [TestMethod]
        public void Game_IsThereAMine_OneMarked()
        {
            var Spiel = new Game(3, 3, 3);
            Spiel.CreateGameField();

            Spiel.Fields.Cast<Field>().ToList().Where(w => w.Type == Type.Mine).ToList().First().Flag = Mark.yes;
            Assert.IsFalse(Spiel.IsThereAMine());

            var countsOfMarks = Spiel.Fields.Cast<Field>().ToList().Count(w => w.Flag == Mark.yes);
            Assert.AreEqual(1, countsOfMarks, "Anzahl der Markierungen stimmt nicht überein");
        }

        [TestMethod]
        public void Game_IsThereAMine_AllDetectedNoMarked()
        {
            var Spiel = new Game(3, 3, 3);
            Spiel.CreateGameField();

            Spiel.Fields.Cast<Field>().ToList().Where(w => w.Type != Type.Mine).ToList().ForEach(f => f.Detected = true);
            Assert.IsTrue(Spiel.IsThereAMine());

            var countsOfMarks = Spiel.Fields.Cast<Field>().ToList().Count(w => w.Flag == Mark.yes);
            Assert.AreEqual(0, countsOfMarks, "Anzahl der Markierungen stimmt nicht überein");
        }

        [TestMethod]
        public void Game_IsThereAMine_OneDetectedNoMarked()
        {
            var Spiel = new Game(3, 3, 3);
            Spiel.CreateGameField();

            Spiel.Fields.Cast<Field>().ToList().Where(w => w.Type != Type.Mine).ToList().First().Detected = true;
            Assert.IsFalse(Spiel.IsThereAMine());

            var countsOfMarks = Spiel.Fields.Cast<Field>().ToList().Count(w => w.Flag == Mark.yes);
            Assert.AreEqual(0, countsOfMarks, "Anzahl der Markierungen stimmt nicht überein");
        }
    }
}
