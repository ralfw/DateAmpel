namespace dateampel.adapter.ampelspeicher.daten
{
    public enum Votes
    {
        Leer,
        Gr�n,
        Gelb,
        Rot
    }

    public class DatePartner
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Votes Vote { get; set; }
    }
}