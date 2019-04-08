using System.Web.Mvc;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: Adm/Home
        public ActionResult Index()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult MenuLateral()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

            ViewBag.UsuarioAdm = UsuarioEstaLogado && Usuario.Administrador;
            return PartialView();
        }
    }
}