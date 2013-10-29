using System;
using NUnit.Framework;
using Parse;
using dateampel.adapter.ampelspeicher;
using dateampel.adapter.ampelspeicher.daten;

namespace dateampel.adapter.tests
{
    [TestFixture]
    public class test_ParseAdapter
    {
        private ParseAdapter _sut;

        [SetUp]
        public void Setup()
        {
            var geheim = new Geheimfach(@"..\..\..\..\..\unversioned\geheim.txt");
            _sut = new ParseAdapter(geheim["parseAppId"], geheim["parseWindowsKey"]);

            Clear();
        }

        private async void Clear()
        {
            var list = await ParseObject.GetQuery(typeof (Ampel).Name).FindAsync();
            foreach (var e in list)
                await e.DeleteAsync();
        }


        [Test, Explicit]
        public async void Ampel_anlegen_und_ändern()
        {
            var ampel = new Ampel("1357", "thorin@", "thorin");
            ampel = await _sut.AmpelSpeichernAsync(ampel);
            Console.WriteLine(ampel.ObjectId);

            ampel.Owner.Name += "x";

            await _sut.AmpelSpeichernAsync(ampel);

            var parseAmpel = await ParseObject.GetQuery("Ampel").GetAsync(ampel.ObjectId);
            ampel = parseAmpel.ToAmpel();
            Assert.AreEqual("thorinx", ampel.Owner.Name);
        }


        [Test, Explicit]
        public async void Ampel_über_Code_laden()
        {
            var ampel = new Ampel("2468", "gloin@", "gloin");
            ampel = await _sut.AmpelSpeichernAsync(ampel);
            Console.WriteLine(ampel.ObjectId);

            ampel = await _sut.AmpelLadenAsync("2468");

            Assert.AreEqual("gloin", ampel.Owner.Name);

            var n = await ParseObject.GetQuery(typeof(Ampel).Name).CountAsync();
            Console.WriteLine("to be torn down {0}", n);
        }
    }
}
