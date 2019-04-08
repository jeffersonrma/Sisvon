using System;
using System.Collections.Generic;
using Sisvon.BO.Interfaces;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Entities;
using Sisvon.Model.Interfaces.Repositories;

namespace Sisvon.BO
{
    public class ItemPedidoBo: BoBase<ItemPedido>, IItemPedidoBo
    {
        private readonly IItemPedidoRepository _itemPedidoRepository;
        public ItemPedidoBo(SisvonContext context) 
            : base(context)
        {
            _itemPedidoRepository = new ItemPedidoRepository(context);
        }


        public ICollection<ItemPedido> BuscarPorData(DateTime dataInicial, DateTime dataFinal)
        {
            return _itemPedidoRepository.BuscarPorData(dataInicial, dataFinal);
        }
    }
}
