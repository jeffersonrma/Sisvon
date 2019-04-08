using Sisvon.Model.Entities;

namespace SisvonMVC.ViewModels
{
    public class ItemCarrinhoVM
    {
        public int CarrinhoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int Qtde { get; set; }
        public decimal Total
        {
            get { return Produto.Preco*Qtde; }
        }

    }
}
