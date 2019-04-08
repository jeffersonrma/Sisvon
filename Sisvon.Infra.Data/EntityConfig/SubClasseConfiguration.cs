
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class SubClasseConfiguration: EntityTypeConfiguration<SubClasse>
    {
        public SubClasseConfiguration()
        {
            HasKey(c => c.SubClasseId);
            Property(x => x.Nome)
                .IsRequired();
        }
    }
}
