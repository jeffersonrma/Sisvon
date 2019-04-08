
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Sisvon.Model.Entities;
using Sisvon.Infra.Data.EntityConfig;

namespace Sisvon.Infra.Data.Context
{
    public class SisvonContext : DbContext
    {
        public SisvonContext()
            : base("SisvonMSSQL")
        {
            Database.SetInitializer<SisvonContext>(null);
            //Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<SubClasse> SubClasses { get; set; }
        public DbSet<Sincronizar> Sincronizar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("Varchar"));
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(255));
            modelBuilder.Properties<DateTime>()
                .Configure(p=>p.HasColumnType("datetime2"));

            
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new FuncionarioConfiguration());
            modelBuilder.Configurations.Add(new GrupoConfiguration());
            modelBuilder.Configurations.Add(new ItemPedidoConfiguration());
            modelBuilder.Configurations.Add(new PedidoConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new SubClasseConfiguration());
            modelBuilder.Configurations.Add(new SincronizarConfiguration());

        }
    }
}
