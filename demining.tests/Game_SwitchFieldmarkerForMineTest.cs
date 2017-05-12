namespace demining.tests
{
    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using demining.bl;
    using demining.bl.Fakes;

    [TestClass]
    public class Game_SwitchFieldmarkerForMineTest
    {
        [TestMethod]
        public void SwitchFieldmarkerForMine_Mine_NoMarkToYes()
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

            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[1, 1]); // no -> yes

            Assert.AreEqual(1, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("!", result, "Result");
            Assert.AreEqual(Mark.yes, spiel.Fields[1, 1].Flag, "Mark.Yes");
        }

        [TestMethod]
        public void SwitchFieldmarkerForMine_Mine_YesMarkToMayBe()
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

            spiel.SwitchFieldmarkerForMine(spiel.Fields[1, 1]); // no -> yes
            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[1, 1]); // yes -> maybe

            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("?", result, "Result");
            Assert.AreEqual(Mark.maybe, spiel.Fields[1, 1].Flag, "Mark.maybe");
        }

        [TestMethod]
        public void SwitchFieldmarkerForMine_Mine_MayBeMarkToNo()
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

            spiel.SwitchFieldmarkerForMine(spiel.Fields[1, 1]);              // no -> yes
            spiel.SwitchFieldmarkerForMine(spiel.Fields[1, 1]);              // yes -> maybe
            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[1, 1]); // maybe -> no

            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual(string.Empty, result, "Result");
            Assert.AreEqual(Mark.no, spiel.Fields[1, 1].Flag, "Mark.No");
        }

        

        [TestMethod]
        public void SwitchFieldmarkerForMine_FieldUnDetected_NoMarkToYes()
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

            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // no -> yes

            Assert.AreEqual(1, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("!", result, "Result");
            Assert.AreEqual(Mark.yes, spiel.Fields[0, 0].Flag, "Mark.Yes");
        }

        [TestMethod]
        public void SwitchFieldmarkerForMine_FieldUnDetected_YesMarkToMayBe()
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

            spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // no -> yes
            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // yes -> mayBe

            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("?", result, "Result");
            Assert.AreEqual(Mark.maybe, spiel.Fields[0, 0].Flag, "Mark.maybe");
        }

        [TestMethod]
        public void SwitchFieldmarkerForMine_FieldUnDetected_MayBeMarkToNo()
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

            spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // no -> yes
            spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // yes -> mayBe
            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // maybe -> no

            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual(string.Empty, result, "Result");
            Assert.AreEqual(Mark.no, spiel.Fields[0, 0].Flag, "Mark.No");
        }
        
        [TestMethod]
        public void SwitchFieldmarkerForMine_FieldDetected_NoMark()
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

            spiel.Fields[0, 0].Detected = true;

            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // no -> yes

            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("1", result, "Result");
            Assert.AreEqual(Mark.no, spiel.Fields[0, 0].Flag, "Mark.no");
        }

        [TestMethod]
        public void SwitchFieldmarkerForMine_FieldDetected_YesMarkToMayBe()
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

            spiel.Fields[0, 0].Detected = true;

            spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // no -> yes
            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // yes -> mayBe

            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("1", result, "Result");
            Assert.AreEqual(Mark.no, spiel.Fields[0, 0].Flag, "Mark.no");
        }

        [TestMethod]
        public void SwitchFieldmarkerForMine_FieldDetected_MayBeMarkToNo()
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
            spiel.Fields[0, 0].Detected = true;

            spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // no -> yes
            spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // yes -> mayBe
            var result = spiel.SwitchFieldmarkerForMine(spiel.Fields[0, 0]); // maybe -> no
                        
            Assert.AreEqual(0, spiel.NumberOfFlags, "NumberOfFlags");
            Assert.AreEqual("1", result, "Result");
            Assert.AreEqual(Mark.no, spiel.Fields[0, 0].Flag, "Mark.no");
        }
    }
}