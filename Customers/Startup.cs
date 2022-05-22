using Customers.Business.Abstract;
using Customers.Business.Concrete;
using Customers.Dal.Command.Request;
using Customers.Dal.Query.Request;
using Customers.Dto;
using Customers.Validation;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers
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
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddTransient<IValidator<CreateCustomerCommandRequestDto>, CreateCustomerCommandRequestDtoValidation>();
            services.AddTransient<IValidator<DeleteCustomerCommandRequest>, DeleteCustomerCommandRequestValidation>();
            services.AddTransient<IValidator<GetCustomerQueryRequest>, GetCustomerQueryRequestValidation>();
            services.AddTransient<IValidator<UpdateCustomerCommandRequestDto>, UpdateCustomerCommandRequestValidation>();
            services.AddTransient<IValidator<ValidateCustomerQueryRequest>, ValidateCustomerQueryRequestValidation>();
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
