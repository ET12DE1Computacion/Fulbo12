using Fulbo12.Core;
using Fulbo12.Core.Mvc.ViewModels;
using Fulbo12.Core.Persistencia;
using Fulbo12.Core.Persistencia.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace Fulbo12.Core.Mvc.Controllers;

public class PersonaController : Controller
{
    private readonly IUnidad _unidad;

    public PersonaController(IUnidad unidad) => _unidad = unidad;

    public async Task<IActionResult> Listado()
    {
        var personas = await _unidad.RepoPersona.ObtenerAsync
                            (orden: ps => ps.OrderBy(p => p.Nombre));
        return View("Busqueda", personas);
    }
    public async Task<IActionResult> Detalle(short id)
    {
        var persona = (await _unidad.RepoPersona.ObtenerAsync(filtro: p => p.Id == id, null, "Pais")).FirstOrDefault();
        return View(persona);
    }

    [HttpGet]
    public async Task<IActionResult> Alta()
    {
        var paises = await _unidad.RepoPais.ObtenerAsync
                        (orden: ps => ps.OrderBy(p => p.Nombre));
        var vmPersona = new VMPersonaJuego(paises);
        return View("Upsert", vmPersona);
    }

    [HttpGet]

    public async Task<IActionResult> Modificar(short? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var persona = await _unidad.RepoPersona.ObtenerPorIdAsync(id);
        if (persona is null)
            return NotFound();

        return View("Upsert", persona);
    }

    [HttpGet]
    public async Task<IActionResult> Buscar(string? busqueda)
    {
        IEnumerable<PersonaJuego>? personas = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            personas = await _unidad.RepoPersona.BusquedaPersonaAsync(busqueda);
            if (personas.Count() == 0)
                return View("NoEncontrado");
        }
        personas = personas ?? new List<PersonaJuego>();
        return View("Busqueda", personas);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(VMPersonaJuego vmPersonaJuego)
    {
        if (!ModelState.IsValid)
        {
            vmPersonaJuego.AsignarPaises(await _unidad.RepoPais.ObtenerAsync());
            return View("Upsert", vmPersonaJuego);
        }

        if (vmPersonaJuego.IdPersona == 0)
        {
            var personaJuego = vmPersonaJuego.CrearPersona(_unidad);
            await _unidad.RepoPersona.AltaAsync(personaJuego);
        }
        else
        {
            var personaRepo = await _unidad.RepoPersona.ObtenerPorIdAsync(vmPersonaJuego.IdPersona);
            if (personaRepo is null)
                return NotFound();
            personaRepo.Nombre = vmPersonaJuego.NombrePersona;
            _unidad.RepoPersona.Modificar(personaRepo);
        }
        try
        {
            await _unidad.GuardarAsync();
        }
        catch (EntidadDuplicadaException)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Listado));
    }
}