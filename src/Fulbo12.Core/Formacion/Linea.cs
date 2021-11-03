using System.Collections.Generic;
namespace Fulbo12.Core.Formacion
{
    public class Linea
    {
        public byte NumeroDeLinea { get; set; }
        public List<PosicionEnCancha> PosicionesEnCancha { get; set; }
        public Linea() => PosicionesEnCancha = new List<PosicionEnCancha>();

    }
}