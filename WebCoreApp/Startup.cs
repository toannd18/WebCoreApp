using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Text;
using WebCoreApp.Data;
using WebCoreApp.Extensions.Signlar;
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

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme + "," +
                                                JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme + "," +
                                                JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme + "," +
                                                JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;

                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://ptscqng.com.vn",
                        ValidAudience = "http://ptscqng.com.vn",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My Securety Key for ptsc.com.vn")),
                        ClockSkew = TimeSpan.Zero
                    };
                })
                       .AddCookie(o =>
                       {
                           o.LoginPath = "/Account/Login";
                           o.AccessDeniedPath = "/Account/Login";
                       });

            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            }).AddJsonOptions(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // SignalR Hub

            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            // Services Database

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IDiaryRepository, DiaryRepository>();

            services.AddTransient<IDetailDiaryRepository, DetailDiaryRepository>();

            services.AddTransient<IJobRepository, JobRepository>();

            services.AddTransient<IToRepository, ToRepository>();

            services.AddTransient<IBPRepository, BPRepository>();

            services.AddTransient<INotificationRepository, NotificationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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