using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace A1.Pages.Pedidos
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly A1Context _context;

        public CheckoutModel(A1Context context)
        {
            _context = context;
        }

        public ItemCardapio ItemDoPedido { get; set; } = default!;

        // --- NOVAS PROPRIEDADES PARA A LÓGICA DINÂMICA ---
        public decimal PrecoBase { get; set; }
        public decimal Desconto { get; set; }
        public decimal PrecoComDesconto => PrecoBase - Desconto;
        public decimal TaxaDeliveryProprio { get; set; }
        public decimal TaxaDeliveryApp { get; set; }

        [BindProperty]
        public string TipoAtendimento { get; set; } = default!;

        [TempData]
        public string MensagemSucesso { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _context.ItensCardapio.FindAsync(id);
            if (item == null)
            {
                return RedirectToPage("/Menu");
            }
            ItemDoPedido = item;

            // --- LÓGICA DE CÁLCULO MOVIDA PARA O ONGET ---
            PrecoBase = (decimal)item.PrecoBase;

            // Verifica se é sugestão do chefe
            var hoje = DateOnly.FromDateTime(DateTime.Now);
            bool ehSugestao = await _context.SugestoesDiarias.AnyAsync(s =>
                s.Data == hoje &&
                s.Periodo == item.Periodo &&
                s.ItemCardapioId == item.Id);

            if (ehSugestao)
            {
                Desconto = PrecoBase * 0.20M;
            }

            // Calcula as taxas
            TaxaDeliveryProprio = 5.00M;
            TaxaDeliveryApp = (PrecoComDesconto * 0.10M) + 2.00M;

            return Page();
        }

        // O OnPostAsync continua o mesmo da última vez
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var itemCardapio = await _context.ItensCardapio.FindAsync(id);
            if (itemCardapio == null) { return RedirectToPage("/Menu"); }

            decimal precoDosItens = (decimal)itemCardapio.PrecoBase;
            decimal taxaDeEntrega = 0M;
            decimal desconto = 0M;

            var hoje = DateOnly.FromDateTime(DateTime.Now);
            bool ehSugestao = await _context.SugestoesDiarias.AnyAsync(s => s.Data == hoje && s.Periodo == itemCardapio.Periodo && s.ItemCardapioId == itemCardapio.Id);

            if (ehSugestao) { desconto = precoDosItens * 0.20M; }

            Atendimento atendimento;
            switch (TipoAtendimento)
            {
                case "DeliveryProprio":
                    taxaDeEntrega = 5.00M;
                    atendimento = new AtendimentoDeliveryProprio { DataHoraInicio = DateTime.Now, TaxaDeEntregaFixa = taxaDeEntrega };
                    break;
                case "DeliveryApp":
                    taxaDeEntrega = ((precoDosItens - desconto) * 0.10M) + 2.00M;
                    atendimento = new AtendimentoDeliveryAplicativo { DataHoraInicio = DateTime.Now, ComissaoParceiro = 0.10M, TaxaParceiro = 2.00M };
                    break;
                default:
                    atendimento = new AtendimentoPresencial { DataHoraInicio = DateTime.Now };
                    break;
            }

            decimal valorTotal = (precoDosItens - desconto) + taxaDeEntrega;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var novoPedido = new Pedido
            {
                DataHora = DateTime.Now,
                Status = "Recebido",
                UsuarioId = userId,
                ValorTotal = valorTotal,
                Atendimento = atendimento,
                Itens = new List<PedidoItem> { new PedidoItem { ItemCardapioId = itemCardapio.Id, Quantidade = 1, PrecoUnitario = (decimal)itemCardapio.PrecoBase } }
            };

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();
            MensagemSucesso = $"Pedido nº {novoPedido.Id} realizado com sucesso! Valor final: {valorTotal:C}";
            return RedirectToPage("/Index");
        }
    }
}