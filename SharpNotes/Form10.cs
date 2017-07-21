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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Title = "Select File(s)";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.FileNames.Length > 1)
                {
                    listBox1.Items.AddRange(open.FileNames);
                }
                else
                {
                    listBox1.Items.Add(open.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string file = System.IO.File.ReadAllText(listBox1.Items[i].ToString(), Encoding.Default);
                string[] find = textBox1.Text.Split(',');
                string[] replace = textBox2.Text.Split(',');
                for (int s = 0; s < find.Length; s++)
                {
                    if (find[s].ToString() != "")
                    {
                        file = file.Replace(find[s].ToString(), replace[s].ToString());
                    }
                }
                System.IO.File.WriteAllText(listBox1.Items[i].ToString(), file, Encoding.UTF8);
            }
            textBox1.Text = null;
            textBox2.Text = null;
            MessageBox.Show("Progress has been done!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
