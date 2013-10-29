using System;

namespace dateampel.adapter.ampelspeicher.daten
{
    public class Ampel
    {
        public Ampel() {}
        public Ampel(string code, string ownerEmail, string ownerName)
        {
            Code = code;
            Owner = new DatePartner {Email = ownerEmail, Name = ownerName};
            AngelegtAm = DateTime.Now;
        }

        public string ObjectId { get; internal set; }

        public string Code { get; internal set; }

        public DatePartner Owner;
        public DatePartner Date;

        public DateTime AngelegtAm { get; internal set; }
    }
}