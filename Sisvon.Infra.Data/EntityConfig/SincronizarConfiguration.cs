
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class SincronizarConfiguration: EntityTypeConfiguration<Sincronizar>
    {
        public SincronizarConfiguration()
        {
            HasKey(x => x.SincronizarId);
            Property(x => x.Ordem).
                IsRequired();

            Property(x => x.SessionId)
                .HasMaxLength(32);
            Property(x => x.Preco)
               .IsRequired()
               .HasPrecision(13, 2);
        }
    }
}
