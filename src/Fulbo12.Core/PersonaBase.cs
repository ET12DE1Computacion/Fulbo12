using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Fulbo12.Core;
public abstract class PersonaBase
{
    [Column("idPersona")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }

    [Column("nacimiento")]
    public DateOnly Nacimiento { get; set; }
    public required Pais Pais { get; set; }
    public PersonaBase() { }
    [SetsRequiredMembers]
    public PersonaBase(short id, string nombre, string apellido, DateOnly nacimiento, Pais pais)
        => (Id, Nombre, Apellido, Nacimiento, Pais) = (id, nombre, apellido, nacimiento, pais);

    [NotMapped]
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
    public bool MismaNacionalidad(PersonaJuego persona)
        => persona.Pais == this.Pais;

    [NotMapped]
    public string NombreCompleto => $"{Nombre}, {Apellido}";
}