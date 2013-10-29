using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dateampel.adapter
{
    public class Geheimfach
    {
        private readonly string _dateiname;
        public Geheimfach() {}
        public Geheimfach(string dateiname) { _dateiname = dateiname; }


        public string this[string schlüssel]
        {
            get
            {
                if (File.Exists(_dateiname))
                    return Laden_aus_Datei(schlüssel);
                return Laden_aus_AppConfig(schlüssel);
            }
        }


        private string Laden_aus_Datei(string schlüssel)
        {
            return File.ReadAllLines(_dateiname)
                       .Select(l =>
                           {
                               var parts = l.Split('=');
                               return new {Schlüssel = parts[0].Trim(), Wert = parts[1].Trim()};
                           })
                       .First(_ => _.Schlüssel.ToLower() == schlüssel.ToLower())
                       .Wert;
        }


        private string Laden_aus_AppConfig(string schlüssel)
        {
            throw new NotImplementedException();
        }
    }
}
