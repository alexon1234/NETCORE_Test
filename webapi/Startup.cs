using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webapi.src.Payment.Domain;
using webapi.src.Payment.Infrastructure;
using webapi.src.Shared.Domain.Command;
using webapi.src.Shared.Domain.Event;
using webapi.src.Shared.Infrastructure;
using webapi.src.Shared.Infrastructure.Command;
using webapi.src.Shared.Infrastructure.Middleware;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services
                .AddDbContext<PaymentDbContext>(options =>
                    options.UseNpgsql(connectionString)
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
