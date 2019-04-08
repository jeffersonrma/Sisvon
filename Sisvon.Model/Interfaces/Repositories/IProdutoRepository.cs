
using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        IEnumerable<string> ListarCodigos();
        Produto BuscaProdutoPeloCodigo(string codigo);
        Produto BuscarPorOrdem(int ordem);
        bool PossuiItem(Produto produto);
        IEnumerable<Produto> BuscarVitrine();
        IEnumerable<Produto> BuscarDestaque();
        void ReservarPorPedido(Pedido pedido);

        IEnumerable<string> ListarOrdens();
    }
}
