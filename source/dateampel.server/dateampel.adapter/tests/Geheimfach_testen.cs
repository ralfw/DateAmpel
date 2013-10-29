using NUnit.Framework;

namespace dateampel.adapter.tests
{
    [TestFixture]
    public class Geheimfach_testen
    {
        [Test, Explicit]
        public void Laden_aus_Datei()
        {
            var geheim = new Geheimfach(@"tests\geheim.txt");
            Assert.AreEqual("hello", geheim["a"]);
            Assert.AreEqual("world", geheim["b"]);
        }
    }
}
