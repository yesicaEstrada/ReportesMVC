using Microsoft.AspNetCore.Mvc;

using ReportesMVC.Models;
using ReportesMVC.Data;
using ReportesMVC.Services;
using Microsoft.AspNetCore.Http;

namespace ReportesMVC.Controllers
{
    public class IngresoController : Controller
    {
        private UsuarioDbContext _usuarioDbContext;

        public IngresoController(UsuarioDbContext usuarioDbContext)
        {
            _usuarioDbContext = usuarioDbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String correo, String clave)
        {
            var claveE = UtilityServices.ConvertirSHA256(clave); //la clave que ingresen la encripte
            var IdUsuario = _usuarioDbContext.ValidarUsuario(correo, claveE);

            if(IdUsuario != 0)
            {
                HttpContext.Session.SetString("correo", correo);
                return RedirectToAction("Listar", "Persona");
            }
            else
            {
                ViewData["Mensaje"] = "Credenciales incorrectas, valide por favor!";
                return View();
            }
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(UsuarioDTO usuario)
        {

            if (usuario.Clave == usuario.ConfirmarClave)
            {
                usuario.Clave = UtilityServices.ConvertirSHA256(usuario.Clave);
                var resultado = _usuarioDbContext.RegistrarUsuario(usuario);

                ViewData["Mensaje"] = resultado.mensaje;
                if (resultado.registrado)
                {           
                    return RedirectToAction("Login", "Ingreso"); //que nos redirija al login que esta en el controller de Ingreso
                }
                else
                {
                    return View();
                }
            }   
            else
            {
                ViewBag.Correo = usuario.Correo;
                ViewBag.Clave = usuario.Clave;
                ViewBag.Mensaje = "Las claves no coinciden";
                return View(); //recarga la pagina, pero sin borrar los datos ya registrados
            }
        }

        

    }
}
