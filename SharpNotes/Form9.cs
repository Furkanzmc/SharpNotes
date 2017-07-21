using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpNotes
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GoTo(int line, int column)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                if (line < 0 || column < 0 || formtext.Lines.Count() + 1 < line)
                {
                    return;
                }
                else
                {
                    formtext.Select(formtext.GetFirstCharIndexFromLine(line - 1), column);
                    formtext.ScrollToCaret();
                    formtext.Focus();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                GoTo(Convert.ToInt32(textBox1.Text), 1);
            }
            else
            {
                GoTo(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            }
        }
    }
}
