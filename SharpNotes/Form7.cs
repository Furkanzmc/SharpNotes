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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        bool matchCase;
        int startPos;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                matchCase = true;
            }
            else
            {
                matchCase = false;
            }
        }

        Form1 form1 = Application.OpenForms["Form1"] as Form1;
        private void button1_Click(object sender, EventArgs e)
        {
            TextBox formtext = new TextBox();
            foreach (TextBox textboxes in form1.tabControl1.SelectedTab.Controls)
            {
                formtext = textboxes;
            }
                bool found = false;
                StringComparison type;
                string text = textBox1.Text;
                type = matchCase == true ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                if (startPos == 0)
                {
                    startPos = formtext.Text.IndexOf(text, 0, type);
                }
                else
                {
                    startPos = formtext.Text.IndexOf(text, startPos + text.Length, type);
                    found = true;
                }
                if (!(startPos > -1))
                {
                    if (found != true)
                    {
                        MessageBox.Show("Search text: '" + text + "' could not be found", "Text Not Found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Reached the end of the document!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    found = false;
                    startPos = 0;
                    return;
                }
                else
                {
                    formtext.Select(startPos, text.Length);
                    formtext.ScrollToCaret();
                    if (startPos == 0)
                    {
                        startPos++;
                    }
                }
        }

        #region Load
        private void Form7_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.type == "find")
            {
                this.Text = "Find";
                checkBox1.Location = new Point(98, 43);
                this.Size = new Size(396, 100);
                button2.Location = new Point(293, 33);
                textBox1.Location = new Point(48, 6);
                button1.Location = new Point(293, 4);
                checkBox1.Location = new Point(93, 39);
            }
            else
            {
                this.Text = "Replace";
                button3.Visible = true;
                button4.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;
                button2.Location = new Point(326, 92);
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                formtext.SelectedText = textBox2.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                StringComparison type;
                string text = textBox1.Text;
                type = matchCase == true ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                while (startPos > -1)
                {
                    if (startPos == 0)
                    {
                        startPos = formtext.Text.IndexOf(text, 0, type);
                    }
                    else
                    {
                        if (startPos + text.Length < formtext.Text.Length)
                        {
                            startPos = formtext.Text.IndexOf(text, startPos + text.Length, type);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!(startPos > -1))
                    {
                        MessageBox.Show("Search text: '" + text + "' could not be found", "Text Not Found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        startPos = 0;
                        return;
                    }
                    else
                    {
                        formtext.Select(startPos, text.Length);
                        formtext.SelectedText = textBox2.Text;
                        if (startPos == 0)
                        {
                            startPos++;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
            {
                this.Close();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
            {
                this.Close();
            }
        }

        private void Form7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Escape")
            {
                this.Close();
            }
        }
    }
}
