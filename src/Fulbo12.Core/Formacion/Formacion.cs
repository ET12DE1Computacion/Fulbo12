using System.Collections.Generic;
namespace Fulbo12.Core.Formacion
{
    public class Formacion
    {
        public List<Linea> Lineas { get; set; }
        public string Nombre { get; set; }
        public Formacion() => Lineas = new List<Linea>();
    }
}