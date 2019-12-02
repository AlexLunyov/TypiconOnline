using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Tests.Common;
using TypiconOnline.AppServices.Extensions;
using System.Linq;

namespace TypiconOnline.AppServices.Tests.OutputForm
{
    [TestFixture]
    public class WordMergerTest
    {
        private const string weekVar = "[таблица]";

        [Test]
        public void Merge()
        {
            using (WordprocessingDocument doc = OpenCopyWord(GetPath("week.docx"), GetPath("WordMergerTestResult.docx")))
            {
                //находим место для вставки
                var placeToPasteSchedule = doc.MainDocumentPart.Document.Body
                    .FindElementsByText(weekVar)
                    .FirstOrDefault();

                if (placeToPasteSchedule != null)
                {
                    //ищем нужный шаблон дня

                    using (var dayDoc = OpenWord(GetPath("0.docx"), false))
                    {
                        var body = dayDoc.MainDocumentPart.Document.Body.Elements();
                        foreach (var ins in body)
                        {
                            var clone = ins.CloneNode(true);
                            placeToPasteSchedule.InsertBeforeSelf(clone);
                        }

                        placeToPasteSchedule.Parent.RemoveChild(placeToPasteSchedule);

                        doc.Save();
                        //doc.SaveAs(GetPath("WordMergerTestResult.docx"));
                    }
                }
            }

            Assert.Pass("Success");
        }


        private string GetPath(string fileName) => Path.Combine(TestFileCommander.ExecFolder, "Data", "OutputForm", fileName);

        private WordprocessingDocument OpenCopyWord(string fileSource, string fileDestination)
        {
            if (string.IsNullOrEmpty(fileSource))
            {
                throw new ArgumentException("message", nameof(fileSource));
            }

            if (!File.Exists(fileSource))
            {
                throw new FileNotFoundException(fileSource);
            }

            File.Copy(fileSource, fileDestination, true);

            return WordprocessingDocument.Open(File.Open(fileDestination, FileMode.Open), true);
        }

        private WordprocessingDocument OpenWord(string fileName, bool isEditable)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            return WordprocessingDocument.Open(File.Open(fileName, FileMode.Open), isEditable);
        }
    }
}
