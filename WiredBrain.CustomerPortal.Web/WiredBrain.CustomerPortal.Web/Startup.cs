using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WiredBrain.CustomerPortal.Web.Data;
using WiredBrain.CustomerPortal.Web.Repositories;

namespace WiredBrain.CustomerPortal.Web
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews(c =>
            //{
            //    c.Filters.Add(new SecurityHeadersAttribute());
            //});

            services.AddControllersWithViews();

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CustomerPortalDbContext>().UseSqlite(connection).Options;
            var context = new CustomerPortalDbContext(options);
            context.Database.EnsureCreated();
            context.Seed();

            services.AddSingleton(context);
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSingleton(config);
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("HeaderType", "HeaderValue");
            //    await next();
            //});

            app.UseSecurityHeaders();

            //app.UseCsp(options => options.DefaultSources(s => s.Self())
            //.FrameAncestors(f => f.None())

            //.StyleSources(s => s.Self().CustomSources("https://stackpath.bootstrapcdn.com"))
            //    .ReportUris(r => r.Uris("/report")));

            //app.UseXContentTypeOptions();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
