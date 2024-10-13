using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusinesssLayer.Abstract
{
    public class CurrencyService
    {
        private readonly CurrencyDbContext _context;

        public CurrencyService(CurrencyDbContext context)
        {
            _context = context;
        }

        public async Task FetchAndStoreRatesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://www.tcmb.gov.tr/kurlar/today.xml");
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response);

                // USD, EURO ve GBP için verileri al
                var usdRate = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling");
                var euroRate = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling");
                var gbpRate = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling");

                // Verileri kaydet
                if (usdRate != null)
                {
                    _context.CurrencyRates.Add(new CurrencyRate
                    {
                        CurrencyCode = "USD",
                        BuyingRate = decimal.Parse(usdRate.InnerText),
                        Date = DateTime.Now
                    });
                }

                if (euroRate != null)
                {
                    _context.CurrencyRates.Add(new CurrencyRate
                    {
                        CurrencyCode = "EUR",
                        BuyingRate = decimal.Parse(euroRate.InnerText),
                        Date = DateTime.Now
                    });
                }

                if (gbpRate != null)
                {
                    _context.CurrencyRates.Add(new CurrencyRate
                    {
                        CurrencyCode = "GBP",
                        BuyingRate = decimal.Parse(gbpRate.InnerText),
                        Date = DateTime.Now
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        public object GetRatesByDate(DateTime parsedDate)
        {
            throw new NotImplementedException();
        }
    }
}
