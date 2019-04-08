
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class PedidoConfiguration: EntityTypeConfiguration<Pedido>
    {
        public PedidoConfiguration()
        {
            HasKey(c => c.PedidoId);

            Property(x => x.FormaPagamento)
                .IsRequired();
            Property(x => x.Aprovado)
                .IsRequired();
            Property(x => x.DataPedido)
                .IsRequired();
            Property(x => x.ValorFrete)
                .HasPrecision(13,2);
            Property(x => x.ValorTotal)
                .IsRequired()
                .HasPrecision(13,2);
        }
    }
}
