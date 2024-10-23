using Microsoft.EntityFrameworkCore;
using StoryTailBlazor;
using StoryTailBlazor.Components;
using StoryTailBlazor.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura o contexto do Entity Framework com MySQL/MariaDB
builder.Services.AddDbContext<StoryTailDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 32)) // Versão do MariaDB
    ));

// Adicionar serviços do Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registra o HttpClient para acessar a API
builder.Services.AddHttpClient("StoryTailApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5019"); 
});


var app = builder.Build();

// Configurar o seeding de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Executar o seeder para popular o banco de dados
        DatabaseSeeder.SeedDatabase(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao executar o seeding da base de dados.");
    }
}

// Configure o pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Mapeia as rotas do Blazor Server
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();