//Name:  Mikael Melo
//student number: 26172

using BankingMVC.Data;

using Microsoft.EntityFrameworkCore;
using BankingMVC.Controllers;
using BankingMVCApp.Controllers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

// Registering controllers
builder.Services.AddScoped<BankEmployeeController>(); // BankEmployeeController
builder.Services.AddScoped<CustomerController>(); // CustomerController

// Registering the database context
builder.Services.AddDbContext<BankingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
