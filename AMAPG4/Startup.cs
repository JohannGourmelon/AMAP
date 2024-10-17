using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using AMAPG4.Models.Command;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace AMAPG4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";

            });

            services.AddControllersWithViews();

            // Ajout des DAL en tant que services
            services.AddScoped<UserAccountDal>();
            services.AddScoped<IndividualDal>();
            services.AddScoped<CEDal>();
            services.AddScoped<ProducerDal>();
            services.AddScoped<ProductDal>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Initialisation des données
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                UserAccountDal userAccountDal = scope.ServiceProvider.GetRequiredService<UserAccountDal>();
                userAccountDal.InitializeDataBase();

                IndividualDal individualDal = scope.ServiceProvider.GetRequiredService<IndividualDal>();
                individualDal.Initialize();

                CEDal ceDal = scope.ServiceProvider.GetRequiredService<CEDal>();
                ceDal.Initialize();

                ProducerDal producerDal = scope.ServiceProvider.GetRequiredService<ProducerDal>();
                producerDal.Initialize();

                ProductDal productDal = scope.ServiceProvider.GetRequiredService<ProductDal>();
                productDal.InitializeDataBase();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
