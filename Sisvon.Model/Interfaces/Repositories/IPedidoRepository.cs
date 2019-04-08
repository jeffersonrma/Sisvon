
using System;
using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        bool PossuiItem(Pedido obj);
        ICollection<Pedido> GetByCliente(Cliente obj);
        IQueryable<Pedido> GetNaoAprovado();
        IQueryable<Pedido> GetAprovado();
        ICollection<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal);
    }
}
