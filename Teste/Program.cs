// See https://aka.ms/new-console-template for more information
using BussinessLogicalLayer;


Console.WriteLine("Hello, World!");
/*
List<string> symbols = WebScraperBLL.Scrape();

for (int i = 0; i < 656; i++)
{
    System.Console.WriteLine(symbols[i]);

}
*/

foreach (var item in WebScraperBLL.Tradingview())
{
    foreach (var stock in item.D)
    {
        System.Console.WriteLine(stock.Symbol + stock.Preco.ToString() + stock.Currency);
    }
}

