using CustomerInquiry.Provider;
using EquipmentRental.DataAccess;
using EquipmentRental.DataAccess.DbContext;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Domain.Entities;
using EquipmentRental.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;


namespace EquipmentRental
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var conn = Configuration["connectionStrings:sqlConnection"];
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(conn, b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName)));
            services.AddIdentity<Customer, IdentityRole>(
                   opts =>
                   {
                       opts.Password.RequireDigit = true;
                       opts.Password.RequireLowercase = true;
                       opts.Password.RequireUppercase = true;
                       opts.Password.RequireNonAlphanumeric = false;
                       opts.Password.RequiredLength = 7;
                   })
               .AddEntityFrameworkStores<SqlDbContext>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped(typeof(IInventoryProvider), typeof(InventoryProvider));
            services.AddScoped(typeof(ITransactionProvider), typeof(TransactionProvider));
            services.AddScoped(typeof(IGenericEfRepository<>), typeof(GenericEfRepository<>));

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Inventory, InventoryDto>();
                config.CreateMap<InventoryDto, Inventory>();
                config.CreateMap<Transaction, TransactionDTo>();
                config.CreateMap<TransactionDTo, Transaction>();
            });

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Equipment Rental API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Equipment Rental API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });


            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>
                    ().CreateScope())
            {
                var dbContext =
                    serviceScope.ServiceProvider.GetService<SqlDbContext>();
                var roleManager =
                    serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>
                        ();
                var userManager =
                    serviceScope.ServiceProvider.GetService<UserManager<Customer>>
                        ();
                // Create the Db if it doesn't exist and applies any pending migration.
                //dbContext.Database.Migrate();
                // Seed the Db.
                DbSeeder.Seed(dbContext, roleManager, userManager);
            }
        }
    }
}
