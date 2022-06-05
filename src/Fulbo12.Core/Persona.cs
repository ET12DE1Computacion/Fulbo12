namespace Fulbo12.Core;
public class Persona
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateOnly Nacimiento { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }
    public Pais Pais { get; set; }

    public byte Edad
    {
        get
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
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
    public bool MismaNacionalidad(Persona persona)
        =>  persona.Pais==this.Pais;
}