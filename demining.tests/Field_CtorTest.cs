namespace demining.tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using demining.bl;

    [TestClass]
    public class Field_CtorTest
    {
        [TestMethod]
        public void Feld_Ctor_Leer()
        {
            var feld = new Field();

            Assert.AreEqual(false, feld.Detected);
            Assert.AreEqual(Type.Count, feld.Type);
            Assert.AreEqual(Mark.no, feld.Flag);
            Assert.AreEqual(string.Empty, feld.Value);
            Assert.AreEqual(-1, feld.Position.X);
            Assert.AreEqual(-1, feld.Position.Y);
            Assert.AreEqual(0, feld.Neighbours.Count);
        }

        [TestMethod]
        public void Feld_Ctor_MineFalse()
        {
            var feld = new Field(false);

            Assert.AreEqual(false, feld.Detected);
            Assert.AreEqual(Type.Count, feld.Type);
            Assert.AreEqual(Mark.no, feld.Flag);
            Assert.AreEqual(string.Empty, feld.Value);
            Assert.AreEqual(-1, feld.Position.X);
            Assert.AreEqual(-1, feld.Position.Y);
            Assert.AreEqual(0, feld.Neighbours.Count);
        }

        [TestMethod]
        public void Feld_Ctor_MineTrue()
        {
            var feld = new Field(true);

            Assert.AreEqual(false, feld.Detected);
            Assert.AreEqual(Type.Mine, feld.Type);
            Assert.AreEqual(Mark.no, feld.Flag);
            Assert.AreEqual(string.Empty, feld.Value);
            Assert.AreEqual(-1, feld.Position.X);
            Assert.AreEqual(-1, feld.Position.Y);
            Assert.AreEqual(0, feld.Neighbours.Count);
        }
    }
}
