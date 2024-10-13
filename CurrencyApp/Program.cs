using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using BusinesssLayer.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantý dizesini ayarlayýn
builder.Services.AddDbContext<CurrencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CurrencyService ve CurrencyRateHostedService servislerini ekleyin
builder.Services.AddScoped<CurrencyService>(); // Scoped olarak ekleniyor
builder.Services.AddHostedService<CurrencyRateHostedService>(); // HostedService olarak ekleniyor

// MVC için gerekli servisleri ekleyin
builder.Services.AddControllersWithViews(); // Bu satýr MVC yapýsýný etkinleþtirir

var app = builder.Build();

// Middleware ve yönlendirme ayarlarý
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Currency}/{action=Index}/{id?}");
});

app.Run();
