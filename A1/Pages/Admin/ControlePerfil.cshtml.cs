using A1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A1.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ControlePerfilModel : PageModel
    {
        //private readonly A1.Data.A1Context _context;

        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ControlePerfilModel(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

       /* public ControlePerfilModel(A1.Data.A1Context context)
        {
            _context = context;
        }*/

        public List<UserViewModel> Users { get; set; }

        public class UserViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public bool IsAdmin { get; set; }
        }

        public async Task OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync(); // Materializa a lista

            Users = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Agora funciona sem conflito
                Users.Add(new UserViewModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    IsAdmin = roles.Contains("Admin")
                });
            }
        }


        public async Task<IActionResult> OnPostPromoverAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound();
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "Usuário já é administrador.");
                return Page();
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            return RedirectToPage();
        }
    }
}
