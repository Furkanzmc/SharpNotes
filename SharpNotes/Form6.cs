using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SharpNotes
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        int sayı;
        private void Form6_Load(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            foreach (TextBox formtext in form1.tabControl1.SelectedTab.Controls)
            {
                string ilkyazı = form1.Text;
                form1.Text = "Analyzing...";
                int index = 0;
                int indeks = 0;

                #region Total Lines, Size, Total Char, Uniqe Chars
                label14.Text = formtext.Lines.Count().ToString();
                label2.Text = formtext.Text.Length.ToString() + " Byte(s)";
                StringBuilder totalchar = new StringBuilder();
                totalchar.Append(formtext.Text);
                string[] chars = new string[1];
                string[] chrs = new string[1];
                string[] usedchar = new string[1];
                foreach (char chr in totalchar.ToString())
                {
                    if (Convert.ToInt32(chr) < 33)
                    {
                    }
                    else
                    {
                        usedchar.SetValue(chr.ToString().ToUpper(), indeks);
                        Array.Resize(ref usedchar, usedchar.Count() + 1);
                        chrs.SetValue(chr.ToString().ToUpper(), indeks);
                        Array.Resize(ref chrs, chrs.Count() + 1);
                        indeks++;
                        if (chars.Contains(chr.ToString().ToUpper()) == false)
                        {
                            chars.SetValue(chr.ToString().ToUpper(), index);
                            Array.Resize(ref chars, chars.Count() + 1);
                            index++;
                            Application.DoEvents();
                        }
                        sayı++;
                    }
                    Application.DoEvents();
                }
                indeks = 0;
                label4.Text = sayı.ToString();
                sayı = -1;
                dataGridView1.Rows.Add(chars.Length - 1);
                Array.Sort(chars);
                Array.Sort(chrs);
                for (int i = 1; i < chars.Count(); i++)
                {
                    if (Convert.ToInt32(Convert.ToChar(chars[i])) < 33)
                    {
                    }
                    else
                    {
                        sayı++;
                        dataGridView1.Rows[sayı].Cells[0].Value = chars[i];
                    }
                    Application.DoEvents();
                }
                label6.Text = (sayı + 1).ToString();
                Array.Sort(usedchar);
                string usedstringchar = null;
                indeks = -1;
                index = 0;
                for (int i = 0; i < chrs.Count(); i++)
                {
                    if (usedchar[i] != usedstringchar)
                    {
                        for (int s = 0; s < chrs.Count(); s++)
                        {
                            if (chrs[s] != null)
                            {
                                if (usedchar[i] == chrs[s])
                                {
                                    chrs[s] = null;
                                    index++;
                                }
                            }
                        }
                        indeks++;
                        string charusage = (Convert.ToDouble(100 * index) / (usedchar.Count() - 1)).ToString();
                        if (charusage.Length < 4)
                        {
                            dataGridView1.Rows[indeks].Cells[2].Value = charusage;
                        }
                        else
                        {
                            dataGridView1.Rows[indeks].Cells[2].Value = charusage.Substring(0, 4);
                        }
                        dataGridView1.Rows[indeks].Cells[1].Value = index.ToString();
                        index = 0;
                        usedstringchar = usedchar[i];
                        Application.DoEvents();
                    }
                }

                #endregion

                #region Total and Uniqe Words
                #region Dizi
                string[] dizi = new string[44];
                dizi.SetValue("1", 0);
                dizi.SetValue("2", 1);
                dizi.SetValue("3", 2);
                dizi.SetValue("4", 3);
                dizi.SetValue("5", 4);
                dizi.SetValue("6", 5);
                dizi.SetValue("7", 6);
                dizi.SetValue("8", 7);
                dizi.SetValue("9", 8);
                dizi.SetValue("0", 9);
                dizi.SetValue("a", 10);
                dizi.SetValue("w", 11);
                dizi.SetValue("e", 12);
                dizi.SetValue("r", 13);
                dizi.SetValue("t", 14);
                dizi.SetValue("y", 15);
                dizi.SetValue("u", 16);
                dizi.SetValue("ı", 18);
                dizi.SetValue("o", 19);
                dizi.SetValue("p", 20);
                dizi.SetValue("ğ", 21);
                dizi.SetValue("ü", 22);
                dizi.SetValue("a", 23);
                dizi.SetValue("s", 24);
                dizi.SetValue("d", 25);
                dizi.SetValue("f", 26);
                dizi.SetValue("g", 27);
                dizi.SetValue("h", 28);
                dizi.SetValue("j", 29);
                dizi.SetValue("k", 30);
                dizi.SetValue("l", 31);
                dizi.SetValue("ş", 32);
                dizi.SetValue("i", 33);
                dizi.SetValue("z", 34);
                dizi.SetValue("x", 35);
                dizi.SetValue("c", 36);
                dizi.SetValue("v", 37);
                dizi.SetValue("b", 38);
                dizi.SetValue("n", 39);
                dizi.SetValue("m", 40);
                dizi.SetValue("ö", 41);
                dizi.SetValue("ç", 42);
                dizi.SetValue("q", 43);
                #endregion

                #region Total Words
                index = 0;
                indeks = 0;
                int sonmu = 0;
                string değer = null;
                string[] array = new string[1];
                string[] usedarrayword = new string[1];
                foreach (char isim in formtext.Text)
                {
                    sonmu++;
                    if (dizi.Contains(isim.ToString().ToLower()))
                    {
                        değer += isim.ToString().ToLower();
                        if (sonmu == formtext.Text.Length)
                        {
                            usedarrayword.SetValue(değer, index);
                            array.SetValue(değer, index);
                        }
                    }
                    else
                    {
                        if (değer == null)
                        {
                        }
                        else
                        {
                            usedarrayword.SetValue(değer, index);
                            Array.Resize(ref usedarrayword, usedarrayword.Count() + 1);
                            array.SetValue(değer, index);
                            Array.Resize(ref array, array.Count() + 1);
                            index++;
                            değer = null;
                            Application.DoEvents();
                        }
                    }
                    Application.DoEvents();
                }
                label8.Text = (array.Count()).ToString();
                #endregion

                #region Uniqe Words, Word Frequency
                index = 0;
                indeks = 0;
                string[] farklı = new string[1];
                değer = null;
                for (int i = 0; i < usedarrayword.Count(); i++)
                {
                    if (farklı.Contains(usedarrayword[i]) == false)
                    {
                        farklı.SetValue(usedarrayword[i], indeks);
                        if (i != usedarrayword.Count() - 1)
                        {
                            Array.Resize(ref farklı, farklı.Count() + 1);
                        }
                        indeks++;
                    }
                }
                label10.Text = (farklı.Count()).ToString();
                indeks = 0;
                Array.Sort(farklı);
                if (farklı.Count() == 1)
                {
                    dataGridView2.Rows.Add(farklı.Count());
                }
                else
                {
                    if (farklı.Contains(null) == false)
                    {
                        dataGridView2.Rows.Add(farklı.Count());
                    }
                    else
                    {
                        dataGridView2.Rows.Add(farklı.Count() - 1);
                    }
                }
                if (farklı.Count() == 1)
                {
                    for (int i = 0; i < farklı.Count(); i++)
                    {
                        dataGridView2.Rows[0].Cells[0].Value = farklı[0];
                        Application.DoEvents();
                    }
                }
                else
                {
                    if (farklı.Contains(null))
                    {
                        for (int i = 1; i < farklı.Count(); i++)
                        {
                            dataGridView2.Rows[indeks].Cells[0].Value = farklı[i];
                            indeks++;
                            Application.DoEvents();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < farklı.Count(); i++)
                        {
                            dataGridView2.Rows[indeks].Cells[0].Value = farklı[i];
                            indeks++;
                            Application.DoEvents();
                        }
                    }
                }
                indeks = -1;
                index = 0;
                Array.Sort(usedarrayword);
                Array.Sort(array);
                string usedword = null;
                for (int i = 0; i < array.Count(); i++)
                {
                    if (usedarrayword[i] != usedword)
                    {
                        for (int s = 0; s < array.Count(); s++)
                        {
                            if (array[s] != null)
                            {
                                if (usedarrayword[i] == array[s])
                                {
                                    array[s] = null;
                                    index++;
                                }
                            }
                        }
                        indeks++;
                        string wordusage = (Convert.ToDouble(100 * index) / (usedarrayword.Count())).ToString();
                        if (wordusage.Length < 4)
                        {
                            dataGridView2.Rows[indeks].Cells[2].Value = wordusage;
                        }
                        else
                        {
                            dataGridView2.Rows[indeks].Cells[2].Value = wordusage.Substring(0, 4);
                        }
                        dataGridView2.Rows[indeks].Cells[1].Value = index.ToString();
                        index = 0;
                        usedword = usedarrayword[i];
                        Application.DoEvents();
                    }
                }
                indeks = 0;
                index = 0;
                #endregion
                #endregion

                #region Uniqe Lines, Line Frequency
                index = 0;
                indeks = -1;
                string[] lines = formtext.Lines;
                Array.Sort(lines);
                string[] dif = new string[1];
                string nameline = lines[0];
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (dif.Contains(lines[i]) == false)
                    {
                        dif.SetValue(lines[i], index);
                        Array.Resize(ref dif, index + 2);
                        index++;
                    }
                    Application.DoEvents();
                }
                label16.Text = (dif.Count() - 1).ToString();
                dataGridView3.Rows.Add(dif.Count() - 1);
                for (int i = 0; i < dif.Count() - 1; i++)
                {
                    dataGridView3.Rows[i].Cells[0].Value = dif[i];
                    Application.DoEvents();
                }
                index = 0;
                string[] used = formtext.Lines;
                Array.Sort(used);
                string usedline = null;
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (used[i] != usedline)
                    {
                        for (int s = 0; s < lines.Count(); s++)
                        {
                            if (lines[s] != null)
                            {
                                if (used[i] == lines[s])
                                {
                                    lines[s] = null;
                                    index++;
                                }
                            }
                        }
                        indeks++;
                        string lineusage = (Convert.ToDouble(100 * index) / (used.Count())).ToString();
                        if (lineusage.Length < 4)
                        {
                            dataGridView3.Rows[indeks].Cells[2].Value = lineusage;
                        }
                        else
                        {
                            dataGridView3.Rows[indeks].Cells[2].Value = lineusage.Substring(0, 4);
                        }
                        dataGridView3.Rows[indeks].Cells[1].Value = index.ToString();
                        index = 0;
                        usedline = used[i];
                        Application.DoEvents();
                    }
                    Application.DoEvents();
                }
                indeks = 0;
                index = 0;
                #endregion

                #region Richness
                string richness = (Convert.ToDouble(label8.Text) / Convert.ToDouble(label10.Text)).ToString();
                if (richness.Length < 4)
                {
                    label12.Text = richness;
                }
                else
                {
                    label12.Text = richness.Substring(0, 4);
                }
                #endregion
                form1.Text = ilkyazı;
            }
        }

        #region Char
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save";
            string csvbaşı = null;
            StringBuilder csv = new StringBuilder();
            save.Filter = "Csv Files |*.csv";
            if (save.ShowDialog() == DialogResult.OK)
            {
                csv.AppendLine("Char;Usage;Ratio");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int s = 0; s < dataGridView1.Rows[i].Cells.Count; s++)
                    {
                        csvbaşı += '"' + (dataGridView1.Rows[i].Cells[s].Value.ToString() + '"' + ";");
                    }
                    csv.AppendLine(csvbaşı);
                    csvbaşı = null;
                }
                FileStream fs = new FileStream(save.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                writer.Write(csv.ToString());
                writer.Close();
            }
        }
        #endregion

        #region Word
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save";
            string csvbaşı = null;
            StringBuilder csv = new StringBuilder();
            save.Filter = "Csv Files |*.csv";
            if (save.ShowDialog() == DialogResult.OK)
            {
                csv.AppendLine("Word;Usage;Ratio");
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int s = 0; s < dataGridView2.Rows[i].Cells.Count; s++)
                    {
                        csvbaşı += '"' + (dataGridView2.Rows[i].Cells[s].Value.ToString() + '"' + ";");
                    }
                    csv.AppendLine(csvbaşı);
                    csvbaşı = null;
                }
                FileStream fs = new FileStream(save.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                writer.Write(csv.ToString());
                writer.Close();
            }
        }
        #endregion

        #region Line
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save";
            string csvbaşı = null;
            StringBuilder csv = new StringBuilder();
            save.Filter = "Csv Files |*.csv";
            if (save.ShowDialog() == DialogResult.OK)
            {
                csv.AppendLine("Line;Usage;Ratio");
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    for (int s = 0; s < dataGridView3.Rows[i].Cells.Count; s++)
                    {
                        csvbaşı += '"' + (dataGridView3.Rows[i].Cells[s].Value.ToString() + '"' + ";");
                    }
                    csv.AppendLine(csvbaşı);
                    csvbaşı = null;
                }
                FileStream fs = new FileStream(save.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                writer.Write(csv.ToString());
                writer.Close();
            }
        }
        #endregion
    }
}
