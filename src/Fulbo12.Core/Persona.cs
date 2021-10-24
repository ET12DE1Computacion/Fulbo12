using System;
namespace Fulbo12.Core
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public Pais Pais { get; set; }

        public byte Edad
        {
            get
            {
                var hoy = DateTime.Today;
                var anios = Convert.ToByte((hoy.Year - Nacimiento.Year));
                
                if (hoy.Month < Nacimiento.Month)
                {
                    return --anios;
                }
                else if (hoy.Month == Nacimiento.Month && hoy.Day >= Nacimiento.Day)
                {
                    return anios;
                }
                else return --anios;
            }
        }
        public bool MismaNacionalidad(Persona persona1,Persona persona2)
        {
            if(persona1.Pais==persona2.Pais)
            {
                return true;
            }
            else return false;
        }

    }
}