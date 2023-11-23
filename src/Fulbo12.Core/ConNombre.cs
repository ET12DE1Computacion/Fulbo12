using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Fulbo12.Core;
/// <summary>
/// Clase sencilla que expone un id: byte y un string de descripción
/// </summary>
public abstract class ConNombre
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [MaxLength(30)]
    [Required]
    public required string Nombre { get; set; }
    public ConNombre() { }
    [SetsRequiredMembers]
    public ConNombre(string nombre) => Nombre = nombre;
    [SetsRequiredMembers]
    public ConNombre(string nombre, byte id) : this(nombre)
        => Id = id;
    public override string ToString()
        => Nombre;
}