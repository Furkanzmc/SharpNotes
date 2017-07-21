using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace SharpNotes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region OpenFile When Program Is running
        public void OpenFile(string filename)
        {
            int selectedtabindex = 0;
            string başlık = filename.Substring(filename.LastIndexOf("\\"), filename.Length - filename.LastIndexOf("\\"));
            başlık = başlık.Remove(0, 1);
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader Reader = new StreamReader(fs, Encoding.UTF8);
            String FileText = Reader.ReadToEnd();
            Reader.Close();
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Text == başlık)
                {
                    selectedtabindex = -1;
                    tabControl1.SelectedIndex = i;
                    break;
                }
            }
            if (selectedtabindex == 0)
            {
                if (tabControl1.SelectedTab.Controls[0].Name == "ava")
                {
                    tabControl1.SelectedTab.Controls[0].Text = FileText;
                    tabControl1.SelectedTab.Controls[0].Name = filename;
                    tabControl1.SelectedTab.Text = başlık;
                    this.Text = başlık + " - SharpNotes";
                    tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                    System.Collections.ArrayList list = new System.Collections.ArrayList();
                    if (Properties.Settings.Default.recent != null)
                    {
                        list.AddRange(Properties.Settings.Default.recent.ToArray());
                    }
                    if (list.Contains(filename) == false)
                    {
                        list.Add(filename);
                    }
                    Properties.Settings.Default.recent = list;
                    Properties.Settings.Default.Save();
                    int Width = tabControl1.Size.Width;
                    int Height = tabControl1.Size.Height;
                    tabControl1.SelectedTab.Size = new Size(Width + 5, Height);
                }
                else
                {
                    TextBox textbox = new TextBox();
                    textbox.AcceptsTab = true;
                    textbox.WordWrap = false;
                    textbox.Multiline = true;
                    textbox.Dock = DockStyle.Fill;
                    textbox.ScrollBars = ScrollBars.Both;
                    textbox.AllowDrop = true;
                    textbox.HideSelection = false;
                    textbox.MaxLength = 2147483647;
                    textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
                    textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
                    textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                    textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
                    textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
                    textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
                    textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
                    textbox.BackColor = Properties.Settings.Default.background;
                    textbox.Font = Properties.Settings.Default.font;
                    textbox.ForeColor = Properties.Settings.Default.color;
                    textbox.Name = filename;
                    tabControl1.TabPages.Add(başlık);
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
                    textbox.Text = FileText;
                    tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                    this.Text = başlık + " - SharpNotes";
                    textbox.BackColor = Properties.Settings.Default.background;
                    textbox.Font = Properties.Settings.Default.font;
                    textbox.ForeColor = Properties.Settings.Default.color;
                    System.Collections.ArrayList list = new System.Collections.ArrayList();
                    if (Properties.Settings.Default.recent != null)
                    {
                        list.AddRange(Properties.Settings.Default.recent.ToArray());
                    }
                    if (list.Contains(textbox.Name) == false)
                    {
                        list.Add(textbox.Name);
                    }
                    Properties.Settings.Default.recent = list;
                    Properties.Settings.Default.Save();
                    textbox.TextChanged += new EventHandler(textbox_TextChanged);
                }
            }
        }
        #endregion

        #region Sending The Message To Open The File
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_COPYDATA)
            {
                NativeMethods.COPYDATASTRUCT mystr = new NativeMethods.COPYDATASTRUCT();
                Type mytype = mystr.GetType();
                mystr = (NativeMethods.COPYDATASTRUCT)m.GetLParam(mytype);
                OpenFile(mystr.lpData);
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            base.WndProc(ref m);
        }
        #endregion

        #region OOP's
        string yol;
        #region File
        #region New
        int filename = 0;
        public void New()
        {
            TextBox textbox = new TextBox();
            textbox.WordWrap = false;
            textbox.AcceptsTab = true;
            textbox.Multiline = true;
            textbox.Dock = DockStyle.Fill;
            textbox.ScrollBars = ScrollBars.Both;
            textbox.AllowDrop = true;
            textbox.MaxLength = 2147483647;
            textbox.Name = "ava";
            textbox.BackColor = Properties.Settings.Default.background;
            textbox.Font = Properties.Settings.Default.font;
            textbox.ForeColor = Properties.Settings.Default.color;
            textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
            textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
            textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
            textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
            textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
            textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
            textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
            textbox.TextChanged += new EventHandler(textbox_TextChanged);
            textbox.BackColor = Properties.Settings.Default.background;
            textbox.Font = Properties.Settings.Default.font;
            textbox.ForeColor = Properties.Settings.Default.color;
            filename++;
            tabControl1.TabPages.Add("New " + filename.ToString());
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }
        #endregion

        #region Open
        public void Open()
        {
            int selectedtabindex = 0;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text Files(*.txt) |*.txt| All Files(*.*) |*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    if (tabControl1.TabPages[i].Text == open.SafeFileName)
                    {
                        selectedtabindex = -1;
                        tabControl1.SelectedIndex = i;
                        break;
                    }
                }
                if (selectedtabindex == 0)
                {
                    FileStream fs = new FileStream(open.FileName, FileMode.Open);
                    StreamReader Reader = new StreamReader(fs, Encoding.UTF8);
                    String FileText = Reader.ReadToEnd();
                    Reader.Close();
                    if (tabControl1.SelectedTab.Controls[0].Name == "ava")
                    {
                        tabControl1.SelectedTab.Controls[0].Text = FileText;
                        tabControl1.SelectedTab.Controls[0].Name = open.FileName;
                        tabControl1.SelectedTab.Text = open.SafeFileName;
                        this.Text = open.SafeFileName + " - SharpNotes";
                        tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                        System.Collections.ArrayList list = new System.Collections.ArrayList();
                        if (Properties.Settings.Default.recent != null)
                        {
                            list.AddRange(Properties.Settings.Default.recent.ToArray());
                        }
                        if (list.Contains(open.FileName) == false)
                        {
                            list.Add(open.FileName);
                        }
                        Properties.Settings.Default.recent = list;
                        Properties.Settings.Default.Save();
                        int Width = tabControl1.Size.Width;
                        int Height = tabControl1.Size.Height;
                        tabControl1.SelectedTab.Size = new Size(Width + 5, Height);
                    }
                    else
                    {
                        TextBox textbox = new TextBox();
                        textbox.WordWrap = false;
                        textbox.AcceptsTab = true;
                        textbox.Multiline = true;
                        textbox.Dock = DockStyle.Fill;
                        textbox.ScrollBars = ScrollBars.Both;
                        textbox.AllowDrop = true;
                        textbox.HideSelection = false;
                        textbox.MaxLength = 2147483647;
                        textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
                        textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
                        textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                        textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
                        textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
                        textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
                        textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
                        textbox.TextChanged += new EventHandler(textbox_TextChanged);
                        textbox.BackColor = Properties.Settings.Default.background;
                        textbox.Font = Properties.Settings.Default.font;
                        textbox.ForeColor = Properties.Settings.Default.color;
                        textbox.Name = open.FileName;
                        tabControl1.TabPages.Add(open.SafeFileName);
                        tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
                        textbox.Text = FileText;
                        textbox.Name = "ava";
                        tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                        this.Text = open.SafeFileName + " - SharpNotes";
                        textbox.BackColor = Properties.Settings.Default.background;
                        textbox.Font = Properties.Settings.Default.font;
                        textbox.ForeColor = Properties.Settings.Default.color;
                        System.Collections.ArrayList list = new System.Collections.ArrayList();
                        if (Properties.Settings.Default.recent != null)
                        {
                            list.AddRange(Properties.Settings.Default.recent.ToArray());
                        }
                        if (list.Contains(textbox.Name) == false)
                        {
                            list.Add(textbox.Name);
                        }
                        Properties.Settings.Default.recent = list;
                        Properties.Settings.Default.Save();
                        int Width = tabControl1.Size.Width;
                        int Height = tabControl1.Size.Height;
                        tabControl1.TabPages[tabControl1.TabPages.Count - 1].Size = new Size(Width + 5, Height);
                    }
                    open.Dispose();
                }
            }
        }
        #endregion

        #region Save
        public void Save()
        {
            if (tabControl1.SelectedTab.Controls[0].Name.IndexOf("\\") > 0)
            {
                if (tabControl1.SelectedTab.Controls[0].Name.Contains('*') == true)
                {
                    tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Replace("~Saved", null);
                    tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Replace("*Save", null);
                    FileStream fs = new FileStream(tabControl1.SelectedTab.Controls[0].Name, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                    writer.Write(tabControl1.SelectedTab.Controls[0].Text);
                    writer.Close();
                    if (tabControl1.SelectedTab.Controls[0].Name.Contains("~Saved") == false)
                    {
                        tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Insert(tabControl1.SelectedTab.Controls[0].Name.Length, "~Saved");
                    }
                    string başlık = tabControl1.SelectedTab.Controls[0].Name.Substring(tabControl1.SelectedTab.Controls[0].Name.LastIndexOf("\\"), tabControl1.SelectedTab.Controls[0].Name.Length - tabControl1.SelectedTab.Controls[0].Name.LastIndexOf("\\"));
                    başlık = başlık.Remove(0, 1);
                    if (başlık.IndexOf("*Save") > 0)
                    {
                        başlık = başlık.Remove(başlık.Length - 11, 11);
                    }
                    else
                    {
                        başlık = başlık.Remove(başlık.Length - 6, 6);
                    }
                    this.Text = başlık + " - SharpNotes";
                    tabControl1.SelectedTab.Text = başlık;
                }
                else
                {
                    tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Replace("~Saved", null);
                    tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Replace("*Save", null);
                    FileStream fs = new FileStream(tabControl1.SelectedTab.Controls[0].Name, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                    writer.Write(tabControl1.SelectedTab.Controls[0].Text);
                    writer.Close();
                    if (tabControl1.SelectedTab.Controls[0].Name.Contains("~Saved") == false)
                    {
                        tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Insert(tabControl1.SelectedTab.Controls[0].Name.Length, "~Saved");
                    }
                    string başlık = tabControl1.SelectedTab.Controls[0].Name.Substring(tabControl1.SelectedTab.Controls[0].Name.LastIndexOf("\\"), tabControl1.SelectedTab.Controls[0].Name.Length - tabControl1.SelectedTab.Controls[0].Name.LastIndexOf("\\"));
                    başlık = başlık.Remove(0, 1);
                    if (başlık.IndexOf("*Save") > 0)
                    {
                        başlık = başlık.Remove(başlık.Length - 11, 12);
                    }
                    else
                    {
                        başlık = başlık.Remove(başlık.Length - 6, 6);
                    }
                    this.Text = başlık + " - SharpNotes";
                    tabControl1.SelectedTab.Text = başlık;
                }
            }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Save";
                save.Filter = "Text Files |*.txt| All Files |*.*";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(save.FileName, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                    writer.Write(tabControl1.SelectedTab.Controls[0].Text);
                    yol = save.FileName;
                    writer.Close();
                    tabControl1.SelectedTab.Controls[0].Name = save.FileName;
                    tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Insert(tabControl1.SelectedTab.Controls[0].Name.Length, "~Saved");
                    string başlık = save.FileName.Substring(save.FileName.LastIndexOf("\\"), save.FileName.Length - save.FileName.LastIndexOf("\\"));
                    başlık = başlık.Remove(0, 1);
                    this.Text = başlık + " - SharpNotes";
                    tabControl1.SelectedTab.Text = başlık;
                }
            }
        }
        #endregion

        private int linesPrinted;
        private string[] lines;
        #region Page Setup
        public void PageSetup()
        {
            pageSetupDialog1.ShowDialog();
        }
        #endregion

        #region Print
        public void Print()
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        #endregion

        #endregion

        #region Extra
        string değer;

        #region Generate Word List
        public void GenerateWordList()
        {
            #region Dizi
            string[] dizi = new string[43];
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
            #endregion

            StringBuilder yazı = new StringBuilder();
            string[] farklı = new string[1];
            int index = 0, sonmu = 0;
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                foreach (char isim in formtext.Text)
                {
                    sonmu++;
                    if (dizi.Contains(isim.ToString().ToLower()))
                    {
                        değer += isim.ToString().ToLower();
                        if (sonmu == formtext.Text.Length)
                        {
                            if (farklı.Contains(değer) == false)
                            {
                                farklı.SetValue(değer, index);
                                yazı.AppendLine(değer);
                            }
                            else
                            {
                                Array.Resize(ref farklı, farklı.Count() - 1);
                            }
                        }
                    }
                    else
                    {
                        if (farklı.Contains(değer) == false)
                        {
                            farklı.SetValue(değer, index);
                            Array.Resize(ref farklı, farklı.Count() + 1);
                            yazı.AppendLine(değer);
                            index++;
                        }
                        değer = null;
                    }
                }
                formtext.Text = yazı.ToString().Trim('\r', '\n');
                yazı = null;
                farklı = null;
                değer = null;
            }
        }
        #endregion

        #region Insert Numbers
        public void InsertNumbers()
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("No line exists!");
                }
                else
                {
                    string[] lines = formtext.Lines;
                    formtext.Text = null;
                    StringBuilder outputBuilder = new StringBuilder();
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (removeNumbersToolStripMenuItem.Enabled == true)
                        {
                            if (lines[i].Contains((i + 1).ToString()))
                            {
                                lines[i] = lines[i].Replace(lines[i].Substring(0, (i + 1).ToString().Length + 2), (i + 1).ToString() + "- ");
                                outputBuilder.AppendLine(lines[i]);
                            }
                            else
                            {
                                outputBuilder.AppendLine((i + 1).ToString() + "- " + lines[i]);
                            }
                        }
                        else
                        {
                            outputBuilder.AppendLine((i + 1).ToString() + "- " + lines[i]);
                        }
                    }
                    formtext.Text = outputBuilder.ToString().Remove(outputBuilder.Length - 2).ToString();
                    outputBuilder = null;
                    removeNumbersToolStripMenuItem.Enabled = true;
                    toolStripButton6.Enabled = true;
                }
            }
        }
        #endregion

        #region Remove Numbers
        public void RemoveNumbers()
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                formtext.Text = null;
                StringBuilder outputBuilder = new StringBuilder();
                for (int i = 0; i < lines.Count(); i++)
                {
                    outputBuilder.AppendLine(lines[i]);
                    outputBuilder.Replace((i + 1).ToString() + "- ", "");
                }
                formtext.Text = outputBuilder.ToString().Remove(outputBuilder.Length - 2).ToString(); ;
                removeNumbersToolStripMenuItem.Enabled = false;
            }
        }
        #endregion

        #region Delete Empty Lines (Can Include Spaces And Tabs)
        private void emptyLinesCanİncludeSpacesAndTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int sayı = 0;
                string[] lines = formtext.Lines;
                string old_txt = toolStripStatusLabel1.Text;
                toolStripStatusLabel1.Text = "Deleting...";
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Trim().ToString().Length == 0)
                    {
                        sayı++;
                    }
                    else
                    {
                        builder.AppendLine(lines[i]);
                    }
                }
                formtext.Text = builder.ToString().Trim('\r', '\n');
                toolStripStatusLabel1.Text = old_txt;
                MessageBox.Show("Deleted empty lines: " + sayı.ToString());
            }
        }
        #endregion

        #region Delete Empty Lines
        void DeleteEmptyLines(string[] satırlar)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int sayı = 0;
                string[] lines = satırlar;
                string old_txt = toolStripStatusLabel1.Text;
                toolStripStatusLabel1.Text = "Deleting...";
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Length == 0)
                    {
                        sayı++;
                    }
                    else
                    {
                        builder.AppendLine(lines[i]);
                    }
                }
                formtext.Text = builder.ToString().Trim('\r', '\n');
                toolStripStatusLabel1.Text = old_txt;
                MessageBox.Show("Deleted empty lines: " + sayı.ToString());
            }
        }
        #endregion

        #region Delete Same Lines
        void DeleteSameLines(string[] satırlar)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int sayı = 0, indeks = 0;
                string[] lines = formtext.Lines;
                string[] line = new string[2];
                string old_txt = toolStripStatusLabel1.Text;
                toolStripStatusLabel1.Text = "Deleting...";
                for (int i = 0; i < formtext.Lines.Count(); i++)
                {
                    if (line.Contains(lines[i].ToString()) == false)
                    {
                        line.SetValue(lines[i], indeks);
                        indeks++;
                        Array.Resize(ref line, indeks + 1);
                        Application.DoEvents();
                    }
                    else
                    {
                        sayı++;
                    }
                }
                if (line.Contains(null))
                {
                    Array.Resize(ref line, line.Count() - 1);
                }
                formtext.Lines = line;
                formtext.Text.Trim('\r', '\n');
                toolStripStatusLabel1.Text = old_txt;
                MessageBox.Show("Deleted Same Line Numbers: " + sayı.ToString());
            }
        }
        #endregion

        #endregion

        #endregion

        #region File Menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            if (Properties.Settings.Default.recent != null)
            {
                list.AddRange(Properties.Settings.Default.recent.ToArray());
            }
            if (list.Contains(yol) == false)
            {
                list.Add(yol);
            }
            Properties.Settings.Default.recent = list;
            Properties.Settings.Default.Save();
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Controls[0].Name == "ava")
            {
                if (tabControl1.SelectedTab.Controls[0].Name.Contains("*Save") == false)
                {
                    tabControl1.SelectedTab.Controls[0].Name = null;
                    if (tabControl1.SelectedTab.Controls[0].Name.Contains("*Save") == false)
                    {
                        tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Insert(tabControl1.SelectedTab.Controls[0].Name.Length, "*Save");
                    }
                }
            }
            else
            {
                if (tabControl1.SelectedTab.Controls[0].Name.Contains("~Saved") == true)
                {
                    tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Replace("~Saved", "*Save");
                }
                else
                {
                    if (tabControl1.SelectedTab.Controls[0].Name.Contains("*Save") == false)
                    {
                        tabControl1.SelectedTab.Controls[0].Name = tabControl1.SelectedTab.Controls[0].Name.Insert(tabControl1.SelectedTab.Controls[0].Name.Length, "*Save");
                    }
                }
            }
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    toolStripStatusLabel1.Text = "Modififed (Empty)";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Modified";
                }
                toolStripStatusLabel2.Text = formtext.Lines.Count() + " Lines";
                toolStripStatusLabel3.Text = formtext.Text.Length.ToString() + " Bytes";
                toolStripStatusLabel4.Text = "Line: " + (formtext.GetLineFromCharIndex(formtext.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                toolStripStatusLabel5.Text = "Column: " + formtext.SelectionStart.ToString();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save As";
            save.Filter = "Text Files |*.txt| All Files |*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
                {
                    FileStream fs = new FileStream(save.FileName, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                    writer.Write(formtext.Text);
                    yol = save.FileName;
                    writer.Close();
                }
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetup();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                whichone = "exit";
                if (yol != null)
                {
                    DialogResult result = MessageBox.Show("Do you want to save " + yol + " before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        StreamWriter writer = File.CreateText(yol);
                        writer.Write(formtext.Text);
                        writer.Close();
                        Application.Exit();
                    }
                    else if (result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else if (result == DialogResult.Cancel)
                    {

                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to save the current file before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.Filter = "Text Files |*.txt";
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter writer = File.CreateText(save.FileName);
                            writer.Write(formtext.Text);
                            writer.Close();
                        }
                        Application.Exit();
                    }
                    else if (result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else if (result == DialogResult.Cancel)
                    {

                    }
                }
            }
        }

        #endregion

        #region Edit Menu
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Undo();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Copy();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Cut();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Paste();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.SelectedText = null;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.SelectAll();
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                Properties.Settings.Default.type = "find";
                Properties.Settings.Default.Save();
                Form7 form7 = new Form7();
                form7.textBox1.Text = formtext.SelectedText;
                form7.Show();
            }
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                Properties.Settings.Default.type = "replace";
                Properties.Settings.Default.Save();
                Form7 form7 = new Form7();
                form7.textBox1.Text = formtext.SelectedText;
                form7.Show();
            }
        }
        #endregion

        #region Always on Top and Options
        private void alwaysOnTopDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (alwaysOnTopDisabledToolStripMenuItem.Checked == false)
            {
                this.TopMost = true;
                alwaysOnTopDisabledToolStripMenuItem.Checked = true;
            }
            else
            {
                this.TopMost = false;
                alwaysOnTopDisabledToolStripMenuItem.Checked = false;
            }
        }

        private void backgroundColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.Color = tabControl1.SelectedTab.Controls[0].BackColor;
            if (color.ShowDialog() == DialogResult.OK)
            {
                tabControl1.SelectedTab.Controls[0].BackColor = color.Color;
                color.Dispose();
                Properties.Settings.Default.background = tabControl1.SelectedTab.Controls[0].BackColor;
                Properties.Settings.Default.Save();
            }
        }

        private void fontPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = tabControl1.SelectedTab.Controls[0].Font;
            font.Color = tabControl1.SelectedTab.Controls[0].ForeColor;
            font.ShowColor = true;
            if (font.ShowDialog() == DialogResult.OK)
            {
                tabControl1.SelectedTab.Controls[0].ForeColor = font.Color;
                tabControl1.SelectedTab.Controls[0].Font = font.Font;
                font.Dispose();
                Properties.Settings.Default.font = tabControl1.SelectedTab.Controls[0].Font;
                Properties.Settings.Default.color = tabControl1.SelectedTab.Controls[0].ForeColor;
                Properties.Settings.Default.Save();
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                foreach (TextBox i in tabControl1.SelectedTab.Controls)
                {
                    i.WordWrap = false;
                    toolStripStatusLabel2.Text = (i.GetLineFromCharIndex(i.Text.Length) + 1).ToString() + " Lines";
                }
                wordWrapToolStripMenuItem.Checked = false;
            }
            else
            {
                foreach (TextBox i in tabControl1.SelectedTab.Controls)
                {
                    i.WordWrap = true;
                    toolStripStatusLabel2.Text = (i.GetLineFromCharIndex(i.Text.Length) + 1).ToString() + " Lines";
                }
                wordWrapToolStripMenuItem.Checked = true;
            }
        }
        #endregion

        #region Extra Menu
        private void surroundWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.type = "surround";
            Properties.Settings.Default.Save();
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void splitLinesWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.type = "split";
            Properties.Settings.Default.Save();
            Form3 form3 = new Form3();
            form3.Show();
        }

        int sayı = -1;
        private void analyzeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("There is no text to analyze!");
                }
                else
                {
                    Form6 form6 = new Form6();
                    form6.Show();
                }
            }
        }

        private void combineLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("There is no text!");
                }
                else
                {
                    Properties.Settings.Default.type = "combine";
                    Properties.Settings.Default.Save();
                    Form3 form3 = new Form3();
                    form3.Show();
                }
            }
        }

        private void codeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("There is no text!");
                }
                else
                {
                    SharpNotes.Properties.Settings.Default.type = "Code";
                    SharpNotes.Properties.Settings.Default.Save();
                    Form5 form5 = new Form5();
                    form5.Show();
                }
            }
        }

        private void decodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("There is no text!");
                }
                else
                {
                    SharpNotes.Properties.Settings.Default.type = "decode";
                    SharpNotes.Properties.Settings.Default.Save();
                    Form5 form5 = new Form5();
                    form5.Show();
                }
            }
        }

        private void currentLineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] line = formtext.Lines;
                int satır = Convert.ToInt32(formtext.GetLineFromCharIndex(formtext.GetFirstCharIndexOfCurrentLine()));
                if (line[satır].Length >= 4)
                {
                    if (line[satır].Substring(0, 4) == "    ")
                    {
                        formtext.Text = formtext.Text.Remove(formtext.GetFirstCharIndexOfCurrentLine(), 4);
                    }
                }
            }
        }

        private void goToLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
        }

        private void allLinesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Substring(0, 4) == "    ")
                    {
                        lines[i] = lines[i].Remove(0, 4);
                    }
                }
                formtext.Lines = lines;
            }
        }

        private void emptyLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                DeleteEmptyLines(formtext.Lines);
            }
        }

        private void sameLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                DeleteSameLines(formtext.Lines);
            }
        }

        private void extractTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.ShowDialog();
        }

        private void filterLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void generateWordListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateWordList();
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text.Trim('\r', '\n');
            }
        }

        private void currentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = formtext.Text.Insert(formtext.GetFirstCharIndexOfCurrentLine(), "    ");
            }
        }

        private void allLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                for (int i = 0; i < lines.Count(); i++)
                {
                    lines[i] = "    " + lines[i];
                }
                formtext.Lines = lines;
            }
        }

        private void ınsertNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertNumbers();
        }

        private void toBeginningOfLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpNotes.Properties.Settings.Default.type = "begin";
            SharpNotes.Properties.Settings.Default.Save();
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void toEndOfLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpNotes.Properties.Settings.Default.type = "end";
            SharpNotes.Properties.Settings.Default.Save();
            Form4 form4 = new Form4();
            form4.Show();
        }

        Timer timer = new Timer();
        StringBuilder outcomputer = new StringBuilder();
        private void computerWillPressKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("There is no text!");
                }
                else
                {
                    StringBuilder incomputer = new StringBuilder();
                    string[] lines = formtext.Lines;
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        incomputer.AppendLine(lines[i]);
                    }
                    outcomputer = incomputer;
                    formtext.Text = null;
                    timer.Interval = 50;
                    timer.Tick += new EventHandler(timer_Tick);
                    timer.Start();
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text.Length == outcomputer.Length - 2)
                {
                    timer.Stop();
                    sayı = -1;
                    timer.Dispose();
                    timer.Tick -= new EventHandler(timer_Tick);
                }
                else
                {
                    if (sayı != outcomputer.Length - 2)
                    {
                        sayı++;
                        formtext.AppendText(outcomputer[sayı].ToString());
                    }
                }
            }
        }

        StringBuilder builder = new StringBuilder();
        private void userWillPressKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text == "")
                {
                    MessageBox.Show("There is no text!");
                }
                else
                {
                    string[] lines = formtext.Lines;
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        builder.AppendLine(lines[i]);
                    }
                    formtext.Text = null;
                    formtext.KeyDown += new KeyEventHandler(formtext_KeyDown);
                }
            }
        }

        void formtext_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                if (formtext.Text.Length == builder.Length - 2)
                {
                    formtext.KeyDown -= new KeyEventHandler(formtext_KeyDown);
                    sayı = -1;
                    builder = null;
                }
                else
                {
                    if (sayı != builder.Length - 2)
                    {
                        e.SuppressKeyPress = true;
                        sayı++;
                        formtext.AppendText(builder[sayı].ToString());
                    }
                }
            }
        }

        private void randomizeLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                string[] satırlar = new string[formtext.Lines.Count()];
                Random random = new Random();
                for (int i = 0; i < formtext.Lines.Count(); i++)
                {
                basla:
                    int rnd = random.Next(0, lines.Count());
                    if (lines[rnd] != null)
                    {
                        satırlar.SetValue(lines[rnd], i);
                        lines[rnd] = null;
                    }
                    else
                    {
                        goto basla;
                    }
                }
                formtext.Lines = satırlar;
            }
        }

        private void removeNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNumbers();
        }

        private void reverseLineSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                Array.Reverse(lines);
                formtext.Lines = lines;
            }
        }

        string text;
        private void reverseLineContentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                int sayı = 0;
                for (int i = 0; i < lines.Count(); i++)
                {
                    string[] line = new string[lines[i].Length];
                    foreach (char a in lines[i])
                    {
                        line.SetValue(a.ToString(), sayı);
                        sayı++;
                    }
                    Array.Reverse(line);
                    foreach (string b in line)
                    {
                        text += b.ToString();
                    }
                    lines[i] = text;
                    sayı = 0;
                    text = null;
                }
                formtext.Lines = lines;
            }
        }

        private void sortLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                string[] lines = formtext.Lines;
                Array.Sort(lines);
                formtext.Lines = lines;
                lines = null;
            }
        }

        private void trimBothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = formtext.Text.Trim();
            }
        }

        private void trimLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = formtext.Text.TrimStart();
            }
        }

        private void trimRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = formtext.Text.TrimEnd();
            }
        }

        private void entireDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = formtext.Text.ToUpper();
            }
        }

        private void selectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.SelectedText = formtext.SelectedText.ToUpper();
            }
        }

        private void entireDocumentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = formtext.Text.ToLower();
            }
        }

        private void üToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.SelectedText = formtext.SelectedText.ToUpper();
            }
        }
        #endregion

        #region Help
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }
        #endregion

        #region Form Closing
        string whichone;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                foreach (TextBox formtext in tabControl1.TabPages[i].Controls)
                {
                    if (whichone != null)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        if (tabControl1.TabPages[i].Controls[0].Name.IndexOf("~") < 0 && tabControl1.TabPages[i].Controls[0].Name.Length == 5)
                        {
                            if (tabControl1.TabPages[i].Controls[0].Name.IndexOf("*") >= 0 && tabControl1.TabPages[i].Controls[0].Name.Length == 5)
                            {
                                DialogResult result = MessageBox.Show("Do you want to save \"" + tabControl1.TabPages[i].Text + "\" before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    SaveFileDialog save = new SaveFileDialog();
                                    save.Filter = "Text Files |*.txt";
                                    if (save.ShowDialog() == DialogResult.OK)
                                    {
                                        StreamWriter writer = File.CreateText(save.FileName);
                                        writer.Write(formtext.Text);
                                        writer.Close();
                                    }
                                }
                                else if (result == DialogResult.Cancel)
                                {
                                    e.Cancel = true;
                                }
                            }
                        }
                        else
                        {
                            if (tabControl1.TabPages[i].Controls[0].Name.IndexOf("~") < 0)
                            {
                                if (tabControl1.TabPages[i].Controls[0].Name.IndexOf("*") > 0 && tabControl1.TabPages[i].Controls[0].Name.Length > 5)
                                {
                                    DialogResult result = MessageBox.Show("Do you want to save \"" + tabControl1.TabPages[i].Controls[0].Name.Substring(0, tabControl1.TabPages[i].Controls[0].Name.Length - 5) + "\" file before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        StreamWriter writer = File.CreateText(tabControl1.TabPages[i].Controls[0].Name.Substring(0, tabControl1.TabPages[i].Controls[0].Name.Length - 5));
                                        writer.Write(formtext.Text);
                                        writer.Close();
                                    }
                                    else if (result == DialogResult.Cancel)
                                    {
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }
                    }
                }
                Properties.Settings.Default.background = tabControl1.TabPages[i].Controls[0].BackColor;
                Properties.Settings.Default.font = tabControl1.TabPages[i].Controls[0].Font;
                Properties.Settings.Default.color = tabControl1.TabPages[i].Controls[0].ForeColor;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region FormLoad
        public string SelectedFile { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            int selectedtabindex = 0;
            toolStripButton6.Image = removeNumbersToolStripMenuItem.Image;
            toolStripButton5.Image = fontPropertiesToolStripMenuItem.Image;
            toolStripButton4.Image = backgroundColourToolStripMenuItem.Image;
            toolStripButton3.Image = ınsertNumbersToolStripMenuItem.Image;
            toolStripButton1.Image = analyzeTextToolStripMenuItem.Image;
            string başlık;
            if (!String.IsNullOrEmpty(SelectedFile))
            {
                başlık = SelectedFile.ToString().Substring(SelectedFile.LastIndexOf("\\"), SelectedFile.ToString().Length - SelectedFile.ToString().LastIndexOf("\\"));
                başlık = başlık.Remove(0, 1);
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    if (tabControl1.TabPages[i].Text == başlık)
                    {
                        tabControl1.SelectedIndex = i;
                        selectedtabindex = -1;
                        break;
                    }
                }
                if (selectedtabindex == 0)
                {
                    FileStream fs1 = new FileStream(SelectedFile.ToString(), FileMode.Open);
                    StreamReader Reader = new StreamReader(fs1, Encoding.UTF8);
                    String FileText = Reader.ReadToEnd();
                    TextBox textbox = new TextBox();
                    textbox.Text = FileText;
                    Reader.Close();
                    yol = SelectedFile.ToString();
                    this.Text = başlık + " - SharpNotes";
                    System.Collections.ArrayList list = new System.Collections.ArrayList();
                    if (Properties.Settings.Default.recent != null)
                    {
                        list.AddRange(Properties.Settings.Default.recent.ToArray());
                    }
                    if (list.Contains(yol) == false)
                    {
                        list.Add(yol);
                    }
                    Properties.Settings.Default.recent = list;
                    Properties.Settings.Default.Save();
                    textbox.WordWrap = false;
                    textbox.AcceptsTab = true;
                    textbox.Name = SelectedFile.ToString();
                    textbox.HideSelection = false;
                    textbox.MaxLength = 2147483647;
                    textbox.Multiline = true;
                    textbox.AllowDrop = true;
                    textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
                    textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
                    textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                    textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
                    textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
                    textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
                    textbox.TextChanged += new EventHandler(textbox_TextChanged);
                    textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
                    textbox.Dock = DockStyle.Fill;
                    textbox.ScrollBars = ScrollBars.Both;
                    if (tabControl1.TabPages.Count != 0)
                    {
                        tabControl1.TabPages.Add(başlık);
                        tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
                    }
                    else
                    {
                        tabControl1.TabPages.Add(başlık);
                        tabControl1.TabPages[0].Controls.Add(textbox);
                    }
                    textbox.BackColor = Properties.Settings.Default.background;
                    textbox.Font = Properties.Settings.Default.font;
                    textbox.ForeColor = Properties.Settings.Default.color;
                    foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
                    {
                        if (formtext.Text == "")
                        {
                            toolStripStatusLabel1.Text = "Modififed (Empty)";
                        }
                        else
                        {
                            toolStripStatusLabel1.Text = "Modified";
                        }
                        toolStripStatusLabel2.Text = formtext.Lines.Count() + " Lines";
                        toolStripStatusLabel3.Text = formtext.Text.Length.ToString() + " Bytes";
                        toolStripStatusLabel4.Text = "Line: " + (formtext.GetLineFromCharIndex(formtext.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                        toolStripStatusLabel5.Text = "Column: " + formtext.SelectionStart.ToString();
                    }
                }
            }
            else
            {
                this.Text = "New - SharpNotes";
                TextBox textbox = new TextBox();
                textbox.AcceptsTab = true;
                textbox.WordWrap = false;
                textbox.Multiline = true;
                textbox.Dock = DockStyle.Fill;
                textbox.MaxLength = 2147483647;
                textbox.ScrollBars = ScrollBars.Both;
                textbox.AllowDrop = true;
                textbox.HideSelection = false;
                textbox.Name = "ava";
                textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
                textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
                textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
                textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
                textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
                textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
                textbox.TextChanged += new EventHandler(textbox_TextChanged);
                tabControl1.TabPages.Add("New");
                if (tabControl1.TabPages.Count == 0)
                {
                    tabControl1.TabPages[0].Controls.Add(textbox);
                }
                else
                {
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
                }
                textbox.BackColor = Properties.Settings.Default.background;
                textbox.Font = Properties.Settings.Default.font;
                textbox.ForeColor = Properties.Settings.Default.color;
                foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
                {
                    if (formtext.Text == "")
                    {
                        toolStripStatusLabel1.Text = "Modififed (Empty)";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Modified";
                    }
                    toolStripStatusLabel2.Text = formtext.Lines.Count() + " Lines";
                    toolStripStatusLabel3.Text = formtext.Text.Length.ToString() + " Bytes";
                    toolStripStatusLabel4.Text = "Line: " + (formtext.GetLineFromCharIndex(formtext.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                    toolStripStatusLabel5.Text = "Column: " + formtext.SelectionStart.ToString();
                }
            }
        }
        #endregion

        #region Drag and Drop
        void textbox_DragDrop(object sender, DragEventArgs e)
        {
            int selectedtabindex = 0;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string başlık = files[0].Substring(files[0].LastIndexOf("\\"), files[0].Length - files[0].LastIndexOf("\\"));
            başlık = başlık.Remove(0, 1);
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Text == başlık)
                {
                    tabControl1.SelectedIndex = i;
                    selectedtabindex = -1;
                    break;
                }
            }
            if (selectedtabindex == 0)
            {
                FileStream fs = new FileStream(files[0], FileMode.Open);
                StreamReader Reader = new StreamReader(fs, Encoding.UTF8);
                String FileText = Reader.ReadToEnd();
                Reader.Close();
                if (tabControl1.SelectedTab.Controls[0].Name == "ava")
                {
                    tabControl1.SelectedTab.Controls[0].Text = FileText;
                    tabControl1.SelectedTab.Controls[0].Name = files[0];
                    tabControl1.SelectedTab.Text = başlık;
                    this.Text = başlık + " - SharpNotes";
                    System.Collections.ArrayList list = new System.Collections.ArrayList();
                    if (Properties.Settings.Default.recent != null)
                    {
                        list.AddRange(Properties.Settings.Default.recent.ToArray());
                    }
                    if (list.Contains(files[0]) == false)
                    {
                        list.Add(files[0]);
                    }
                    Properties.Settings.Default.recent = list;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    TextBox textbox = new TextBox();
                    textbox.AcceptsTab = true;
                    textbox.WordWrap = false;
                    textbox.MaxLength = 2147483647;
                    textbox.AllowDrop = true;
                    textbox.HideSelection = false;
                    textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
                    textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
                    textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                    textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
                    textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
                    textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
                    textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
                    textbox.Multiline = true;
                    textbox.BackColor = Properties.Settings.Default.background;
                    textbox.Font = Properties.Settings.Default.font;
                    textbox.ForeColor = Properties.Settings.Default.color;
                    textbox.Dock = DockStyle.Fill;
                    textbox.ScrollBars = ScrollBars.Both;
                    textbox.Text = FileText;
                    textbox.HideSelection = false;
                    textbox.Name = files[0].ToString();
                    textbox.Name = "ava";
                    this.Text = başlık + " - SharpNotes";
                    yol = files[0];
                    tabControl1.TabPages.Add(başlık);
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
                    tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                    System.Collections.ArrayList list = new System.Collections.ArrayList();
                    if (Properties.Settings.Default.recent != null)
                    {
                        list.AddRange(Properties.Settings.Default.recent.ToArray());
                    }
                    if (list.Contains(textbox.Name) == false)
                    {
                        list.Add(textbox.Name);
                    }
                    Properties.Settings.Default.recent = list;
                    Properties.Settings.Default.Save();
                }
            }
        }

        void textbox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        #endregion

        #region Date
        private void addDateToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolStripMenuItem2.Text = DateTime.Now.ToString("d");
            toolStripMenuItem3.Text = DateTime.Now.ToString("D");
            toolStripMenuItem4.Text = DateTime.Now.ToString("f");
            toolStripMenuItem5.Text = DateTime.Now.ToString("F");
            toolStripMenuItem6.Text = DateTime.Now.ToString("g");
            toolStripMenuItem7.Text = DateTime.Now.ToString("G");
            toolStripMenuItem8.Text = DateTime.Now.ToString("M");
            toolStripMenuItem9.Text = DateTime.Now.ToString("R");
            toolStripMenuItem10.Text = DateTime.Now.ToString("s");
            toolStripMenuItem11.Text = DateTime.Now.ToString("t");
            toolStripMenuItem12.Text = DateTime.Now.ToString("T");
            toolStripMenuItem13.Text = DateTime.Now.ToString("u");
            toolStripMenuItem15.Text = DateTime.Now.ToString("Y");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int selection = formtext.SelectionStart;
                formtext.Text = formtext.Text.Insert(formtext.SelectionStart, (sender as ToolStripMenuItem).Text);
                formtext.SelectionStart = selection + (sender as ToolStripMenuItem).Text.Length;
            }
        }
        #endregion

        #region Column And Line And Selected Chars Lenght
        void textbox_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                toolStripStatusLabel4.Text = "Line: " + (text.GetLineFromCharIndex(text.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                toolStripStatusLabel5.Text = "Column: " + text.SelectionStart.ToString();
                toolStripStatusLabel6.Text = "Selected: " + text.SelectionLength.ToString() + " (" + text.SelectionLength.ToString() + " Bytes)";
            }
        }

        void textbox_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (System.Windows.Forms.Clipboard.ContainsText())
                    {
                        int selection = text.SelectionStart;
                        text.Text = text.Text.Insert(text.SelectionStart, System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Text).ToString());
                        text.SelectionStart = selection + System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Text).ToString().Length;
                    }
                }
                right = true;
                toolStripStatusLabel4.Text = "Line: " + (text.GetLineFromCharIndex(text.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                toolStripStatusLabel5.Text = "Column: " + text.SelectionStart.ToString();
                toolStripStatusLabel6.Text = "Selected: " + text.SelectionLength.ToString() + " (" + text.SelectionLength.ToString() + " Bytes)";
            }
        }

        void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                toolStripStatusLabel4.Text = "Line: " + (text.GetLineFromCharIndex(text.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                toolStripStatusLabel5.Text = "Column: " + text.SelectionStart.ToString();
                toolStripStatusLabel6.Text = "Selected: " + text.SelectionLength.ToString() + " (" + text.SelectionLength.ToString() + " Bytes)";
            }
        }
        #endregion

        #region Print Preview
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
        #endregion

        #region BeginPrint
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                char[] param = { '\n' };
                if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
                {
                    lines = formtext.SelectedText.Split(param);
                }
                else
                {
                    lines = formtext.Text.Split(param);
                }

                int i = 0;
                char[] trimParam = { '\r' };
                foreach (string s in lines)
                {
                    lines[i++] = s.TrimEnd(trimParam);
                }
            }
        }
        #endregion

        #region PrintPage
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                Brush brush = new SolidBrush(Color.Black);
                while (linesPrinted < lines.Length)
                {
                    e.Graphics.DrawString(lines[linesPrinted++],
                        formtext.Font, brush, x, y);
                    y += 15;
                    if (y >= e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                linesPrinted = 0;
                e.HasMorePages = false;
            }
        }
        #endregion

        #region HTML Encoding
        private void hTMLEncodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder encode = new StringBuilder();
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                encode.Append(formtext.Text);
            }
            encode.Replace("ş", "&#351;");
            encode.Replace("ğ", "&#287;");
            encode.Replace("ı", "&#305;");
            encode.Replace("Ğ", "&#286;");
            encode.Replace("İ", "&#304;");
            encode.Replace("Ş", "&#350;");
            encode.Replace("Ç", "&Ccedil;");
            encode.Replace("ç", "&ccedil;");
            encode.Replace("Ü", "&Uuml;");
            encode.Replace("ü", "&uuml;");
            encode.Replace("ö", "&ouml;");
            encode.Replace("Ö", "&Ouml;");
            encode.Replace("ö", "&ouml;");
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = encode.ToString().Trim('\r', '\n');
            }
        }
        #endregion

        #region HTML Decoding
        private void hTMLDecodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder encode = new StringBuilder();
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                encode.Append(formtext.Text);
            }
            encode.Replace("&#351;", "ş");
            encode.Replace("&#287;", "ğ");
            encode.Replace("&#305;", "ı");
            encode.Replace("&#286;", "Ğ");
            encode.Replace("&#304;", "İ");
            encode.Replace("&#350;", "Ş");
            encode.Replace("&Ccedil;", "Ç");
            encode.Replace("&ccedil;", "ç");
            encode.Replace("&Uuml;", "Ü");
            encode.Replace("&uuml;", "ü");
            encode.Replace("&ouml;", "ö");
            encode.Replace("&Ouml;", "Ö");
            encode.Replace("&ouml;", "ö");
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = encode.ToString().Trim('\r', '\n');
            }
        }
        #endregion

        #region Text Align
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                if (text is TextBox)
                {
                    text.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                }
            }
            toolStripMenuItem14.Checked = true;
            toolStripMenuItem16.Checked = false;
            toolStripMenuItem17.Checked = false;
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                if (text is TextBox)
                {
                    text.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                }
            }
            toolStripMenuItem14.Checked = false;
            toolStripMenuItem16.Checked = true;
            toolStripMenuItem17.Checked = false;
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                if (text is TextBox)
                {
                    text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                }
            }
            toolStripMenuItem14.Checked = false;
            toolStripMenuItem16.Checked = false;
            toolStripMenuItem17.Checked = true;
        }
        #endregion

        #region Selected Chars Lenght Mouse Event
        bool right = false;
        void textbox_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                if (right == true)
                {
                    toolStripStatusLabel6.Text = "Selected: " + text.SelectionLength.ToString() + " (" + text.SelectionLength.ToString() + " Bytes)";
                    toolStripStatusLabel4.Text = "Line: " + (text.GetLineFromCharIndex(text.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                    toolStripStatusLabel5.Text = "Column: " + text.SelectionStart.ToString();
                }
            }
        }

        void textbox_MouseUp(object sender, MouseEventArgs e)
        {
            right = false;
        }
        #endregion

        #region File's Click Event
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recentlyOpenedToolStripMenuItem.DropDownItems.Clear();
            if (Properties.Settings.Default.recent != null)
            {
                recentlyOpenedToolStripMenuItem.Enabled = true;
                for (int i = 0; i < Properties.Settings.Default.recent.ToArray().Count(); i++)
                {
                    if (Properties.Settings.Default.recent.ToArray()[i] != null)
                    {
                        if (Properties.Settings.Default.recent.ToArray()[i].ToString() != "")
                        {
                            ToolStripMenuItem dropdown = new ToolStripMenuItem();
                            dropdown.Click += new EventHandler(dropdown_Click);
                            dropdown.MouseDown += new MouseEventHandler(dropdown_MouseDown);
                            if (File.Exists(Properties.Settings.Default.recent.ToArray()[i].ToString()) == false)
                            {
                                dropdown.Text = Properties.Settings.Default.recent.ToArray()[i].ToString() + " (Deleted)";
                            }
                            else
                            {
                                dropdown.Text = Properties.Settings.Default.recent.ToArray()[i].ToString();
                            }
                            recentlyOpenedToolStripMenuItem.DropDownItems.Add(dropdown);
                        }
                    }
                }
                ToolStripMenuItem clear = new ToolStripMenuItem();
                clear.Text = "Clear History";
                clear.Click += new EventHandler(clear_Click);
                recentlyOpenedToolStripMenuItem.DropDownItems.Add(clear);
            }
            else
            {
                recentlyOpenedToolStripMenuItem.Enabled = false;
            }
        }
        #endregion

        #region Open In A New Windows By Cliking The Mouse Middle Button Or Remove
        void dropdown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if ((sender as ToolStripMenuItem).Text != "Clear History")
                {
                    if ((sender as ToolStripMenuItem).Text.IndexOf("(Deleted)") < 0)
                    {
                        System.Collections.ArrayList list = new System.Collections.ArrayList();
                        if (Properties.Settings.Default.recent != null)
                        {
                            list.AddRange(Properties.Settings.Default.recent.ToArray());
                        }
                        list.Remove((sender as ToolStripMenuItem).Text);
                        Properties.Settings.Default.recent = list;
                        Properties.Settings.Default.Save();
                        fileToolStripMenuItem.PerformClick();
                    }
                    else
                    {
                        System.Collections.ArrayList list = new System.Collections.ArrayList();
                        if (Properties.Settings.Default.recent != null)
                        {
                            list.AddRange(Properties.Settings.Default.recent.ToArray());
                        }
                        list.Remove((sender as ToolStripMenuItem).Text.Substring(0, (sender as ToolStripMenuItem).Text.Length - 10));
                        Properties.Settings.Default.recent = list;
                        Properties.Settings.Default.Save();
                        fileToolStripMenuItem.PerformClick();
                    }
                }
            }
        }
        #endregion

        #region Clear Recently Opened File History
        void clear_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.recent = null;
            Properties.Settings.Default.Save();

        }
        #endregion

        #region Recently Opened Files Click Evet
        void dropdown_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text.IndexOf("(Deleted)") < 0)
            {
                int selectedtabindex = 0;
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    string başlık = (sender as ToolStripMenuItem).Text.Substring((sender as ToolStripMenuItem).Text.LastIndexOf("\\"), (sender as ToolStripMenuItem).Text.Length - (sender as ToolStripMenuItem).Text.LastIndexOf("\\"));
                    başlık = başlık.Remove(0, 1);
                    if (tabControl1.TabPages[i].Text == başlık)
                    {
                        tabControl1.SelectedIndex = i;
                        selectedtabindex = -1;
                        break;
                    }
                }
                if (selectedtabindex == 0)
                {
                    if (tabControl1.SelectedTab.Controls[0].Name == "ava")
                    {
                        FileStream fs = new FileStream((sender as ToolStripMenuItem).Text, FileMode.Open);
                        StreamReader Reader = new StreamReader(fs, Encoding.UTF8);
                        String FileText = Reader.ReadToEnd();
                        Reader.Close();
                        string başlık = (sender as ToolStripMenuItem).Text.Substring((sender as ToolStripMenuItem).Text.LastIndexOf("\\"), (sender as ToolStripMenuItem).Text.Length - (sender as ToolStripMenuItem).Text.LastIndexOf("\\"));
                        başlık = başlık.Remove(0, 1);
                        tabControl1.SelectedTab.Controls[0].Text = FileText;
                        this.Text = başlık + " - SharpNotes";
                        tabControl1.SelectedTab.Controls[0].Name = başlık;
                        tabControl1.SelectedTab.Text = başlık;
                    }
                    else
                    {
                        FileStream fs = new FileStream((sender as ToolStripMenuItem).Text, FileMode.Open);
                        StreamReader Reader = new StreamReader(fs, Encoding.UTF8);
                        String FileText = Reader.ReadToEnd();
                        Reader.Close();
                        TextBox textbox = new TextBox();
                        textbox.AcceptsTab = true;
                        textbox.WordWrap = false;
                        textbox.Name = yol;
                        textbox.HideSelection = false;
                        textbox.Multiline = true;
                        textbox.AllowDrop = true;
                        textbox.DragEnter += new DragEventHandler(textbox_DragEnter);
                        textbox.DragDrop += new DragEventHandler(textbox_DragDrop);
                        textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
                        textbox.MouseDown += new MouseEventHandler(textbox_MouseDown);
                        textbox.KeyUp += new KeyEventHandler(textbox_KeyUp);
                        textbox.MouseUp += new MouseEventHandler(textbox_MouseUp);
                        textbox.MouseMove += new MouseEventHandler(textbox_MouseMove);
                        textbox.Dock = DockStyle.Fill;
                        textbox.ScrollBars = ScrollBars.Both;
                        textbox.Text = FileText;
                        textbox.BackColor = Properties.Settings.Default.background;
                        textbox.Font = Properties.Settings.Default.font;
                        textbox.ForeColor = Properties.Settings.Default.color;
                        textbox.Name = (sender as ToolStripMenuItem).Text;
                        string başlık = (sender as ToolStripMenuItem).Text.Substring((sender as ToolStripMenuItem).Text.LastIndexOf("\\"), (sender as ToolStripMenuItem).Text.Length - (sender as ToolStripMenuItem).Text.LastIndexOf("\\"));
                        başlık = başlık.Remove(0, 1);
                        this.Text = başlık + " - SharpNotes";
                        tabControl1.TabPages.Add(başlık);
                        tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(textbox);
                        tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                    }
                }
            }
        }
        #endregion

        #region TabControl Selected Index Changed
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Text = tabControl1.SelectedTab.Text + " - SharpNotes";
                foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
                {
                    if (formtext.Text == "")
                    {
                        toolStripStatusLabel1.Text = "Modififed (Empty)";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Modified";
                    }
                    if (formtext.WordWrap == true)
                    {
                        wordWrapToolStripMenuItem.Checked = true;
                    }
                    else
                    {
                        wordWrapToolStripMenuItem.Checked = false;
                    }
                    toolStripStatusLabel2.Text = formtext.Lines.Count() + " Lines";
                    toolStripStatusLabel3.Text = formtext.Text.Length.ToString() + " Bytes";
                    toolStripStatusLabel4.Text = "Line: " + (formtext.GetLineFromCharIndex(formtext.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                    toolStripStatusLabel5.Text = "Column: " + formtext.SelectionStart.ToString();
                }
            }
            catch
            {
                this.Text = "Untitled - SharpNotes";
                foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
                {
                    if (formtext.Text == "")
                    {
                        toolStripStatusLabel1.Text = "Modififed (Empty)";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Modified";
                    }
                    toolStripStatusLabel2.Text = formtext.Lines.Count() + " Lines";
                    toolStripStatusLabel3.Text = formtext.Text.Length.ToString() + " Bytes";
                    toolStripStatusLabel4.Text = "Line: " + (formtext.GetLineFromCharIndex(formtext.GetFirstCharIndexOfCurrentLine()) + 1).ToString();
                    toolStripStatusLabel5.Text = "Column: " + formtext.SelectionStart.ToString();
                }
            }
        }
        #endregion

        #region A New Tab By Double-Clicking
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            New();
        }
        #endregion

        #region ShortCuts
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            removeNumbersToolStripMenuItem.PerformClick();
            toolStripButton6.Enabled = false;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem.PerformClick();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem.PerformClick();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.PerformClick();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printToolStripMenuItem.PerformClick();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem.PerformClick();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem.PerformClick();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem.PerformClick();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            analyzeTextToolStripMenuItem.PerformClick();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            hTMLEncodingToolStripMenuItem.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ınsertNumbersToolStripMenuItem.PerformClick();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            backgroundColourToolStripMenuItem.PerformClick();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            fontPropertiesToolStripMenuItem.PerformClick();
        }
        #endregion

        #region ASCII To Chars
        private void convertASCIICodesToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] ascıı = null;
            string chars = null;
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                ascıı = formtext.Text.Split(' ');
            }
            for (int i = 0; i < ascıı.Length; i++)
            {
                chars += Convert.ToChar(Convert.ToInt32(ascıı[i]));
            }
            foreach (TextBox formtext in tabControl1.SelectedTab.Controls)
            {
                formtext.Text = chars.ToString().Trim('\r', '\n');
            }
        }
        #endregion

        #region Multiple Find And Replace
        private void multipleFindAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
        }
        #endregion

        #region ExtractText Help
        private void extractTextHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("extracthelp.html");
        }
        #endregion

        #region Watch Clipboard
        private void watchClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (watchClipboardToolStripMenuItem.Text == "Watch Clipboard")
            {
                timer1.Start();
                watchClipboardToolStripMenuItem.Text = "Stop Watching Clipboard";
            }
            else
            {
                timer1.Stop();
                watchClipboardToolStripMenuItem.Text = "Watch Clipboard";
            }
        }

        string temp;
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (TextBox text in tabControl1.SelectedTab.Controls)
            {
                if (text is TextBox)
                {
                    if (System.Windows.Forms.Clipboard.ContainsText())
                    {
                        string clipboard = System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Text).ToString();
                        if (clipboard != temp)
                        {
                            int selection = text.SelectionStart;
                            text.Text = text.Text.Insert(text.SelectionStart, System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Text).ToString() + " ");
                            text.SelectionStart = selection + System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Text).ToString().Length + 1;
                        }
                        temp = System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.Text).ToString();
                    }
                }
            }
        }
        #endregion

        #region Clear Clipboard
        private void clearClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.Clear();
        }
        #endregion
    }
}