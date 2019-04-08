using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class ClienteController : ControllerBase
    {
        private readonly IClienteBo _clienteBo;

        public ClienteController()
        {
            _clienteBo = new ClienteBo(_context);
        }
        public ActionResult Index(int? page, string busca)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

                ViewBag.busca = busca;

                IQueryable<Cliente> lista = _clienteBo.GetAll();
                if (!string.IsNullOrEmpty(busca))
                    lista = lista.Where(x => x.Nome.Contains(busca) || x.Cpf.Contains(busca));

                IPagedList<Cliente> model = lista.OrderBy(x => x.Nome).ToPagedList(page ?? 1, 10);
                return View(model);

            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }

        public ActionResult ConsultarCliente(int? id)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                if (id == null)
                {
                    ErroMessage = "Endereço inválido!";
                    return RedirectToAction("Index");
                }

                return View(_clienteBo.GetById((int)id));

            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return View();
            }
            catch (Exception exception)
            {
                return View("Error");
            }
        }
    }
}