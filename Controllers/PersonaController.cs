using Microsoft.AspNetCore.Mvc;
using ReportesMVC.Data;
using ReportesMVC.Models;
using ReportesMVC.Services;

namespace ReportesMVC.Controllers
{
    [ValidarSesionAttributeService]
    public class PersonaController : Controller
    {
        PersonaDbContext _personaContext = new PersonaDbContext();

        public IActionResult Listar()
        {
            var obtLista = _personaContext.GetPersonas();
            //mandamos al html los la lista
            return View(obtLista);
        }
        
        public IActionResult Actualizar(int IdPersona)
        {
            var persona = _personaContext.ObtenerId(IdPersona);
            return View(persona);
        }
        [HttpPost]
        public IActionResult Actualizar(PersonaDTO persona)
        {
            var respuesta = _personaContext.ActualizarPersona(persona);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }



    }
}
