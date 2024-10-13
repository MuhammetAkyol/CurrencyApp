using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Concrete;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly CurrencyDbContext _context;

        public CurrencyController(CurrencyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // En son kaydedilen döviz kurlarını veritabanından al
            var latestRates = _context.CurrencyRates
                .OrderByDescending(c => c.Date)
                .Take(3) // USD, EUR, GBP
                .ToList();

            return View(latestRates);
        }
        [HttpGet]
        public JsonResult GetRatesByDate(DateTime date)
        {
            var rates = _context.CurrencyRates
                .Where(r => r.Date.Date == date.Date) // Tarih kısmını karşılaştır
                .Select(r => new
                {
                    currencyCode = r.CurrencyCode,
                    buyingRate = r.BuyingRate,
                    date = r.Date
                })
                .ToList();

            return Json(rates);
        }


    }
}
