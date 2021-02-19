using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursoEFCore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(p =>
            {
                p.ToTable("Clientes");
                p.HasKey(c => c.Id);
                p.Property(c => c.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(c => c.Telefone).HasColumnType("CHAR(11)").IsRequired();
                p.Property(c => c.CEP).HasColumnType("CHAR(8)").IsRequired();
                p.Property(c => c.Estado).HasColumnType("CHAR(2)").IsRequired();
                p.Property(c => c.Cidade).HasMaxLength(60).IsRequired();

                p.HasIndex(c => c.Telefone).HasName("idx_cliente_telefone");
            });

            modelBuilder.Entity<Produto>(p =>
            {
                p.ToTable("Produtos");
                p.HasKey(i => i.Id);
                p.Property(i => i.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                p.Property(i => i.Descricao).HasColumnType("VARCHAR(60)");
                p.Property(i => i.Valor).IsRequired();
                p.Property(i => i.TipoProduto).HasConversion<string>();
            });

            modelBuilder.Entity<Pedido>(p =>
            {
                p.ToTable("Pedidos");
                p.HasKey(i => i.Id);
                p.Property(i => i.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                p.Property(i => i.StatusPedido).HasConversion<string>(); ;
                p.Property(i => i.TipoFrete).HasConversion<int>(); ;
                p.Property(i => i.Observacao).HasColumnType("VARCHAR(512)");

                p.HasMany(i => i.Itens)
                     .WithOne(i => i.Pedido)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PedidoItem>(p =>
            {
                p.ToTable("PedidoItens");
                p.HasKey(i => i.Id);
                p.Property(i => i.Quantidade).HasDefaultValue(1).IsRequired();
                p.Property(i => i.Valor).IsRequired();
                p.Property(i => i.Desconto).IsRequired();
            });
        }
    }
}
