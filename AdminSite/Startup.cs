using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using AdminSite.Framework;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AdminSite.Service;
using Microsoft.AspNetCore.SpaServices;

namespace AdminSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();


            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                if (HostingEnvironment.IsDevelopment())
                {
                    configuration.RootPath = "ClientApp/dist";
                }
                else
                {
                    configuration.RootPath = "ClientApp";
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // need to uncomment during development
                //endpoints.MapToVueCliProxy(
                //   "{*path}",
                //   new SpaOptions { SourcePath = "ClientApp" },
                //   npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
                //   forceKill: true);

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ConnectionHub>("/chatHub");
            });
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
              
            });
            
        }
    }
}
