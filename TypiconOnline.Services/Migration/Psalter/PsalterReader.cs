using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.AppServices.Migration.Psalter
{
    public class PsalterRuReader : IPsalterReader
    {
        protected string KATHISMA_TEXT = "КАФИ́СМА";
        protected string PSALM_TEXT = "Псалом";
        protected string SLAVA_TEXT = "Слава:";

        //public string FolderPath { get; }
        public string Language { get; }

        int FileIndex;
        string[] FoundFiles;
        StreamReader Reader;
        string parsingString;

        public PsalterRuReader(string folderPath, string language)
        {
            if (!Directory.Exists(folderPath)) throw new DirectoryNotFoundException(folderPath);

            //находим все файлы с указанным языком
            FoundFiles = Directory.GetFiles(folderPath, $"*{language}*", SearchOption.TopDirectoryOnly);

            if (FoundFiles == null)
                throw new FileNotFoundException($"Не найдены файлы с указанным языком \"{language}\" в директории \"{folderPath}\".");

            //FolderPath = folderPath;
            Language = language;

            //инициируем reader первым файлом из полученного списка
            FileIndex = 0;
            Reader = new StreamReader(FoundFiles[FileIndex]);
        }

        public PsalterElementKind ElementType { get; private set; }

        public IPsalterElement Element { get; private set; }
        
        public bool Read()
        {
            if (Reader.EndOfStream)
            {
                Reader = GetNewReader();
            }

            bool eof = Reader.EndOfStream && string.IsNullOrEmpty(parsingString);

            if (!eof)
            {
                if (string.IsNullOrEmpty(parsingString))
                {
                    //если строка для распознавания пуста, читаем новый элемент
                    if (!Reader.EndOfStream)
                    {
                        parsingString = Reader.ReadLine();
                    }

                    InitiateParsing();
                }
                else
                {
                    ContinueParsing();
                }
            }
            
            return !eof;
        }

        private StreamReader GetNewReader()
        {
            if (FileIndex < FoundFiles.Count()-1)
            {
                FileIndex++;
                return new StreamReader(FoundFiles[FileIndex]);
            }

            return Reader;
        }

        private void InitiateParsing()
        {
            if (parsingString.StartsWith(KATHISMA_TEXT))
            {
                //кафизма
                ElementType = PsalterElementKind.Kathisma;
                Element = CreateKathisma() as IPsalterElement;
                parsingString = null;
            }
            else if (parsingString.StartsWith(PSALM_TEXT))
            {
                //псалом
                ElementType = PsalterElementKind.Psalm;
                Element = CreatePsalm() as IPsalterElement;
                parsingString = null;
            }
            else if (parsingString.StartsWith(SLAVA_TEXT))
            {
                //слава
                ElementType = PsalterElementKind.Slava;
                Element = null;
                parsingString = null;
            }
            else
            {
                //стих
                //смотрим предыдущее состояние
                if (ElementType == PsalterElementKind.Psalm
                    /*|| ElementType == PsalterElementKind.PsalmAnnotation*/)
                {
                    //Аннотация
                    ElementType = PsalterElementKind.PsalmAnnotation;
                    ContinueParsing();
                }
                else
                {
                    //Текст
                    ElementType = PsalterElementKind.PsalmText;
                    ContinueParsing();
                }
            }


        }

        private void ContinueParsing()
        {
            var fragments = parsingString.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string text = "";
            int? number = null;

            foreach (var str in fragments)
            {
                if (IsDigit(str, out int i))
                {
                    //цифра - значит начало стиха
                    if (number == null)
                    {
                        number = i;
                        RemoveFragment(str);
                    }
                    else
                    {
                        //новый стих, который нам уже не нужен
                        break;
                    }
                }
                else
                {
                    //просто текст - добавляем его
                    text += $"{str} ";
                    RemoveFragment(str);
                }
            }

            Element = CreateStihos(number, text);

            void RemoveFragment(string f)
            {
                int length = (parsingString.Length > f.Length) ? f.Length + 1 : f.Length;
                parsingString = parsingString.Remove(0, length);
            }
        }

        /// <summary>
        /// переопределяемый метод для определения, числовое ли значение в строковом параметре
        /// </summary>
        /// <param name="str"></param>
        /// <param name="i">выходное значение</param>
        /// <returns></returns>
        protected virtual bool IsDigit(string str, out int i)
        {
            return int.TryParse(str, out i);
        }

        private BookStihos CreateStihos(int? number, string text)
        {
            var stihos = new BookStihos() { StihosNumber = number };
            stihos.AddElement(Language, text);

            return stihos;
        }

        private Psalm CreatePsalm()
        {
            var numberString = parsingString.Replace(PSALM_TEXT, string.Empty).Replace(".", string.Empty);
            int.TryParse(numberString, out int number);

            return new Psalm()
            {
                Number = number
            };
        }

        private Kathisma CreateKathisma()
        {
            //парсим номер из имени файла
            string numberString = Path.GetFileNameWithoutExtension(FoundFiles[FileIndex]);
            numberString = numberString.Split(new Char[] { '.' }).FirstOrDefault();
            int.TryParse(numberString, out int number);

            //добавляем в качестве строкового значения полученную строку из Reader-a
            var numberName = new ItemText();
            numberName.AddElement(Language, parsingString);

            return new Kathisma()
            {
                Number = number,
                NumberName = numberName
            };
        }
    }
}
