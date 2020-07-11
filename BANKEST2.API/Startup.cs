using System;
using System.Text;
using BANKEST2.Core.AppServices.AccountMapper;
using BANKEST2.Core.AppServices.NewFolder;
using BANKEST2.Core.AppServices.WireTransfer;
using BANKEST2.Core.AppServices.WithDrawalService;
using BANKEST2.Core.Interfaces;
using BANKEST2.Core.Services.AccountQuery;
using BANKEST2.Core.Services.Auth;
using BANKEST2.Infrastructure.UnitOfWork;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BANKEST2.API
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
            services.AddCors(options => options.AddPolicy("AllowWebApp",
      builder => builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
   {            
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = Configuration["Jwt:Issuer"],
           ValidAudience = Configuration["JWT:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
           ClockSkew = TimeSpan.Zero
       };
   });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IQueryService), typeof(QueryService));
            services.AddScoped(typeof(IAccountMapper), typeof(AccountMapper));
            services.AddScoped(typeof(IDepositService), typeof(DepositService));
            services.AddScoped(typeof(IWithDrawalAppService), typeof(WithDrawalAppService));
            services.AddScoped(typeof(IWireTransferAppService), typeof(WireTransferAppService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


            services.AddControllers();

            services.AddDbContext<BankDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("BankDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowWebApp");


            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
