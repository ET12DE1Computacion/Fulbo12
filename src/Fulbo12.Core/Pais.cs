using System.Diagnostics.CodeAnalysis;

namespace Fulbo12.Core;
public class Pais : ConNombre
{
    public required string Abreviatura { get; set; }
    public Pais() : base() { }
    
    [SetsRequiredMembers]
    public Pais(byte id, string nombre, string abreviatura) : base(nombre, id)
        => Abreviatura = abreviatura;
    public override string ToString()
        => Nombre;
}