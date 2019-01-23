using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace UsingDependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await MainAsyns()).Wait();
        }

        static async Task MainAsyns()
        {
            var p = new Program();
            p.InitializeServices();
            p.ConfigureLogging();
            var services = p.Container.GetService<BooksService>();
            await services.AddBooksAsync();
            await services.ReadBooksAsync();
        }

        public void ConfigureLogging()
        {
            ILoggerFactory loggerFactory = Container.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Information);
        }

        public void InitializeServices()
        {
            const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=Roka;trusted_connection=true";
            var services = new ServiceCollection();
            services.AddTransient<BooksService>()
                .AddLogging()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<BooksContext>(options =>
                    options.UseSqlServer(ConnectionString));
            Container = services.BuildServiceProvider();
        }

        public ServiceProvider Container { get; private set; }
    }
}
