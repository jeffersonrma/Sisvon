using System.Web.Mvc;
using Sisvon.BO;
using Sisvon.BO.Interfaces;
using Sisvon.Model.Entities.Enum;
using SisvonMVC.ViewModels;

namespace SisvonMVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoBo _produtoBo;
        public CarrinhoController()
        {
            _produtoBo = new ProdutoBo(_context);
        }
        // GET: Carrinho
        public ActionResult Index()
        {
            return View(Carrinho);
        }
        [HttpPost]
        public ActionResult Adicionar(int ProdutoId)
        {
            if(Carrinho == null) Carrinho = new CarrinhoVM();

            Carrinho.AddItemCarrinho = new ItemCarrinhoVM
            {
                Produto = _produtoBo.GetById(ProdutoId),
                Qtde = 1
            };

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Alterar(int txtItemCarrinhoId, int txtQtde, string btnSubmit)
        {
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            if (btnSubmit == "alterar")
                Carrinho.AlterarItemCarrinho(txtItemCarrinhoId,txtQtde);
            if (btnSubmit == "remover")
                Carrinho.RemoverItemCarrinho(txtItemCarrinhoId);
            
            
            return RedirectToAction("Index");
        }

        public ActionResult CalcularFrete(Frete FreteSelecionado)
        {
            if (!CarrinhoAny)
            {
                ErroMessage = "Carrinho Vazio";
                return RedirectToAction("Index", "Home");
            }
            Carrinho.FreteSelecionado = FreteSelecionado;
            return RedirectToAction("Index");
        }
    }
}