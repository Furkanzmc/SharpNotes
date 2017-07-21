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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                int indeks;
                StringBuilder yardımcı = new StringBuilder();
                for (int i = 0; i < lines.Count(); i++)
                {
                    if ((int)numericUpDown1.Value > lines[i].Length)
                    {
                        lines[i] = "";
                    }
                    else
                    {
                        lines[i] = lines[i].Substring((int)numericUpDown1.Value);
                        indeks = lines[i].Length - (int)numericUpDown2.Value;
                        if (indeks < 0)
                        {
                            lines[i] = "";
                        }
                        else
                        {
                            lines[i] = lines[i].Remove(indeks, (int)numericUpDown2.Value);
                            yardımcı.AppendLine(lines[i]);
                        }
                    }
                }
                formtext.Text = yardımcı.ToString().Trim('\r', '\n');
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
