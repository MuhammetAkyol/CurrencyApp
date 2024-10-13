using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CurrencyRate
    {
        public int Id { get; set; } // Anahtar
        public string CurrencyCode { get; set; } // Para birimi kodu (örn. USD, EUR)
        public decimal BuyingRate { get; set; } // Alış fiyatı
        public DateTime Date { get; set; } // Tarih
    }


}
