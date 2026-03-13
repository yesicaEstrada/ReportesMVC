using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace ReportesMVC.Services
{
    public class ValidarSesionAttributeService : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var correo = context.HttpContext.Session.GetString("correo");
            if (correo == null)
            {
                context.Result = new RedirectResult("~/Ingreso/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}
