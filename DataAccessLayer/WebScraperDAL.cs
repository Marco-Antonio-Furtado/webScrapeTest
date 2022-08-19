using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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
        public static List<string> ScrapeInstagramWithDefaultAccount(bool headless, string profile)
        {

            FirefoxOptions options = new();

            //makes the browser invisible
            if (headless)
            {
                options.AddArgument("--headless");
            }

            FirefoxDriver driver = new FirefoxDriver(options);

            //opens the website and wait it load
            driver.Navigate().GoToUrl("https://www.instagram.com/");
            Thread.Sleep(4000);

            //waits and targets the username and password inputs
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            IWebElement username = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[1]/div/label/input")));
            IWebElement password = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[2]/div/label/input")));


            //writes the account's username and password and clicks to login
            username.SendKeys(DotEnv.DEFAULT_USERNAME);
            password.SendKeys(DotEnv.DEFAULT_PASSWORD);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[3]/button"))).Click();

            //handle pop ups
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/div/div/div/div/button"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]/button[2]"))).Click();

            //searches the user profile
            IWebElement search = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/nav/div[2]/div/div/div[2]/input")));
            search.SendKeys(profile);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/nav/div[2]/div/div/div[2]/div[3]/div/div[2]/div/div[1]/a/div"))).Click();

            //scrolls down to scrape more images
            //maybe the index could be a parameter, so the user could define how much they want to scroll
            List<string> sources = new();

            for (int i = 1; i < 16; i = i + 4)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div[1]/div/div/div/div[1]/div[1]/section/main/div/div[3]/article/div[1]/div/div[" + i + "]")));
                driver.ExecuteScript("window.scrollTo(0, 4000);");

                //targets all images on the page
                ReadOnlyCollection<IWebElement> imgs = driver.FindElements(By.TagName("img"));

                foreach (IWebElement img in imgs)
                {
                    //TA DANDO EXCEPTION AQUI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    sources.Add(img.GetAttribute("src").ToString());
                }
            }

            return sources;
        }

    }
}