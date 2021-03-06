using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using BusinessCore.Interfaces;
using BusinessCore.Services;
using DALCore;
using DALCore.Entity;
using DALCore.Repositories;
using MediatR;
using Wallace740_SmartApi.Query;
using Wallace740_SmartApi.Command;

namespace Wallace740_SmartApi
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
            services.AddScoped<IDbRepository<Product>, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMediator, Mediator>();

            services.AddMediatR(typeof(ProductQueryHandler));
            services.AddMediatR(typeof(ProductsQueryHandler));
            services.AddMediatR(typeof(InsertCommandHandler));
            services.AddMediatR(typeof(UpdateCommandHandler));


            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];
            });

            // Register the scope authorization handler
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:products", policy => policy.Requirements.Add(new HasScopeRequirement("read:products", domain)));
                options.AddPolicy("write:products", policy => policy.Requirements.Add(new HasScopeRequirement("write:products", domain)));
            });
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 401)
                {
                    await context.HttpContext.Response.WriteAsync("Custom Unauthorized request - Check OAuth credentials");
                }
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
