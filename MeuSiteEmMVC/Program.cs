using ControleContatos.Data;
using ControleContatos.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Conexão com o postgreSQL:
builder.Services
    .AddDbContext<BancoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ServerConnection")));

//Injecção de Dependencia : 
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();

//Habilitando a compilação em tempo de execução 
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contato}/{action=Index}/{id?}");

app.Run();
