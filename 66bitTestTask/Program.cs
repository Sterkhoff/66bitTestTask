using _66bitTestTask.Data;
using _66bitTestTask.Data.Repository;
using _66bitTestTask.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace _66bitTestTask;

public static class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<MyAppContext>(ServiceLifetime.Transient);
        builder.Services.AddTransient<IMyAppRepository, MyAppRepository>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddSignalR();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();        
        app.UseRouting();

        app.UseAuthorization();
        app.MapHub<MyHub>("/Updater");
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=CreateSoccerPlayer}");

        app.Run();
    }
}
