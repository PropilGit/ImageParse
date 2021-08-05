using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Words;
using Aspose.Words.Tables;
using ImageParse.Models;

namespace ImageParse.Service
{
    class AsposeWordController
    {
        Document doc;
        DocumentBuilder builder;
        Table table;

        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;

        public void CreateTable()
        {
            doc = new Document();
            builder = new DocumentBuilder(doc);

            table = builder.StartTable();
            builder.InsertCell();
            builder.EndRow();
            table.AllowAutoFit = false;

            /*byte[] image = File.ReadAllBytes("test2.jpg");
            Product[] products = {
                new Product(1, "Product 1", "111111", 10, "шт", image),
                new Product(1, "Product 1", "111111", 10, "шт", image),
                new Product(1, "Product 1", "111111", 10, "шт", image),
                new Product(1, "Product 1", "111111", 10, "шт", image)
            };*/

            //AddRow(products);

            //doc.Save("result.docx");
        }

        public void SaveTable(int number)
        {
            builder.EndTable();
            doc.Save("result/table" + number + ".docx");
        }

        public void AddRow(Product[] products)
        {
            try
            {
                foreach (var pr in products)
                {
                    builder.InsertCell();
                    builder.InsertImage(pr.Image, 100, 100);
                }
                //builder.RowFormat.Height = 100;
                builder.EndRow();


                foreach (var pr in products)
                {
                    builder.InsertCell();
                    builder.Write(
                        pr.Name + "\n" +
                        pr.InvNum + "\n" +
                        pr.Count + " " + pr.MeasureUnit);
                }
                builder.EndRow();
            }
            catch (Exception ex)
            {
                onAddLog("AddRow()[ids: " + products[0].Id + ", " + products[1].Id + ", " + products[2].Id + ", " + products[3].Id + ", " + "]: " + ex.Message);
            }
        }
    }
}
