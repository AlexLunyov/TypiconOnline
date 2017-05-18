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
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Easter;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules;

namespace ScheduleForm
{
    public partial class StartForm : Form
    {
        //private ScheduleHandling.ScheduleHandler _sh = null;
        private IUnitOfWork _unitOfWork;
        private ScheduleService _scheduleService;
        TypiconEntity _typiconEntity;

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

            _typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            _scheduleService = new ScheduleService();

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

            labelNextScheduleWeek.Text = BookStorage.Oktoikh.GetSundayName(date/*_selectedDate*/);

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
                    string[] splittedFileName = foundFile.Split(new Char[] { ' ' });
                    try
                    {
                        DateTime date = DateTime.Parse(splittedFileName[2]);

                        _selectedDate = date.AddDays(1);

                        labelNextScheduleWeek.Text = BookStorage.Oktoikh.GetSundayName(_selectedDate);
                    }
                    catch
                    {
                        labelNextScheduleWeek.Text = BookStorage.Oktoikh.GetSundayName(_selectedDate);
                        _selectedDate = DateTime.Today;
                    }
                }
                else
                {
                    labelNextScheduleWeek.Text = BookStorage.Oktoikh.GetSundayName(_selectedDate);
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

            try
            {
                GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
                {
                    Date = SelectedDate,
                    TypiconEntity = _typiconEntity,
                    Mode = HandlingMode.AstronimicDay,
                    RuleHandler = new ScheduleHandler()
                };

                GetScheduleWeekResponse weekResponse = _scheduleService.GetScheduleWeek(weekRequest);

                _unitOfWork.Commit();

                textBoxResult.Clear();

                foreach (ScheduleDay day in weekResponse.Days)
                {
                    textBoxResult.AppendText("--------------------------" + Environment.NewLine);
                    textBoxResult.AppendText(day.Date.ToShortDateString() + Environment.NewLine);
                    textBoxResult.AppendText(day.Name + Environment.NewLine);
                    foreach(RuleElement element in day.Schedule.ChildElements)
                    {
                        if (element is Service)
                        {
                            textBoxResult.AppendText((element as Service).Time + " " + (element as Service).Name + " " + (element as Service).AdditionalName + Environment.NewLine);
                        }
                        else if (element is Notice)
                        {
                            textBoxResult.AppendText((element as Notice).Name + " " + (element as Notice).AdditionalName + Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetFileName(DateTime date)
        {
            string result = textBoxFilePath.Text + "\\";
            result += _scheduleFileStart + date.ToString("yyyy-MM-dd") + " " + date.AddDays(6).ToString("yyyy-MM-dd") + " " + BookStorage.Oktoikh.GetSundayName(date);
            return result;
        }

        private void CheckEnablingExecuteButton()
        {
            buttonExecute.Enabled = checkBoxDocx.Checked || checkBoxTxt.Checked;
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
            Execute();
        }

        private void btnClearModifiedYears_Click(object sender, EventArgs e)
        {
            using (TypiconEntityService service = new TypiconEntityService(_unitOfWork))
            {
                service.ClearModifiedYears(1);
            }

            btnClearModifiedYears.Enabled = false;
        }

        private void tabAdminPage_Enter(object sender, EventArgs e)
        {
            btnClearModifiedYears.Enabled = (_typiconEntity.ModifiedYears.Count > 0);
        }

        private void btnReloadRules_Click(object sender, EventArgs e)
        {
            using (TypiconEntityService service = new TypiconEntityService(_unitOfWork))
            {
                service.ReloadRules(1, Properties.Settings.Default.RulesFolder);
            }
        }

        private void buttonRulePathSettings_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogSettings.ShowDialog() == DialogResult.OK)
            {
                textBoxRulePath.Text = folderBrowserDialogSettings.SelectedPath;
            }
        }
    }
}
