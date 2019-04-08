
using System;
using System.Collections.Generic;
using Sisvon.Model.Entities;

namespace Sisvon.BO.Interfaces
{
    public interface IItemPedidoBo:IBoBase<ItemPedido>
    {
        ICollection<ItemPedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal);
    }
}
