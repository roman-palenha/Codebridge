using AspNetCoreRateLimit;
using Codebridge.Business;
using Codebridge.Business.Interfaces;
using Codebridge.Business.Services;
using Codebridge.Configs.Interfaces;
using Codebridge.Configs;
using Codebridge.DataLayer;
using Codebridge.DataLayer.Interfaces;
using Codebridge.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Codebridge.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutomapperProfile));
            services.Configure<InformationConfig>(configuration.GetSection("Information"));
            services.AddSingleton<IInformationConfig>(sp => sp.GetRequiredService<IOptions<InformationConfig>>().Value);

            services.AddScoped<IDogService, DogService>();
            services.AddScoped<IDogRepository, DogRepository>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }
    }
}
