using ScheduleHandling;
namespace ScheduleForm
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewSigns = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceSignsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scheduleDBDataSet = new ScheduleHandling.ScheduleDBDataSet();
            this.dataGridViewFeatures = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kindDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isDayBeforeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.serviceSignsServiceFeaturesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.таблицыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlTables = new System.Windows.Forms.TabControl();
            this.tabPageServiceSigns = new System.Windows.Forms.TabPage();
            this.tabPageMineinik = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mineinikBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageTriodion = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayFromEasterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasMineaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.signIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.triodionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPagePaskhalia = new System.Windows.Forms.TabPage();
            this.checkBoxShortName = new System.Windows.Forms.CheckBox();
            this.labelWeekName = new System.Windows.Forms.Label();
            this.labelCheck = new System.Windows.Forms.Label();
            this.buttonCheckPaskha = new System.Windows.Forms.Button();
            this.dateTimePickerCheck = new System.Windows.Forms.DateTimePicker();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paskhaliaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageTest = new System.Windows.Forms.TabPage();
            this.panelTest = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStripLabel = new System.Windows.Forms.StatusStrip();
            this.labelSundayName = new System.Windows.Forms.Label();
            this.buttonFillTemplate = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonGetXML = new System.Windows.Forms.Button();
            this.textBoxXML = new System.Windows.Forms.TextBox();
            this.monthCalendarXML = new System.Windows.Forms.MonthCalendar();
            this.serviceSignsTableAdapter = new ScheduleHandling.ScheduleDBDataSetTableAdapters.ServiceSignsTableAdapter();
            this.serviceFeaturesTableAdapter = new ScheduleHandling.ScheduleDBDataSetTableAdapters.ServiceFeaturesTableAdapter();
            this.mineinikTableAdapter = new ScheduleHandling.ScheduleDBDataSetTableAdapters.MineinikTableAdapter();
            this.triodionTableAdapter = new ScheduleHandling.ScheduleDBDataSetTableAdapters.TriodionTableAdapter();
            this.paskhaliaTableAdapter = new ScheduleHandling.ScheduleDBDataSetTableAdapters.PaskhaliaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSigns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceSignsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeatures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceSignsServiceFeaturesBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControlTables.SuspendLayout();
            this.tabPageServiceSigns.SuspendLayout();
            this.tabPageMineinik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineinikBindingSource)).BeginInit();
            this.tabPageTriodion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triodionBindingSource)).BeginInit();
            this.tabPagePaskhalia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paskhaliaBindingSource)).BeginInit();
            this.panelTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewSigns
            // 
            this.dataGridViewSigns.AutoGenerateColumns = false;
            this.dataGridViewSigns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSigns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridViewSigns.DataSource = this.serviceSignsBindingSource;
            this.dataGridViewSigns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewSigns.Location = new System.Drawing.Point(6, 9);
            this.dataGridViewSigns.Name = "dataGridViewSigns";
            this.dataGridViewSigns.Size = new System.Drawing.Size(347, 390);
            this.dataGridViewSigns.TabIndex = 0;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // serviceSignsBindingSource
            // 
            this.serviceSignsBindingSource.DataMember = "ServiceSigns";
            this.serviceSignsBindingSource.DataSource = this.scheduleDBDataSet;
            // 
            // scheduleDBDataSet
            // 
            this.scheduleDBDataSet.DataSetName = "ScheduleDBDataSet";
            this.scheduleDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridViewFeatures
            // 
            this.dataGridViewFeatures.AutoGenerateColumns = false;
            this.dataGridViewFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFeatures.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn1,
            this.kindDataGridViewTextBoxColumn,
            this.parentIDDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn1,
            this.isDayBeforeDataGridViewCheckBoxColumn});
            this.dataGridViewFeatures.DataSource = this.serviceSignsServiceFeaturesBindingSource;
            this.dataGridViewFeatures.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewFeatures.Location = new System.Drawing.Point(377, 9);
            this.dataGridViewFeatures.Name = "dataGridViewFeatures";
            this.dataGridViewFeatures.Size = new System.Drawing.Size(656, 390);
            this.dataGridViewFeatures.TabIndex = 1;
            this.dataGridViewFeatures.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewFeatures_RowsAdded);
            this.dataGridViewFeatures.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewFeatures_UserAddedRow);
            // 
            // iDDataGridViewTextBoxColumn1
            // 
            this.iDDataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn1.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn1.Name = "iDDataGridViewTextBoxColumn1";
            // 
            // kindDataGridViewTextBoxColumn
            // 
            this.kindDataGridViewTextBoxColumn.DataPropertyName = "Kind";
            this.kindDataGridViewTextBoxColumn.HeaderText = "Kind";
            this.kindDataGridViewTextBoxColumn.Name = "kindDataGridViewTextBoxColumn";
            // 
            // parentIDDataGridViewTextBoxColumn
            // 
            this.parentIDDataGridViewTextBoxColumn.DataPropertyName = "ParentID";
            this.parentIDDataGridViewTextBoxColumn.HeaderText = "ParentID";
            this.parentIDDataGridViewTextBoxColumn.Name = "parentIDDataGridViewTextBoxColumn";
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // isDayBeforeDataGridViewCheckBoxColumn
            // 
            this.isDayBeforeDataGridViewCheckBoxColumn.DataPropertyName = "IsDayBefore";
            this.isDayBeforeDataGridViewCheckBoxColumn.HeaderText = "IsDayBefore";
            this.isDayBeforeDataGridViewCheckBoxColumn.Name = "isDayBeforeDataGridViewCheckBoxColumn";
            // 
            // serviceSignsServiceFeaturesBindingSource
            // 
            this.serviceSignsServiceFeaturesBindingSource.DataMember = "ServiceSignsServiceFeatures";
            this.serviceSignsServiceFeaturesBindingSource.DataSource = this.serviceSignsBindingSource;
            this.serviceSignsServiceFeaturesBindingSource.Filter = "Kind=0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.тестToolStripMenuItem,
            this.таблицыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1092, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.loadToolStripMenuItem.Text = "Загрузить";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click_1);
            // 
            // тестToolStripMenuItem
            // 
            this.тестToolStripMenuItem.Name = "тестToolStripMenuItem";
            this.тестToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.тестToolStripMenuItem.Text = "Тест";
            this.тестToolStripMenuItem.Click += new System.EventHandler(this.тестToolStripMenuItem_Click);
            // 
            // таблицыToolStripMenuItem
            // 
            this.таблицыToolStripMenuItem.Name = "таблицыToolStripMenuItem";
            this.таблицыToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.таблицыToolStripMenuItem.Text = "Таблицы";
            this.таблицыToolStripMenuItem.Click += new System.EventHandler(this.таблицыToolStripMenuItem_Click);
            // 
            // tabControlTables
            // 
            this.tabControlTables.Controls.Add(this.tabPageServiceSigns);
            this.tabControlTables.Controls.Add(this.tabPageMineinik);
            this.tabControlTables.Controls.Add(this.tabPageTriodion);
            this.tabControlTables.Controls.Add(this.tabPagePaskhalia);
            this.tabControlTables.Controls.Add(this.tabPageTest);
            this.tabControlTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTables.Location = new System.Drawing.Point(0, 24);
            this.tabControlTables.Name = "tabControlTables";
            this.tabControlTables.SelectedIndex = 0;
            this.tabControlTables.Size = new System.Drawing.Size(1092, 545);
            this.tabControlTables.TabIndex = 1;
            // 
            // tabPageServiceSigns
            // 
            this.tabPageServiceSigns.Controls.Add(this.dataGridViewSigns);
            this.tabPageServiceSigns.Controls.Add(this.dataGridViewFeatures);
            this.tabPageServiceSigns.Location = new System.Drawing.Point(4, 22);
            this.tabPageServiceSigns.Name = "tabPageServiceSigns";
            this.tabPageServiceSigns.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServiceSigns.Size = new System.Drawing.Size(1084, 519);
            this.tabPageServiceSigns.TabIndex = 0;
            this.tabPageServiceSigns.Text = "Знаки служб";
            this.tabPageServiceSigns.UseVisualStyleBackColor = true;
            // 
            // tabPageMineinik
            // 
            this.tabPageMineinik.Controls.Add(this.dataGridView1);
            this.tabPageMineinik.Location = new System.Drawing.Point(4, 22);
            this.tabPageMineinik.Name = "tabPageMineinik";
            this.tabPageMineinik.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMineinik.Size = new System.Drawing.Size(1084, 519);
            this.tabPageMineinik.TabIndex = 1;
            this.tabPageMineinik.Text = "Минейник";
            this.tabPageMineinik.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn2,
            this.dateDataGridViewTextBoxColumn,
            this.dateBDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn2,
            this.signIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.mineinikBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1078, 513);
            this.dataGridView1.TabIndex = 0;
            // 
            // iDDataGridViewTextBoxColumn2
            // 
            this.iDDataGridViewTextBoxColumn2.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn2.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn2.Name = "iDDataGridViewTextBoxColumn2";
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // dateBDataGridViewTextBoxColumn
            // 
            this.dateBDataGridViewTextBoxColumn.DataPropertyName = "DateB";
            this.dateBDataGridViewTextBoxColumn.HeaderText = "DateB";
            this.dateBDataGridViewTextBoxColumn.Name = "dateBDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn2
            // 
            this.nameDataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn2.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn2.Name = "nameDataGridViewTextBoxColumn2";
            this.nameDataGridViewTextBoxColumn2.Width = 300;
            // 
            // signIDDataGridViewTextBoxColumn
            // 
            this.signIDDataGridViewTextBoxColumn.DataPropertyName = "SignID";
            this.signIDDataGridViewTextBoxColumn.HeaderText = "SignID";
            this.signIDDataGridViewTextBoxColumn.Name = "signIDDataGridViewTextBoxColumn";
            // 
            // mineinikBindingSource
            // 
            this.mineinikBindingSource.DataMember = "Mineinik";
            this.mineinikBindingSource.DataSource = this.scheduleDBDataSet;
            // 
            // tabPageTriodion
            // 
            this.tabPageTriodion.Controls.Add(this.dataGridView2);
            this.tabPageTriodion.Location = new System.Drawing.Point(4, 22);
            this.tabPageTriodion.Name = "tabPageTriodion";
            this.tabPageTriodion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTriodion.Size = new System.Drawing.Size(1084, 519);
            this.tabPageTriodion.TabIndex = 2;
            this.tabPageTriodion.Text = "Триодион";
            this.tabPageTriodion.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn3,
            this.dayFromEasterDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn3,
            this.hasMineaDataGridViewCheckBoxColumn,
            this.signIDDataGridViewTextBoxColumn1});
            this.dataGridView2.DataSource = this.triodionBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1078, 513);
            this.dataGridView2.TabIndex = 0;
            // 
            // iDDataGridViewTextBoxColumn3
            // 
            this.iDDataGridViewTextBoxColumn3.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn3.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn3.Name = "iDDataGridViewTextBoxColumn3";
            // 
            // dayFromEasterDataGridViewTextBoxColumn
            // 
            this.dayFromEasterDataGridViewTextBoxColumn.DataPropertyName = "DayFromEaster";
            this.dayFromEasterDataGridViewTextBoxColumn.HeaderText = "DayFromEaster";
            this.dayFromEasterDataGridViewTextBoxColumn.Name = "dayFromEasterDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn3
            // 
            this.nameDataGridViewTextBoxColumn3.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn3.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn3.Name = "nameDataGridViewTextBoxColumn3";
            this.nameDataGridViewTextBoxColumn3.Width = 300;
            // 
            // hasMineaDataGridViewCheckBoxColumn
            // 
            this.hasMineaDataGridViewCheckBoxColumn.DataPropertyName = "HasMinea";
            this.hasMineaDataGridViewCheckBoxColumn.HeaderText = "HasMinea";
            this.hasMineaDataGridViewCheckBoxColumn.Name = "hasMineaDataGridViewCheckBoxColumn";
            // 
            // signIDDataGridViewTextBoxColumn1
            // 
            this.signIDDataGridViewTextBoxColumn1.DataPropertyName = "SignID";
            this.signIDDataGridViewTextBoxColumn1.HeaderText = "SignID";
            this.signIDDataGridViewTextBoxColumn1.Name = "signIDDataGridViewTextBoxColumn1";
            // 
            // triodionBindingSource
            // 
            this.triodionBindingSource.DataMember = "Triodion";
            this.triodionBindingSource.DataSource = this.scheduleDBDataSet;
            // 
            // tabPagePaskhalia
            // 
            this.tabPagePaskhalia.Controls.Add(this.checkBoxShortName);
            this.tabPagePaskhalia.Controls.Add(this.labelWeekName);
            this.tabPagePaskhalia.Controls.Add(this.labelCheck);
            this.tabPagePaskhalia.Controls.Add(this.buttonCheckPaskha);
            this.tabPagePaskhalia.Controls.Add(this.dateTimePickerCheck);
            this.tabPagePaskhalia.Controls.Add(this.dataGridView3);
            this.tabPagePaskhalia.Location = new System.Drawing.Point(4, 22);
            this.tabPagePaskhalia.Name = "tabPagePaskhalia";
            this.tabPagePaskhalia.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePaskhalia.Size = new System.Drawing.Size(1084, 519);
            this.tabPagePaskhalia.TabIndex = 3;
            this.tabPagePaskhalia.Text = "Пасхалия";
            this.tabPagePaskhalia.UseVisualStyleBackColor = true;
            // 
            // checkBoxShortName
            // 
            this.checkBoxShortName.AutoSize = true;
            this.checkBoxShortName.Location = new System.Drawing.Point(709, 45);
            this.checkBoxShortName.Name = "checkBoxShortName";
            this.checkBoxShortName.Size = new System.Drawing.Size(74, 17);
            this.checkBoxShortName.TabIndex = 5;
            this.checkBoxShortName.Text = "Коротко?";
            this.checkBoxShortName.UseVisualStyleBackColor = true;
            this.checkBoxShortName.CheckedChanged += new System.EventHandler(this.dateTimePickerCheck_ValueChanged);
            // 
            // labelWeekName
            // 
            this.labelWeekName.AutoSize = true;
            this.labelWeekName.Location = new System.Drawing.Point(795, 49);
            this.labelWeekName.Name = "labelWeekName";
            this.labelWeekName.Size = new System.Drawing.Size(0, 13);
            this.labelWeekName.TabIndex = 4;
            // 
            // labelCheck
            // 
            this.labelCheck.AutoSize = true;
            this.labelCheck.Location = new System.Drawing.Point(610, 86);
            this.labelCheck.Name = "labelCheck";
            this.labelCheck.Size = new System.Drawing.Size(0, 13);
            this.labelCheck.TabIndex = 3;
            // 
            // buttonCheckPaskha
            // 
            this.buttonCheckPaskha.Location = new System.Drawing.Point(499, 81);
            this.buttonCheckPaskha.Name = "buttonCheckPaskha";
            this.buttonCheckPaskha.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckPaskha.TabIndex = 2;
            this.buttonCheckPaskha.Text = "Триодь?";
            this.buttonCheckPaskha.UseVisualStyleBackColor = true;
            this.buttonCheckPaskha.Click += new System.EventHandler(this.buttonCheckPaskha_Click);
            // 
            // dateTimePickerCheck
            // 
            this.dateTimePickerCheck.Location = new System.Drawing.Point(488, 42);
            this.dateTimePickerCheck.Name = "dateTimePickerCheck";
            this.dateTimePickerCheck.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerCheck.TabIndex = 1;
            this.dateTimePickerCheck.ValueChanged += new System.EventHandler(this.dateTimePickerCheck_ValueChanged);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDataGridViewTextBoxColumn1});
            this.dataGridView3.DataSource = this.paskhaliaBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(8, 6);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(404, 331);
            this.dataGridView3.TabIndex = 0;
            // 
            // dateDataGridViewTextBoxColumn1
            // 
            this.dateDataGridViewTextBoxColumn1.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn1.Name = "dateDataGridViewTextBoxColumn1";
            // 
            // paskhaliaBindingSource
            // 
            this.paskhaliaBindingSource.DataMember = "Paskhalia";
            this.paskhaliaBindingSource.DataSource = this.scheduleDBDataSet;
            // 
            // tabPageTest
            // 
            this.tabPageTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageTest.Name = "tabPageTest";
            this.tabPageTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTest.Size = new System.Drawing.Size(1084, 519);
            this.tabPageTest.TabIndex = 4;
            this.tabPageTest.Text = "Баловство";
            this.tabPageTest.UseVisualStyleBackColor = true;
            // 
            // panelTest
            // 
            this.panelTest.Controls.Add(this.button1);
            this.panelTest.Controls.Add(this.statusStripLabel);
            this.panelTest.Controls.Add(this.labelSundayName);
            this.panelTest.Controls.Add(this.buttonFillTemplate);
            this.panelTest.Controls.Add(this.buttonTest);
            this.panelTest.Controls.Add(this.buttonGetXML);
            this.panelTest.Controls.Add(this.textBoxXML);
            this.panelTest.Controls.Add(this.monthCalendarXML);
            this.panelTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTest.Location = new System.Drawing.Point(0, 24);
            this.panelTest.Name = "panelTest";
            this.panelTest.Size = new System.Drawing.Size(1092, 545);
            this.panelTest.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Тест Text";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.Location = new System.Drawing.Point(0, 523);
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(1092, 22);
            this.statusStripLabel.TabIndex = 13;
            this.statusStripLabel.Text = "statusStrip1";
            // 
            // labelSundayName
            // 
            this.labelSundayName.AutoSize = true;
            this.labelSundayName.Location = new System.Drawing.Point(14, 184);
            this.labelSundayName.Name = "labelSundayName";
            this.labelSundayName.Size = new System.Drawing.Size(69, 13);
            this.labelSundayName.TabIndex = 12;
            this.labelSundayName.Text = "sundayName";
            this.labelSundayName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonFillTemplate
            // 
            this.buttonFillTemplate.Location = new System.Drawing.Point(14, 263);
            this.buttonFillTemplate.Name = "buttonFillTemplate";
            this.buttonFillTemplate.Size = new System.Drawing.Size(164, 23);
            this.buttonFillTemplate.TabIndex = 11;
            this.buttonFillTemplate.Text = "Заполнить шаблон";
            this.buttonFillTemplate.UseVisualStyleBackColor = true;
            this.buttonFillTemplate.Click += new System.EventHandler(this.buttonFillTemplate_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(12, 306);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(164, 23);
            this.buttonTest.TabIndex = 10;
            this.buttonTest.Text = "Тест Docx";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonGetXML
            // 
            this.buttonGetXML.Location = new System.Drawing.Point(13, 217);
            this.buttonGetXML.Name = "buttonGetXML";
            this.buttonGetXML.Size = new System.Drawing.Size(164, 23);
            this.buttonGetXML.TabIndex = 9;
            this.buttonGetXML.Text = "Получить XML";
            this.buttonGetXML.UseVisualStyleBackColor = true;
            this.buttonGetXML.Click += new System.EventHandler(this.buttonGetXML_Click);
            // 
            // textBoxXML
            // 
            this.textBoxXML.Location = new System.Drawing.Point(189, 0);
            this.textBoxXML.Multiline = true;
            this.textBoxXML.Name = "textBoxXML";
            this.textBoxXML.Size = new System.Drawing.Size(903, 421);
            this.textBoxXML.TabIndex = 8;
            // 
            // monthCalendarXML
            // 
            this.monthCalendarXML.Location = new System.Drawing.Point(13, 9);
            this.monthCalendarXML.MaxSelectionCount = 1;
            this.monthCalendarXML.Name = "monthCalendarXML";
            this.monthCalendarXML.TabIndex = 7;
            this.monthCalendarXML.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarXML_DateChanged);
            // 
            // serviceSignsTableAdapter
            // 
            this.serviceSignsTableAdapter.ClearBeforeFill = true;
            // 
            // serviceFeaturesTableAdapter
            // 
            this.serviceFeaturesTableAdapter.ClearBeforeFill = true;
            // 
            // mineinikTableAdapter
            // 
            this.mineinikTableAdapter.ClearBeforeFill = true;
            // 
            // triodionTableAdapter
            // 
            this.triodionTableAdapter.ClearBeforeFill = true;
            // 
            // paskhaliaTableAdapter
            // 
            this.paskhaliaTableAdapter.ClearBeforeFill = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 569);
            this.Controls.Add(this.panelTest);
            this.Controls.Add(this.tabControlTables);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Расписание богослужений";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSigns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceSignsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeatures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceSignsServiceFeaturesBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlTables.ResumeLayout(false);
            this.tabPageServiceSigns.ResumeLayout(false);
            this.tabPageMineinik.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineinikBindingSource)).EndInit();
            this.tabPageTriodion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triodionBindingSource)).EndInit();
            this.tabPagePaskhalia.ResumeLayout(false);
            this.tabPagePaskhalia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paskhaliaBindingSource)).EndInit();
            this.panelTest.ResumeLayout(false);
            this.panelTest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSigns;
        private System.Windows.Forms.DataGridView dataGridViewFeatures;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlTables;
        private System.Windows.Forms.TabPage tabPageServiceSigns;
        private System.Windows.Forms.TabPage tabPageMineinik;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageTriodion;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPagePaskhalia;
        private System.Windows.Forms.DataGridView dataGridView3;
        private ScheduleDBDataSet scheduleDBDataSet;
        private System.Windows.Forms.BindingSource serviceSignsBindingSource;
        private ScheduleHandling.ScheduleDBDataSetTableAdapters.ServiceSignsTableAdapter serviceSignsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource serviceSignsServiceFeaturesBindingSource;
        private ScheduleHandling.ScheduleDBDataSetTableAdapters.ServiceFeaturesTableAdapter serviceFeaturesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn kindDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isDayBeforeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource mineinikBindingSource;
        private ScheduleHandling.ScheduleDBDataSetTableAdapters.MineinikTableAdapter mineinikTableAdapter;
        private System.Windows.Forms.BindingSource triodionBindingSource;
        private ScheduleHandling.ScheduleDBDataSetTableAdapters.TriodionTableAdapter triodionTableAdapter;
        private System.Windows.Forms.BindingSource paskhaliaBindingSource;
        private ScheduleHandling.ScheduleDBDataSetTableAdapters.PaskhaliaTableAdapter paskhaliaTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn signIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn afterLiturgyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayFromEasterDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasMineaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn signIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button buttonCheckPaskha;
        private System.Windows.Forms.DateTimePicker dateTimePickerCheck;
        private System.Windows.Forms.Label labelCheck;
        private System.Windows.Forms.CheckBox checkBoxShortName;
        private System.Windows.Forms.Label labelWeekName;
        private System.Windows.Forms.TabPage tabPageTest;
        private System.Windows.Forms.ToolStripMenuItem тестToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem таблицыToolStripMenuItem;
        private System.Windows.Forms.Panel panelTest;
        private System.Windows.Forms.Button buttonGetXML;
        private System.Windows.Forms.TextBox textBoxXML;
        private System.Windows.Forms.MonthCalendar monthCalendarXML;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonFillTemplate;
        private System.Windows.Forms.Label labelSundayName;
        private System.Windows.Forms.StatusStrip statusStripLabel;
        private System.Windows.Forms.Button button1;
    }
}