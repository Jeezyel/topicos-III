using A1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace A1.Data
{
    public class A1Context : IdentityDbContext<Usuario>
    {
        public A1Context(DbContextOptions<A1Context> options) : base(options)
        {
        }

        // --- DbSets para as tabelas do nosso sistema ---
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemCardapio> ItensCardapio { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<SugestaoDiaria> SugestoesDiarias { get; set; }
        public DbSet<ParceiroApp> ParceirosApp { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }
        public DbSet<ItemIngrediente> ItemIngredientes { get; set; } // DbSet para a entidade de junção


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- CONFIGURAÇÕES DE MODELO ---

            // Índice único para Sugestão do Dia
            modelBuilder.Entity<SugestaoDiaria>()
                .HasIndex(s => new { s.Data, s.Periodo })
                .IsUnique();

            // Chave primária composta para a tabela de junção ItemIngrediente
            modelBuilder.Entity<ItemIngrediente>()
                .HasKey(ii => new { ii.ItemCardapioId, ii.IngredienteId });

            // --- POVOAMENTO DE DADOS (SEED) ---

            // Seed para Perfis (Roles)
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "f4dbf4dd-1df8-4e6a-9a15-abc123456789", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "3b14a6f1-ef0b-4c89-b6b0-def987654321", Name = "Usuario", NormalizedName = "USUARIO" }
            );

            // Seed para o Usuário Administrador
            var hasher = new PasswordHasher<Usuario>();
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = "1e8e011d-7fde-4b16-8078-6775d7fca57e",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    Nome = "Administrador",
                    // PasswordHash = hasher.HashPassword(null, "Admin@123"), // Senha é "Admin@123"
                    // O hash da senha "Admin@123" foi pré-calculado e colado como um valor fixo.
                    PasswordHash = "AQAAAAIAAYagAAAAENxiM+eS0Ag9KL6O40a1TEUpV+jH0nxCFioLIPdrOJ9Y5x2Sx28OaWLn8dHwCML5nQ==",
                    SecurityStamp = "d6f5c999-46de-4a7f-9c67-123456789abc",
                    ConcurrencyStamp = "c3aef999-96de-4a7f-9c67-abcdef123456"
                }
            );

            // Seed para relacionar o usuário Admin com a Role Admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1e8e011d-7fde-4b16-8078-6775d7fca57e", RoleId = "f4dbf4dd-1df8-4e6a-9a15-abc123456789" }
            );

            // Seed para a tabela de Ingredientes
            modelBuilder.Entity<Ingrediente>().HasData(
                new Ingrediente { Id = 1, Nome = "Tomate" },
                new Ingrediente { Id = 2, Nome = "Queijo Mussarela" },
                new Ingrediente { Id = 3, Nome = "Manjericão" },
                new Ingrediente { Id = 4, Nome = "Massa de Pizza" },
                new Ingrediente { Id = 5, Nome = "Frango Desfiado" },
                new Ingrediente { Id = 6, Nome = "Catupiry" },
                new Ingrediente { Id = 7, Nome = "Pão de Hambúrguer" },
                new Ingrediente { Id = 8, Nome = "Carne de Hambúrguer" },
                new Ingrediente { Id = 9, Nome = "Alface" },
                new Ingrediente { Id = 10, Nome = "Bacon" }
            );

            // Seed para a tabela de Mesas
            modelBuilder.Entity<Mesa>().HasData(
                new Mesa { Id = 1, Numero = 1, Capacidade = 4 },
                new Mesa { Id = 2, Numero = 2, Capacidade = 4 },
                new Mesa { Id = 3, Numero = 3, Capacidade = 2 },
                new Mesa { Id = 4, Numero = 4, Capacidade = 2 },
                new Mesa { Id = 5, Numero = 5, Capacidade = 6 },
                new Mesa { Id = 6, Numero = 6, Capacidade = 8 }
            );

            // Seed para a tabela de Itens do Cardápio
            modelBuilder.Entity<ItemCardapio>().HasData(
                new ItemCardapio { Id = 1, Nome = "Feijoada Completa", Descricao = "Feijoada tradicional com arroz, couve, farofa e laranja.", PrecoBase = 55.00, Periodo = Periodo.Almoco },
                new ItemCardapio { Id = 2, Nome = "Frango a Parmegiana", Descricao = "Filé de frango empanado, coberto com queijo e molho de tomate. Acompanha arroz e fritas.", PrecoBase = 48.00, Periodo = Periodo.Almoco },
                new ItemCardapio { Id = 3, Nome = "Salada Caesar com Frango", Descricao = "Alface romana, croutons, queijo parmesão e tiras de frango grelhado.", PrecoBase = 42.00, Periodo = Periodo.Almoco },
                new ItemCardapio { Id = 4, Nome = "Pizza Margherita", Descricao = "Molho de tomate, mussarela fresca e manjericão.", PrecoBase = 60.00, Periodo = Periodo.Jantar },
                new ItemCardapio { Id = 5, Nome = "Risoto de Cogumelos", Descricao = "Arroz arbóreo cremoso com mix de cogumelos frescos.", PrecoBase = 65.00, Periodo = Periodo.Jantar },
                new ItemCardapio { Id = 6, Nome = "Hambúrguer Gourmet", Descricao = "Pão brioche, hambúrguer de 180g, queijo cheddar, bacon e salada.", PrecoBase = 50.00, Periodo = Periodo.Jantar }
            );

            // Seed para a tabela de JUNÇÃO ItemIngrediente
            modelBuilder.Entity<ItemIngrediente>().HasData(
                new ItemIngrediente { ItemCardapioId = 4, IngredienteId = 1 }, // Pizza com Tomate
                new ItemIngrediente { ItemCardapioId = 4, IngredienteId = 2 }, // Pizza com Queijo
                new ItemIngrediente { ItemCardapioId = 4, IngredienteId = 3 }, // Pizza com Manjericão
                new ItemIngrediente { ItemCardapioId = 4, IngredienteId = 4 }, // Pizza com Massa
                new ItemIngrediente { ItemCardapioId = 6, IngredienteId = 7 }, // Hambúrguer com Pão
                new ItemIngrediente { ItemCardapioId = 6, IngredienteId = 8 }, // Hambúrguer com Carne
                new ItemIngrediente { ItemCardapioId = 6, IngredienteId = 9 }, // Hambúrguer com Alface
                new ItemIngrediente { ItemCardapioId = 6, IngredienteId = 10 } // Hambúrguer com Bacon
            );
        }
    }
}