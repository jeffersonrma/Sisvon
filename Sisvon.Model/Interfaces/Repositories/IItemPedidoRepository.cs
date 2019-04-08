
using System;
using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IItemPedidoRepository : IRepositoryBase<ItemPedido>
    {
        ICollection<ItemPedido> BuscarPorPedido(Pedido obj);
        ICollection<ItemPedido> BuscarPorProduto(Produto obj);
        ICollection<ItemPedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal);
    }
}
