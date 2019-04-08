using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(SisvonContext db)
            : base(db)
        {
        }
        public IEnumerable<string> ListarCodigos()
        {
            return Db.Produtos.Select(p => p.Codigo).ToList();
        }
        
        public Produto BuscaProdutoPeloCodigo(string codigo)
        {
            return Db.Produtos.FirstOrDefault(produto => produto.Codigo == codigo);
        }

        public Produto BuscarPorOrdem(int ordem)
        {
            return Db.Produtos.FirstOrDefault(produto => produto.Ordem == ordem);
        }

        public bool PossuiItem(Produto obj)
        {
            return new ItemPedidoRepository(Db).BuscarPorProduto(obj).Any();
        }

        public IEnumerable<Produto> BuscarVitrine()
        {
            return Db.Produtos
                .Where(p => p.Vitrine && p.Ativo)
                .Take(20);
        }

        public IEnumerable<Produto> BuscarDestaque()
        {
            return Db.Produtos
                .Where(p => p.Destaque && p.Ativo)
                .Take(10);
        }

        public void ReservarPorPedido(Pedido pedido)
        {
            foreach (var item in pedido.ItemsPedido)
            {
                item.Produto.Reserva = item.Produto.Reserva + item.Qtde;
                Update(item.Produto);
            }
        }

        public IEnumerable<string> ListarOrdens()
        {
            return Db.Produtos.Select(p => p.Ordem.ToString()).ToList();            
        }
    }
}
