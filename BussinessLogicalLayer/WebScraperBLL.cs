using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BussinessLogicalLayer
{
    public class WebScraperBLL
    {

        public static List<WebScraperDAL.Datum> Tradingview()
        {
            return WebScraperDAL.GetStocks();
        }

        public static List<string> Scrape()
        {
            return WebScraperDAL.GetStockSymbols();
        }
    }
}