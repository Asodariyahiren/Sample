using FoodApplication.Data;
using FoodApplication.Models;
using FoodApplication.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
var dbcs = builder.Configuration.GetConnectionString("dbcs");
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FoodDBContext>(options => options.UseSqlServer(dbcs));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<FoodDBContext>();
//builder.Services.AddTransient<IData, Data>();
builder.Services.AddTransient<IData, Data>();

var app = builder.Build();
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
