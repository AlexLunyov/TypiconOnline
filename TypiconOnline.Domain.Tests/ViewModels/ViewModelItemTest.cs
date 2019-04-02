using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Output.Factories;

namespace TypiconOnline.Domain.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelItemTest
    {
        [Test]
        public void ViewModelItem_ToJSON()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(OutputElementCollection));

            string json = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, GetModel());
                json = Encoding.UTF8.GetString(ms.ToArray());
            }

            Assert.IsNotEmpty(json);
            Assert.Pass(json);
        }

        private OutputElementCollection GetModel()
        {
            return new OutputElementCollection()
            {
                new OutputSection()
                {
                    Kind = ElementViewModelKind.Choir,
                    KindText = new ItemText() { Items = new List<ItemTextUnit>() { new ItemTextUnit() { Language = "cs-ru", Text = "Хор" } } },
                    Paragraphs = new List<ItemTextNoted>()
                    {
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 1", Language = "cs-ru"}
                            }
                        },
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 2", Language = "cs-ru"}
                            }
                        },
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 3", Language = "cs-ru"}
                            }
                        }
                    }
                },
                new OutputSection()
                {
                    Kind = ElementViewModelKind.Choir,
                    KindText = new ItemText() { Items = new List<ItemTextUnit>() { new ItemTextUnit() { Language = "cs-ru", Text = "Хор" } } },
                    Paragraphs = new List<ItemTextNoted>()
                    {
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 4", Language = "cs-ru"}
                            }
                        },
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 5", Language = "cs-ru"}
                            }
                        },
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 6", Language = "cs-ru"}
                            }
                        }
                    }
                },
                new OutputSection()
                {
                    Kind = ElementViewModelKind.Priest,
                    KindText = new ItemText() { Items = new List<ItemTextUnit>() { new ItemTextUnit() { Language = "cs-ru", Text = "Священник" } } },
                    Paragraphs = new List<ItemTextNoted>()
                    {
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 7", Language = "cs-ru"}
                            }
                        }
                    }
                },
                new OutputSection()
                {
                    Kind = ElementViewModelKind.Choir,
                    KindText = new ItemText() { Items = new List<ItemTextUnit>() { new ItemTextUnit() { Language = "cs-ru", Text = "Хор" } } },
                    Paragraphs = new List<ItemTextNoted>()
                    {
                        new ItemTextNoted() { Items = new List<ItemTextUnit>()
                            {
                                new ItemTextUnit() { Text = "Строка 8", Language = "cs-ru"}
                            }
                        }
                    }
                }
            };
        }
    }
}
