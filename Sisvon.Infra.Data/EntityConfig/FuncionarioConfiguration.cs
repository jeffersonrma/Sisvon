
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class FuncionarioConfiguration: EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioConfiguration()
        {
            HasKey(c => c.FuncionarioId);

            Property(x => x.Administrador)
                .IsRequired();
            Property(x => x.Login)
                .IsRequired();
            Property(x => x.Nome)
                .IsRequired();
        }
    }
}
