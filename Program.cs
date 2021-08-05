using ImageParse.Models;
using ImageParse.Service;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageParse
{
    class Program
    {
        

        static async Task Main(string[] args)
        {
            try
            {
                //WordController wC = new WordController();
                //wC.onAddLog += AddLog;

                //NPOIWordController wC = new NPOIWordController();
                //wC.onAddLog += AddLog;

                AsposeWordController wC = new AsposeWordController();
                wC.onAddLog += AddLog;

                //wC.CreateTable();
                //wC.SaveTable();


                //AngleSharpController asC = new AngleSharpController();
                //asC.onAddLog += AddLog;
                
                SeleniumController sC = new SeleniumController();
                sC.onAddLog += AddLog;

                byte[] errImage = File.ReadAllBytes("error.jpg");

                string dataPath = "data.txt";
                string[] dataLines;
                if (File.Exists(dataPath)) dataLines = File.ReadAllLines(dataPath);
                else return;

                int tableCounter = 1;
                for (int i = 0; i < dataLines.Length;) 
                {
                    wC.CreateTable();

                    for (int r = 0; r < 12; r++) // ROWS in file
                    {
                        if (i >= dataLines.Length) continue;
                        Product[] products = new Product[4];

                        for (int j = 0; j < 4; j++, i++) // ROW
                        {
                            if (i >= dataLines.Length) continue;
                            //Console.WriteLine("[" + i + "] Start");

                            products[j] = new Product(dataLines[i]);
                            if (products[j] == null)
                            {
                                AddLog("[" + i + "] Ошибка считывания товара");
                                products[j] = new Product(i, "ОШИБКА", "---", 0, "шт");
                                products[j].Image = errImage;
                                continue;
                            }
                            else
                            {
                                AddLog("[" + products[j].Id + "] " + products[j].Name);
                            }

                            products[j].Image = await sC.ParseProductImageAsync(products[j].Name);
                            if (products[j].Image == null)
                            {
                                products[j].Image = errImage;
                                AddLog("[" + products[j].Id + "] Ошибка парсинга изображения", true);
                            }
                        }

                        wC.AddRow(products);
                    }

                    wC.SaveTable(tableCounter);
                    AddLog("table" + tableCounter + " saved ===================================================================");
                    tableCounter++;
                }
                
                AddLog("Finish =========================================================================================");
                Console.ReadLine();
            }
            catch (Exception ex)
            {

            }
        }

        
        static void AddLog(string msg, bool isError = false)
        {
            if (isError) msg = "[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] Error: " + msg;

            Console.WriteLine("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "] " + msg);
        }
    }
}
