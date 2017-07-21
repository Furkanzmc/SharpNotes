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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                StringBuilder stringbuilder = new StringBuilder();
                if (this.Text == "Combine Lines")
                {
                    stringbuilder.Append(lines[0]);
                    for (int i = 1; i < formtext.Lines.Count(); i++)
                    {
                        stringbuilder.Append(textBox1.Text + lines[i].ToString());
                        Application.DoEvents();
                    }
                    formtext.Text = stringbuilder.ToString().Trim('\r', '\n');
                    Application.DoEvents();
                    formtext.Text.Trim('\r', '\n');
                    this.Close();
                }
                else if (this.Text == "Split Line With...")
                {
                    for (int i = 0; i < lines[(int)numericUpDown1.Value - 1].Split(Convert.ToChar(textBox1.Text)).Count(); i++)
                    {
                        stringbuilder.AppendLine(lines[(int)numericUpDown1.Value - 1].Split(Convert.ToChar(textBox1.Text))[i]);
                    }
                    lines[(int)numericUpDown1.Value - 1] = stringbuilder.ToString();
                    formtext.Lines = lines;
                    formtext.Text.Trim('\r', '\n');
                }
                else if (this.Text == "Surround Lines With...")
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        lines[i] = lines[i].Insert(0, textBox1.Text);
                        lines[i] = lines[i].Insert(lines[i].Length, textBox2.Text);
                    }
                    formtext.Lines = lines;
                    formtext.Text.Trim('\r', '\n');
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.type == "surround")
            {
                this.Text = "Surround Lines With...";
                label1.Text = "First Char";
                textBox1.MaxLength = 1;
                label2.Visible = false;
                numericUpDown1.Visible = false;
                label3.Visible = true;
                textBox2.Visible = true;
            }
            else if (Properties.Settings.Default.type == "split")
            {
                this.Size = new Size(222, 130);
                this.Text = "Split Line With...";
                label1.Text = "Split Line With...";
                textBox1.MaxLength = 1;
                button1.Location = new Point(9, 76);
                button2.Location = new Point(129, 76);
            }
            else if (Properties.Settings.Default.type == "combine")
            {
                label2.Visible = false;
                numericUpDown1.Visible = false;
                this.Size = new Size(222, 90);
                button1.Location = new Point(9, 37);
                button2.Location = new Point(129, 39);
            }
        }
    }
}
