using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using Services.IServices;
using Services;
using WebApp.Helpers;
using WebApp.Hubs;
using Azure.Storage.Blobs;
using Hangfire;
using Hangfire.SqlServer;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace WebApp
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


            services.AddDbContext<AppIdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdentityConnection"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("WebApp")));




            services.AddSingleton(x => new BlobServiceClient(Configuration.GetValue<string>("BlobConnectionString")));


            services.AddSingleton<IBlobService, BlobService>();

            services.AddHttpClient<IProductService, ProductService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddHttpClient<IAssetService, AssetService>();
            services.AddScoped<IAssetService, AssetService>();

            services.AddHttpClient<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddHttpClient<IOrderService, OrderService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddHttpClient<ICartItemService, CartItemService>();
            services.AddScoped<ICartItemService, CartItemService>();

            services.AddHttpClient<IBrandService, BrandService>();
            services.AddScoped<IBrandService, BrandService>();

            services.AddHttpClient<IStatusService, StatusService>();
            services.AddScoped<IStatusService, StatusService>();

            services.AddHttpClient<IReviewService, ReviewService>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddHttpClient<IFavouriteService, FavouriteService>();
            services.AddScoped<IFavouriteService, FavouriteService>();


            services.AddTransient<ISendGridEmail, SendGridEmail>();

            services.AddHangfire(configuration =>
            configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(
                Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();


            services.AddSession();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSignalR();

            services.AddAuthentication()
                .AddGoogle(options =>
                {

                    options.ClientId = "test";
                    options.ClientSecret = "test";

                    //options.ClientId = Configuration.GetValue<string>("GoogleClientId");
                   // options.ClientSecret = Configuration.GetValue<string>("GoogleClientSecret");


                });

            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                })
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);


            services.AddRazorPages();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("uk")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                AppDbInitializer.SeedUsers(userManager, roleManager);
            }
            app.UseRequestLocalization();

            app.UseHttpsRedirection();

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SupportHub>("/supportHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Shop}/{action=List}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
