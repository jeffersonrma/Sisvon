using System;
using System.Linq;
using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.BO.Validation;
using Sisvon.Model.Entities;
using Sisvon.Model.Entities.Enum;
using SisvonMVC.ViewModels;

namespace SisvonMVC.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoBo _pedidoBo;
        private readonly IClienteBo _clienteBo;
        private readonly IProdutoBo _produtoBo;

        public PedidoController()
        {
            _pedidoBo = new PedidoBo(_context);
            _clienteBo = new ClienteBo(_context);
            _produtoBo = new ProdutoBo(_context);

        }

        public ActionResult Login()
        {
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            if (!Carrinho.PossuiEstoque())
            {
                ErroMessage = "Alguns produtos do carrinho não possuem estoque suficiente";
                return RedirectToAction("Index", "Carrinho");
            }
            if (UsuarioEstaLogado) return RedirectToAction("Frete");

            return View();
        }

        public ActionResult Frete()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Login");
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                return View(Carrinho);
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult Frete(CarrinhoVM carrinhoVm, string btnSubmit)
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Login");
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            if (carrinhoVm.FreteSelecionado == 0)
            {
                ErroMessage = "Nem uma forma de frete selecionado";
                return View(Carrinho);
            }
            try
            {
                Carrinho.FreteSelecionado = carrinhoVm.FreteSelecionado;
                Carrinho.FormaPagamento = carrinhoVm.FormaPagamento;
                return btnSubmit == "selecionar" ? RedirectToAction("Frete") : RedirectToAction("Finalizar");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

       

        public ActionResult Finalizar()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Login");
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            try
            {

                return View(Carrinho);
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult GerarPedido()
        {
            if (!UsuarioEstaLogado) return RedirectToAction("Login");
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var carrinho = Carrinho;
                var pedido = new Pedido()
                {
                    TipoFrete = carrinho.FreteSelecionado,
                    FormaPagamento = carrinho.FormaPagamento,
                    ValorFrete = carrinho.ValorFrete,
                    Cliente = _clienteBo.GetById(Usuario.ClienteId),
                    DataPedido = DateTime.Now,
                    Status = Status.AguardandoPagamento,
                    ValorTotal = carrinho.Total,
                    DataEnvio = DateTime.MinValue,
                    DataPagamento = DateTime.MinValue
                };

                pedido.ItemsPedido = carrinho.ItensCarrinho.Select(p =>
                    new ItemPedido(pedido)
                    {
                        Produto = _produtoBo.GetById(p.Produto.ProdutoId),
                        Qtde = p.Qtde,
                        PrecoTotal = p.Total
                    }).ToArray();

                _pedidoBo.Add(pedido);
                TempData["pedidoId"] = pedido.PedidoId;
                Carrinho = null;
                return RedirectToAction("Pagamento");
            }
            catch (BoException boException)
            {
                ErroMessage = boException.Message;
                return RedirectToAction("Finalizar");
            }
            catch (Exception exception)
            {
                LogMessage = exception;
                return View("Error");
            }

        }

        public ActionResult Pagamento()
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Voce precisa entrar";
                return RedirectToAction("Index", "Home");
            }
            if (TempData["pedidoId"] == null)
            {
                ErroMessage = "Erro ao carregar, verifique seus pedidos";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                return View(_pedidoBo.GetById((int)TempData["pedidoId"]));
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

        public ActionResult Deposito(decimal valor)
        {
            if (!UsuarioEstaLogado) return View("Error");

            return PartialView(valor);
        }

        public ActionResult Boleto(int? pedidoId)
        {
            if (!UsuarioEstaLogado)
            {
                ErroMessage = "Voce precisa entrar";
                return RedirectToAction("Index", "Home");
            }
            if (pedidoId == null)
                return View("Error");
            Pedido pedido = _pedidoBo.GetOneByCliente(Usuario, (int) pedidoId);
            var val = Convert.ToString(pedido.ValorTotal + pedido.ValorFrete).Replace(",","");
            val = val.Replace(",", "");
            ViewBag.valCodBar = val;

            return View(pedido);

        }
    }
}