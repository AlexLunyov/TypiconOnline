namespace ScheduleForm
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.panelTest = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsDocxOpen = new System.Windows.Forms.CheckBox();
            this.checkBoxWordpress = new System.Windows.Forms.CheckBox();
            this.checkBoxTxt = new System.Windows.Forms.CheckBox();
            this.checkBoxDocx = new System.Windows.Forms.CheckBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.panelCustom = new System.Windows.Forms.Panel();
            this.monthCalendarXML = new System.Windows.Forms.MonthCalendar();
            this.radioButtonNext = new System.Windows.Forms.RadioButton();
            this.labelNextScheduleWeek = new System.Windows.Forms.Label();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.tabPagesSettings = new System.Windows.Forms.TabPage();
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBaseUrl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRulePathSettings = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRulePath = new System.Windows.Forms.TextBox();
            this.buttonTemplatePath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTemplatePath = new System.Windows.Forms.TextBox();
            this.buttonPathSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.tabAdminPage = new System.Windows.Forms.TabPage();
            this.btnLoadDaysXml = new System.Windows.Forms.Button();
            this.btnSaveDaysXml = new System.Windows.Forms.Button();
            this.btnReloadRules = new System.Windows.Forms.Button();
            this.btnClearModifiedYears = new System.Windows.Forms.Button();
            this.tabPageTesting = new System.Windows.Forms.TabPage();
            this.btnTest40mart = new System.Windows.Forms.Button();
            this.textBoxTesting = new System.Windows.Forms.TextBox();
            this.dateTimePickerTesting = new System.Windows.Forms.DateTimePicker();
            this.tabPageSequence = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtSequence = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnGetSequence = new System.Windows.Forms.Button();
            this.monthCalendarSequence = new System.Windows.Forms.MonthCalendar();
            this.folderBrowserDialogSettings = new System.Windows.Forms.FolderBrowserDialog();
            this.templateFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.panelTest.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelCustom.SuspendLayout();
            this.tabPagesSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabAdminPage.SuspendLayout();
            this.tabPageTesting.SuspendLayout();
            this.tabPageSequence.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPagesSettings);
            this.tabControlMain.Controls.Add(this.tabAdminPage);
            this.tabControlMain.Controls.Add(this.tabPageTesting);
            this.tabControlMain.Controls.Add(this.tabPageSequence);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(971, 487);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.panelTest);
            this.tabPageMain.Location = new System.Drawing.Point(4, 25);
            this.tabPageMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageMain.Size = new System.Drawing.Size(963, 458);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Главная";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // panelTest
            // 
            this.panelTest.Controls.Add(this.groupBox5);
            this.panelTest.Controls.Add(this.groupBox4);
            this.panelTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTest.Location = new System.Drawing.Point(4, 4);
            this.panelTest.Margin = new System.Windows.Forms.Padding(4);
            this.panelTest.Name = "panelTest";
            this.panelTest.Size = new System.Drawing.Size(955, 450);
            this.panelTest.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxResult);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox5.Location = new System.Drawing.Point(350, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(605, 450);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Результат";
            // 
            // textBoxResult
            // 
            this.textBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResult.Location = new System.Drawing.Point(3, 18);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(599, 429);
            this.textBoxResult.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.buttonExecute);
            this.groupBox4.Controls.Add(this.panelCustom);
            this.groupBox4.Controls.Add(this.radioButtonNext);
            this.groupBox4.Controls.Add(this.labelNextScheduleWeek);
            this.groupBox4.Controls.Add(this.radioButtonCustom);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(487, 450);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Сформировать расписание";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxIsDocxOpen);
            this.groupBox3.Controls.Add(this.checkBoxWordpress);
            this.groupBox3.Controls.Add(this.checkBoxTxt);
            this.groupBox3.Controls.Add(this.checkBoxDocx);
            this.groupBox3.Location = new System.Drawing.Point(8, 266);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(335, 135);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Сохранить результаты";
            // 
            // checkBoxIsDocxOpen
            // 
            this.checkBoxIsDocxOpen.AutoSize = true;
            this.checkBoxIsDocxOpen.Enabled = false;
            this.checkBoxIsDocxOpen.Location = new System.Drawing.Point(29, 50);
            this.checkBoxIsDocxOpen.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxIsDocxOpen.Name = "checkBoxIsDocxOpen";
            this.checkBoxIsDocxOpen.Size = new System.Drawing.Size(233, 21);
            this.checkBoxIsDocxOpen.TabIndex = 3;
            this.checkBoxIsDocxOpen.Text = "Открыть после формирования";
            this.checkBoxIsDocxOpen.UseVisualStyleBackColor = true;
            // 
            // checkBoxWordpress
            // 
            this.checkBoxWordpress.AutoSize = true;
            this.checkBoxWordpress.Enabled = false;
            this.checkBoxWordpress.Location = new System.Drawing.Point(29, 102);
            this.checkBoxWordpress.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxWordpress.Name = "checkBoxWordpress";
            this.checkBoxWordpress.Size = new System.Drawing.Size(240, 21);
            this.checkBoxWordpress.TabIndex = 2;
            this.checkBoxWordpress.Text = "Выложить расписание на сайте";
            this.checkBoxWordpress.UseVisualStyleBackColor = true;
            // 
            // checkBoxTxt
            // 
            this.checkBoxTxt.AutoSize = true;
            this.checkBoxTxt.Location = new System.Drawing.Point(8, 76);
            this.checkBoxTxt.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxTxt.Name = "checkBoxTxt";
            this.checkBoxTxt.Size = new System.Drawing.Size(149, 21);
            this.checkBoxTxt.TabIndex = 1;
            this.checkBoxTxt.Text = "Текстовая версия";
            this.checkBoxTxt.UseVisualStyleBackColor = true;
            this.checkBoxTxt.CheckedChanged += new System.EventHandler(this.checkBoxTxt_CheckedChanged);
            // 
            // checkBoxDocx
            // 
            this.checkBoxDocx.AutoSize = true;
            this.checkBoxDocx.Location = new System.Drawing.Point(9, 25);
            this.checkBoxDocx.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxDocx.Name = "checkBoxDocx";
            this.checkBoxDocx.Size = new System.Drawing.Size(193, 21);
            this.checkBoxDocx.TabIndex = 0;
            this.checkBoxDocx.Text = "Шаблон документа Word";
            this.checkBoxDocx.UseVisualStyleBackColor = true;
            this.checkBoxDocx.CheckedChanged += new System.EventHandler(this.checkBoxDocx_CheckedChanged);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(69, 409);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(219, 28);
            this.buttonExecute.TabIndex = 9;
            this.buttonExecute.Text = "Выполнить";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // panelCustom
            // 
            this.panelCustom.Controls.Add(this.monthCalendarXML);
            this.panelCustom.Enabled = false;
            this.panelCustom.Location = new System.Drawing.Point(132, 55);
            this.panelCustom.Margin = new System.Windows.Forms.Padding(4);
            this.panelCustom.Name = "panelCustom";
            this.panelCustom.Size = new System.Drawing.Size(211, 203);
            this.panelCustom.TabIndex = 18;
            // 
            // monthCalendarXML
            // 
            this.monthCalendarXML.Location = new System.Drawing.Point(0, 0);
            this.monthCalendarXML.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendarXML.Name = "monthCalendarXML";
            this.monthCalendarXML.TabIndex = 7;
            this.monthCalendarXML.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarXML_DateSelected);
            // 
            // radioButtonNext
            // 
            this.radioButtonNext.AutoSize = true;
            this.radioButtonNext.Checked = true;
            this.radioButtonNext.Location = new System.Drawing.Point(16, 31);
            this.radioButtonNext.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonNext.Name = "radioButtonNext";
            this.radioButtonNext.Size = new System.Drawing.Size(106, 21);
            this.radioButtonNext.TabIndex = 15;
            this.radioButtonNext.TabStop = true;
            this.radioButtonNext.Text = "Следующее";
            this.radioButtonNext.UseVisualStyleBackColor = true;
            this.radioButtonNext.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // labelNextScheduleWeek
            // 
            this.labelNextScheduleWeek.AutoSize = true;
            this.labelNextScheduleWeek.Location = new System.Drawing.Point(128, 31);
            this.labelNextScheduleWeek.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNextScheduleWeek.Name = "labelNextScheduleWeek";
            this.labelNextScheduleWeek.Size = new System.Drawing.Size(93, 17);
            this.labelNextScheduleWeek.TabIndex = 17;
            this.labelNextScheduleWeek.Text = "[Нет данных]";
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.AutoSize = true;
            this.radioButtonCustom.Location = new System.Drawing.Point(16, 55);
            this.radioButtonCustom.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(104, 21);
            this.radioButtonCustom.TabIndex = 16;
            this.radioButtonCustom.Text = "Выборочно";
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            this.radioButtonCustom.CheckedChanged += new System.EventHandler(this.radioButtonCustom_CheckedChanged);
            // 
            // tabPagesSettings
            // 
            this.tabPagesSettings.Controls.Add(this.buttonSaveSettings);
            this.tabPagesSettings.Controls.Add(this.groupBox2);
            this.tabPagesSettings.Controls.Add(this.groupBox1);
            this.tabPagesSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPagesSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabPagesSettings.Name = "tabPagesSettings";
            this.tabPagesSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagesSettings.Size = new System.Drawing.Size(963, 458);
            this.tabPagesSettings.TabIndex = 1;
            this.tabPagesSettings.Text = "Настройки";
            this.tabPagesSettings.UseVisualStyleBackColor = true;
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Location = new System.Drawing.Point(103, 394);
            this.buttonSaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(152, 28);
            this.buttonSaveSettings.TabIndex = 2;
            this.buttonSaveSettings.Text = "Сохранить";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxUserName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxBaseUrl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(4, 164);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(955, 208);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wordpress";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 143);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Пароль";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(12, 167);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '•';
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(335, 22);
            this.textBoxPassword.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Имя пользователя";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Enabled = false;
            this.textBoxUserName.Location = new System.Drawing.Point(12, 110);
            this.textBoxUserName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            this.textBoxUserName.Size = new System.Drawing.Size(335, 22);
            this.textBoxUserName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Адрес сайта";
            // 
            // textBoxBaseUrl
            // 
            this.textBoxBaseUrl.Enabled = false;
            this.textBoxBaseUrl.Location = new System.Drawing.Point(12, 52);
            this.textBoxBaseUrl.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBaseUrl.Name = "textBoxBaseUrl";
            this.textBoxBaseUrl.ReadOnly = true;
            this.textBoxBaseUrl.Size = new System.Drawing.Size(335, 22);
            this.textBoxBaseUrl.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRulePathSettings);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxRulePath);
            this.groupBox1.Controls.Add(this.buttonTemplatePath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxTemplatePath);
            this.groupBox1.Controls.Add(this.buttonPathSettings);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxFilePath);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(955, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Файлы";
            // 
            // buttonRulePathSettings
            // 
            this.buttonRulePathSettings.Location = new System.Drawing.Point(671, 48);
            this.buttonRulePathSettings.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRulePathSettings.Name = "buttonRulePathSettings";
            this.buttonRulePathSettings.Size = new System.Drawing.Size(35, 28);
            this.buttonRulePathSettings.TabIndex = 8;
            this.buttonRulePathSettings.Text = "...";
            this.buttonRulePathSettings.UseVisualStyleBackColor = true;
            this.buttonRulePathSettings.Click += new System.EventHandler(this.buttonRulePathSettings_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Путь к Правилам";
            // 
            // textBoxRulePath
            // 
            this.textBoxRulePath.Location = new System.Drawing.Point(371, 49);
            this.textBoxRulePath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRulePath.Name = "textBoxRulePath";
            this.textBoxRulePath.ReadOnly = true;
            this.textBoxRulePath.Size = new System.Drawing.Size(291, 22);
            this.textBoxRulePath.TabIndex = 6;
            // 
            // buttonTemplatePath
            // 
            this.buttonTemplatePath.Location = new System.Drawing.Point(308, 106);
            this.buttonTemplatePath.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTemplatePath.Name = "buttonTemplatePath";
            this.buttonTemplatePath.Size = new System.Drawing.Size(35, 28);
            this.buttonTemplatePath.TabIndex = 5;
            this.buttonTemplatePath.Text = "...";
            this.buttonTemplatePath.UseVisualStyleBackColor = true;
            this.buttonTemplatePath.Click += new System.EventHandler(this.buttonTemplatePath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 82);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Путь к docx шаблону";
            // 
            // textBoxTemplatePath
            // 
            this.textBoxTemplatePath.Location = new System.Drawing.Point(8, 107);
            this.textBoxTemplatePath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTemplatePath.Name = "textBoxTemplatePath";
            this.textBoxTemplatePath.ReadOnly = true;
            this.textBoxTemplatePath.Size = new System.Drawing.Size(291, 22);
            this.textBoxTemplatePath.TabIndex = 3;
            // 
            // buttonPathSettings
            // 
            this.buttonPathSettings.Location = new System.Drawing.Point(308, 46);
            this.buttonPathSettings.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPathSettings.Name = "buttonPathSettings";
            this.buttonPathSettings.Size = new System.Drawing.Size(35, 28);
            this.buttonPathSettings.TabIndex = 2;
            this.buttonPathSettings.Text = "...";
            this.buttonPathSettings.UseVisualStyleBackColor = true;
            this.buttonPathSettings.Click += new System.EventHandler(this.buttonPathSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Путь к выходным папкам ";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(8, 47);
            this.textBoxFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(291, 22);
            this.textBoxFilePath.TabIndex = 0;
            this.textBoxFilePath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tabAdminPage
            // 
            this.tabAdminPage.Controls.Add(this.btnLoadDaysXml);
            this.tabAdminPage.Controls.Add(this.btnSaveDaysXml);
            this.tabAdminPage.Controls.Add(this.btnReloadRules);
            this.tabAdminPage.Controls.Add(this.btnClearModifiedYears);
            this.tabAdminPage.Location = new System.Drawing.Point(4, 25);
            this.tabAdminPage.Name = "tabAdminPage";
            this.tabAdminPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdminPage.Size = new System.Drawing.Size(963, 458);
            this.tabAdminPage.TabIndex = 2;
            this.tabAdminPage.Text = "Администрирование";
            this.tabAdminPage.UseVisualStyleBackColor = true;
            this.tabAdminPage.Enter += new System.EventHandler(this.tabAdminPage_Enter);
            // 
            // btnLoadDaysXml
            // 
            this.btnLoadDaysXml.Location = new System.Drawing.Point(18, 154);
            this.btnLoadDaysXml.Name = "btnLoadDaysXml";
            this.btnLoadDaysXml.Size = new System.Drawing.Size(195, 28);
            this.btnLoadDaysXml.TabIndex = 3;
            this.btnLoadDaysXml.Text = "Load days\' names as xml";
            this.btnLoadDaysXml.UseVisualStyleBackColor = true;
            this.btnLoadDaysXml.Click += new System.EventHandler(this.btnLoadDaysXml_Click);
            // 
            // btnSaveDaysXml
            // 
            this.btnSaveDaysXml.Location = new System.Drawing.Point(18, 109);
            this.btnSaveDaysXml.Name = "btnSaveDaysXml";
            this.btnSaveDaysXml.Size = new System.Drawing.Size(195, 28);
            this.btnSaveDaysXml.TabIndex = 2;
            this.btnSaveDaysXml.Text = "Save days\' names as xml";
            this.btnSaveDaysXml.UseVisualStyleBackColor = true;
            this.btnSaveDaysXml.Click += new System.EventHandler(this.btnSaveDaysXml_Click);
            // 
            // btnReloadRules
            // 
            this.btnReloadRules.Location = new System.Drawing.Point(18, 62);
            this.btnReloadRules.Name = "btnReloadRules";
            this.btnReloadRules.Size = new System.Drawing.Size(195, 28);
            this.btnReloadRules.TabIndex = 1;
            this.btnReloadRules.Text = "Reload Rules";
            this.btnReloadRules.UseVisualStyleBackColor = true;
            this.btnReloadRules.Click += new System.EventHandler(this.btnReloadRules_Click);
            // 
            // btnClearModifiedYears
            // 
            this.btnClearModifiedYears.Location = new System.Drawing.Point(18, 16);
            this.btnClearModifiedYears.Name = "btnClearModifiedYears";
            this.btnClearModifiedYears.Size = new System.Drawing.Size(195, 28);
            this.btnClearModifiedYears.TabIndex = 0;
            this.btnClearModifiedYears.Text = "Clear ModifiedYears";
            this.btnClearModifiedYears.UseVisualStyleBackColor = true;
            this.btnClearModifiedYears.Click += new System.EventHandler(this.btnClearModifiedYears_Click);
            // 
            // tabPageTesting
            // 
            this.tabPageTesting.Controls.Add(this.btnTest40mart);
            this.tabPageTesting.Controls.Add(this.textBoxTesting);
            this.tabPageTesting.Controls.Add(this.dateTimePickerTesting);
            this.tabPageTesting.Location = new System.Drawing.Point(4, 25);
            this.tabPageTesting.Name = "tabPageTesting";
            this.tabPageTesting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTesting.Size = new System.Drawing.Size(963, 458);
            this.tabPageTesting.TabIndex = 3;
            this.tabPageTesting.Text = "Тестирование";
            this.tabPageTesting.UseVisualStyleBackColor = true;
            // 
            // btnTest40mart
            // 
            this.btnTest40mart.Location = new System.Drawing.Point(31, 56);
            this.btnTest40mart.Name = "btnTest40mart";
            this.btnTest40mart.Size = new System.Drawing.Size(200, 23);
            this.btnTest40mart.TabIndex = 2;
            this.btnTest40mart.Text = "все Пасхи";
            this.btnTest40mart.UseVisualStyleBackColor = true;
            this.btnTest40mart.Click += new System.EventHandler(this.btnTest40mart_Click);
            // 
            // textBoxTesting
            // 
            this.textBoxTesting.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxTesting.Location = new System.Drawing.Point(273, 3);
            this.textBoxTesting.Multiline = true;
            this.textBoxTesting.Name = "textBoxTesting";
            this.textBoxTesting.ReadOnly = true;
            this.textBoxTesting.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTesting.Size = new System.Drawing.Size(687, 452);
            this.textBoxTesting.TabIndex = 1;
            // 
            // dateTimePickerTesting
            // 
            this.dateTimePickerTesting.Location = new System.Drawing.Point(31, 19);
            this.dateTimePickerTesting.Name = "dateTimePickerTesting";
            this.dateTimePickerTesting.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerTesting.TabIndex = 0;
            this.dateTimePickerTesting.ValueChanged += new System.EventHandler(this.dateTimePickerTesting_ValueChanged);
            // 
            // tabPageSequence
            // 
            this.tabPageSequence.Controls.Add(this.groupBox7);
            this.tabPageSequence.Controls.Add(this.groupBox6);
            this.tabPageSequence.Location = new System.Drawing.Point(4, 25);
            this.tabPageSequence.Name = "tabPageSequence";
            this.tabPageSequence.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSequence.Size = new System.Drawing.Size(963, 458);
            this.tabPageSequence.TabIndex = 4;
            this.tabPageSequence.Text = "Последование";
            this.tabPageSequence.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtSequence);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox7.Location = new System.Drawing.Point(225, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(735, 452);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Текст";
            // 
            // txtSequence
            // 
            this.txtSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSequence.Location = new System.Drawing.Point(3, 18);
            this.txtSequence.Multiline = true;
            this.txtSequence.Name = "txtSequence";
            this.txtSequence.ReadOnly = true;
            this.txtSequence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSequence.Size = new System.Drawing.Size(729, 431);
            this.txtSequence.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnGetSequence);
            this.groupBox6.Controls.Add(this.monthCalendarSequence);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(216, 452);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Календарь";
            // 
            // btnGetSequence
            // 
            this.btnGetSequence.Location = new System.Drawing.Point(9, 250);
            this.btnGetSequence.Name = "btnGetSequence";
            this.btnGetSequence.Size = new System.Drawing.Size(192, 29);
            this.btnGetSequence.TabIndex = 9;
            this.btnGetSequence.Text = "Сформировать";
            this.btnGetSequence.UseVisualStyleBackColor = true;
            this.btnGetSequence.Click += new System.EventHandler(this.btnGetSequence_Click);
            // 
            // monthCalendarSequence
            // 
            this.monthCalendarSequence.Location = new System.Drawing.Point(9, 29);
            this.monthCalendarSequence.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendarSequence.MaxSelectionCount = 1;
            this.monthCalendarSequence.Name = "monthCalendarSequence";
            this.monthCalendarSequence.ShowToday = false;
            this.monthCalendarSequence.ShowTodayCircle = false;
            this.monthCalendarSequence.TabIndex = 8;
            // 
            // templateFileDialog
            // 
            this.templateFileDialog.Filter = "Docx файлы | *.docx";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 487);
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расписание богослужений";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartForm_FormClosed);
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.panelTest.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelCustom.ResumeLayout(false);
            this.tabPagesSettings.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabAdminPage.ResumeLayout(false);
            this.tabPageTesting.ResumeLayout(false);
            this.tabPageTesting.PerformLayout();
            this.tabPageSequence.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPagesSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSettings;
        private System.Windows.Forms.Button buttonPathSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxBaseUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Button buttonSaveSettings;
        private System.Windows.Forms.Panel panelTest;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.RadioButton radioButtonNext;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.MonthCalendar monthCalendarXML;
        private System.Windows.Forms.Panel panelCustom;
        private System.Windows.Forms.Label labelNextScheduleWeek;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxWordpress;
        private System.Windows.Forms.CheckBox checkBoxTxt;
        private System.Windows.Forms.CheckBox checkBoxDocx;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxIsDocxOpen;
        private System.Windows.Forms.Button buttonTemplatePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTemplatePath;
        private System.Windows.Forms.OpenFileDialog templateFileDialog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.TabPage tabAdminPage;
        private System.Windows.Forms.Button btnClearModifiedYears;
        private System.Windows.Forms.Button btnReloadRules;
        private System.Windows.Forms.Button buttonRulePathSettings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxRulePath;
        private System.Windows.Forms.TabPage tabPageTesting;
        private System.Windows.Forms.TextBox textBoxTesting;
        private System.Windows.Forms.DateTimePicker dateTimePickerTesting;
        private System.Windows.Forms.Button btnTest40mart;
        private System.Windows.Forms.Button btnSaveDaysXml;
        private System.Windows.Forms.Button btnLoadDaysXml;
        private System.Windows.Forms.TabPage tabPageSequence;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtSequence;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnGetSequence;
        private System.Windows.Forms.MonthCalendar monthCalendarSequence;
    }
}