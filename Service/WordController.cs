using System;
using System.Collections.Generic;
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

            Product[] products = {
                new Product(1, "Product 1", "111111", 10, "шт"),
                new Product(1, "Product 1", "111111", 10, "шт"),
                new Product(1, "Product 1", "111111", 10, "шт"),
                new Product(1, "Product 1", "111111", 10, "шт")
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
                builder.InsertCell();
                builder.InsertCell();
                builder.InsertCell();

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
                builder.RowFormat.HeightRule = HeightRule.Auto;
                builder.EndRow();

                table.AutoFit(AutoFitBehavior.AutoFitToWindow);

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
