using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sisvon.Model.Entities.Enum;

namespace SisvonMVC.ViewModels
{
    public class CarrinhoVM
    {
        public CarrinhoVM()
        {
            ItensCarrinho = new List<ItemCarrinhoVM>();
        }
        private int UltimoItemId
        {
            get
            {
                var item = ItensCarrinho.LastOrDefault(vm => vm != null);

                return item == null ? 0 : item.CarrinhoId;
            }
        }
        private int GetByProdutoId(int produtoId)
        {
            var item = ItensCarrinho.FirstOrDefault(p => p.Produto.ProdutoId == produtoId);

            return item == null ? 0 : item.CarrinhoId;
        }

        public ICollection<ItemCarrinhoVM> ItensCarrinho { get; private set; }
        public ItemCarrinhoVM AddItemCarrinho
        {
            set
            {
                var item = value;
                var id = GetByProdutoId(item.Produto.ProdutoId);
                if (id != 0)
                {
                    ItensCarrinho.First(i => i.CarrinhoId == id).Qtde++;
                }
                else
                {
                    item.CarrinhoId = UltimoItemId + 1;
                    ItensCarrinho.Add(item);
                }

            }
        }
        public void RemoverItemCarrinho(int txtItemCarrinhoId)
        {
            if (txtItemCarrinhoId != 0)
                ItensCarrinho.Remove(
                    ItensCarrinho.First(i => i.CarrinhoId == txtItemCarrinhoId)
                );
        }

        public void AlterarItemCarrinho(int txtItemCarrinhoId, int txtQtde)
        {
            if (txtQtde > 0 && txtItemCarrinhoId != 0)
                ItensCarrinho.First(i => i.CarrinhoId == txtItemCarrinhoId).Qtde = txtQtde;
        }

        [Required]
        public Frete FreteSelecionado { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal ValorFrete 
        {
            get
            {
                if (FreteSelecionado == 0 || ItensCarrinho == null || !ItensCarrinho.Any()) return 0;
                
                switch (FreteSelecionado)
                {
                    case Frete.Pac:
                        return ItensCarrinho
                            .OrderByDescending(i => i.Produto.ValorPac).First().Produto.ValorPac;
                        
                    case Frete.Sedex:
                        return ItensCarrinho
                            .OrderByDescending(i => i.Produto.ValorSedex).First().Produto.ValorSedex;

                }
                return -1;
            }
        }
        [Required]
        public FormaPagamento FormaPagamento { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalProdutos
        {
            get
            {
                if (ItensCarrinho == null || !ItensCarrinho.Any()) return 0;

                return ItensCarrinho.Sum(item => item.Total);
            }
        }
        [DataType(DataType.Currency)]
        public decimal Total
        {
            get
            {
                return TotalProdutos + ValorFrete;
            }
        }

        public bool PossuiEstoque()
        {
            return !ItensCarrinho.Any(x => x.Produto.EstoqueDisponivel < x.Qtde);
        }
    }
}