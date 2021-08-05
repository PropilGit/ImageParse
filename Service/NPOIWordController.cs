using ImageParse.Models;
using NPOI.XWPF.UserModel;
using System;
using System.IO;

namespace ImageParse.Service
{
    class NPOIWordController
    {
        //Word.Application app;
        object missing = System.Reflection.Missing.Value;
        XWPFDocument doc;
        //XWPFTable table;

        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;

        public void CreateTable()
        {
            doc = new XWPFDocument();
            XWPFTable table = doc.CreateTable(1, 4);
        }

        public void SaveTable()
        {
            FileStream out1 = new FileStream("result.docx", FileMode.Create);
            doc.Write(out1);
            out1.Close();
        }

        public void AddRow(Product[] products)
        {
            XWPFTable table = doc.CreateTable(2, 4);
            try
            {
                for (int i = 0; i < products.Length; i++)
                {
                    var par = table.GetRow(0).GetCell(i).AddParagraph();
                    var run = par.CreateRun();

                    table.GetRow(1).GetCell(i).SetText(
                        products[i].Name + "\n" +
                        products[i].InvNum + "\n" +
                        products[i].Count + " " + products[i].MeasureUnit);

                    /*using (MemoryStream stream = new MemoryStream())
                    {
                        run.AddPicture(stream.Write(products[i].Image, 0, products[i].Image.Length), 6, products[i].Id.ToString(), 100, 100);
                    }
                    run.AddPicture(products[i].Image, 6, products[i].Id.ToString(), 100, 100);*/
                }
                //builder.RowFormat.Height = 100;
            }
            catch (Exception ex)
            {
                onAddLog("AddRow()[ids: " + products[0].Id + ", " + products[1].Id + ", " + products[2].Id + ", " + products[3].Id + ", " + "]: " + ex.Message);
            }
        }
    }
}
