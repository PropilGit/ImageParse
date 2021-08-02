using ImageParse.Service;
using System;

namespace ImageParse
{
    class Program
    {
        

        static void Main(string[] args)
        {
            WordController wC = new WordController();
            wC.onAddLog += AddLog;


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
