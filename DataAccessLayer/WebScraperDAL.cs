using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataAccessLayer
{


    public class WebScraperDAL
    {
        public class TradingViewResponseObject
        {
            public int TotalCount { get; set; }
            public List<Datum> Data { get; set; }
        }

        public class Datum
        {
            public string S { get; set; }
            public List<Stock> D { get; set; }
        }

        public class Stock
        {
            public string Nome { get; set; }
            public string Symbol { get; set; }
            public double Preco { get; set; }
            public double VariacaoPorcentagem { get; set; }
            public double VariacaoBruta { get; set; }
            public double Tendencia { get; set; }
            public int Volume { get; set; }
            public int VolumeVezesPreco { get; set; }
            public long ValorDeMercado { get; set; }
            public double PL { get; set; }
            public double ESP { get; set; }
            public int Funcionarios { get; set; }
            public string Setor { get; set; }
            public string DisplayName { get; set; }
            public string StockType { get; set; }
            public string Common { get; set; }
            public string DelayedStreaming { get; set; }
            public int RdmNumber { get; set; }
            public int RdmNumber2 { get; set; }
            public string RdmBoolean { get; set; }
            public int RdmNumber3 { get; set; }
            public string Currency { get; set; }
            public string Currency2 { get; set; }


        }

        private static IWebDriver driver = new ChromeDriver("/home/marco/Desktop/projetos/mintMoney/DataAccessLayer/bin/Debug/net6.0");
        public static List<Datum> ScrapeTradingView()
        {
            driver.Navigate().GoToUrl("https://br.tradingview.com/screener/");
            Thread.Sleep(3000);


            driver.FindElement(By.XPath("/html/body/div[3]/div/div[3]/table/thead/tr/th[1]/div")).Click();

            var element = driver.FindElement(By.TagName("body"));
            element.SendKeys(Keys.End);
            Thread.Sleep(2000);
            element.SendKeys(Keys.End);
            Thread.Sleep(2000);
            element.SendKeys(Keys.End);
            Thread.Sleep(2000);
            element.SendKeys(Keys.End);
            Thread.Sleep(2000);
            element.SendKeys(Keys.End);
            List<Datum> a = GetStocks();
            Thread.Sleep(3000);
            return a;
        }

        public static List<Datum> GetStocks()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new(HttpMethod.Get, "https://scanner.tradingview.com/brazil/scan");
            
            client.DefaultRequestHeaders.Add("TESTE", "OPA");
            Thread.Sleep(600);
            request.Headers.Add("TESTE2", "OPA");
            var kdfhdjk = client.DefaultRequestHeaders;

            string realResults = null;
            IEnumerable<string> searchResults;
            if(client.SendAsync(request).Result.Headers.TryGetValues("via", out searchResults))
            {
                realResults = searchResults.First();
            }
            string jsonStringResponse = client.SendAsync(request)
                                              .Result.Content.ReadAsStringAsync().Result;
            TradingViewResponseObject res = JsonConvert.DeserializeObject<TradingViewResponseObject>(jsonStringResponse);
            return res.Data;
        }

        public static List<string> GetStockSymbols()
        {
            HttpClient client = new HttpClient();
            List<string> symbols = new();
            string[] arr = new string[6] { "x", "y", "z", "0", "1", "2" };

            for (int i = 0; i < 6; i++)
            {

                string jsonStringResponse = client.GetAsync("https://sistemaswebb3-listados.b3.com.br/indexProxy/indexCall/GetStockIndex/eyJwYWdlTnVtYmVyIjo" + arr[i] + "LCJwYWdlU2l6ZSI6MTIwfQ==")
                                                  .Result.Content.ReadAsStringAsync().Result;
                jsonObjectResponse jsonObject = JsonConvert.DeserializeObject<jsonObjectResponse>(jsonStringResponse);
                foreach (var item in jsonObject.Results)
                {
                    symbols.Add(item.Code);
                }

            }

            return symbols;
        }


    }

    public class jsonObjectResponse
    {
        public Page Page { get; set; }
        public Header Header { get; set; }
        public List<Result> Results { get; set; }
    }

    public class Header
    {
        public DateTimeOffset Update { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
        public int Year { get; set; }
    }

    public class Page
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }

    public class Result
    {
        public string Company { get; set; }
        public string Spotlight { get; set; }
        public string Code { get; set; }
        public string Indexes { get; set; }
    }







}