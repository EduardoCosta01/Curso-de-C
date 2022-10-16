using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context
{
    public class AppDpContext : DbContext
    {
        public AppDpContext(DbContextOptions<AppDpContext> options) : base(options) 
        { }

        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder mb) 
        {
            //Tabela Categoria
            mb.Entity<Categoria>().HasKey(c => c.CategoriaId);

            mb.Entity<Categoria>().Property(c => c.Nome)
                                   .HasMaxLength(100)
                                    .IsRequired();

            mb.Entity<Categoria>().Property(c => c.Descricao)
                                   .HasMaxLength(150)
                                    .IsRequired();


            //Tabela Produto
            mb.Entity<Produto>().HasKey(c => c.ProdutoId);

            mb.Entity<Produto>().Property(c => c.Nome)
                                 .HasMaxLength(100)
                                  .IsRequired();

            mb.Entity<Produto>().Property(c => c.Descricao)
                                 .HasMaxLength(150)
                                  .IsRequired();

            mb.Entity<Produto>().Property(c => c.Imagem)
                                 .HasMaxLength(150);

            mb.Entity<Produto>().Property(c => c.Preco)
                                 .HasPrecision(6, 2);

            //Relacionamento entre tabelas
            mb.Entity<Produto>()
                        .HasOne<Categoria>(c => c.Categoria)
                         .WithMany(p => p.Produtos)
                          .HasForeignKey(c => c.CategoriaId);
        }
    }
}
