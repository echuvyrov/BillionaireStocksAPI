using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillionaireStocksAPI.Controllers
{
    public class BillionaireGroupedStocksData
    {
        public String BillionaireName { get; set; }
        public String StockTicker { get; set; }
        public Decimal? PricePaid { get; set; }
        public Decimal? LastPrice { get; set; }
        public Double? DividendYield { get; set; }

        public string StockCompanyName { get; set; }

        public int StockId { get; set; }
    }
}
