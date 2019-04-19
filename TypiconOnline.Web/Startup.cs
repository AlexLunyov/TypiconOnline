using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypiconOnline.Web.Services;
using SimpleInjector.Lifestyles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector.Integration.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SimpleInjector;
using TypiconOnline.Domain.Identity;
using TypiconOnline.WebServices.Identity;
using SmartBreadcrumbs.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Repository.EFCore.DataBase;
using Newtonsoft.Json.Serialization;

namespace TypiconOnline.Web
{
    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TypiconDBContext>()
                .AddDefaultTokenProviders();
            
            //services.AddTransient<IUserStore<User>, UserStore>();
            //services.AddTransient<IRoleStore<UserRole>, RoleStore>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddBreadcrumbs(GetType().Assembly);

            IntegrateSimpleInjector(services);

            services.AddTypiconOnlineService(Configuration, container);

            services
                .AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN")
                .AddMvc(config =>
                {
                    // using Microsoft.AspNetCore.Mvc.Authorization;
                    // using Microsoft.AspNetCore.Authorization;
                    var policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            //if (env.IsDevelopment())
            //{
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);
        }
    }
}
