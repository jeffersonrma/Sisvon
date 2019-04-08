using System;
using System.Collections.Generic;
using Sisvon.Model.Entities.Enum;

namespace Sisvon.Model.Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public bool Aprovado { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorTotal { get; set; }
        public virtual Frete TipoFrete { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual Status Status { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ItemPedido> ItemsPedido { get; set; }
    }
}
