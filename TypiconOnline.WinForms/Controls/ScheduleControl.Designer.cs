namespace TypiconOnline.WinForms.Controls
{
    partial class ScheduleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsBigDocxOpen = new System.Windows.Forms.CheckBox();
            this.checkBoxBigDocx = new System.Windows.Forms.CheckBox();
            this.checkBoxException = new System.Windows.Forms.CheckBox();
            this.checkBoxIsDocxOpen = new System.Windows.Forms.CheckBox();
            this.checkBoxWordpress = new System.Windows.Forms.CheckBox();
            this.checkBoxTxt = new System.Windows.Forms.CheckBox();
            this.checkBoxDocx = new System.Windows.Forms.CheckBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.monthCalendarXML = new System.Windows.Forms.MonthCalendar();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.monthCalendarXML);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.buttonExecute);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(302, 527);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Сформировать расписание";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxIsBigDocxOpen);
            this.groupBox3.Controls.Add(this.checkBoxBigDocx);
            this.groupBox3.Controls.Add(this.checkBoxException);
            this.groupBox3.Controls.Add(this.checkBoxIsDocxOpen);
            this.groupBox3.Controls.Add(this.checkBoxWordpress);
            this.groupBox3.Controls.Add(this.checkBoxTxt);
            this.groupBox3.Controls.Add(this.checkBoxDocx);
            this.groupBox3.Location = new System.Drawing.Point(6, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 182);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Сохранить результаты";
            // 
            // checkBoxIsBigDocxOpen
            // 
            this.checkBoxIsBigDocxOpen.AutoSize = true;
            this.checkBoxIsBigDocxOpen.Checked = global::TypiconOnline.WinForms.Properties.Settings.Default.IsBigDocxOpen;
            this.checkBoxIsBigDocxOpen.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TypiconOnline.WinForms.Properties.Settings.Default, "IsBigDocxOpen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxIsBigDocxOpen.Enabled = false;
            this.checkBoxIsBigDocxOpen.Location = new System.Drawing.Point(22, 84);
            this.checkBoxIsBigDocxOpen.Name = "checkBoxIsBigDocxOpen";
            this.checkBoxIsBigDocxOpen.Size = new System.Drawing.Size(185, 18);
            this.checkBoxIsBigDocxOpen.TabIndex = 6;
            this.checkBoxIsBigDocxOpen.Text = "Открыть после формирования";
            this.checkBoxIsBigDocxOpen.UseCompatibleTextRendering = true;
            this.checkBoxIsBigDocxOpen.UseVisualStyleBackColor = true;
            // 
            // checkBoxBigDocx
            // 
            this.checkBoxBigDocx.AutoSize = true;
            this.checkBoxBigDocx.Checked = global::TypiconOnline.WinForms.Properties.Settings.Default.IsBigDocxChecked;
            this.checkBoxBigDocx.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TypiconOnline.WinForms.Properties.Settings.Default, "IsBigDocxChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxBigDocx.Location = new System.Drawing.Point(7, 64);
            this.checkBoxBigDocx.Name = "checkBoxBigDocx";
            this.checkBoxBigDocx.Size = new System.Drawing.Size(286, 18);
            this.checkBoxBigDocx.TabIndex = 5;
            this.checkBoxBigDocx.Text = "Шаблон документа Word (КРУПНЫМ ШРИФТОМ)";
            this.checkBoxBigDocx.UseCompatibleTextRendering = true;
            this.checkBoxBigDocx.UseVisualStyleBackColor = true;
            this.checkBoxBigDocx.CheckedChanged += new System.EventHandler(this.checkBoxBigDocx_CheckedChanged);
            // 
            // checkBoxException
            // 
            this.checkBoxException.AutoSize = true;
            this.checkBoxException.ForeColor = System.Drawing.Color.Red;
            this.checkBoxException.Location = new System.Drawing.Point(7, 155);
            this.checkBoxException.Name = "checkBoxException";
            this.checkBoxException.Size = new System.Drawing.Size(273, 18);
            this.checkBoxException.TabIndex = 4;
            this.checkBoxException.Text = "Исключение, если неверно заполнено правило";
            this.checkBoxException.UseCompatibleTextRendering = true;
            this.checkBoxException.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsDocxOpen
            // 
            this.checkBoxIsDocxOpen.AutoSize = true;
            this.checkBoxIsDocxOpen.Checked = global::TypiconOnline.WinForms.Properties.Settings.Default.IsDocxOpen;
            this.checkBoxIsDocxOpen.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TypiconOnline.WinForms.Properties.Settings.Default, "IsDocxOpen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxIsDocxOpen.Enabled = false;
            this.checkBoxIsDocxOpen.Location = new System.Drawing.Point(22, 41);
            this.checkBoxIsDocxOpen.Name = "checkBoxIsDocxOpen";
            this.checkBoxIsDocxOpen.Size = new System.Drawing.Size(185, 18);
            this.checkBoxIsDocxOpen.TabIndex = 3;
            this.checkBoxIsDocxOpen.Text = "Открыть после формирования";
            this.checkBoxIsDocxOpen.UseCompatibleTextRendering = true;
            this.checkBoxIsDocxOpen.UseVisualStyleBackColor = true;
            // 
            // checkBoxWordpress
            // 
            this.checkBoxWordpress.AutoSize = true;
            this.checkBoxWordpress.Checked = global::TypiconOnline.WinForms.Properties.Settings.Default.IsWrodpressChecked;
            this.checkBoxWordpress.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TypiconOnline.WinForms.Properties.Settings.Default, "IsWrodpressChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxWordpress.Enabled = false;
            this.checkBoxWordpress.Location = new System.Drawing.Point(22, 132);
            this.checkBoxWordpress.Name = "checkBoxWordpress";
            this.checkBoxWordpress.Size = new System.Drawing.Size(190, 18);
            this.checkBoxWordpress.TabIndex = 2;
            this.checkBoxWordpress.Text = "Выложить расписание на сайте";
            this.checkBoxWordpress.UseCompatibleTextRendering = true;
            this.checkBoxWordpress.UseVisualStyleBackColor = true;
            // 
            // checkBoxTxt
            // 
            this.checkBoxTxt.AutoSize = true;
            this.checkBoxTxt.Checked = global::TypiconOnline.WinForms.Properties.Settings.Default.IsTxtChecked;
            this.checkBoxTxt.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TypiconOnline.WinForms.Properties.Settings.Default, "IsTxtChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxTxt.Location = new System.Drawing.Point(7, 108);
            this.checkBoxTxt.Name = "checkBoxTxt";
            this.checkBoxTxt.Size = new System.Drawing.Size(117, 18);
            this.checkBoxTxt.TabIndex = 1;
            this.checkBoxTxt.Text = "Текстовая версия";
            this.checkBoxTxt.UseCompatibleTextRendering = true;
            this.checkBoxTxt.UseVisualStyleBackColor = true;
            this.checkBoxTxt.CheckedChanged += new System.EventHandler(this.checkBoxTxt_CheckedChanged);
            // 
            // checkBoxDocx
            // 
            this.checkBoxDocx.AutoSize = true;
            this.checkBoxDocx.Checked = global::TypiconOnline.WinForms.Properties.Settings.Default.IsDocxChecked;
            this.checkBoxDocx.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TypiconOnline.WinForms.Properties.Settings.Default, "IsDocxChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxDocx.Location = new System.Drawing.Point(7, 20);
            this.checkBoxDocx.Name = "checkBoxDocx";
            this.checkBoxDocx.Size = new System.Drawing.Size(154, 18);
            this.checkBoxDocx.TabIndex = 0;
            this.checkBoxDocx.Text = "Шаблон документа Word";
            this.checkBoxDocx.UseCompatibleTextRendering = true;
            this.checkBoxDocx.UseVisualStyleBackColor = true;
            this.checkBoxDocx.CheckedChanged += new System.EventHandler(this.checkBoxDocx_CheckedChanged);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(68, 387);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(164, 23);
            this.buttonExecute.TabIndex = 9;
            this.buttonExecute.Text = "Выполнить";
            this.buttonExecute.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxResult);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(302, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(571, 527);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Результат";
            // 
            // textBoxResult
            // 
            this.textBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResult.Location = new System.Drawing.Point(2, 15);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(567, 510);
            this.textBoxResult.TabIndex = 0;
            // 
            // monthCalendarXML
            // 
            this.monthCalendarXML.Location = new System.Drawing.Point(68, 25);
            this.monthCalendarXML.Name = "monthCalendarXML";
            this.monthCalendarXML.TabIndex = 7;
            // 
            // ScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Name = "ScheduleControl";
            this.Size = new System.Drawing.Size(873, 527);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxIsBigDocxOpen;
        private System.Windows.Forms.CheckBox checkBoxBigDocx;
        private System.Windows.Forms.CheckBox checkBoxException;
        private System.Windows.Forms.CheckBox checkBoxIsDocxOpen;
        private System.Windows.Forms.CheckBox checkBoxWordpress;
        private System.Windows.Forms.CheckBox checkBoxTxt;
        private System.Windows.Forms.CheckBox checkBoxDocx;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.MonthCalendar monthCalendarXML;
    }
}
