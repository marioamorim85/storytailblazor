using Microsoft.EntityFrameworkCore;
using StoryTailBlazor.Data; // Referência ao DbContext
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura o contexto do Entity Framework com MySQL/MariaDB
builder.Services.AddDbContext<StoryTailDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 32)) // Versão do MariaDB
    ));

// Adiciona suporte a controladores e configura o JSON para evitar ciclos
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Evita ciclos nas relações e simplifica o JSON (removendo $id e $values)
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Habilitar CORS para permitir que o Blazor Server consuma a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("http://localhost:5011") // Verifique se esta é a porta correta do Blazor Server
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configurar OpenAPI/Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoryTail API", Version = "v1" });
});

var app = builder.Build();

// Configurações para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryTail API v1");
    });
}

// Habilitar HTTPS e Redirecionamento (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Habilitar CORS (permite que Blazor consuma a API)
app.UseCors("AllowBlazor");

// Adicionar a autorização (caso decida usar autenticação no futuro)
app.UseAuthorization();

// Mapear os controladores da API
app.MapControllers();

// Redirecionar a rota principal para Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
