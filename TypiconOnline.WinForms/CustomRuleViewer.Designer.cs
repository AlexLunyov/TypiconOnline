namespace TypiconOnline.WinForms
{
    partial class CustomRuleViewer
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
            this.tabControlCustomRule = new System.Windows.Forms.TabControl();
            this.tabPageCustomRule = new System.Windows.Forms.TabPage();
            this.tabPageCustomRuleOutput = new System.Windows.Forms.TabPage();
            this.webCustomRule = new System.Windows.Forms.WebBrowser();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCustomRule = new System.Windows.Forms.Button();
            this.monthCalendarCustomRule = new System.Windows.Forms.MonthCalendar();
            this.txtCustomRule = new System.Windows.Forms.TextBox();
            this.tabControlCustomRule.SuspendLayout();
            this.tabPageCustomRule.SuspendLayout();
            this.tabPageCustomRuleOutput.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlCustomRule
            // 
            this.tabControlCustomRule.Controls.Add(this.tabPageCustomRule);
            this.tabControlCustomRule.Controls.Add(this.tabPageCustomRuleOutput);
            this.tabControlCustomRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCustomRule.Location = new System.Drawing.Point(234, 0);
            this.tabControlCustomRule.Name = "tabControlCustomRule";
            this.tabControlCustomRule.SelectedIndex = 0;
            this.tabControlCustomRule.Size = new System.Drawing.Size(880, 697);
            this.tabControlCustomRule.TabIndex = 5;
            // 
            // tabPageCustomRule
            // 
            this.tabPageCustomRule.Controls.Add(this.txtCustomRule);
            this.tabPageCustomRule.Location = new System.Drawing.Point(4, 25);
            this.tabPageCustomRule.Name = "tabPageCustomRule";
            this.tabPageCustomRule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCustomRule.Size = new System.Drawing.Size(872, 668);
            this.tabPageCustomRule.TabIndex = 0;
            this.tabPageCustomRule.Text = "Текст";
            this.tabPageCustomRule.UseVisualStyleBackColor = true;
            // 
            // tabPageCustomRuleOutput
            // 
            this.tabPageCustomRuleOutput.Controls.Add(this.webCustomRule);
            this.tabPageCustomRuleOutput.Location = new System.Drawing.Point(4, 25);
            this.tabPageCustomRuleOutput.Name = "tabPageCustomRuleOutput";
            this.tabPageCustomRuleOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCustomRuleOutput.Size = new System.Drawing.Size(872, 668);
            this.tabPageCustomRuleOutput.TabIndex = 1;
            this.tabPageCustomRuleOutput.Text = "Результат";
            this.tabPageCustomRuleOutput.UseVisualStyleBackColor = true;
            // 
            // webCustomRule
            // 
            this.webCustomRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webCustomRule.Location = new System.Drawing.Point(3, 3);
            this.webCustomRule.MinimumSize = new System.Drawing.Size(20, 20);
            this.webCustomRule.Name = "webCustomRule";
            this.webCustomRule.Size = new System.Drawing.Size(866, 662);
            this.webCustomRule.TabIndex = 0;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button1);
            this.groupBox9.Controls.Add(this.btnCustomRule);
            this.groupBox9.Controls.Add(this.monthCalendarCustomRule);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox9.Location = new System.Drawing.Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(234, 697);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Календарь";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(9, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Настройки...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnCustomRule
            // 
            this.btnCustomRule.Location = new System.Drawing.Point(9, 324);
            this.btnCustomRule.Name = "btnCustomRule";
            this.btnCustomRule.Size = new System.Drawing.Size(192, 29);
            this.btnCustomRule.TabIndex = 9;
            this.btnCustomRule.Text = "Сформировать";
            this.btnCustomRule.UseVisualStyleBackColor = true;
            this.btnCustomRule.Click += new System.EventHandler(this.btnCustomRule_Click);
            // 
            // monthCalendarCustomRule
            // 
            this.monthCalendarCustomRule.Location = new System.Drawing.Point(9, 29);
            this.monthCalendarCustomRule.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendarCustomRule.MaxSelectionCount = 1;
            this.monthCalendarCustomRule.Name = "monthCalendarCustomRule";
            this.monthCalendarCustomRule.ShowToday = false;
            this.monthCalendarCustomRule.ShowTodayCircle = false;
            this.monthCalendarCustomRule.TabIndex = 8;
            this.monthCalendarCustomRule.TodayDate = new System.DateTime(2017, 9, 28, 0, 0, 0, 0);
            // 
            // txtCustomRule
            // 
            this.txtCustomRule.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TypiconOnline.WinForms.Properties.Settings.Default, "CustomRule", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCustomRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCustomRule.Location = new System.Drawing.Point(3, 3);
            this.txtCustomRule.Multiline = true;
            this.txtCustomRule.Name = "txtCustomRule";
            this.txtCustomRule.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCustomRule.Size = new System.Drawing.Size(866, 662);
            this.txtCustomRule.TabIndex = 2;
            this.txtCustomRule.Text = global::TypiconOnline.WinForms.Properties.Settings.Default.CustomRule;
            // 
            // CustomRuleViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 697);
            this.Controls.Add(this.tabControlCustomRule);
            this.Controls.Add(this.groupBox9);
            this.Name = "CustomRuleViewer";
            this.Text = "CustomRuleViewer";
            this.tabControlCustomRule.ResumeLayout(false);
            this.tabPageCustomRule.ResumeLayout(false);
            this.tabPageCustomRule.PerformLayout();
            this.tabPageCustomRuleOutput.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlCustomRule;
        private System.Windows.Forms.TabPage tabPageCustomRule;
        private System.Windows.Forms.TextBox txtCustomRule;
        private System.Windows.Forms.TabPage tabPageCustomRuleOutput;
        private System.Windows.Forms.WebBrowser webCustomRule;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCustomRule;
        private System.Windows.Forms.MonthCalendar monthCalendarCustomRule;
    }
}