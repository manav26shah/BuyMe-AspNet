using BuyMe.API.Config;
using BuyMe.BL;
using BuyMe.BL.Implementation;
using BuyMe.BL.Interface;
using BuyMe.DL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuyMe.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace BuyMe.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        // Dependecy Injection- Design pattern , creational Design Pattern
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddSwagger(services);
            RegisterDbServices(services);
            RegisterBusinessServces(services);
            RegisterConfigurations(services);
            RegisterAuthentication(services);
        }

      
        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options=> {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                options.IncludeXmlComments(path);
            });
        }
        public void RegisterBusinessServces(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();
        }
        public void RegisterConfigurations(IServiceCollection services)
        {
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            services.Configure<AuthConfig>(Configuration.GetSection("Auth"));
            services.Configure<JWTConfig>(Configuration.GetSection("Jwt"));
        }

        public void RegisterDbServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConectionString"));
            });

            services.AddScoped<IRepo, Repo>();
        }

        public void RegisterAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            
            app.UseSwagger();
            app.UseSwaggerUI();
            app.AddCustomHeader();
            app.UseHttpsRedirection();
            
            app.UseRouting(); // this middleware decides which action method from which controler call, what value to apss, in the querystring 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }

      public RequestDelegate Test(RequestDelegate options)
        {
            
            return options;
        }

        
    }
}
