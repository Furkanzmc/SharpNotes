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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                StringBuilder yazıcı = new StringBuilder(lines.Count());
                if (this.Text == "Insert To End")
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        yazıcı.AppendLine(lines[i] + textBox1.Text);
                    }
                    formtext.Text = yazıcı.ToString().Trim('\r', '\n');
                }
                else
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        yazıcı.AppendLine(lines[i].Insert(0, textBox1.Text));
                    }
                    formtext.Text = yazıcı.ToString().Trim('\r', '\n');
                }
                this.Close();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (SharpNotes.Properties.Settings.Default.type == "end")
            {
                this.Text = "Insert To End";
            }
            else
            {
                this.Text = "Insert To Beginning";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
