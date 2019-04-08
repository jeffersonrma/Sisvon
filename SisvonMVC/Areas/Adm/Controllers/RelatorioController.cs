using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;
using SisvonMVC.Areas.Adm.ViewModels;

namespace SisvonMVC.Areas.Adm.Controllers
{
    public class RelatorioController : ControllerBase
    {
        private readonly IClienteBo _clienteBo;
        private readonly IPedidoBo _pedidoBo;
        private readonly IItemPedidoBo _itemPedidoBo;

        public RelatorioController()
        {
            _clienteBo = new ClienteBo(_context);
            _pedidoBo = new PedidoBo(_context);
            _itemPedidoBo = new ItemPedidoBo(_context);
        }

        public ActionResult RelatorioPedidos()
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

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
        [HttpPost]
        public ActionResult ImprimirRelatorioPedidos(DateTime? dataInicial, DateTime? dataFinal)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");
                if (dataInicial == null || dataFinal == null || dataFinal < dataInicial)
                {
                    ErroMessage = "Data inválida!";
                    return RedirectToAction("RelatorioPedidos");
                }
                ICollection<Pedido> listaPedidos = _pedidoBo.BuscarPorData((DateTime)dataInicial, (DateTime)dataFinal);

                if (listaPedidos == null || listaPedidos.Count < 1)
                {
                    ErroMessage = "Nem um Pedido encontrado durante o período Selecionado";
                    return RedirectToAction("RelatorioPedidos");
                }

                return View(new RelatorioPedidoVm(listaPedidos)
                {
                    DataInicial = (DateTime)dataInicial,
                    Datafinal = (DateTime)dataFinal,
                });

            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return RedirectToAction("RelatorioPedidos");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        public ActionResult RelatorioProdutos()
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");

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

        [HttpPost]
        public ActionResult ImprimirRelatorioProdutos(DateTime? dataInicial, DateTime? dataFinal)
        {
            try
            {
                if (!UsuarioEstaLogado || !UsuarioAdm) return RedirectToAction("Index", "Login");
                if (dataInicial == null || dataFinal == null || dataFinal < dataInicial)
                {
                    ErroMessage = "Data inválida!";
                    return RedirectToAction("RelatorioPedidos");
                }
                ICollection<ItemPedido> listaItensPedidos = _itemPedidoBo.BuscarPorData((DateTime) dataInicial,
                    (DateTime) dataFinal);
                if (listaItensPedidos == null || listaItensPedidos.Count < 1)
                {
                    ErroMessage = "Nem um Produto Vendido durante o período Selecionado";
                    return RedirectToAction("RelatorioPedidos");
                }

                return View(new RelatorioProdutosVm(listaItensPedidos)
                {
                    DataInicial = (DateTime) dataInicial,
                    Datafinal = (DateTime) dataFinal
                });

            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return RedirectToAction("RelatorioProdutos");

            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }
    }
}