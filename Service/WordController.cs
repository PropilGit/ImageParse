using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ImageParse.Models;
using Word = Microsoft.Office.Interop.Word;

namespace ImageParse.Service
{
    class WordController
    {
        Word.Application app;
        object missing = System.Reflection.Missing.Value;
        Word.Document doc;
        Word.Table table;     

        public delegate void AddLog(string message, bool isError = true);
        public event AddLog onAddLog;

        public void CreateTable()
        {
            app = new Word.Application();
            app.Visible = true;
            //doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            doc = app.Documents.Add();
            doc.Activate();


            table = doc.Tables.Add(doc.Paragraphs[0].Range, 2, 4, ref missing, ref missing); //1
            table.Borders.Enable = 1;
            table.AllowAutoFit = false;
        }

        public void SaveTable()
        {
            /*Object fileName = @"result.doc";
            Object fileFormat = Word.WdSaveFormat.wdFormatDocument;
            Object lockComments = false;
            Object password = "";
            Object addToRecentFiles = false;
            Object writePassword = "";
            Object readOnlyRecommended = false;
            Object embedTrueTypeFonts = false;
            Object saveNativePictureFormat = false;
            Object saveFormsData = false;
            Object saveAsAOCELetter = Type.Missing;
            Object encoding = Type.Missing;
            Object insertLineBreaks = Type.Missing;
            Object allowSubstitutions = Type.Missing;
            Object lineEnding = Type.Missing;
            Object addBiDiMarks = Type.Missing;
            doc.SaveAs(ref fileName, ref fileFormat, ref lockComments,
                ref password, ref addToRecentFiles, ref writePassword,
                ref readOnlyRecommended, ref embedTrueTypeFonts,
                ref saveNativePictureFormat, ref saveFormsData,
                ref saveAsAOCELetter, ref encoding, ref insertLineBreaks,
                ref allowSubstitutions, ref lineEnding, ref addBiDiMarks);*/
            doc.SaveAs(@"C:\result2.doc");
        }

        public void AddRow(Product[] products)
        {/*
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
            }*/
        }
    }
}
