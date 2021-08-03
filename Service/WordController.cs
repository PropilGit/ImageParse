using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Words;
using Aspose.Words.Tables;
using ImageParse.Models;

namespace ImageParse.Service
{
    class WordController
    {
        Document doc;
        DocumentBuilder builder;

        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;

        public void CreateTable()
        {
            doc = new Document();
            builder = new DocumentBuilder(doc);

            byte[] image = File.ReadAllBytes("test2.jpg");
            Product[] products = {
                new Product(1, "Product 1", "111111", 10, "шт", image),
                new Product(1, "Product 1", "111111", 10, "шт", image),
                new Product(1, "Product 1", "111111", 10, "шт", image),
                new Product(1, "Product 1", "111111", 10, "шт", image)
            };

            AddRow(products);

            doc.Save("result.docx");
        }

        Table AddRow(Product[] products)
        {
            try
            {

                Table table = builder.StartTable();

                builder.InsertCell();
                builder.EndRow();
                //table.AutoFit(AutoFitBehavior.AutoFitToWindow);
                table.AllowAutoFit = false;

                foreach (var pr in products)
                {
                    builder.InsertCell();
                    builder.InsertImage(pr.Image);
                }
                builder.RowFormat.Height = 100;
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

                builder.EndTable();
                return table;
            }
            catch (Exception ex)
            {
                onAddLog("AddRow()[ids: " + products[0].Id + ", " + products[1].Id + ", " + products[2].Id + ", " + products[3].Id + ", " + "]: " + ex.Message);
                return null;
            }
        }
    }
}
