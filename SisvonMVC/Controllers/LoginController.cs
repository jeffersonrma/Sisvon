using System;
using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IClienteBo _clienteBo;
        public LoginController()
        {
            _clienteBo = new ClienteBo(_context);
        }

        // GET: Adm/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(string txtLogin, string txtSenha)
        {
            try
            {
                Cliente cliente = _clienteBo.Logar(txtLogin, txtSenha);
                if (cliente != null)
                {
                    LogarCliente(cliente);
                    SucessoMessage = "Logado com sucesso";
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
                ErroMessage = "E-mail ou senha inválidos!";
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            catch (Exception exception)
            {

                return View("Error");
            }

        }

        public ActionResult Deslogar()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Index", "Home");

            DeslogarCliente();
            return RedirectToAction("Index","Home");
        }
        

        public ActionResult UsuarioLogado()
        {
            if (!UsuarioEstaLogado) return PartialView("UsuarioNaoLogado");

            ViewBag.UsuarioNome = Usuario.Nome;
            ViewBag.UsuarioEmail = Usuario.Email;
            return PartialView();
        }
    }
}