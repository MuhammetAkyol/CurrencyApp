using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Abstract
{
    public class CurrencyRateHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public CurrencyRateHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Timer'ı ayarla; her dakika başında çalışacak şekilde ayarlıyoruz
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;


            // günlük formatta istiyorsan açıklama satırı olan yerleri aç test için dakika'da bir alsın diye kısa formatta yaptım.
            //var currentTime = DateTime.Now;
            //var targetTime = DateTime.Today.AddHours(9); // Her gün sabah 9:00'da çalışacak

            //if (currentTime > targetTime)
            //{
            //    targetTime = targetTime.AddDays(1); // Eğer şu anki zaman 9:00'ı geçmişse, bir sonraki gün çalıştır
            //}

            //var timeToGo = targetTime - currentTime;

            //_timer = new Timer(DoWork, null, timeToGo, TimeSpan.FromDays(1)); // İlk çalıştırma zamanı ve her gün tekrar
            //return Task.CompletedTask;


        }


        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var currencyService = scope.ServiceProvider.GetRequiredService<CurrencyService>();
                currencyService.FetchAndStoreRatesAsync().Wait(); // Kur bilgilerini çek ve veritabanına kaydet
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
