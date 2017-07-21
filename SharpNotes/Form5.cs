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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (SharpNotes.Properties.Settings.Default.type == "Code")
            {
                this.Text = "Code";
            }
            else
            {
                this.Text = "Decode";
            }
        }

        int şans = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                StringBuilder build = new StringBuilder();
                if (this.Text == "Code")
                {
                    string[] lines = formtext.Lines;
                    string line = null;
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        foreach (char text in lines[i])
                        {
                            int ilk = Convert.ToInt32(text) * 6;
                            line += Convert.ToChar(ilk).ToString();
                        }
                        build.AppendLine(line);
                        line = null;
                    }
                    formtext.Text = null;
                    formtext.Text = build.ToString().Trim('\r', '\n');
                    foreach (char id in textBox1.Text)
                    {
                        int ilk = Convert.ToInt32(id) * 6;
                        line += Convert.ToChar(ilk).ToString();
                    }
                    formtext.AppendText("\r\n" + line);
                    line = null;
                    this.Close();
                }
                else
                {
                    string[] satırlar = formtext.Lines;
                    string[] satır = new string[satırlar.Count() - 1];
                    string satir = null;
                    foreach (char name in satırlar[satırlar.Count() - 1])
                    {
                        int ilk = Convert.ToInt32(name);
                        int son = ilk / 6;
                        satir += Convert.ToChar(son).ToString();
                    }
                    if (satir == textBox1.Text)
                    {
                        satırlar[satırlar.Count() - 1] = "";
                        for (int i = 0; i < satırlar.Count(); i++)
                        {
                            foreach (char isim in satırlar[i])
                            {
                                int ilk = Convert.ToInt32(isim);
                                int son = ilk / 6;
                                satır[i] += Convert.ToChar(son).ToString();
                            }
                        }
                        formtext.Lines = satır;
                        satir = null;
                        formtext.Text.Trim('\r', '\n');
                    }
                    else
                    {
                        şans++;
                        if (şans == 1)
                        {
                            MessageBox.Show("You have two chances left! Be careful!");
                        }
                        if (şans == 2)
                        {
                            MessageBox.Show("You have one chance left! Be careful!");
                        }
                        if (şans == 3)
                        {
                            MessageBox.Show("You idiot! This was your last chance! You've lost your crytped text!");
                            foreach (char name in "idiot")
                            {
                                int ilk = Convert.ToInt32(name);
                                int son = ilk * 6;
                                satir += Convert.ToChar(son).ToString();
                            }
                            formtext.AppendText(satir);
                        }
                        formtext.Text.Trim('\r', '\n');
                    }
                }
                formtext.Text.Trim('\r', '\n');
                build = null;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
