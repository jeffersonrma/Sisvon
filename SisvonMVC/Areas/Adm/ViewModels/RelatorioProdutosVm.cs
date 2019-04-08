using System;
using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;

namespace SisvonMVC.Areas.Adm.ViewModels
{
    public class RelatorioProdutosVm
    {
        private RelatorioProdutosVm() { }

        public RelatorioProdutosVm(ICollection<ItemPedido> listaItemPedidos)
        {
            //ListaRelatorioProdutosVms = listaItemPedidos
            //    .GroupBy(p => p.Produto.ProdutoId
            //    , p => new RelatorioProdutosVm()
            //    {
            //        NomeProduto = p.Produto.Nome,
            //        QtdeProduto = p.Qtde,
            //        ValorProduto = p.PrecoTotal
            //    })
            //    .Select(p => p.First())
            //    .OrderByDescending(p => p.QtdeProduto)
            //    .ToList();

            ListaRelatorioProdutosVms = listaItemPedidos
                .GroupBy(p => p.Produto.ProdutoId)
                .Select(p => new RelatorioProdutosVm()
                {
                    NomeProduto = p.First().Produto.Nome,
                    QtdeProduto = p.Sum(x => x.Qtde),
                    ValorProduto = p.Sum(x => x.PrecoTotal)})
                .OrderByDescending(p => p.QtdeProduto)
                .ToList();
        }

        public ICollection<RelatorioProdutosVm> ListaRelatorioProdutosVms { get; set; }

        public DateTime DataInicial { get; set; }
        public DateTime Datafinal { get; set; }
        public int QtdeTotal
        {
            get { return ListaRelatorioProdutosVms.Sum(p => p.QtdeProduto); }
        }
        public decimal ValorTotal
        {
            get { return ListaRelatorioProdutosVms.Sum(p => p.ValorProduto); }
        }

        public string NomeProduto { get; set; }
        public int QtdeProduto { get; set; }
        public decimal ValorProduto { get; set; }

    }
}