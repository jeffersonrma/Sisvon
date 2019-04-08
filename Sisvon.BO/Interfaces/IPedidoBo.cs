using System;
using System.Collections.Generic;
using System.Linq;
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface IPedidoBo: IBoBase<Pedido>
    {

        ICollection<Pedido> BuscarPorCliente(Cliente cliente);
        Pedido GetOneByCliente(Cliente cliente, int pedidoId);
        IQueryable<Pedido> GetNaoAprovado();
        IQueryable<Pedido> GetAprovado();
        void Aprovar(Pedido pedido);
        Pedido GetPedidoPendente(int id);
        ICollection<Pedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal);
    }
}
