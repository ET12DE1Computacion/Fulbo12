using System.Diagnostics.CodeAnalysis;

namespace Fulbo12.Core.Futbol;
public class Liga : ConNombre
{
    public required List<Equipo> Equipos { get; set; }
    public required Pais Pais { get; set; }
    [SetsRequiredMembers]
    public Liga(string nombre, Pais pais) : base(nombre)
    {
        Pais = pais;
        Equipos = new List<Equipo>();
    }
    public Liga() { }
    public override string ToString()
        => $"{Nombre}({Pais.Nombre})";
}