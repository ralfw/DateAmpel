using System;

namespace dateampel.logik
{
    public class Operation
    {
        public static string Ampelcode_erzeugen()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}