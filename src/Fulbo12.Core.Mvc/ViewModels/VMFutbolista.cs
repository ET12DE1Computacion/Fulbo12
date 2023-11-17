using System.ComponentModel.DataAnnotations;
using Fulbo12.Core.Futbol;
using Fulbo12.Core.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fulbo12.Core.Mvc.ViewModels;

public class VMFutbolista
{
    public PersonaJuego PersonaJugador { get; set; }
    public short IdPersonaJuego { get; set; }
    public SelectList PosicionesJugador { get; set; }
    public SelectList EquipoJugador { get; set; }
    public SelectList Tipojugador { get; set; }
    public byte IdTipo { get; set; }
    public byte IdFutbolista {get; set; }
    public byte IdEquipo { get; set; }
    public byte IdPosicion { get; set; }

    [Range(50, 99, ErrorMessage = "La valoracion del jugador tiene que estar entre el rango definido")]

    public byte Valoracion { get; set; } 
    public VMFutbolista(){}


    public VMFutbolista(PersonaJuego personaJuego , IEnumerable<Posicion> posiciones, IEnumerable<Equipo> equipos, IEnumerable<TipoFutbolista> tipos, byte valoracion)
    {
        PersonaJugador = personaJuego;
        IdPersonaJuego = personaJuego.Id;
        Valoracion = valoracion;
        PosicionesJugador = new SelectList(posiciones,
                                    dataTextField: nameof(Posicion.Abreviado),
                                    dataValueField: nameof(Posicion.Id));

        EquipoJugador = new SelectList(equipos,
                                    dataTextField: nameof(Equipo.Nombre),
                                    dataValueField: nameof(Equipo.Id));
        Tipojugador = new SelectList(tipos,
                            dataTextField: nameof(TipoFutbolista.Nombre),
                            dataValueField: nameof(TipoFutbolista.Id));
    }
    public void AsignarPosiciones(IEnumerable<Posicion> posiciones)
    {
        PosicionesJugador = new SelectList(posiciones,
                                            dataTextField: nameof(Posicion.Abreviado),
                                            dataValueField: nameof(Posicion.Id));
    }
    public void AsignarEquipo (IEnumerable<Equipo> equipos)
    {
                EquipoJugador = new SelectList(equipos,
                                    dataTextField: nameof(Equipo.Nombre),
                                    dataValueField: nameof(Equipo.Id));
    }
        public void AsignarTipo (IEnumerable<TipoFutbolista> tipos)
    {
        Tipojugador = new SelectList(tipos,
                            dataTextField: nameof(TipoFutbolista.Nombre),
                            dataValueField: nameof(TipoFutbolista.Id));
    }

    internal async Task<Futbolista> CrearFutbolistaAsync(IUnidad unidad)
    {
        // IdPais se setea en base a una opción seleccionada del SelectList
        //var personaBase = await unidad.RepoPersona.ObtenerPorIdAsync(PersonaJugador.Id);
        var TipoJugador = await unidad.RepoTipoFutbolista.ObtenerPorIdAsync(IdTipo);
        var EquipoJugador =  await unidad.RepoEquipo.ObtenerPorIdAsync(IdEquipo);
        return new Futbolista()
        {
            Persona = PersonaJugador,
            Valoracion = Valoracion,
            Posiciones = new List<Posicion>(),
            //pensar en como hacer para que el futbolista acepte mas de una posicion.
            Tipofutbolista = TipoJugador!,
            Equipo = EquipoJugador!,
        };
    }
}