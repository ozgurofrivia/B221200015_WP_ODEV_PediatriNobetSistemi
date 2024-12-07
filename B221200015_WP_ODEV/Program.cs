using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static B221200015_WP_ODEV.Helper.MailHelper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IMailHelper, MailHelper>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Giriþ sayfasý
        options.AccessDeniedPath = "/Login"; // Eriþim reddedildiðinde
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));

var app = builder.Build();


// Veritabanýný oluþturun veya migration'larý uygulayýn
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();

    // Migration'larý uygulayýn
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anasayfa}/{action=Anasayfa}/{id?}");

app.Run();
