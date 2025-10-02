using A1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Data
{
    public class A1Context : IdentityDbContext<Usuario>/**/
    {
        public A1Context (DbContextOptions<A1Context> options)
            : base(options)
        {
        }

        // Tabelas principais
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemCardapio> ItensCardapio { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<SugestaoDiaria> SugestoesDiarias { get; set; }
        public DbSet<ParceiroApp> ParceirosApp { get; set; }

        // Tabelas de Atendimento
        public DbSet<ItemCardapio> ItemCardapio { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<AtendimentoPresencial> AtendimentosPresenciais { get; set; }
        public DbSet<AtendimentoDeliveryProprio> AtendimentosDeliveryProprio { get; set; }
        public DbSet<AtendimentoDeliveryAplicativo> AtendimentosDeliveryAplicativo { get; set; }

        public DbSet<PedidoItem> PedidoItens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índice único que você já tinha
            modelBuilder.Entity<SugestaoDiaria>()
                .HasIndex(s => new { s.Data, s.Periodo })
                .IsUnique();

            // Criação de roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "f4dbf4dd-1df8-4e6a-9a15-abc123456789", // GUID fixo
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "3b14a6f1-ef0b-4c89-b6b0-def987654321",
                    Name = "Usuario",
                    NormalizedName = "USUARIO"
                }
            );

            // Usuário administrador
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = "1e8e011d-7fde-4b16-8078-6775d7fca57e",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    Nome = "Administrador", // seu campo extra
                    PasswordHash = "AQAAAAIAAYagAAAAENxiM+eS0Ag9KL6O40a1TEUpV+jH0nxCFioLIPdrOJ9Y5x2Sx28OaWLn8dHwCML5nQ==",
                    SecurityStamp = "d6f5c999-46de-4a7f-9c67-123456789abc", // <<< valor fixo
                    ConcurrencyStamp = "c3aef999-96de-4a7f-9c67-abcdef123456" // <<< valor fixo
                }
            );

            // Relaciona o usuário Admin com a Role Admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1e8e011d-7fde-4b16-8078-6775d7fca57e",
                    RoleId = "f4dbf4dd-1df8-4e6a-9a15-abc123456789"
                }
            );
        }
    }
}
