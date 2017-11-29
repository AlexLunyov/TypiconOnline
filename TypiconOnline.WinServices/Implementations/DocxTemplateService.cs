using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.WinServices.Interfaces;
using TypiconOnline.WinServices.Messaging;

namespace TypiconOnline.WinServices.Implementations
{
    public class DocxTemplateService : IDocxTemplateService
    {
        IOktoikhContext oktoikhContext;

        public DocxTemplateService(IOktoikhContext oktoikhContext)
        {
            this.oktoikhContext = oktoikhContext ?? throw new ArgumentNullException("IOktoikhContext");
        }

        public HandleTemplateResponse Operate(HandleTemplateRequest request)
        {
            HandleTemplateResponse response = new HandleTemplateResponse();

            try
            {
                if (request.ScheduleWeek == null
                    || request.ScheduleWeek.Days.Count == 0)
                    throw new ArgumentNullException("request.ScheduleWeek");

                if (!File.Exists(request.FileTemplateName))
                {
                    throw new FileNotFoundException(request.FileTemplateName);
                }

                DateTime date = request.ScheduleWeek.Days[0].Date;

                if (!Directory.Exists(request.OutputFolderPath))
                    Directory.CreateDirectory(request.OutputFolderPath);

                string fileName = GetFileName(request.OutputFolderPath, request.ScheduleFileStart, date);
                if (File.Exists(fileName))
                    File.Delete(fileName);

                File.Copy(request.FileTemplateName, fileName);

                var scheduleWeekViewer = new DocxScheduleWeekViewer(fileName, request.DaysPerTable);

                scheduleWeekViewer.Execute(request.ScheduleWeek);

                response.Message = "Печатная версия была успешно сохранена.";

                if (request.OpenFileAfterHandling)
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = fileName;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
            }
            catch (Exception ex) when (ex is ArgumentNullException 
                                    || ex is IOException 
                                    || ex is InvalidOperationException
                                    || ex is FileNotFoundException)
            {
                response.Exception = ex;
            }

            return response;
        }

        private string GetFileName(string folderPath, string fileStart, DateTime date)
        {
            string fileName = $"{fileStart} {date.ToString("yyyy-MM-dd")} {date.AddDays(6).ToString("yyyy-MM-dd")} {oktoikhContext.GetWeekName(date, true)}.docx";
            return Path.Combine(folderPath, fileName);
        }
    }
}
