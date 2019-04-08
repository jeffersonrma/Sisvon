
namespace Sisvon.Model.Entities
{
    public class ItemPedido
    {

        public ItemPedido(){}
        public ItemPedido(Pedido pedido)
        {
            Pedido = pedido;
        }


        public int ItemPedidoId { get; set; }
        public decimal PrecoTotal { get; set; }
        public int Qtde { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
