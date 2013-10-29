using System;
using System.Collections.Generic;
using Parse;
using dateampel.adapter.ampelspeicher.daten;

namespace dateampel.adapter.ampelspeicher
{
    internal static class ParseSerialisierer
    {
        internal static ParseObject ToParse(this Ampel ampel)
        {
            var po = new ParseObject(ampel.GetType().Name)
                {
                    {"Code", ampel.Code},
                    {"Owner", ampel.Owner.ToParse()},
                    {"Date", ampel.Date != null ? ampel.Date.ToParse() : null}
                };
            po.ObjectId = ampel.ObjectId;
            return po;
        }

        internal static Ampel ToAmpel(this ParseObject parseAmpel)
        {
            return new Ampel()
                {
                    Code = parseAmpel["Code"].ToString(),
                    Owner = (parseAmpel["Owner"] as Dictionary<string,object>).ToDatePartner(),
                    //Date = parseAmpel.ContainsKey("Date") ? (parseAmpel["Date"] as Dictionary<string,object>).ToDatePartner() : null,
                    AngelegtAm = parseAmpel.CreatedAt.Value,

                    ObjectId = parseAmpel.ObjectId
                };
        }


        internal static Dictionary<string, object> ToParse(this DatePartner partner)
        {
            return new Dictionary<string, object>()
                {
                    {"Email", partner.Email},
                    {"Name", partner.Name},
                    {"Vote", (int)partner.Vote}
                };
        }

        internal static DatePartner ToDatePartner(this Dictionary<string,object> parseDatePartner)
        {
            if (parseDatePartner == null) return null;

            return new DatePartner()
                {
                    Email = parseDatePartner["Email"].ToString(),
                    Name = parseDatePartner["Name"].ToString(),
                    Vote = (Votes)(int.Parse(parseDatePartner["Vote"].ToString()))
                };
        }
    }
}