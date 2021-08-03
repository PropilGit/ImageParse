using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.XPath;
using ImageParse.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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

        public async Task<byte[]> ParseProductImageAsync(string url)
        {
            try
            {
                //string html = await GetHtmlWithHttpClient(url);
                //string htmlT = html;


                var document = await GetHtml(url);
                IDocument test = document;

                //                                          /html/body/div[4]/div[1]/div[1]/div[1]/div/div[1]/div/a/img
                var node = document.Body.SelectSingleNode("/html/body/div[4]/div/div/div/div/div/div/a/img");
                //var cells = document.QuerySelector("img");
                var imgSource = node.Text();
                int i = 1;
                //var result = await client.GetAsync(link);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

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
    }
}
