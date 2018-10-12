using GameLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game15.App
{
    public partial class StartForm : Form
    {
        
        public StartForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBoxName.Text))
            {
                errorProvider1.SetError(textBoxName, "Ошибка");
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (! String.IsNullOrWhiteSpace(textBoxName.Text))
            {
                errorProvider1.Clear();
            }
        }
        
        
    }
}
