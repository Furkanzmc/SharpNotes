using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SharpNotes
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        #region Web Page Source
        string getPageSource(string URL)
        {

            System.Net.WebClient webClient = new System.Net.WebClient();
            string strSource = webClient.DownloadString(URL);
            webClient.Dispose();
            return strSource;

        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            string[] lines = null;
            TextBox formtext = null;
            foreach (TextBox textBox in form1.tabControl1.SelectedTab.Controls)
            {
                formtext = textBox;
                lines = textBox.Lines;
            }
            StringBuilder extract = new StringBuilder();

            #region GETURL
            if (textBox1.Text.Substring(0, 6) == "GETURL")
            {
                try
                {
                    if (textBox1.Text.Substring(7, 7) != "http://")
                    {
                        textBox1.Text = textBox1.Text.Insert(7, "http://");
                        textBox2.Text = getPageSource(textBox1.Text.Substring(7, (textBox1.Text.Length - 1) - 7));
                        this.Text = "Getting the content of the web page...";
                        Application.DoEvents();
                    }
                    else
                    {

                        textBox2.Text = getPageSource(textBox1.Text.Substring(7, (textBox1.Text.Length - 1) - 7));
                        this.Text = "Getting the content of the web page...";
                        Application.DoEvents();
                    }
                    this.Text = "Extract Text";
                }
                catch (Exception ex)
                {
                    textBox2.Text = "ERROR!\r\nWrong Command!\r\nEorror Message: \r\n-----------------------------------\r\n" + ex.ToString();
                }
            }
            #endregion

            #region GETLINE
            else
            {
                if (textBox1.Text.Substring(0, 7) == "GETLINE")
                {
                    try
                    {
                        int line = Convert.ToInt32(textBox1.Text.Substring(8, textBox1.Text.Length - 9)) - 1;

                        textBox2.Text = lines[line];

                    }
                    catch (Exception ex)
                    {
                        textBox2.Text = "ERROR!\r\nWrong Command!\r\nEorror Message: \r\n-----------------------------------\r\n" + ex.ToString();
                    }
                }
            #endregion

                #region GETPARTOFLINE1
                else
                {
                    if (textBox1.Text.Substring(0, 14) == "GETPARTOFLINE1")
                    {
                        try
                        {
                            string asdq = textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15);
                            int index = Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15));
                            string starttext = textBox1.Text.Substring(textBox1.Text.IndexOf(";") + 1, textBox1.Text.LastIndexOf(";") - textBox1.Text.IndexOf(";") - 1);
                            string stoptext = textBox1.Text.Substring(textBox1.Text.LastIndexOf(";") + 1, textBox1.Text.LastIndexOf("]") - textBox1.Text.LastIndexOf(";") - 1);
                            if (index != 0)
                            {
                                textBox2.Text = lines[index - 1].Substring(formtext.Text.IndexOf(starttext), formtext.Text.IndexOf(stoptext) - formtext.Text.IndexOf(starttext));
                            }
                            else
                            {
                                for (int i = 0; i < lines.Count(); i++)
                                {
                                    extract.AppendLine(lines[i].Substring(formtext.Text.IndexOf(starttext), formtext.Text.IndexOf(stoptext) - formtext.Text.IndexOf(starttext) - 1));
                                }
                                textBox2.Text = extract.ToString();
                                extract = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            textBox2.Text = "ERROR!\r\nWrong Command!\r\nEorror Message: \r\n-----------------------------------\r\n" + ex.ToString();
                        }

                    }
                #endregion

                    #region GETPARTOFLINE2
                    else
                    {
                        if (textBox1.Text.Substring(0, 14) == "GETPARTOFLINE2")
                        {
                            try
                            {
                                string startindex = textBox1.Text.Substring(textBox1.Text.IndexOf(";") + 1, textBox1.Text.LastIndexOf(";") - textBox1.Text.IndexOf(";") - 1);
                                string stopindex = textBox1.Text.Substring(textBox1.Text.LastIndexOf(";") + 1, textBox1.Text.LastIndexOf("]") - textBox1.Text.LastIndexOf(";") - 1);
                                if (Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15)) != 0)
                                {
                                    textBox2.Text = lines[Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15)) - 1].Substring(Convert.ToInt32(startindex), Convert.ToInt32(stopindex) - Convert.ToInt32(startindex));
                                }
                                else
                                {
                                    for (int i = 0; i < lines.Count(); i++)
                                    {
                                        extract.AppendLine(lines[i].Substring(Convert.ToInt32(startindex), Convert.ToInt32(stopindex) - Convert.ToInt32(startindex)));
                                    }
                                    textBox2.Text = extract.ToString();
                                    extract = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                textBox2.Text = "ERROR!\r\nWrong Command!\r\nEorror Message: \r\n-----------------------------------\r\n" + ex.ToString();
                            }
                        }
                    #endregion

                        #region GETPARTOFLINE3
                        else
                        {
                            if (textBox1.Text.Substring(0, 14) == "GETPARTOFLINE3")
                            {
                                try
                                {
                                    string stopindex = textBox1.Text.Substring(textBox1.Text.LastIndexOf(";") + 1, textBox1.Text.LastIndexOf("]") - textBox1.Text.LastIndexOf(";") - 1);
                                    string startindex = textBox1.Text.Substring(textBox1.Text.IndexOf(";") + 1, textBox1.Text.LastIndexOf(";") - textBox1.Text.IndexOf(";") - 1);
                                    if (Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15)) != 0)
                                    {
                                        textBox2.Text = lines[Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15)) - 1].Substring(Convert.ToInt32(startindex), formtext.Text.IndexOf(stopindex) - formtext.Text.IndexOf(startindex) - 1);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < lines.Count(); i++)
                                        {
                                            extract.AppendLine(lines[i].Substring(Convert.ToInt32(startindex), textBox2.Text.IndexOf(stopindex) - textBox2.Text.IndexOf(startindex) - 2));
                                        }
                                        textBox2.Text = extract.ToString();
                                        extract = null;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    textBox2.Text = "ERROR!\r\nWrong Command!\r\nEorror Message: \r\n-----------------------------------\r\n" + ex.ToString();
                                }
                            }
                        #endregion

                            #region GETPARTOFLINE4
                            else
                            {
                                if (textBox1.Text.Substring(0, 14) == "GETPARTOFLINE4")
                                {
                                    try
                                    {
                                        string startindex = textBox1.Text.Substring(textBox1.Text.IndexOf(";") + 1, textBox1.Text.LastIndexOf(";") - textBox1.Text.IndexOf(";") - 1);
                                        string stopindex = textBox1.Text.Substring(textBox1.Text.LastIndexOf(";") + 1, textBox1.Text.LastIndexOf("]") - textBox1.Text.LastIndexOf(";") - 1);
                                        if (Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15)) != 0)
                                        {
                                            textBox2.Text = lines[Convert.ToInt32(textBox1.Text.Substring(15, textBox1.Text.IndexOf(";") - 15)) - 1].Substring(formtext.Text.IndexOf(startindex), Convert.ToInt32(stopindex) - formtext.Text.IndexOf(startindex) - 1);
                                        }
                                        else
                                        {
                                            for (int i = 0; i < lines.Count(); i++)
                                            {
                                                extract.AppendLine(lines[i].Substring(formtext.Text.IndexOf(startindex), Convert.ToInt32(stopindex) - formtext.Text.IndexOf(startindex) - 1));
                                            }
                                            textBox2.Text = extract.ToString();
                                            extract = null;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        textBox2.Text = "ERROR!\r\nWrong Command!\r\nEorror Message: \r\n-----------------------------------\r\n" + ex.ToString();
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            this.Size = new Size(779, 337);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.Size = new Size(476, 337);
            }
            else
            {
                this.Size = new Size(476, 140);
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath.ToString() + "\\extracthelp.html");
        }
    }
}
