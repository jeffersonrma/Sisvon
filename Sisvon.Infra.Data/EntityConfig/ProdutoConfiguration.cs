
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class ProdutoConfiguration: EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            HasKey(c => c.ProdutoId);
            Property(x => x.Ordem)
                .IsRequired();
            Property(x => x.ValorPac)
                .IsRequired()
                .HasPrecision(13,2);
            Property(x => x.ValorSedex)
                .IsRequired()
                .HasPrecision(13,2);
            Property(x => x.Codigo)
                .IsRequired();
            Property(x => x.Nome)
                .IsRequired();
            Property(x => x.Preco)
                .IsRequired()
                .HasPrecision(13,2);
            Property(x => x.Descricao)
                .IsMaxLength();
        }
    }
}
