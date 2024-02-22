using System.ComponentModel.DataAnnotations;
using Fulbo12.Core.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fulbo12.Core.Mvc.ViewModels
{
    public class VMPersonaJuego
    {
        public SelectList? Paises { get; set; }
        public string NombrePersona { get; set; } = string.Empty;
        public string ApellidoPersona { get; set; } = string.Empty;
        [Required]
        [Range(35,160,ErrorMessage = "El peso tiene que estar entre {1} y {2}")]
        public float PesoPersona { get; set; }

        [Required]
        [Range(1.0,2.5,ErrorMessage = "La altura tiene que estar entre {1} y {2}")]
        public float AlturaPersona { get; set; }
        public DateOnly NacimientoPersona { get; set; }
        [Range(1, byte.MaxValue, ErrorMessage = "Seleccione un pais por favor")]
        public byte IdPais {get; set;}
        public short IdPersona { get; set; }
        public VMPersonaJuego() { }
        public VMPersonaJuego(IEnumerable<Pais> paises) => AsignarPaises(paises);

        public void AsignarPaises(IEnumerable<Pais> paises)
        {
            Paises = new SelectList(paises,
                                                dataTextField: nameof(Pais.Nombre),
                                                dataValueField: nameof(Pais.Id));
        }

        public VMPersonaJuego(IEnumerable<Pais> paises, PersonaJuego persona)
        {
            Paises = new SelectList(paises,
                                    dataTextField: nameof(Pais.Nombre),
                                    dataValueField: nameof(Pais.Id),
                                    selectedValue: persona.Id);
            NombrePersona = persona.Nombre;
            ApellidoPersona = persona.Apellido;
            PesoPersona = persona.Peso;
            AlturaPersona = persona.Altura;
            NacimientoPersona = persona.Nacimiento;
            IdPersona = persona.Id;
        }

        internal PersonaJuego CrearPersona(IUnidad unidad)
        {
            // IdPais se setea en base a una opción seleccionada del SelectList
            var pais = unidad.RepoPais.ObtenerPorId(IdPais);
            return new PersonaJuego()
            {
                Altura = AlturaPersona,
                Nacimiento = NacimientoPersona,
                Nombre = NombrePersona,
                Pais = pais!,
                Peso = PesoPersona,
                Apellido = ApellidoPersona            
            };
        }
        
    }
}