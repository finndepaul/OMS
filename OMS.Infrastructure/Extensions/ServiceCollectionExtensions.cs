using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMS.Application.Interfaces.Repositories.ReadOnly;
using OMS.Application.Interfaces.Repositories.ReadWrite;
using OMS.Infrastructure.Database.AppDbContexts;
using OMS.Infrastructure.Implements.Repositories.ReadOnly;
using OMS.Infrastructure.Implements.Repositories.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            //Cấu hình DbContext
            services.AddDbContext<OMSReadOnlyDbContext>();
            services.AddDbContext<OMSReadWriteDbContext>();
            //Cấu hình Repo
            services.AddScoped<IProductReadWriteRepository, ProductReadWriteRepository>();
            services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();
            services.AddScoped<IOrderReadWriteRepository, OrderReadWriteRepository>();
            services.AddScoped<IOrderReadOnlyRepository, OrderReadOnlyRepository>();
            return services;
        }
    }
}
