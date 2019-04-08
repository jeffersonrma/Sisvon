
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class ItemPedidoConfiguration: EntityTypeConfiguration<ItemPedido>
    {
        public ItemPedidoConfiguration()
        {
            HasKey(c => c.ItemPedidoId);

            Property(x => x.PrecoTotal)
                .IsRequired()
                .HasPrecision(13,2);
            Property(x => x.Qtde)
                .IsRequired();
        }
    }
}
