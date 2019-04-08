
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;
using Sisvon.Model.Entities.Enum;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoBo _pedidoBo;
        public PedidoController()
        {
            _pedidoBo = new PedidoBo(_context);
        }
        // GET: Adm/Pedido
        public ActionResult Index(int? page, string busca)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                ViewBag.busca = busca;

                IQueryable<Pedido> lista = _pedidoBo.GetAprovado();
                if (!string.IsNullOrEmpty(busca))
                    lista = lista.Where(x => x.Cliente.Nome.Contains(busca) || x.PedidoId.ToString().Contains(busca));

                IPagedList<Pedido> model = lista.OrderByDescending(x => x.DataPedido).ToPagedList(page ?? 1, 10);
                return View(model);
            }
            catch (BoException exception)
            {
                ErroMessage = exception.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }
        
        public ActionResult Detalhes(int? id)
        {
            if (id == null) return View("Error");
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                return View(_pedidoBo.GetById((int)id));
            }
            catch (BoException exception)
            {
                ErroMessage = exception.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }
        
        [HttpPost]
        public ActionResult Editar(Pedido pedido)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                var ped = _pedidoBo.GetById(pedido.PedidoId);
                switch (pedido.Status)
                {
                    case Status.PagamentoConfirmado:
                        ped.DataPagamento = DateTime.Now;
                        break;
                    case Status.ProdutoPostado:
                        ped.DataEnvio = DateTime.Now;
                        break;
                }
                ped.Status = pedido.Status;
                _pedidoBo.Update(ped);
                return RedirectToAction("Detalhes", new { id = pedido.PedidoId });
            }
            catch (BoException exception)
            {
                ErroMessage = exception.Message;
                return RedirectToAction("Detalhes", new { id = pedido.PedidoId });
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult PedidosPendentes(int? page, string busca)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                ViewBag.busca = busca;

                IQueryable<Pedido> lista = _pedidoBo.GetNaoAprovado();
                if (!string.IsNullOrEmpty(busca))
                    lista = lista.Where(x => x.Cliente.Nome.Contains(busca) || x.PedidoId.ToString().Contains(busca));

                IPagedList<Pedido> model = lista.OrderByDescending(x => x.DataPedido).ToPagedList(page ?? 1, 10);
                return View(model);
            }
            catch (BoException exception)
            {
                ErroMessage = exception.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult AprovarPedido(int? id)
        {
            if (id == null) return View("Error");
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");
                
                return View(_pedidoBo.GetPedidoPendente((int) id));
            }
            catch (BoException exception)
            {
                ErroMessage = exception.Message;
                return View();
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult AprovarPedido(Pedido pedido)
        {
            try
            {
                if (!UsuarioEstaLogado) return RedirectToAction("Index", "Login");

                _pedidoBo.Aprovar(_pedidoBo.GetById(pedido.PedidoId));
                return RedirectToAction("PedidosPendentes");
            }
            catch (BoException exception)
            {
                ErroMessage = exception.Message;
                return RedirectToAction("PedidosPendentes");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

    }
}
