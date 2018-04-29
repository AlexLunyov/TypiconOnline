using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelItemTest
    {
        [Test]
        public void ViewModelItem_ToJSON()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ElementViewModel));

            string json = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, GetModel());
                json = Encoding.UTF8.GetString(ms.ToArray());
            }

            Assert.IsNotEmpty(json);
            Assert.Pass(json);
        }

        private ElementViewModel GetModel()
        {
            return new ElementViewModel()
            {
                new ViewModelItem()
                {
                    Kind = ViewModelItemKind.Choir,
                    KindStringValue = "Хор",
                    Paragraphs = new List<ParagraphViewModel>()
                    {
                        ParagraphVMFactory.Create("Строка 1", "cs-ru"),
                        ParagraphVMFactory.Create("Строка 2", "cs-ru"),
                        ParagraphVMFactory.Create("Строка 3", "cs-ru"),
                    }
                },
                new ViewModelItem()
                {
                    Kind = ViewModelItemKind.Choir,
                    KindStringValue = "Хор",
                    Paragraphs = new List<ParagraphViewModel>()
                    {
                        ParagraphVMFactory.Create("Строка 4", "cs-ru"),
                        ParagraphVMFactory.Create("Строка 5", "cs-ru"),
                        ParagraphVMFactory.Create("Строка 6", "cs-ru"),
                    }
                },
                new ViewModelItem()
                {
                    Kind = ViewModelItemKind.Priest,
                    KindStringValue = "Священник",
                    Paragraphs = new List<ParagraphViewModel>()
                    {
                        ParagraphVMFactory.Create("Строка 7", "cs-ru")
                    }
                },
                new ViewModelItem()
                {
                    Kind = ViewModelItemKind.Choir,
                    KindStringValue = "Хор",
                    Paragraphs = new List<ParagraphViewModel>()
                    {
                        ParagraphVMFactory.Create("Строка 8", "cs-ru")
                    }
                }
            };
        }
    }
}
