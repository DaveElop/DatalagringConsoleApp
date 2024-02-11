using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleApp.Contexts;
using ConsoleApp.Repositories;
using ConsoleApp.Services;
using ConsoleApp.UserInterface;
using ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var serviceProvider = host.Services;

        using (var scope = serviceProvider.CreateScope())
        {
            var mainService = scope.ServiceProvider.GetRequiredService<IMainService>();
            var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
            var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();

            var ui = new UI(mainService, customerService, productService, addressService);

            try
            {
                ui.ShowMainMenu().Wait();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                ConfigureDbContext<CustomerCatalogContext>(services, hostContext.Configuration, "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Datalagring\\ConsoleApp\\ConsoleApp\\Data\\CustomerCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

                services.AddScoped<ICustomerRepository, CustomerRepository>();
                services.AddScoped<ICustomerService, CustomerService>();
                services.AddScoped<IAddressRepository, AddressRepository>();
                services.AddScoped<IAddressService, AddressService>();

                ConfigureDbContext<ProductCatalogContext>(services, hostContext.Configuration, "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Datalagring\\ConsoleApp\\ConsoleApp\\Data\\ProductCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IProductService, ProductService>();

                services.AddScoped<IMainService, MainService>();
            });

    static void ConfigureDbContext<TContext>(IServiceCollection services, IConfiguration configuration, string connectionName)
        where TContext : DbContext
    {
        var connectionString = configuration.GetConnectionString(connectionName);
        services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
    }
}
