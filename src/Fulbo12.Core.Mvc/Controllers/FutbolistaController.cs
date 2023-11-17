using Fulbo12.Core.Mvc.ViewModels;
using Fulbo12.Core.Persistencia;
using Fulbo12.Core.Persistencia.Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace Fulbo12.Core.Mvc.Controllers;

public class FutbolistaController : Controller
{
    private readonly IUnidad _unidad;
    public FutbolistaController(IUnidad unidad) => _unidad = unidad;


    public async Task<IActionResult> Listado()
    {
        var futbolistas = await _unidad.RepoFutbolista.ObtenerAsync(orden: fs => fs.OrderBy(f => f.Persona.Nombre),
                    includes: "Persona,Equipo,Tipofutbolista");
        return View(futbolistas);
    }
    public async Task<IActionResult> Detalle(short id)
    {
        var futbolista = (await _unidad.RepoFutbolista.ObtenerAsync(filtro: f => f.Id == id, null, "Equipo")).FirstOrDefault();
        return View(futbolista);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }

    [HttpGet]
    public async Task<IActionResult> Alta(short idPersona, byte valoracion)
    {
        var persona = (await _unidad.RepoPersona.ObtenerAsync(p => p.Id == idPersona, includes: "Pais")).FirstOrDefault();
        if (persona is null)
            return NotFound();

        var tipoFutbolistas = await _unidad.RepoTipoFutbolista.ObtenerAsync();
        var posiciones = await _unidad.RepoPosicion.ObtenerAsync();
        var equipos = await _unidad.RepoEquipo.ObtenerAsync(includes: "Liga");

        var vmFutbolista =
            new VMFutbolista(persona, posiciones, equipos, tipoFutbolistas, valoracion);

        return View("Upsert", vmFutbolista);
    }
    [HttpGet]

    public async Task<IActionResult> Modificar(short? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var futbolista = await _unidad.RepoFutbolista.ObtenerPorIdAsync(id);
        if (futbolista is null)
            return NotFound();

        return View("Upsert", futbolista);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(VMFutbolista vMFutbolista)
    {
        if (!ModelState.IsValid)
        {
            vMFutbolista.AsignarPosiciones(await _unidad.RepoPosicion.ObtenerAsync());
            vMFutbolista.AsignarEquipo(await _unidad.RepoEquipo.ObtenerAsync());
            vMFutbolista.AsignarTipo(await _unidad.RepoTipoFutbolista.ObtenerAsync());
            return View("Upsert", vMFutbolista);
        }

        if (vMFutbolista.IdFutbolista == 0)
        {
            var futbolista =  await vMFutbolista.CrearFutbolistaAsync(_unidad);
            await _unidad.RepoFutbolista.AltaAsync(futbolista);
        }
        else
        {
            var futbolistaRepo = await _unidad.RepoFutbolista.ObtenerPorIdAsync(vMFutbolista.IdFutbolista);
            if (futbolistaRepo is null)
                return NotFound();
            futbolistaRepo.Persona.Nombre = futbolistaRepo.Persona.Nombre;
            _unidad.RepoFutbolista.Modificar(futbolistaRepo);
        }
        try
        {
            await _unidad.GuardarAsync();
        }
        catch (EntidadDuplicadaException e)
        {
            return NotFound();
        }
        return RedirectToActionPermanent(nameof(Listado));
    }

}