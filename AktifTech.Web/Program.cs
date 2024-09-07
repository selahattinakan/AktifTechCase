using AktifTech.Database.DataAccessLayer;
using AktifTech.Web.ApiService;
using AktifTech.Web.ApiService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IApiCustomerService, ApiCustomerService>();
builder.Services.AddScoped<IApiProductService, ApiProductService>();
builder.Services.AddScoped<IApiCustomerOrderService, ApiCustomerOrderService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
