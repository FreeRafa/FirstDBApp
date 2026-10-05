using FirstDBApp.Apresentacao.GestaoMenu;
using FirstDBApp.Infraestrutura.Data;
using FirstDBApp.Infraestrutura.Repositorio;
using FirstDBApp.Servico;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

var service = new ServiceCollection();

service.AddDbContext<FirstDBAppContext>(options =>
    options.UseSqlServer(connectionString));

//Repositorio
service.AddScoped<ClienteRepositorio>();

//Servico
service.AddScoped<ClienteServico>();

//Menu
service.AddScoped<ClienteMenu>();

using var serviceProvider = service.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();

var clienteMenu = scope.ServiceProvider.GetRequiredService<ClienteMenu>();
await clienteMenu.ExibirMenuCliente();


