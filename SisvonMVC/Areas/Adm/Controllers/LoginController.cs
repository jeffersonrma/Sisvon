using System;
using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IFuncionarioBo _funcionarioBo;

        public LoginController()
        {
            _funcionarioBo = new FuncionarioBo(_context);
        }
        // GET: Adm/Login
        public ActionResult Index()
        {
            _funcionarioBo.PossuiFuncionario();
            return View();
        }

        [HttpPost]
        public ActionResult Logar(string txtLogin, string txtSenha)
        {
            try
            {
                Funcionario funcionario = _funcionarioBo.Logar(txtLogin, txtSenha);
                if (funcionario == null) return View("Index");

                Session["usuario"] = funcionario;
                return RedirectToAction("Index", "Home");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View("Index");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }

        public ActionResult Deslogar()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

            Session["usuario"] = null;
            return View("Index");
        }


        public ActionResult UsuarioLogado()
        {
            if (UsuarioEstaLogado)
            {
                ViewBag.UsuarioNome = Usuario.Nome;
                return PartialView();
            }
            return PartialView("UsuarioNaoLogado");
        }
    }
}