using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class ItemPedidoRepository : RepositoryBase<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(SisvonContext db)
            : base(db)
        {
        }

        public ICollection<ItemPedido> BuscarPorPedido(Pedido obj)
        {
            return Db.ItensPedido
                .Where(i => i.Pedido.PedidoId == obj.PedidoId).ToList();
        }

        public ICollection<ItemPedido> BuscarPorProduto(Produto obj)
        {
            return Db.ItensPedido
                .Where(i => i.Produto.ProdutoId == obj.ProdutoId).ToList();
        }

        public ICollection<ItemPedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal)
        {
            return
                Db.ItensPedido
                    .Where(p => DbFunctions
                        .TruncateTime(p.Pedido.DataPedido) >= dataInicial &&
                                    DbFunctions.TruncateTime(p.Pedido.DataPedido) <= dataFinal)
                        .ToList();

        }
    }
}
