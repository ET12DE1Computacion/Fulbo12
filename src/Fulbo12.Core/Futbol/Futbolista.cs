namespace Fulbo12.Core.Futbol
{
    public class Futbolista
    {
        public Persona Persona { get; set; }
        public TipoFutbolista Tipofutbolista { get; set; }
        public Equipo Equipo { get; set; }

        public bool MismaNacionalidad(Futbolista futbolista1,Futbolista futbolista2)
        {
           if(Persona.MismaNacionalidad(futbolista1.Persona,futbolista2.Persona))
           {
               return true;
           }
           else return false;
        }
    }
}