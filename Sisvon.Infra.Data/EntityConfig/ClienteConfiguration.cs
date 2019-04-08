
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class ClienteConfiguration: EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            HasKey(c => c.ClienteId);

            Property(x => x.Cidade)
                .IsRequired();
            Property(x => x.Cpf)
                .IsRequired();
            Property(x => x.Email)
                .IsRequired();
            Property(x => x.Numero)
                .IsRequired();
            Property(x => x.Rg)
                .IsRequired();
            Property(x => x.Rua)
                .IsRequired();
            Property(x => x.Senha)
                .HasMaxLength(60)
                .IsRequired();
            Property(x => x.Telefone)
                .IsRequired();
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
