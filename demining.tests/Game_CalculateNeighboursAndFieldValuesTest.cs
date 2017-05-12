namespace demining.tests
{
    using System.Linq;

    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using demining.bl;
    using demining.bl.Fakes;

    [TestClass]
    public class Game_CalculateNeighboursAndFieldValuesTest
    {
        [TestMethod]
        public void CalculateNeighboursAndFieldValues_X3Y3M1()
        {
            var spiel = new Game(3, 3, 1);

            using (ShimsContext.Create())
            {
                ShimGame.AllInstances.DistributeMinesByRandomInt32 = (Game a, int b) =>
                {
                    a.Fields[1, 1] = new Field(true);
                };
                spiel.CreateGameField();
            }

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(1, actualCountsOfMines, "Die Anzahl der Minen stimmt nicht!");

            Assert.AreEqual("1", spiel.Fields[0, 0].Value, "Felder[0, 0]");
            Assert.AreEqual("1", spiel.Fields[0, 1].Value, "Felder[0, 1]");
            Assert.AreEqual("1", spiel.Fields[0, 2].Value, "Felder[0, 2]");
            Assert.AreEqual("1", spiel.Fields[1, 0].Value, "Felder[1, 0]");
            Assert.AreEqual("1", spiel.Fields[1, 2].Value, "Felder[1, 2]");
            Assert.AreEqual("1", spiel.Fields[2, 0].Value, "Felder[2, 0]");
            Assert.AreEqual("1", spiel.Fields[2, 1].Value, "Felder[2, 1]");
            Assert.AreEqual("1", spiel.Fields[2, 2].Value, "Felder[2, 2]");

            Assert.AreEqual(3, spiel.Fields[0, 0].Neighbours.Count, "Felder[0, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[0, 1].Neighbours.Count, "Felder[0, 1].Neighbours");
            Assert.AreEqual(3, spiel.Fields[0, 2].Neighbours.Count, "Felder[0, 2].Neighbours");
            Assert.AreEqual(5, spiel.Fields[1, 0].Neighbours.Count, "Felder[1, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[1, 2].Neighbours.Count, "Felder[1, 2].Neighbours");
            Assert.AreEqual(3, spiel.Fields[2, 0].Neighbours.Count, "Felder[2, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[2, 1].Neighbours.Count, "Felder[2, 1].Neighbours");
            Assert.AreEqual(3, spiel.Fields[2, 2].Neighbours.Count, "Felder[2, 2].Neighbours");
        }

        [TestMethod]
        public void CalculateNeighboursAndFieldValues_X3Y3M8()
        {
            var spiel = new Game(3, 3, 8);

            using (ShimsContext.Create())
            {
                ShimGame.AllInstances.DistributeMinesByRandomInt32 = (Game a, int b) =>
                {
                    a.Fields[0, 0] = new Field(true);
                    a.Fields[0, 1] = new Field(true);
                    a.Fields[0, 2] = new Field(true);
                    a.Fields[1, 0] = new Field(true);
                    a.Fields[1, 2] = new Field(true);
                    a.Fields[2, 0] = new Field(true);
                    a.Fields[2, 1] = new Field(true);
                    a.Fields[2, 2] = new Field(true);
                };
                spiel.CreateGameField();
            }
            
            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(8, actualCountsOfMines, "Die Anzahl der Minen stimmt nicht!");

            Assert.AreEqual("8", spiel.Fields[1, 1].Value, "Felder[1, 1]");

            Assert.AreEqual(8, spiel.Fields[1, 1].Neighbours.Count, "Felder[1, 1].Neighbours");
        }

        [TestMethod]
        public void CalculateNeighboursAndFieldValues_X3Y3M6()
        {
            var spiel = new Game(3, 3, 6);

            using (ShimsContext.Create())
            {
                ShimGame.AllInstances.DistributeMinesByRandomInt32 = (Game a, int b) =>
                {
                    a.Fields[0, 0] = new Field(true);
                    a.Fields[0, 1] = new Field(true);
                    a.Fields[0, 2] = new Field(true);
                    a.Fields[1, 0] = new Field(true);
                    a.Fields[1, 2] = new Field(true);
                    a.Fields[2, 0] = new Field(true);
                };
                spiel.CreateGameField();
            }
            
            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(6, actualCountsOfMines, "Die Anzahl der Minen stimmt nicht!");

            Assert.AreEqual("6", spiel.Fields[1, 1].Value, "Felder[1, 1]");
            Assert.AreEqual("3", spiel.Fields[2, 1].Value, "Felder[2, 1]");
            Assert.AreEqual("1", spiel.Fields[2, 2].Value, "Felder[2, 2]");

            Assert.AreEqual(8, spiel.Fields[1, 1].Neighbours.Count, "Felder[1, 1].Neighbours");
            Assert.AreEqual(5, spiel.Fields[2, 1].Neighbours.Count, "Felder[2, 1].Neighbours");
            Assert.AreEqual(3, spiel.Fields[2, 2].Neighbours.Count, "Felder[2, 2].Neighbours");
        }

        [TestMethod]
        public void CalculateNeighboursAndFieldValues_X4Y3M2()
        {
            var spiel = new Game(4, 3, 2);

            using (ShimsContext.Create())
            {
                ShimGame.AllInstances.DistributeMinesByRandomInt32 = (Game a, int b) =>
                {
                    a.Fields[1, 1] = new Field(true);
                    a.Fields[1, 2] = new Field(true);
                };
                spiel.CreateGameField();
            }
            
            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(2, actualCountsOfMines, "Die Anzahl der Minen stimmt nicht!");

            Assert.AreEqual("1", spiel.Fields[0, 0].Value, "Felder[0, 0]");
            Assert.AreEqual("2", spiel.Fields[0, 1].Value, "Felder[0, 1]");
            Assert.AreEqual("2", spiel.Fields[0, 2].Value, "Felder[0, 2]");
            Assert.AreEqual("1", spiel.Fields[0, 3].Value, "Felder[0, 3]");
            Assert.AreEqual("1", spiel.Fields[1, 0].Value, "Felder[1, 0]");
            Assert.AreEqual("1", spiel.Fields[1, 3].Value, "Felder[1, 3]");
            Assert.AreEqual("1", spiel.Fields[2, 0].Value, "Felder[2, 0]");
            Assert.AreEqual("2", spiel.Fields[2, 1].Value, "Felder[2, 1]");
            Assert.AreEqual("2", spiel.Fields[2, 2].Value, "Felder[2, 2]");
            Assert.AreEqual("1", spiel.Fields[2, 3].Value, "Felder[2, 3]");

            Assert.AreEqual(3, spiel.Fields[0, 0].Neighbours.Count, "Felder[0, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[0, 1].Neighbours.Count, "Felder[0, 1].Neighbours");
            Assert.AreEqual(5, spiel.Fields[0, 2].Neighbours.Count, "Felder[0, 2].Neighbours");
            Assert.AreEqual(3, spiel.Fields[0, 3].Neighbours.Count, "Felder[0, 3].Neighbours");
            Assert.AreEqual(5, spiel.Fields[1, 0].Neighbours.Count, "Felder[1, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[1, 3].Neighbours.Count, "Felder[1, 3].Neighbours");
            Assert.AreEqual(3, spiel.Fields[2, 0].Neighbours.Count, "Felder[2, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[2, 1].Neighbours.Count, "Felder[2, 1].Neighbours");
            Assert.AreEqual(5, spiel.Fields[2, 2].Neighbours.Count, "Felder[2, 2].Neighbours");
            Assert.AreEqual(3, spiel.Fields[2, 3].Neighbours.Count, "Felder[2, 3].Neighbours");
        }

        [TestMethod]
        public void CalculateNeighboursAndFieldValues_X4Y4M2()
        {
            var spiel = new Game(4, 4, 2);

            using (ShimsContext.Create())
            {
                ShimGame.AllInstances.DistributeMinesByRandomInt32 = (Game a, int b) =>
                {
                    a.Fields[0, 0] = new Field(true);
                    a.Fields[3, 3] = new Field(true);
                };
                spiel.CreateGameField();
            }

            var actualFeldList = spiel.Fields.Cast<Field>().ToList();
            var actualCountsOfMines = actualFeldList.Count(c => c.Type == Type.Mine);

            Assert.AreEqual(2, actualCountsOfMines, "Die Anzahl der Minen stimmt nicht!");

            Assert.AreEqual("1", spiel.Fields[0, 1].Value, "Felder[0, 1]");
            Assert.AreEqual(string.Empty, spiel.Fields[0, 2].Value, "Felder[0, 2]");
            Assert.AreEqual(string.Empty, spiel.Fields[0, 3].Value, "Felder[0, 3]");
            Assert.AreEqual("1", spiel.Fields[1, 0].Value, "Felder[1, 0]");
            Assert.AreEqual("1", spiel.Fields[1, 1].Value, "Felder[1, 1]");
            Assert.AreEqual(string.Empty, spiel.Fields[1, 2].Value, "Felder[1, 2]");
            Assert.AreEqual(string.Empty, spiel.Fields[1, 3].Value, "Felder[1, 3]");
            Assert.AreEqual(string.Empty, spiel.Fields[2, 0].Value, "Felder[2, 0]");
            Assert.AreEqual(string.Empty, spiel.Fields[2, 1].Value, "Felder[2, 1]");
            Assert.AreEqual("1", spiel.Fields[2, 2].Value, "Felder[2, 2]");
            Assert.AreEqual("1", spiel.Fields[2, 3].Value, "Felder[2, 3]");
            Assert.AreEqual(string.Empty, spiel.Fields[3, 0].Value, "Felder[3, 0]");
            Assert.AreEqual(string.Empty, spiel.Fields[3, 1].Value, "Felder[3, 1]");
            Assert.AreEqual("1", spiel.Fields[3, 2].Value, "Felder[3, 2]");

            Assert.AreEqual(5, spiel.Fields[0, 1].Neighbours.Count, "Felder[0, 1].Neighbours");
            Assert.AreEqual(5, spiel.Fields[0, 2].Neighbours.Count, "Felder[0, 2].Neighbours");
            Assert.AreEqual(3, spiel.Fields[0, 3].Neighbours.Count, "Felder[0, 3].Neighbours");
            Assert.AreEqual(5, spiel.Fields[1, 0].Neighbours.Count, "Felder[1, 0].Neighbours");
            Assert.AreEqual(8, spiel.Fields[1, 1].Neighbours.Count, "Felder[1, 1].Neighbours");
            Assert.AreEqual(8, spiel.Fields[1, 2].Neighbours.Count, "Felder[1, 2].Neighbours");
            Assert.AreEqual(5, spiel.Fields[1, 3].Neighbours.Count, "Felder[1, 3].Neighbours");
            Assert.AreEqual(5, spiel.Fields[2, 0].Neighbours.Count, "Felder[2, 0].Neighbours");
            Assert.AreEqual(8, spiel.Fields[2, 1].Neighbours.Count, "Felder[2, 1].Neighbours");
            Assert.AreEqual(8, spiel.Fields[2, 2].Neighbours.Count, "Felder[2, 2].Neighbours");
            Assert.AreEqual(5, spiel.Fields[2, 3].Neighbours.Count, "Felder[2, 3].Neighbours");
            Assert.AreEqual(3, spiel.Fields[3, 0].Neighbours.Count, "Felder[3, 0].Neighbours");
            Assert.AreEqual(5, spiel.Fields[3, 1].Neighbours.Count, "Felder[3, 1].Neighbours");
            Assert.AreEqual(5, spiel.Fields[3, 2].Neighbours.Count, "Felder[3, 2].Neighbours");
        }
    }
}