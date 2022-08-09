using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Shared;


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
        public static List<string> ScrapeInstagramWithDefaultAccount()
        {

            FirefoxOptions options = new();

            //makes the browser invisible
            options.AddArgument("--headless");
            FirefoxDriver driverFox = new FirefoxDriver(options);

            //opens the website and wait it load
            driverFox.Navigate().GoToUrl("https://www.instagram.com/");
            Thread.Sleep(4000);


            //WebDriverWait wait = new WebDriverWait(driverFox, TimeSpan.FromSeconds(10));
            //IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//a/h3")));
            //username = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.CSS_SELECTOR, "input[name='username']")))
            //password = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.CSS_SELECTOR, "input[name='password']")))


            //DotEnv.DEFAULT_USERNAME
            //DotEnv.DEFAULT_PASSWORD

            //scrolls down to scrape more images

            //targets all images on the page
            var imgs = driverFox.FindElements(By.TagName("img"));
            List<string> sources = new();

            foreach (var img in imgs)
            {
                sources.Add(img.GetAttribute("src").ToString());
            }
            return sources;
        }

    }
}