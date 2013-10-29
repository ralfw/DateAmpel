using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Parse;
using dateampel.adapter.ampelspeicher.daten;

namespace dateampel.adapter.ampelspeicher
{
    public class ParseAdapter
    {
        public ParseAdapter(string appId, string windowsKey)
        {
            ParseClient.Initialize(appId, windowsKey);
        }


        public async Task<Ampel> AmpelSpeichernAsync(Ampel ampel)
        {
            var parseAmpel = ampel.ToParse();
            await parseAmpel.SaveAsync();

            ampel.ObjectId = parseAmpel.ObjectId;
            return ampel;
        }


        public async Task<Ampel> AmpelLadenAsync(string ampelCode)
        {
            var query = from ampeln in ParseObject.GetQuery(typeof(Ampel).Name)
                        where ampeln.Get<string>("Code") == ampelCode
                        select ampeln;
            var parseAmpel = await query.FirstAsync();
            return parseAmpel.ToAmpel();
        }
    }
}
