using ImageParse.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageParse.Service
{
    class SeleniumController
    {
        WebDriver driver;
        WebDriverWait wait;

        HttpClient client;

        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;

        public SeleniumController()
        {
            this.driver = new ChromeDriver();
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            client = new HttpClient();
        }

        //https://ru.stackoverflow.com/questions/596145/%D0%9A%D0%B0%D0%BA-%D1%81%D0%BA%D0%B0%D1%87%D0%B0%D1%82%D1%8C-%D0%B2%D1%81%D0%B5-%D0%BA%D0%B0%D1%80%D1%82%D0%B8%D0%BD%D0%BA%D0%B8-%D1%81-%D1%81%D0%B0%D0%B9%D1%82%D0%B0-%D1%81
        public async Task<byte[]> ParseProductImageAsync(string name)
        {
            try
            {
                Thread.Sleep(5000);
                driver.Navigate().GoToUrl("https://yandex.ru/images/");
                // /html/body/header/div/div[2]/div[1]/form/div[1]/span/span/input
                TryFindElement("/html/body/header/div/div/div/form/div/span/span/input").SendKeys(name);
                // /html/body/header/div/div[2]/div[1]/form/div[2]/button
                TryFindElement("/html/body/header/div/div[2]/div[1]/form/div[2]/button").Click();

                string imgSource = TryFindElement("/html/body/div/div/div/div/div/div/div/a/img").GetAttribute("src");

                return await GetImgFile(imgSource);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IWebElement TryFindElement(string xPath)
        {
            try
            {
                return wait.Until(e => e.FindElement(By.XPath(xPath)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        async Task<byte[]> GetImgFile(string source)
        {
            try
            {
                return await client.GetByteArrayAsync(source);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
