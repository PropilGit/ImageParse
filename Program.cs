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
            WordController wC = new WordController();
            wC.onAddLog += AddLog;

            AngleSharpController asC = new AngleSharpController();
            asC.onAddLog += AddLog;



            string dataPath = "testdata.txt";
            string[] dataLines;
            if (File.Exists(dataPath)) dataLines = File.ReadAllLines(dataPath);
            else return;

            byte[] image = await asC.ParseProductImageAsync("http://yandex.ru/images/search?text=АНТЕННА%203G%20TELEOFIS%20RC30");
            //byte[] image = await asC.ParseProductImageAsync("http://yandex.ru");

            wC.CreateTable();


            Console.WriteLine("===================================================================");
            Console.ReadLine();
        }

        
        static void AddLog(string msg, bool isError = false)
        {
            if (isError) msg = "[Error]" + msg;

            Console.WriteLine(msg);
        }
    }
}
