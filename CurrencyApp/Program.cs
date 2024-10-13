using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using BusinesssLayer.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant� dizesini ayarlay�n
builder.Services.AddDbContext<CurrencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CurrencyService ve CurrencyRateHostedService servislerini ekleyin
builder.Services.AddScoped<CurrencyService>(); // Scoped olarak ekleniyor
builder.Services.AddHostedService<CurrencyRateHostedService>(); // HostedService olarak ekleniyor

// MVC i�in gerekli servisleri ekleyin
builder.Services.AddControllersWithViews(); // Bu sat�r MVC yap�s�n� etkinle�tirir

var app = builder.Build();

// Middleware ve y�nlendirme ayarlar�
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Currency}/{action=Index}/{id?}");
});

app.Run();
