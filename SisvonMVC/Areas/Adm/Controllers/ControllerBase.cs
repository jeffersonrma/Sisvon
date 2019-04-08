using System;
using System.Web.Mvc;
using Sisvon.Infra.Data.Context;
using Sisvon.Model;
using Sisvon.Model.Entities;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly SisvonContext _context;
        public ControllerBase()
        {
            _context = new SisvonContext();
        }
        protected Funcionario Usuario
        {
            get
            {
                if (UsuarioEstaLogado)
                {
                    return (Funcionario)Session["usuario"];
                }
                return null;
            }
        }

        protected bool UsuarioEstaLogado
        {
            get
            {
                return Session["usuario"] != null;
            }
        }

        protected bool UsuarioAdm
        {
            get { return Usuario.Administrador; }
        }

        //MessageSystem
        protected string ErroMessage
        {
            set { TempData["Erro"] = value; }
        }
        protected string SucessoMessage
        {
            set { TempData["Sucesso"] = value; }
        }
        protected Exception LogMessage
        {
            set
            {
                using (new LogWriter(value,
                    Server.MapPath("~/log"))) ;
            }

        }
    }
}