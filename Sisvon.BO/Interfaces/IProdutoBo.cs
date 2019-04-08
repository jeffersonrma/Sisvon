
using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface IProdutoBo : IBoBase<Produto>
    {
        IEnumerable<string> ListarOrdens();

        void GravarProdutosImportados(ICollection<Sincronizar> list);
        void SincronizarProdutoImportado(Sincronizar sincronizar);
        IQueryable<Produto> BuscarPorNome(string term);
        Produto BuscarPorOrdem(int ordem);
        IQueryable<Produto> GetBySubClasse(SubClasse subclasse);
        IQueryable<Produto> GetByGrupo(Grupo grupo);
        IEnumerable<Produto> GetVitrine();
        IEnumerable<Produto> GetDestaque();
        void LiberarReservaPorPedido(Pedido pedido);
        void ReservarPorPedido(Pedido pedido);
        void AtualizarProdutosImportados();
        bool SincronizarPorPedido(Pedido pedido);

    }
}
