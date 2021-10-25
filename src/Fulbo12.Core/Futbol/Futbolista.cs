namespace Fulbo12.Core.Futbol
{
    public class Futbolista
    {
        public Persona Persona { get; set; }
        public TipoFutbolista Tipofutbolista { get; set; }
        public Equipo Equipo { get; set; }

        public bool MismaNacionalidad(Futbolista futbolista)
            => Persona.MismaNacionalidad(futbolista.Persona);
    }
}