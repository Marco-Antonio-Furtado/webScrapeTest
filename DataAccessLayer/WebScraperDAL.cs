using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DataAccessLayer
{


    public class WebScraperDAL
    {
        public static List<string> ScrapeInstagramSelenium(string userUrl)
        {
            var options = new FirefoxOptions();
            options.AddArgument("--headless");
            var driverFox = new FirefoxDriver(options);

            driverFox.Navigate().GoToUrl("https://www.instagram.com/neymarjr");
            //scroll down to scrape more images

            Thread.Sleep(4000);
            //target all images on the page
            var imgs = driverFox.FindElements(By.TagName("img"));
            List<string> sources = new();

            foreach (var img in imgs)
            {
                sources.Add(img.GetAttribute("src").ToString());
            }
            return sources;

            //images = driver.find_elements_by_tag_name('img');
            //images = [image.get_attribute('src') for image in images];
            //images = images[:-2];

            //print('Number of scraped images: ', len(images))

        }

    }
}