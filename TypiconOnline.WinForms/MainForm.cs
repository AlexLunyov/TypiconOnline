using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ScheduleHandling;

namespace ScheduleForm
{
    public partial class MainForm : Form
    {
        private ScheduleHandler scheduleHandler;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDB();

            scheduleHandler = new ScheduleHandler(scheduleDBDataSet);
        }

        private void dataGridViewFeatures_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ((DataGridView)sender).CurrentRow.Cells[1].Value = "0";
        }

        private void dataGridViewFeatures_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void FillDB()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.Paskhalia". При необходимости она может быть перемещена или удалена.
            this.paskhaliaTableAdapter.Fill(this.scheduleDBDataSet.Paskhalia);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.Triodion". При необходимости она может быть перемещена или удалена.
            this.triodionTableAdapter.Fill(this.scheduleDBDataSet.Triodion);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.Mineinik". При необходимости она может быть перемещена или удалена.
            this.mineinikTableAdapter.Fill(this.scheduleDBDataSet.Mineinik);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.ServiceFeatures". При необходимости она может быть перемещена или удалена.
            this.serviceFeaturesTableAdapter.Fill(this.scheduleDBDataSet.ServiceFeatures);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.ServiceSigns". При необходимости она может быть перемещена или удалена.
            this.serviceSignsTableAdapter.Fill(this.scheduleDBDataSet.ServiceSigns);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.Paskhalia". При необходимости она может быть перемещена или удалена.
            //this.paskhaliaTableAdapter.Fill(this.scheduleDBDataSet.Paskhalia);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.Mineinik". При необходимости она может быть перемещена или удалена.
            //this.mineinikTableAdapter.Fill(this.scheduleDBDataSet.Mineinik);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.ServiceFeatures". При необходимости она может быть перемещена или удалена.
            this.serviceFeaturesTableAdapter.Fill(this.scheduleDBDataSet.ServiceFeatures);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "scheduleDBDataSet.ServiceSigns". При необходимости она может быть перемещена или удалена.
            this.serviceSignsTableAdapter.Fill(this.scheduleDBDataSet.ServiceSigns);
        }

        

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            /*this.mineinikTableAdapter.Update(this.scheduleDBDataSet.Mineinik);
            this.triodionTableAdapter.Update(this.scheduleDBDataSet.Triodion);
            */

            try
            {
                this.Validate();

                string result = "serviceFeaturesTableAdapter ";
                this.serviceSignsServiceFeaturesBindingSource.EndEdit();
                result += this.serviceFeaturesTableAdapter.Update(this.scheduleDBDataSet.ServiceFeatures).ToString();

                this.serviceSignsBindingSource.EndEdit();
                result += " serviceSignsTableAdapter ";
                result += this.serviceSignsTableAdapter.Update(this.scheduleDBDataSet.ServiceSigns).ToString();

                this.mineinikBindingSource.EndEdit();
                result += " mineinikTableAdapter ";
                result += mineinikTableAdapter.Update(scheduleDBDataSet.Mineinik).ToString();
                paskhaliaBindingSource.EndEdit();
                result += " paskhaliaTableAdapter ";
                result += paskhaliaTableAdapter.Update(scheduleDBDataSet.Paskhalia).ToString();
                triodionBindingSource.EndEdit();
                result += " triodionTableAdapter ";
                result += triodionTableAdapter.Update(scheduleDBDataSet.Triodion).ToString();

                MessageBox.Show(result);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillDB();
            statusStripLabel.Text = "Зарузка данных проведена успешно";
        }

        private void buttonCheckPaskha_Click(object sender, EventArgs e)
        {
            string message = "Не входит";
            if (scheduleHandler.IsDateWithinTriodion(dateTimePickerCheck.Value, scheduleHandler.GetEaster(dateTimePickerCheck.Value)))
            {
                message = "Входит";
            }
            labelCheck.Text = message;
        }

        private void dateTimePickerCheck_ValueChanged(object sender, EventArgs e)
        {
            labelWeekName.Text = scheduleHandler.GetWeekName(dateTimePickerCheck.Value, checkBoxShortName.Checked);
        }

        private void buttonGetXML_Click(object sender, EventArgs e)
        {
            System.Xml.XmlDocument xmlDoc = null;

            try
            {
                xmlDoc = scheduleHandler.GetScheduleWeekXML(monthCalendarXML.SelectionRange.Start);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            if (xmlDoc != null)
            {
                textBoxXML.Text = xmlDoc.InnerXml;
                statusStripLabel.Text = "Формирование Docx шаблона проведена успешно.";
            }
            else
            {
                statusStripLabel.Text = "Ничего не получилось";
            }
        }

        private void тестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelTest.Visible = true;
            tabControlTables.Visible = false;
        }

        private void таблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelTest.Visible = false;
            tabControlTables.Visible = true;
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            /*var valuesToFill = new Content(
                new TableContent ("Projects Table")
                    .AddRow(
                        new FieldContent("Name", "Eric"),
                        new FieldContent("Role", "Program Manager"),
                        new TableContent("Projects")
                            .AddRow(new FieldContent("Project", "Project one"))
                            .AddRow(new FieldContent("Project", "Project two")))
                    .AddRow(
                        new FieldContent("Name", "Bob"),
                        new FieldContent("Role", "Developer"),
                        new TableContent("Projects")
                            .AddRow(new FieldContent("Project", "Project one"))
                            .AddRow(new FieldContent("Project", "Project three"))));*/
            /*var valuesToFill = new Content(
                new tablecontent("table")
                    .addrow(
                        new fieldcontent("dayofweek", "вторник"),
                        new TableContent("Dayservices")
                            .AddRow(
                                new FieldContent("time", "1111"),
                            new FieldContent("name", "333333333"))
                            .AddRow(
                                new FieldContent("time", "2222"),
                            new FieldContent("name", "444444444"))
                    )*/


            //new FieldContent("Week name", "Название таблицы"),
            /* var valuesToFill = new Content(
                 new FieldContent("daystable", "")
                         new TableContent("daystable")
                     .AddRow(
                         new FieldContent("headercommon", ""),
                         new ListContent("headergreat")
                             .AddItem(
                                 new FieldContent("sign", "@"),
                                 new FieldContent("dayofweek", "ВТОРНИК"),
                                 new FieldContent("dayname", "Вторник Светлой седмицы.")),
                         new FieldContent("daydate", "25 июля 2017 г."),
                         new ListContent("dayservices")
                             .AddItem(
                                 new FieldContent("time", "06.00"),
                                 new FieldContent("servicename", "Полуношница"))
                             .AddItem(
                                 new FieldContent("time", "09.00"),
                                 new FieldContent("servicename", "Литургия")))*/
            /*.AddRow(
                new FieldContent("sign", "%"),
                new FieldContent("dayofweek", "среда"),
                new FieldContent("dayname", "Среда Светлой седмицы."),
                new FieldContent("daydate", "26 июля 2017 г."),
                new ListContent("dayservices")
                    .AddItem(
                        new FieldContent("time", "09.00"),
                        new FieldContent("servicename", "Литургия")))

            );*/

            /*var valuesToFill = new Content(
                new TableContent("tablesl")
                    .AddRow(
                        new FieldContent("sign", "@"),
                        new FieldContent("weekname", "ПОНЕДЕЛЬНИК"),
                        new FieldContent("name", "Предпразднство Рождества Христова. Прав. Иоанна Кронштадтского")),
                new TableContent("tablebd")
                    .AddRow(
                        new FieldContent("sign", "^"),
                        new FieldContent("weekname", "СУББОТА"),
                        new FieldContent("name", "РОЖДЕСТВО ХРИСТОВО.")),
                new TableContent("tablesl")
                    .AddRow(
                        new FieldContent("sign", "@"),
                        new FieldContent("weekname", "ВТОРНИК"),
                        new FieldContent("name", "Предпразднство Рождества Христова. Прав. Иоанна Кронштадтского")));*/

            System.Xml.XmlDocument xmlDoc = null;

            /*try
            {*/
                //получаем xml с расписанием
                xmlDoc = scheduleHandler.GetScheduleWeekXML(monthCalendarXML.SelectionRange.Start);
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/

            if (xmlDoc != null)
            {
                textBoxXML.Text = xmlDoc.InnerXml;
                string fileName = "Template.docx";
                string testFileName = "Test" + fileName;
                if (System.IO.File.Exists(testFileName))
                    System.IO.File.Delete(testFileName);
                System.IO.File.Copy(fileName, testFileName);

                //заполняем шаблон
                string result = scheduleHandler.FillDocxTemplate(testFileName);

                if (result == "")
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = testFileName;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
                else
                {
                    textBoxXML.Text = result;
                }
            }
            else
            {
                textBoxXML.Text = "Ничего не получилось";
            }

            //string fileName = "Template.docx";
            //string testFileName = "Test" + fileName;
            //if (System.IO.File.Exists(testFileName))
            //    System.IO.File.Delete(testFileName);
            //System.IO.File.Copy(fileName, testFileName);

            //using (var outputDocument = new TemplateProcessor(testFileName)
            //    .SetRemoveContentControls(false))
            //{
            //    outputDocument.FillContent(valuesToFill);
            //    outputDocument.SaveChanges();
            //}

            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.StartInfo.FileName = testFileName;
            //proc.StartInfo.UseShellExecute = true;
            //proc.Start();
        }

        private void buttonFillTemplate_Click(object sender, EventArgs e)
        {
            //var valuesToFill = scheduleHandler.GetScheduleWeekDocxContent(monthCalendarXML.SelectionRange.Start);

            //string fileName = "Template.docx";
            //string testFileName = "Test" + fileName;
            //if (System.IO.File.Exists(testFileName))
            //    System.IO.File.Delete(testFileName);
            //System.IO.File.Copy(fileName, testFileName);

            //using (var outputDocument = new TemplateProcessor(testFileName)
            //    .SetRemoveContentControls(false))
            //{
            //    outputDocument.FillContent(valuesToFill);
            //    outputDocument.SaveChanges();
            //}

            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.StartInfo.FileName = testFileName;
            //proc.StartInfo.UseShellExecute = true;
            //proc.Start();
        }

        private void monthCalendarXML_DateChanged(object sender, DateRangeEventArgs e)
        {
            //labelSundayName.Text = scheduleHandler.GetWeekName(monthCalendarXML.SelectionRange.Start, false);
            labelSundayName.Text = scheduleHandler.GetCurrentSundayName(monthCalendarXML.SelectionRange.Start);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Xml.XmlDocument xmlDoc = null;

            /*try
            {*/
            //получаем xml с расписанием
            xmlDoc = scheduleHandler.GetScheduleWeekXML(monthCalendarXML.SelectionRange.Start);
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/

            if (xmlDoc != null)
            {
                string fileName = "TestTemplate.txt";

                //заполняем шаблон
                string result = scheduleHandler.FillTextTemplate(fileName);

                textBoxXML.Text = result;

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            else
            {
                textBoxXML.Text = "Ничего не получилось";
            }
        }
    }
}
