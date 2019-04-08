using System;
using System.Linq;
using System.Web.Mvc;
using Sisvon.Infra.Data.Context;
using Sisvon.Model;
using Sisvon.Model.Entities;
using SisvonMVC.ViewModels;

namespace SisvonMVC.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly SisvonContext _context;
        public ControllerBase()
        {
            _context = new SisvonContext();
        }
        //Usuario
        protected Cliente Usuario
        {
            get
            {
                if (UsuarioEstaLogado)
                {
                    return (Cliente)Session["cliente"];
                }
                return null;
            }
        }

        protected void LogarCliente(Cliente usuario)
        {
            Session["cliente"] = usuario;
        }

        protected void DeslogarCliente()
        {
            Session["cliente"] = null;
        }

        protected bool UsuarioEstaLogado
        {
            get
            {
                return Session["cliente"] != null;
            }
        }
        //Carrinho
        public bool CarrinhoAny
        {
            get
            {
                return Carrinho != null && Carrinho.ItensCarrinho.Any();
            }
        }
        protected CarrinhoVM Carrinho
        {
            get { return (CarrinhoVM)Session["carrinho"]; }
            set { Session["carrinho"] = value; }
        }
        //Message System
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
                using (new LogWriter(value, Server.MapPath("~/log")));
            }
            
        }
    }
}