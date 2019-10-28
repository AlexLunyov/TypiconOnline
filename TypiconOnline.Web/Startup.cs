using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypiconOnline.Web.Services;
using SimpleInjector;
using TypiconOnline.Domain.Identity;
//using SmartBreadcrumbs.Extensions;
using TypiconOnline.Repository.EFCore.DataBase;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;

namespace TypiconOnline.Web
{
    public class Startup
    {
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TypiconDBContext>()
                .AddDefaultTokenProviders();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.LoginPath = "/Account/Login";
            //    options.LogoutPath = "/Account/Logout";
            //});

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //breadcrumbs
            //services.AddBreadcrumbs(GetType().Assembly);

            //session
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();

            //DI
            IntegrateSimpleInjector(services);
            services.AddTypiconOnlineService(Configuration, container, _hostingEnv);

            services
                //.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN")
                //.AddMvc(config =>
                //{
                //    // using Microsoft.AspNetCore.Mvc.Authorization;
                //    // using Microsoft.AspNetCore.Authorization;
                //    var policy = new AuthorizationPolicyBuilder()
                //                     .RequireAuthenticatedUser()
                //                     .Build();
                //    config.Filters.Add(new AuthorizeFilter(policy));
                //})
                .AddMvc()
                .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ContractResolver =
                          new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container, options =>
            {
                //options.UseLogging();
                //options.UseLocalization();
            });

            container.Verify();

            //if (env.IsDevelopment())
            //{
            //app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            //app.excUseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute().RequireAuthorization();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSingleton<IControllerActivator>(
            //    new SimpleInjectorControllerActivator(container));
            //services.AddSingleton<IViewComponentActivator>(
            //    new SimpleInjectorViewComponentActivator(container));

            //services.EnableSimpleInjectorCrossWiring(container);
            //services.UseSimpleInjectorAspNetRequestScoping(container);

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation()
                    .AddPageModelActivation()
                    .AddTagHelperActivation();
            });
        }
    }
}
