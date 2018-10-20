using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Colorful;
using System.Drawing;
using System.Collections.ObjectModel;

namespace OGStash
{
    class Program
    {
        static string ReadLine(string FilePath, int LineNumber)
        {
            string result = "";
            try
            {
                if (File.Exists(FilePath))
                {
                    using (StreamReader _StreamReader = new StreamReader(FilePath))
                    {
                        for (int a = 0; a < LineNumber; a++)
                        {
                            result = _StreamReader.ReadLine();
                        }
                    }
                }
            }
            catch
            {

            }
            return result;
        }
        static bool run;
        static ChromeOptions options;
        static ChromeDriverService chromeDriverService;
        static IWebDriver driver;
        static int sellingcnt;
        static int introcnt;
        static int replystxtcount;
        static int currentreply;
        static int lastreply = 1;
        static List<IWebElement> elemlist = new List<IWebElement>();
        static void Reply()
        {
            if (currentreply == lastreply)
            {

            }
            else
            {
                driver.Navigate().GoToUrl("https://ogstash.com/Forum-Introductions");
                Thread.Sleep(1000);
                foreach (IWebElement elem in driver.FindElements(By.ClassName(" subject_new")))
                {
                    elemlist.Add(elem);
                    Colorful.Console.WriteLine(elem.Text);
                }
                foreach (IWebElement elem1 in elemlist)
                {
                    elem1.Click();
                    Thread.Sleep(2000);
                    Colorful.Console.WriteLine(driver.FindElement(By.ClassName("post_body")).Text);
                    driver.Navigate().GoToUrl("https://ogstash.com/Forum-Introductions");
                }
            }
            Colorful.Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Colorful.Console.WriteWithGradient<char>("OGStash.com Bot | Made By 9tails\n\n", Color.Red, Color.Violet, 10);
            Colorful.Console.Title = $"OGStash.com Bot | Made By 9tails | Stats: Replied To {sellingcnt} Selling Threads, And {introcnt} Introduction Threads.";
            replystxtcount = File.ReadAllLines("replys.txt").Count();
            Colorful.Console.WriteWithGradient<char>($"Amount Of Replys Available To Send: {replystxtcount}\n", Color.Blue, Color.Violet, 10);
            options = new ChromeOptions();
            options.AddArgument("--headless");
            chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(chromeDriverService);
            driver.Navigate().GoToUrl("https://ogstash.com/member.php?action=login");
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/form/table/tbody/tr[3]/td[2]/input")).SendKeys(ReadLine(@"info.txt", 1));
            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/form/table/tbody/tr[4]/td[2]/input")).SendKeys(ReadLine(@"info.txt", 2));
            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/form/div/input")).Click();
            Thread.Sleep(3000);
            Reply();
        }
    }
}
