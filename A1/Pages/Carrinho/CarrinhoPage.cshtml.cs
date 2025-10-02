using A1.Data;
using A1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace A1.Pages.CarrinhoPage
{
    public class CarrinhoModel : PageModel
    {
        private readonly A1Context _context;
        private readonly UserManager<Usuario> _userManager;

        public CarrinhoModel(A1Context context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<ItemCarrinho> ItensCarrinho { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

        }
    }

}



