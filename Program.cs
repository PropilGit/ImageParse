using ImageParse.Service;
using System;
using System.IO;

namespace ImageParse
{
    class Program
    {
        

        static void Main(string[] args)
        {
            WordController wC = new WordController();
            wC.onAddLog += AddLog;

            SeleniumController sC = new SeleniumController();
            sC.onAddLog += AddLog;



            string dataPath = "testdata.txt";
            string[] dataLines;
            if (File.Exists(dataPath)) dataLines = File.ReadAllLines(dataPath);
            else return;




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
