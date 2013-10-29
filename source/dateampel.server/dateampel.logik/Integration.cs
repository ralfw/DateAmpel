using System;
using System.Threading;
using System.Threading.Tasks;
using dateampel.adapter.ampelspeicher;
using dateampel.adapter.ampelspeicher.daten;

namespace dateampel.logik
{
    public class Integration
    {
        private readonly ParseAdapter _adapter;
        public Integration(ParseAdapter adapter) { _adapter = adapter; }


        public void Ampel_einrichten(dynamic inputModel, Action<Ampel> on_ampel)
        {
            var ampelCode = Operation.Ampelcode_erzeugen();
            var ampel = new Ampel(ampelCode, inputModel.Email, inputModel.Name);

            var are = new AutoResetEvent(false);
            Action sync = async () =>
                {
                    await _adapter.AmpelSpeichernAsync(ampel);
                    are.Set();
                };
            sync();

            are.WaitOne();
            on_ampel(ampel);
        }
    }
}
