using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BillionaireStocksAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<BillionaireGroupedStocksData> Get()
        {
            using (BillionaireStocksEntities entts = new BillionaireStocksEntities())
            {
                IQueryable<BillionaireGroupedStocksData> data = from bills in entts.Billionaires
                    join billsStocks in entts.BillionaireStocks on bills.BillionaireId equals billsStocks.BillionaireId
                    join billsStockPrices in entts.BillionaireStockPrices on billsStocks.BillionaireStockId equals billsStockPrices.BillionaireStockId
                    where billsStockPrices.BillionaireStockPriceDate == entts.BillionaireStockPrices.Where(stock=>stock.BillionaireStockId == billsStockPrices.BillionaireStockId).Max(stockDate=>stockDate.BillionaireStockPriceDate) 
                    select new BillionaireGroupedStocksData
                    {
                        BillionaireName = String.Concat(bills.BillionaireFirstName, " ", bills.BillionaireLastName),
                        StockId = billsStocks.BillionaireStockId,
                        StockTicker = billsStocks.BillionaireStockTicker,
                        StockCompanyName = billsStocks.BillionaireStockCompany,
                        PricePaid = billsStocks.BillionaireStockPricePaid,
                        LastPrice = billsStockPrices.BillionaireStockPriceValue,
                        DividendYield = billsStocks.DividendYield
                    };

                return data.ToList();
            }
        }

        // GET api/values/5
        public IEnumerable<BillionaireStockPriceData> Get(int id)
        {
            using (BillionaireStocksEntities entts = new BillionaireStocksEntities())
            {
                DateTime dtYearAgo = DateTime.Now.AddYears(-1);
                IQueryable<BillionaireStockPriceData> data = from billsStockPrices in entts.BillionaireStockPrices
                                                         where (billsStockPrices.BillionaireStockPriceDate <= DateTime.Now && billsStockPrices.BillionaireStockPriceDate > dtYearAgo)
                                                         && billsStockPrices.BillionaireStockId == id
                                                         select new BillionaireStockPriceData
                                                         {
                                                            Price = billsStockPrices.BillionaireStockPriceValue,
                                                            PriceDate = billsStockPrices.BillionaireStockPriceDate
                                                         };
                return data.ToList();
            }
        }

        // POST api/values
        public void Post(string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}