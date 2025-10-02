using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using A1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<AtendimentoPresencial> AtendimentosPresenciais { get; set; }
        public DbSet<AtendimentoDeliveryProprio> AtendimentosDeliveryProprio { get; set; }
        public DbSet<AtendimentoDeliveryAplicativo> AtendimentosDeliveryAplicativo { get; set; }

        public DbSet<PedidoItem> PedidoItens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SugestaoDiaria>()
                .HasIndex(s => new { s.Data, s.Periodo })
                .IsUnique();
        }
    }
}
