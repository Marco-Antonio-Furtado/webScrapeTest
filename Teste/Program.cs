﻿// See https://aka.ms/new-console-template for more information
using BussinessLogicalLayer;


Console.WriteLine("Hello, World!");

foreach (var src in WebScraperBLL.Scrape())
{
    System.Console.WriteLine("SOURCE: " + src);
}

Thread.Sleep(99999999);