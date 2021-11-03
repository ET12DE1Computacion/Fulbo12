using System.Collections.Generic;
namespace Fulbo12.Core.Formacion
{
    public class Linea
    {
        public List<PosicionEnCancha> PosicionesEnCancha { get; set; }
        public Linea() => PosicionesEnCancha = new List<PosicionEnCancha>();

    }
}