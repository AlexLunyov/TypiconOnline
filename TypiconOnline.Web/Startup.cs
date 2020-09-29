using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SimpleInjector;
//using SmartBreadcrumbs.Extensions;
using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.WebQuery.Context;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Web.Services;
using TypiconOnline.WebServices.Authorization;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.Web
{
    public class Startup
    {
        private const string GetScheduleCorsPolicy = "GetSchedulePolicy";

        private readonly Container container = new Container();
        private readonly IWebHostEnvironment _hostingEnv;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            _hostingEnv = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(
                /*options => options.SignIn.RequireConfirmedAccount = true*/)
                .AddEntityFrameworkStores<TypiconDBContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //breadcrumbs
            //services.AddBreadcrumbs(GetType().Assembly);

            //session
            //services.AddDistributedMemoryCache();
            services.AddSession();

            #region Authorization handlers

            services.AddSingleton<IAuthorizationHandler>(new SimpleInjectorAuthorizationHandler(container));

            //services.AddScoped<IAuthorizationHandler, TypiconCanEditAuthorizationHandler>();
            //services.AddScoped<IAuthorizationHandler, DefaultAuthorization>();

            #endregion

            #region DbContext

            services.AddDbContext<TypiconDBContext>(optionsBuilder =>
            {
                //SqlServer
                //var connectionString = configuration.GetConnectionString("MSSql");
                //optionsBuilder.UseSqlServer(connectionString);

                //SQLite
                //var connectionString = configuration.GetConnectionString("DBTypicon");
                //optionsBuilder.UseSqlite(connectionString);

                //MySQL
                optionsBuilder.UseMySql(Configuration.GetConnectionString("MySql"),
                        mySqlOptions =>
                        {
                            mySqlOptions.ServerVersion(new Version(5, 6, 43), ServerType.MySql);
                        });

                //PostgreSQL
                //optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgre"));
            });

            services.AddDbContext<WebDbContext>(optionsBuilder =>
            {
                //SqlServer
                //var connectionString = configuration.GetConnectionString("MSSql");
                //optionsBuilder.UseSqlServer(connectionString);

                //SQLite
                //var connectionString = configuration.GetConnectionString("DBTypicon");
                //optionsBuilder.UseSqlite(connectionString);

                //MySQL
                optionsBuilder.UseMySql(Configuration.GetConnectionString("MySql"),
                        mySqlOptions =>
                        {
                            mySqlOptions.ServerVersion(new Version(5, 6, 43), ServerType.MySql);
                        });

                //PostgreSQL
                //optionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgre"));
            });

            #endregion

            services.AddControllersWithViews();

            //services.AddLogging();
            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSimpleInjector(container, options =>
            {
                //HostedService
                options.AddHostedService<TimedHostedService<JobExecutor>>();
                container.RegisterInstance(new TimedHostedService<JobExecutor>.Settings(
                    interval: TimeSpan.FromSeconds(1),
                    action: service => service.Execute()));
                container.Register<JobExecutor>();

                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation()
                    .AddPageModelActivation()
                    .AddTagHelperActivation();
            });

            //CORS

            services.AddCors(options =>
            {
                options.AddPolicy(GetScheduleCorsPolicy, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
                options.AddDefaultPolicy( builder =>
                {
                    builder.WithOrigins("https://typicon.online",
                                        "https://localhost:44303");
                });
            });

            //JSON
            services.AddMvc(options =>
                {
                    options.ModelValidatorProviders.Add(
                        new SimpleInjectorModelValidatorProvider(container));
                })
                .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ContractResolver =
                          new DefaultContractResolver());

            #region Authorize policies

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole",
                     policy => policy.RequireRole(RoleConstants.AdministratorsRole));
                options.AddPolicy("RequireEditorRole",
                    policy => policy.RequireRole(RoleConstants.EditorsRole));
                options.AddPolicy("RequireTypesetterRole",
                    policy => policy.RequireRole(RoleConstants.TypesettersRole));
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container, options =>
            {
                //options.UseLogging();
                //options.UseLocalization();
            });

            InitializeContainer();

            container.Verify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeContainer()
        {
            container.AddWebDI(Configuration, _hostingEnv);
        }
    }
}
