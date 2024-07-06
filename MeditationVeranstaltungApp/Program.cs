using MeditationVeranstaltungApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeditationVeranstaltungApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //var servicerProvider = app.Services.GetService<IServiceProvider>();


            //using (var serviceScope = app.Services.CreateScope())
            //{
            //    var services = serviceScope.ServiceProvider;

            //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //    CreateRole(roleManager).Wait();
            //    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            //    CreateDefaultUser(userManager).Wait();
            //}

            app.Run();
        }
        public static async Task CreateRole(RoleManager<IdentityRole> roleManager)
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (adminRoleExists == false)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var userRoleExists = await roleManager.RoleExistsAsync("User");
            if (userRoleExists == false)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
        public static async Task CreateDefaultUser(UserManager<IdentityUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@web.de");
            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    Email = "admin@web.de",
                    UserName = "admin@web.de"

                };
                await userManager.CreateAsync(user, "Waheguru@1");
                 adminUser = await userManager.FindByEmailAsync("admin@web.de");
            }
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
