using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Ugly.Mug.Cafe.API.Service;
using Ugly.Mug.Cafe.Core.Orders;
using Ugly.Mug.Cafe.Core.Products;
using Ugly.Mug.Cafe.Core.Service;
using Ugly.Mug.Cafe.Data;

namespace Ugly.Mug.Cafe.API
{
    public static class StartupExtensions
    {

        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Default"];

            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<PushService>();
            services.AddScoped<ModelValidationServiceFilter>();
            
            return services;
        }

        public static IServiceCollection ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("push", option => { option.BaseAddress = new Uri(configuration["PushUrl"]); });
            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new Info { Title = "Ugly Mug Cafe API", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            return services;
        }
    }
}
