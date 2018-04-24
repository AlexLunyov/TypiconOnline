using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using WordPressSharp;
using WordPressSharp.Models;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.WinServices;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.AppServices.Common;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Interfaces;
using System.Collections.Generic;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.AppServices.Services;
using TypiconOnline.WinServices.Interfaces;
using TypiconOnline.WinServices.Messaging;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.WinForms
{
    public partial class StartForm : Form
    {
        //private ScheduleHandling.ScheduleHandler _sh = null;
        private IUnitOfWork _unitOfWork;
        private IScheduleService _scheduleService;
        BookStorage _bookStorage;
        TypiconEntity _typiconEntity;
        ITypiconEntityService _typiconEntityService;
        IDocxTemplateService _docxTemplateService;

        CustomParamsCollection<IRuleApplyParameter> CustomParameters { get; set; } = new CustomParamsCollection<IRuleApplyParameter>();

        private DateTime _selectedDate;
        private const string _scheduleFileStart = "РАСПИСАНИЕ ";

        public StartForm()
        {
            InitializeComponent();
            InitializeIoC();
            //_sh = new ScheduleHandling.ScheduleHandler(Properties.Settings.Default.ScheduleDBConnectionString);
        }

        private void InitializeIoC()
        {
            var container = new RegisterByContainer().Container;

            _unitOfWork = container.GetInstance<IUnitOfWork>();

            _typiconEntityService = container.With(_unitOfWork).GetInstance<ITypiconEntityService>();

            GetTypiconEntityResponse response = _typiconEntityService.GetTypiconEntity(1);// _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            _typiconEntity = response.TypiconEntity;

            _bookStorage = new BookStorage(
                container.With(_unitOfWork).GetInstance<IEvangelionContext>(),
                container.With(_unitOfWork).GetInstance<IApostolContext>(),
                container.With(_unitOfWork).GetInstance<IOldTestamentContext>(),
                container.With(_unitOfWork).GetInstance<IPsalterContext>(),
                container.With(_unitOfWork).GetInstance<IOktoikhContext>(),
                container.With(_unitOfWork).GetInstance<ITheotokionAppContext>(),
                container.With(_unitOfWork).GetInstance<IEasterContext>(),
                container.With(_unitOfWork).GetInstance<IKatavasiaContext>());

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            IRuleSerializerRoot serializerRoot = container.With(_bookStorage).GetInstance<IRuleSerializerRoot>();
            var settingsFactory = container.GetInstance<IRuleHandlerSettingsFactory>();

            _scheduleService = container.With(settingsFactory).With(serializerRoot).GetInstance<IScheduleService>();

            _docxTemplateService = container.With(_bookStorage.Oktoikh).GetInstance<IDocxTemplateService>();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            textBoxFilePath.Text = Properties.Settings.Default.OutputDirectoryPath;
            textBoxRulePath.Text = Properties.Settings.Default.RulesFolder;
            textBoxTemplatePath.Text = Properties.Settings.Default.TemplateFilePath;
            textBoxBaseUrl.Text = Properties.Settings.Default.BaseUrl;
            textBoxUserName.Text = Properties.Settings.Default.Username;
            textBoxPassword.Text = Properties.Settings.Default.Password;

            checkBoxDocx.Checked = Properties.Settings.Default.IsDocxChecked;
            checkBoxIsDocxOpen.Enabled = Properties.Settings.Default.IsDocxChecked;
            if (checkBoxIsDocxOpen.Enabled)
                checkBoxIsDocxOpen.Checked = Properties.Settings.Default.IsDocxOpen;
            checkBoxIsBigDocxOpen.Enabled = Properties.Settings.Default.IsBigDocxChecked;
            if (checkBoxIsBigDocxOpen.Enabled)
                checkBoxIsBigDocxOpen.Checked = Properties.Settings.Default.IsBigDocxOpen;
            checkBoxTxt.Checked = Properties.Settings.Default.IsTxtChecked;
            checkBoxWordpress.Enabled = Properties.Settings.Default.IsTxtChecked;
            if (checkBoxWordpress.Enabled)
                checkBoxWordpress.Checked = Properties.Settings.Default.IsWrodpressChecked;

            GetNextWeekNameFromFileDirectory();
            CheckEnablingExecuteButton();
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            FillDateCaptions();
        }

        private void radioButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            //panelCustom.Visible = radioButtonCustom.Checked;
            panelCustom.Enabled = radioButtonCustom.Checked;
            FillDateCaptions();
        }

        private void checkBoxTxt_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWordpress.Enabled = checkBoxTxt.Checked;
            if (!checkBoxTxt.Checked)
                checkBoxWordpress.Checked = false;

            CheckEnablingExecuteButton();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.OutputDirectoryPath = textBoxFilePath.Text;
            Properties.Settings.Default.TemplateFilePath = textBoxTemplatePath.Text;
            Properties.Settings.Default.RulesFolder = textBoxRulePath.Text;
            Properties.Settings.Default.BaseUrl = textBoxBaseUrl.Text;
            Properties.Settings.Default.Username = textBoxUserName.Text;
            Properties.Settings.Default.Password = textBoxPassword.Text;
            Properties.Settings.Default.IsDocxChecked = checkBoxDocx.Checked;
            Properties.Settings.Default.IsDocxOpen = checkBoxIsDocxOpen.Checked;
            Properties.Settings.Default.IsBigDocxChecked = checkBoxBigDocx.Checked;
            Properties.Settings.Default.IsBigDocxOpen = checkBoxIsBigDocxOpen.Checked;
            Properties.Settings.Default.IsTxtChecked = checkBoxTxt.Checked;
            Properties.Settings.Default.IsWrodpressChecked = checkBoxWordpress.Checked;

            Properties.Settings.Default.Save();
        }

        private void monthCalendarXML_DateSelected(object sender, DateRangeEventArgs e)
        {
            FillCalendar();
        }

        private void buttonPathSettings_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogSettings.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = folderBrowserDialogSettings.SelectedPath;
            }
        }

        #region Functions
        
        private void FillCalendar()
        {
            DateTime date = monthCalendarXML.SelectionRange.Start;

            //labelNextScheduleWeek.Text = OktoikhCalculator.GetSundayName(date/*_selectedDate*/);

            while (monthCalendarXML.SelectionStart.DayOfWeek != DayOfWeek.Monday)
            {
                monthCalendarXML.SelectionStart = monthCalendarXML.SelectionStart.AddDays(-1);
            }

            monthCalendarXML.SelectionEnd = date;

            while (monthCalendarXML.SelectionEnd.DayOfWeek != DayOfWeek.Sunday)
            {
                monthCalendarXML.SelectionEnd = monthCalendarXML.SelectionEnd.AddDays(1);
            }

            _selectedDate = monthCalendarXML.SelectionRange.Start;
        }

        private void FillDateCaptions()
        {
            if (radioButtonNext.Checked)
            {
                GetNextWeekNameFromFileDirectory();
            }
            else
            {
                FillCalendar();
            }
        }

        private void GetNextWeekNameFromFileDirectory()
        {
            if ((textBoxFilePath.Text != "") && Directory.Exists(textBoxFilePath.Text))
            {
                string[] allFoundFiles = Directory.GetFiles(textBoxFilePath.Text, _scheduleFileStart + "*", SearchOption.TopDirectoryOnly);

                //парсим
                if (allFoundFiles != null)
                {
                    IOrderedEnumerable<string> allSortedFoundFiles = allFoundFiles.OrderByDescending(k => k);

                    string foundFile = Path.GetFileName(allSortedFoundFiles.First());

                    //удаляем "РАСПИСАНИЕ " и "РАСПИСАНИЕ Б"
                    if (foundFile.StartsWith(Properties.Settings.Default.BigScheduleFileStart))
                    {
                        foundFile = foundFile.Replace(Properties.Settings.Default.BigScheduleFileStart, "");
                    }
                    if (foundFile.StartsWith(Properties.Settings.Default.ScheduleFileStart))
                    {
                        foundFile = foundFile.Replace(Properties.Settings.Default.ScheduleFileStart, "");
                    }

                    string[] splittedFileName = foundFile.Split(new Char[] { ' ' });
                    try
                    {
                        DateTime date = DateTime.Parse(splittedFileName[2]);

                        _selectedDate = date.AddDays(1);

                        //labelNextScheduleWeek.Text = OktoikhCalculator.GetSundayName(_selectedDate);
                    }
                    catch
                    {
                        _selectedDate = DateTime.Today;
                        //labelNextScheduleWeek.Text = OktoikhCalculator.GetSundayName(_selectedDate);
                    }
                }
                else
                {
                    //labelNextScheduleWeek.Text = OktoikhCalculator.GetSundayName(_selectedDate);
                    _selectedDate = DateTime.Today;
                }
            }
        }

        private void Execute()
        {
            if (textBoxFilePath.Text == "")
            {
                MessageBox.Show("Задайте путь к папке для сохранения файлов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //try
            //{
                GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
                {
                    Date = SelectedDate,
                    Typicon = _typiconEntity,
                    Handler = new ScheduleHandler(),
                    Language = "cs-ru",
                    ThrowExceptionIfInvalid = checkBoxException.Checked,
                    ApplyParameters = CustomParameters,
                    CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
                };

                GetScheduleWeekResponse weekResponse = _scheduleService.GetScheduleWeek(weekRequest);

                _unitOfWork.SaveChanges();

                //string messageString = "";

                if (checkBoxDocx.Checked)
                {
                    HandleTemplateRequest request = new HandleTemplateRequest()
                    {
                        FileTemplateName = Properties.Settings.Default.TemplateFilePath,
                        OutputFolderPath = Properties.Settings.Default.OutputDirectoryPath,
                        ScheduleFileStart = Properties.Settings.Default.ScheduleFileStart,
                        ScheduleWeek = weekResponse.Week,
                        OpenFileAfterHandling = checkBoxIsDocxOpen.Checked,
                    };

                    var response = _docxTemplateService.Operate(request);

                    if (response.Exception != null)
                    {
                        MessageBox.Show(response.Exception.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //if (textBoxTemplatePath.Text == "")
                    //{
                    //    MessageBox.Show("Определите файл docx шаблона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //string fileTemplateName = textBoxTemplatePath.Text;
                    //string fileOutputName = GetFileName(_selectedDate) + ".docx";

                    //if (File.Exists(fileOutputName))
                    //    File.Delete(fileOutputName);
                    //string dirPath = Path.GetDirectoryName(fileOutputName);
                    //if (!Directory.Exists(dirPath))
                    //    Directory.CreateDirectory(dirPath);
                    //File.Copy(fileTemplateName, fileOutputName);

                    //DocxScheduleWeekViewer docxViewer = new DocxScheduleWeekViewer(fileOutputName);

                    //docxViewer.Execute(weekResponse.Week);

                    //messageString += "\nПечатная версия была успешно сохранена. ";

                    FillDateCaptions();

                    //if (checkBoxIsDocxOpen.Checked)
                    //{
                    //    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    //    proc.StartInfo.FileName = fileOutputName;
                    //    proc.StartInfo.UseShellExecute = true;
                    //    proc.Start();
                    //}
                }

                if (checkBoxBigDocx.Checked)
                {
                    HandleTemplateRequest request = new HandleTemplateRequest()
                    {
                        FileTemplateName = Properties.Settings.Default.BigTemplateFilePath,
                        OutputFolderPath = Properties.Settings.Default.OutputDirectoryPath,
                        ScheduleFileStart = Properties.Settings.Default.BigScheduleFileStart,
                        DaysPerTable = 4,
                        ScheduleWeek = weekResponse.Week,
                        OpenFileAfterHandling = checkBoxIsBigDocxOpen.Checked,
                    };

                    var response = _docxTemplateService.Operate(request);

                    if (response.Exception != null)
                    {
                        MessageBox.Show(response.Exception.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //if (textBoxTemplatePath.Text == "")
                    //{
                    //    MessageBox.Show("Определите файл docx шаблона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //string fileTemplateName = textBoxTemplatePath.Text;
                    //string fileOutputName = GetFileName(_selectedDate) + ".docx";

                    //if (File.Exists(fileOutputName))
                    //    File.Delete(fileOutputName);
                    //string dirPath = Path.GetDirectoryName(fileOutputName);
                    //if (!Directory.Exists(dirPath))
                    //    Directory.CreateDirectory(dirPath);
                    //File.Copy(fileTemplateName, fileOutputName);

                    //DocxScheduleWeekViewer docxViewer = new DocxScheduleWeekViewer(fileOutputName);

                    //docxViewer.Execute(weekResponse.Week);

                    //messageString += "\nПечатная версия была успешно сохранена. ";

                    FillDateCaptions();

                    //if (checkBoxIsDocxOpen.Checked)
                    //{
                    //    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    //    proc.StartInfo.FileName = fileOutputName;
                    //    proc.StartInfo.UseShellExecute = true;
                    //    proc.Start();
                    //}
                }

                if (checkBoxTxt.Checked)
                {
                    HtmlScheduleWeekViewer htmlViewer = new HtmlScheduleWeekViewer();
                    htmlViewer.Execute(weekResponse.Week);

                    //textBoxResult.Clear();
                    //textBoxResult.AppendText(htmlViewer.ResultString);

                    if (checkBoxWordpress.Checked)
                    {
                        using (FormPassword testDialog = new FormPassword())
                        {
                            // Show testDialog as a modal dialog and determine if DialogResult = OK.
                            if (testDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                if (testDialog.textBoxPassword.Text == Properties.Settings.Default.PasswordToPostWordpress)
                                {
                                    DateTime datePub = _selectedDate.AddDays(-8).AddHours(17).AddMinutes(30);

                                    PostToWordPress(weekResponse.Week.Name, htmlViewer.ResultString, datePub);

                                    MessageBox.Show($"Запись размещена на сайте. Дата публикации - {datePub.ToString("hh:mm dd MMMM yyyy года.")}", "Информация",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    // Read the contents of testDialog's TextBox.
                                    MessageBox.Show("Неверный пароль. Расписание не было опубликовано на сайте.", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                        }
                    }

                }

            //MessageBox.Show(messageString, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #region to TextBox

            textBoxResult.Clear();

            textBoxResult.AppendText(weekResponse.Week.Name + Environment.NewLine);

            foreach (ScheduleDay day in weekResponse.Week.Days)
            {
                textBoxResult.AppendText("--------------------------" + Environment.NewLine);
                textBoxResult.AppendText(day.Date.ToShortDateString() + Environment.NewLine);
                textBoxResult.AppendText(day.Name + Environment.NewLine);
                textBoxResult.AppendText("Знак службы: " + day.SignName + Environment.NewLine);
                foreach (var el in day.Schedule.Worships)
                {
                    string str = !string.IsNullOrEmpty(el.Time) ? $"{el.Time} " : "";
                 
                    str += $"{el.Name} {el.AdditionalName + Environment.NewLine}";

                    textBoxResult.AppendText(str);
                }
            }

                #endregion
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void CheckEnablingExecuteButton()
        {
            buttonExecute.Enabled = checkBoxDocx.Checked || checkBoxBigDocx.Checked|| checkBoxTxt.Checked;
        }

        private void PostToWordPress(string title, string content, DateTime publishDate)
        {
            var post = new Post
            {
                PostType = "schedule",
                Title = title,
                Content = content,
                PublishDateTime = publishDate
            };

            using (var client = new WordPressClient(new WordPressSiteConfig
            {
                BaseUrl = textBoxBaseUrl.Text,
                Username = textBoxUserName.Text,
                Password = textBoxPassword.Text,
                BlogId = 1
            }))
            {
                var id = Convert.ToInt32(client.NewPost(post));
            }
        }

        private DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
        }

        #endregion
        
        private void checkBoxDocx_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxIsDocxOpen.Enabled = checkBoxDocx.Checked;
            if (!checkBoxDocx.Checked)
                checkBoxIsDocxOpen.Checked = false;

            CheckEnablingExecuteButton();
        }

        private void buttonTemplatePath_Click(object sender, EventArgs e)
        {
            if (templateFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxTemplatePath.Text = templateFileDialog.FileName;
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            //try
            //{
                Execute();
            //}
            //catch (Exception ex)
            //{
            //    textBoxResult.Clear();
            //    textBoxResult.AppendText(ex.Message);
            //}
        }

        private void btnClearModifiedYears_Click(object sender, EventArgs e)
        {
            _typiconEntityService.ClearModifiedYears(1);

            btnClearModifiedYears.Enabled = false;
        }

        private void tabAdminPage_Enter(object sender, EventArgs e)
        {
            btnClearModifiedYears.Enabled = (_typiconEntity.ModifiedYears.Count > 0);
        }

        private void btnReloadRules_Click(object sender, EventArgs e)
        {
            _typiconEntityService.ReloadRules(1, Properties.Settings.Default.RulesFolder);
        }

        private void buttonRulePathSettings_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogSettings.ShowDialog() == DialogResult.OK)
            {
                textBoxRulePath.Text = folderBrowserDialogSettings.SelectedPath;
            }
        }

        private void dateTimePickerTesting_ValueChanged(object sender, EventArgs e)
        {
            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = dateTimePickerTesting.Value,
                Typicon = _typiconEntity,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            GetScheduleDayResponse dayResponse = _scheduleService.GetScheduleDay(dayRequest);

            _unitOfWork.SaveChanges();

            textBoxTesting.Clear();

            DateTime easter = _bookStorage.Easters.GetCurrentEaster(dateTimePickerTesting.Value.Year);

            int daysFromEaster = dateTimePickerTesting.Value.Subtract(easter).Days;

            textBoxTesting.AppendText(dayResponse.Day.Date.ToShortDateString() + Environment.NewLine);
            textBoxTesting.AppendText(dayResponse.Day.Name + ". " + daysFromEaster + " дней до Пасхи." +Environment.NewLine);
            foreach (var el in dayResponse.Day.Schedule.Worships)
            {
                string str = string.IsNullOrEmpty(el.Time) ? $"{el.Time} " : "";
                 
                str += $"{el.Name} {el.AdditionalName + Environment.NewLine}";

                textBoxTesting.AppendText(str);
            }
        }

        private void btnTest40mart_Click(object sender, EventArgs e)
        {
            textBoxTesting.Clear();

            var menologyRule = _typiconEntity.GetMenologyRule(dateTimePickerTesting.Value);

            if (menologyRule == null)
            {
                textBoxTesting.AppendText("Правило по заданной дате не найдено.");
                return;
            }

            foreach (EasterItem easterItem in _bookStorage.Easters.GetAll())
            {
                DateTime innerDate = (DateTime.IsLeapYear(easterItem.Date.Year)) 
                    ? new DateTime(easterItem.Date.Year, menologyRule.DateB.Month, menologyRule.DateB.Day) 
                    : new DateTime(easterItem.Date.Year, menologyRule.Date.Month, menologyRule.Date.Day);
                int daysFromEaster = easterItem.Date.Subtract(innerDate).Days;

                textBoxTesting.AppendText(easterItem.Date.Year + " год. " + daysFromEaster + " дней до Пасхи." + Environment.NewLine);
            }
            
        }

        private void btnSaveDaysXml_Click(object sender, EventArgs e)
        {
            
            string folder = Properties.Settings.Default.RulesFolder + "\\Days\\";

            //Menology
            XmlToFileSaver saver = new XmlToFileSaver(folder + "Menology.xml");
            DayXmlHelper<MenologyDay> menXmlHelper = new DayXmlHelper<MenologyDay>(_unitOfWork, saver);
            menXmlHelper.Save();

            //Triodion
            saver.FileName = folder + "Triodion.xml";
            DayXmlHelper<TriodionDay> triodXmlHelper = new DayXmlHelper<TriodionDay>(_unitOfWork, saver);
            triodXmlHelper.Save();
        }

        private void btnLoadDaysXml_Click(object sender, EventArgs e)
        {
            string folder = Properties.Settings.Default.RulesFolder + "\\Days\\";

            //Menology
            XmlToFileSaver saver = new XmlToFileSaver(folder + "Menology.xml");
            DayXmlHelper<MenologyDay> menXmlHelper = new DayXmlHelper<MenologyDay>(_unitOfWork, saver);
            menXmlHelper.Load();

            //Triodion
            saver.FileName = folder + "Triodion.xml";
            DayXmlHelper<TriodionDay> triodXmlHelper = new DayXmlHelper<TriodionDay>(_unitOfWork, saver);
            triodXmlHelper.Load();
        }

        private void btnGetSequence_Click(object sender, EventArgs e)
        {
            //try
            //{
                GetScheduleDayRequest request = new GetScheduleDayRequest()
                {
                    Date = monthCalendarSequence.SelectionStart,
                    Typicon = _typiconEntity,
                    Handler = new ServiceSequenceHandler(),
                    Language = "cs-ru",
                    ApplyParameters = CustomParameters,
                    CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.All)
                };

            request.Handler.Settings.Language = LanguageSettingsFactory.Create("cs-ru");

                GetScheduleDayResponse dayResponse = _scheduleService.GetScheduleDay(request);

                TextScheduleDayViewer viewer = new TextScheduleDayViewer();
                string result = viewer.Execute(dayResponse.Day);

                txtSequence.Clear();
                txtSequence.AppendText(result);
            //}
            //catch (Exception ex)
            //{
            //    txtSequence.Clear();
            //    txtSequence.AppendText(ex.Message);
            //}
        }

        private void btnSequenceCustomParams_Click(object sender, EventArgs e)
        {
            using (FormCustomParams dialog = new FormCustomParams())
            {
                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    CustomParameters = dialog.CustomParameters;
                }
            }
        }

        private void checkBoxBigDocx_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxIsBigDocxOpen.Enabled = checkBoxBigDocx.Checked;
            if (!checkBoxBigDocx.Checked)
                checkBoxIsBigDocxOpen.Checked = false;

            CheckEnablingExecuteButton();
        }

        private void btnCustomRule_Click(object sender, EventArgs e)
        {
            using (CustomRuleViewer dialog = new CustomRuleViewer())
            {
                // Show testDialog as a modal dialog and determine if DialogResult = OK.
            }
        }
    }
}
