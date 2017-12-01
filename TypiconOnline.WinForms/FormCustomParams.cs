using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.WinForms
{
    public partial class FormCustomParams : Form
    {
        public CustomParamsCollection<IRuleApplyParameter> CustomParameters { get; set; }

        public FormCustomParams()
        {
            InitializeComponent();

            CustomParameters = new CustomParamsCollection<IRuleApplyParameter>();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Stichera
            KekragariaCustomParameter kekragaria = new KekragariaCustomParameter();
            kekragaria.ShowPsalm = checkBoxShowPsalm.Checked;
            if (!string.IsNullOrEmpty(txtTotalYmnosCount.Text))
            {
                kekragaria.TotalYmnosCount = int.Parse(txtTotalYmnosCount.Text);
            }

            CustomParameters.Add(kekragaria);
        }
    }
}
