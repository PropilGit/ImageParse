using ImageParse.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

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
        async Product ParseProduct(string seachRequest)
        {
            try
            {
                driver.Navigate().GoToUrl(seachRequest);
                string link = TryFindElement("/html/body/div[6]/div[1]/div[1]/div[1]/div/div[1]/div/a/img").GetAttribute("src");
                //byte[] image = client.
                var result = await client.GetAsync(link);
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

    }
}
