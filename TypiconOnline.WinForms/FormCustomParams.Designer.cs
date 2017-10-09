namespace ScheduleForm
{
    partial class FormCustomParams
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabStichera = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowPsalm = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalYmnosCount = new System.Windows.Forms.MaskedTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabStichera.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabStichera);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(651, 457);
            this.tabControl1.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Location = new System.Drawing.Point(4, 25);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(643, 428);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Основные";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabStichera
            // 
            this.tabStichera.Controls.Add(this.groupBox1);
            this.tabStichera.Location = new System.Drawing.Point(4, 25);
            this.tabStichera.Name = "tabStichera";
            this.tabStichera.Padding = new System.Windows.Forms.Padding(3);
            this.tabStichera.Size = new System.Drawing.Size(643, 428);
            this.tabStichera.TabIndex = 1;
            this.tabStichera.Text = "Стихиры";
            this.tabStichera.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTotalYmnosCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxShowPsalm);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 168);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Господи воззвах";
            // 
            // checkBoxShowPsalm
            // 
            this.checkBoxShowPsalm.AutoSize = true;
            this.checkBoxShowPsalm.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxShowPsalm.Location = new System.Drawing.Point(6, 21);
            this.checkBoxShowPsalm.Name = "checkBoxShowPsalm";
            this.checkBoxShowPsalm.Size = new System.Drawing.Size(163, 21);
            this.checkBoxShowPsalm.TabIndex = 0;
            this.checkBoxShowPsalm.Text = "Показывать псалмы";
            this.checkBoxShowPsalm.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Общее количество стихир";
            // 
            // txtTotalYmnosCount
            // 
            this.txtTotalYmnosCount.Location = new System.Drawing.Point(194, 48);
            this.txtTotalYmnosCount.Mask = "00";
            this.txtTotalYmnosCount.Name = "txtTotalYmnosCount";
            this.txtTotalYmnosCount.Size = new System.Drawing.Size(100, 22);
            this.txtTotalYmnosCount.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(143, 459);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Принять";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(407, 459);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormCustomParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 493);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCustomParams";
            this.Text = "Настройки ";
            this.tabControl1.ResumeLayout(false);
            this.tabStichera.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabStichera;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxShowPsalm;
        private System.Windows.Forms.MaskedTextBox txtTotalYmnosCount;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}