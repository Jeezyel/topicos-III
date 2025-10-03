using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using A1.Data;
using A1.Customs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using A1.Models;

var builder = WebApplication.CreateBuilder(args);

// --- Adiciona serviços ao contêiner ---
builder.Services.AddDbContext<A1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("A1Context") ?? throw new InvalidOperationException("Connection string 'A1Context' not found.")));

builder.Services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<A1Context>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();


// --- ADICIONADO: Configuração da Sessão ---
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// --- CORRIGIDO: Lógica de inicialização do banco de dados e perfis ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<A1Context>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Cuidado: isso apaga o banco TODA vez que rodar em desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            context.Database.EnsureDeleted(); // apaga
            context.Database.Migrate();      // recria e aplica migrations
        }

        // Cria os perfis "Admin" e "Usuario" se não existirem
        string[] roles = { "Admin", "Usuario" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um erro ocorreu ao popular o banco de dados.");
    }
}


// --- Configura o pipeline de requisições HTTP ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- ADICIONADO: Ativa o middleware de Sessão ---
// Deve vir ANTES de UseAuthorization e MapRazorPages
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();