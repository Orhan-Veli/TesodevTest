using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.Business.Abstract;
using Orders.Business.Concrete;
using Orders.Dal.Command.Request;
using Orders.Dal.Query.Request;
using Orders.Dto;
using Orders.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders
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
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IOrderService, OrderService>();
            services.AddTransient<IValidator<ChangeStatusOrderCommandRequest>, ChangeStatusOrderCommandRequestValidation>();
            services.AddTransient<IValidator<CreateOrderCommandRequestDto>, CreateOrderCommandRequestDtoValidation>();
            services.AddTransient<IValidator<GetOrderQueryRequest>, GetOrderQueryRequestValidation>();
            services.AddTransient<IValidator<DeleteOrderCommandRequest>, DeleteOrderCommandRequestValidation>();
            services.AddTransient<IValidator<UpdateOrderCommandRequestDto>, UpdateOrderCommandRequestDtoValidation>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
