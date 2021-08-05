using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.XPath;
using ImageParse.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageParse.Service
{
    class AngleSharpController
    {
        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;


        HttpClient client;

        public AngleSharpController()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client = new HttpClient();
        }

        public async Task<byte[]> ParseProductImageAsync(string name)
        {
            try
            {
                string url = CreateUrl(name);
                string imgSource = await GetImgSource(url);

                return await GetImgFile("https:"+ imgSource);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /*
        async Task<string> GetHtmlWithHttpClient(string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                string source = null;

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                }

                return source;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        */

        string CreateUrl(string name)
        {
            //http://yandex.ru/images/search?text=АНТЕННА%203G%20TELEOFIS%20RC30
            return "http://yandex.ru/images/search?text=" + name.Replace(" ", "%20");

        }
        async Task<IDocument> GetHtml(string url)
        {
            try
            {
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                return await context.OpenAsync(url);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        async Task<string> GetImgSource(string url)
        {
            Thread.Sleep(10000);
            var document = await GetHtml(url);

            //                                         /html/body/div[4]/div[1]/div[1]/div[1]/div/div[1]/div/a/img
            //                                         /html/body/div[4]/div/div/div/div/div/div/a/img
            var node = document.Body.SelectSingleNode("/html/body/div/div/div/div/div/div/div/a/img");
            var imgHtml = node.ToHtml();

            int start = imgHtml.IndexOf(" src=") + 6;
            int end = imgHtml.IndexOf(" data-error-handler=") - 1;
            return imgHtml.Substring(start, end - start);
            
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

        string FindRegExp(string text, string regExp)
        {
            Regex regex = new Regex(regExp);
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0)
            {
                return matches[0].Value;
            }
            return "";
        }
    }
}
