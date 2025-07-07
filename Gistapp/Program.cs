/*---------------------------------------------------dernière version Fonctionnel--------------------------------------------------------------------------------*/
// Fichier principal de configuration de l’application (injection de dépendances, pipeline HTTP)

//using gistapp.Data;
//using Gistapp.Repositories;
using Gistapp.Services;
using Gistapp.Data;
using Gistapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using gistapp.Services;
using gistapp.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuration du DbContext avec la chaîne de connexion "DefaultConnection"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Page de connexion
        options.AccessDeniedPath = "/Account/AccessDenied"; // Page d'accès refusé
    });
builder.Services.AddAuthorization();
// Ajoutez les autres services (ex. Identity, ControllersWithViews, etc.)


// Enregistrer le service utilisateur personnalisé
builder.Services.AddScoped<IUserService, UserService>(); // Crée un service pour gérer les utilisateurs
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>(); // Crée un service pour gérer les projets

builder.Services.AddControllersWithViews();

// Configuration de la session (pour stocker des données d'authentification simples)
builder.Services.AddSession();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


// Utiliser la session avant l'authentification/authorization si nécessaire
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    // Applique toutes les migrations en attente à la base de données
    //context.Database.Migrate();
}


app.Run();

