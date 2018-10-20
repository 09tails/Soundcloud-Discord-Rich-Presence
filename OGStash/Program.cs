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
        static ChromeOptions options;
        static ChromeDriverService chromeDriverService;
        static IWebDriver driver;
        static bool skip;
        static bool run;
        static private CustomDiscordRPC.DiscordRpc.RichPresence presence;
        static CustomDiscordRPC.DiscordRpc.EventHandlers handlers;
        private static void ReadyCallback()
        {
            System.Console.WriteLine("Ready");
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            System.Console.WriteLine("Disconnect " + errorCode.ToString() + ": " + message);
        }
        
        private static void ErrorCallback(int errorCode, string message)
        {
            System.Console.WriteLine("Error " + errorCode.ToString() + ": " + message);
        }

        static string command;
        static bool notplaying = true;
        static void DoStuff()
        {
            //System.Console.WriteLine(driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/div/a/span[2]")).Text);
            //System.Console.WriteLine(driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/a")).Text);Thread.Sleep(5000);
            handlers = new CustomDiscordRPC.DiscordRpc.EventHandlers();
            handlers.readyCallback = ReadyCallback;
            handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;
            CustomDiscordRPC.DiscordRpc.RPCInit("503076155507605509", ref handlers, true, null);
            Colorful.Console.WriteWithGradient<char>($"Enter Command (help for help): \n", Color.Red, Color.Violet, 10);
            while (true)
            {
                try
                {
                    if (playing)
                    {

                    }
                    else
                    {
                        command = System.Console.ReadLine();
                    }

                    if (command.Contains("https://"))
                    {
                        if (playing == false)
                        {
                            driver.Navigate().GoToUrl(command);
                        }
                        Thread.Sleep(1000);
                        presence.details = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/div/a/span[2]")).Text;
                        presence.state = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/a")).Text;
                        presence.largeImageKey = "image";
                        CustomDiscordRPC.DiscordRpc.RPCUpdate(ref presence);
                        if (playing == false)
                        {
                            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[4]/div/div[2]/div[2]/div/div/div[1]/a")).Click();
                        }
                        currengsong = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/div/a/span[2]")).Text;
                        //https://soundcloud.com/stationsix/imsosickofthis
                        playing = true;
                    }
                    if (command == "skip")
                    {
                        driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/button[3]")).Click();
                        presence.details = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/div/a/span[2]")).Text;
                        presence.state = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/a")).Text;
                        presence.largeImageKey = "image";
                        CustomDiscordRPC.DiscordRpc.RPCUpdate(ref presence);
                    }
                    if (command == "help")
                    {
                        System.Console.WriteLine("Paste The URL To Play One.\nskip To Skip.");
                    }
                    presence.details = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/div/a/span[2]")).Text;
                    presence.state = driver.FindElement(By.XPath("//*[@id=\"app\"]/div[4]/div/div/div[3]/div[6]/div/div[1]/a")).Text;
                    presence.largeImageKey = "image";
                    CustomDiscordRPC.DiscordRpc.RPCUpdate(ref presence);
                }
                catch
                {

                }
            }
        }

        static string currengsong;
        static bool playing;
        static void Main(string[] args)
        {
            Colorful.Console.Title = "Soundcloud DRPC | Made By 9tails";
            Colorful.Console.WriteWithGradient<char>("Soundcloud DRPC | Made By 9tails\n\n", Color.Red, Color.Violet, 10);
            options = new ChromeOptions();
            options.AddArgument("--headless");
            chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(chromeDriverService);
            Thread thread = new Thread(DoStuff);
            thread.Start();
        }
    }
}
