using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(SisvonContext db)
            : base(db)
        {
        }

        public bool PossuiItem(Pedido obj)
        {
            return new ItemPedidoRepository(Db).BuscarPorPedido(obj).Count != 0;
        }

        public ICollection<Pedido> GetByCliente(Cliente obj)
        {
            return Db.Pedidos
                .Where(p => p.Cliente.ClienteId == obj.ClienteId).ToList();
        }

        public IQueryable<Pedido> GetNaoAprovado()
        {
            return Db.Pedidos.Where(p => !p.Aprovado);
        }

        public IQueryable<Pedido> GetAprovado()
        {
            return Db.Pedidos.Where(p => p.Aprovado);
        }


        public ICollection<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal)
        {
            return Db.Pedidos.Where(p => DbFunctions.TruncateTime(p.DataPedido) >= dataInicial && DbFunctions.TruncateTime(p.DataPedido) <= dataFinal).ToList();
        }
    }
}
