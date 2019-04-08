using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IProdutoBo _produtoBo;

        public HomeController()
        {
            _produtoBo = new ProdutoBo(_context);
        }
        
        public ActionResult Index()
        {
            try
            {
               return View();
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult Destaque()
        {
            try
            {
                IEnumerable<Produto> prod = _produtoBo.GetDestaque();
                if (!prod.Any())
                    return View("Error");

                return PartialView(prod);
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return PartialView();

            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }
        public ActionResult Vitrine()
        {
            try
            {
                IEnumerable<Produto> prod = _produtoBo.GetVitrine();
                if (!prod.Any())
                    return View("Error");

                return PartialView(prod);
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return PartialView();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult NavbarBusca()
        {
            return PartialView();
        }
        public ActionResult NavbarCarrinho()
        {
            return PartialView(CarrinhoAny ? Carrinho.ItensCarrinho.Count : 0);
        }
    }
}