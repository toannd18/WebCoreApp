using AutoMapper;
using DataContext;
using DataContext.Drapper.Implemention;
using DataContext.Drapper.Interface;
using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Text;
using WebCoreApp.Extensions.Email;
using WebCoreApp.Extensions.Polices;
using WebCoreApp.Extensions.RazorTemplate;
using WebCoreApp.Extensions.Signlar;
using WebCoreApp.Extensions.Signlarr;
using WebCoreApp.Infrastructure.Interfaces;
using WebCoreApp.Infrastructure.Interfaces.Pipes;
using WebCoreApp.Infrastructure.Repositores.Pipes;
using WebCoreApp.Service.Interfaces;
using WebCoreApp.Service.Repositores;

namespace WebCoreApp
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
            services.AddDbContext<EFContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<EFContext>()
                .AddDefaultTokenProviders();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.ConfigureApplicationCookie(o =>
            {
                o.Cookie.HttpOnly = false;
                o.LoginPath = "/Account/Login";
                o.AccessDeniedPath = "/Account/Login";
                o.SlidingExpiration = true;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme + ",Identity.Application";
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme + ",Identity.Application";
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme + ",Identity.Application";
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtConfig:ValidIssuer"],
                        ValidAudience = Configuration["JwtConfig:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddJsonOptions(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Configure MailKit

            services.AddMailKit(o =>
            {
                o.UseMailKit(new MailKitOptions()
                {
                    Server = Configuration["EmailConfig:Server"],
                    Port = Configuration.GetValue<int>("EmailConfig:Port"),
                    SenderEmail = Configuration["EmailConfig:EmailSender"],
                    SenderName = Configuration["EmailConfig:NameSender"],
                    Security = Configuration.GetValue<bool>("EmailConfig:SSL"),
                    Account = Configuration["EmailConfig:EmailSender"],
                    Password = Configuration["EmailConfig:Password"]
                });
            });

            services.AddTransient<IEmailService, EmailService>();

            // Services Templates

            services.AddTransient<IRazorTemplate, RazorTemplate>();

            // SignalR Hub

            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            // AuthorizationProvider
            services.AddSingleton<IAuthorizationPolicyProvider, RoleClaimsPolicyProvider>();

            services.AddTransient<IAuthorizationHandler, RoleClaimsHandler>();

            services.AddAutoMapper();
            // Services Database

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IDiaryRepository, DiaryRepository>();

            services.AddTransient<IDetailDiaryRepository, DetailDiaryRepository>();

            services.AddTransient<IJobRepository, JobRepository>();

            services.AddTransient<IToRepository, ToRepository>();

            services.AddTransient<IBPRepository, BPRepository>();

            services.AddTransient<INotificationRepository, NotificationRepository>();

            services.AddTransient<IPositionRepository, PositionRepository>();

            services.AddTransient<IFunctionRepository, FunctionRepository>();

            services.AddTransient<IWelderRepository, WelderRepository>();

            services.AddTransient<IMaterialPipeRepository, MaterialPipeRepository>();

            services.AddTransient<ITypeJointRepository, TypeJointRepository>();

            services.AddTransient<IIsometricRepository, IsometricRepository>();

            services.AddTransient<IIsoJointRepository, IsoJointRepository>();

            services.AddTransient<IProjectRepository, ProjectRepository>();

            services.AddTransient<DbInitializer>();

            //Service Dapper
            services.AddTransient<IReportService, ReportService>();

            services.AddTransient<IWareHouseService, WareHouseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var supportedCultures = new[]
                {
                    new CultureInfo("en"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-AU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<PTSCHub>("/ptscHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Api",
                    template: "{area:exists}/api/{controller}/{id?}");
            });
        }
    }
}