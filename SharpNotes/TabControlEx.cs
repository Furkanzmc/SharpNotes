using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Yoramo.GuiLib
{
    public delegate bool PreRemoveTab(int indx);
    public class TabControlEx : TabControl
    {
        public TabControlEx()
            : base()
        {
            PreRemoveTabPage = null;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        public PreRemoveTab PreRemoveTabPage;
        
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle r = e.Bounds;
            r = GetTabRect(e.Index);
            r.Offset(4, 3);
            r.Width = 6;
            r.Height = 6;
            Brush b = new SolidBrush(Color.Red);
            Pen p = new Pen(b);
            e.Graphics.DrawLine(p, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
            e.Graphics.DrawLine(p, r.X + r.Width, r.Y, r.X, r.Y + r.Height);
            Brush s = new SolidBrush(Color.Black);
            string titel = this.TabPages[e.Index].Text;
            Font f = this.Font;
            if (titel.IndexOf('.') > 0)
            {
                e.Graphics.DrawString(titel.Remove(titel.IndexOf('.')), f, s, new PointF(r.X + 7, r.Y));
            }
            else
            {
                e.Graphics.DrawString(titel, f, s, new PointF(r.X + 7, r.Y));
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle r = GetTabRect(i);
                r.Offset(4, 3);
                r.Width = 9;
                r.Height = 9;
                if (r.Contains(p))
                {
                    if (TabPages[i].Controls[0].Name.IndexOf("~") < 0 && TabPages[i].Controls[0].Name.Length == 5)
                        {
                            if (TabPages[i].Controls[0].Name.IndexOf("*") >= 0 && TabPages[i].Controls[0].Name.Length == 5)
                            {
                                DialogResult result = MessageBox.Show("Do you want to save \"" + TabPages[i].Text + "\" before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    SaveFileDialog save = new SaveFileDialog();
                                    save.Filter = "Text Files |*.txt";
                                    if (save.ShowDialog() == DialogResult.OK)
                                    {
                                        StreamWriter writer = File.CreateText(save.FileName);
                                        writer.Write(TabPages[i].Controls[0].Text);
                                        writer.Close();
                                        CloseTab(i);
                                    }
                                }
                                else if (result == DialogResult.Cancel)
                                {
                                    break;
                                }
                                else if (result == DialogResult.No)
                                {
                                    CloseTab(i);
                                }
                            }
                            else
                            {
                                CloseTab(i);
                            }
                        }
                        else
                        {
                            if (TabPages[i].Controls[0].Name.IndexOf("~") < 0)
                            {
                                if (TabPages[i].Controls[0].Name.IndexOf("*") > 0 && TabPages[i].Controls[0].Name.Length > 5)
                                {
                                    DialogResult result = MessageBox.Show("Do you want to save \"" + TabPages[i].Controls[0].Name.Substring(0, TabPages[i].Controls[0].Name.Length - 5) + "\" file before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        StreamWriter writer = File.CreateText(TabPages[i].Controls[0].Name.Substring(0, TabPages[i].Controls[0].Name.Length - 5));
                                        writer.Write(TabPages[i].Controls[0].Text);
                                        writer.Close();
                                        CloseTab(i);
                                    }
                                    else if (result == DialogResult.Cancel)
                                    {
                                        break;
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        CloseTab(i);
                                    }
                                }
                                else
                                {
                                    CloseTab(i);
                                }
                            }
                            else
                            {
                                CloseTab(i);
                            }
                        }
                    }
                }
            }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Point p = e.Location;
                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle r = GetTabRect(i);
                    r.Offset(4, 3);
                    r.Width = 67;
                    r.Height = 18;
                    if (r.Contains(p))
                    {
                        if (TabPages[i].Controls[0].Name.IndexOf("~") < 0 && TabPages[i].Controls[0].Name.Length == 5)
                        {
                            if (TabPages[i].Controls[0].Name.IndexOf("*") >= 0 && TabPages[i].Controls[0].Name.Length == 5)
                            {
                                DialogResult result = MessageBox.Show("Do you want to save \"" + TabPages[i].Text + "\" before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    SaveFileDialog save = new SaveFileDialog();
                                    save.Filter = "Text Files |*.txt";
                                    if (save.ShowDialog() == DialogResult.OK)
                                    {
                                        StreamWriter writer = File.CreateText(save.FileName);
                                        writer.Write(TabPages[i].Controls[0].Text);
                                        writer.Close();
                                        CloseTab(i);
                                    }
                                }
                                else if (result == DialogResult.Cancel)
                                {
                                    break;
                                }
                                else if (result == DialogResult.No)
                                {
                                    CloseTab(i);
                                }
                            }
                            else
                            {
                                CloseTab(i);
                            }
                        }
                        else
                        {
                            if (TabPages[i].Controls[0].Name.IndexOf("~") < 0)
                            {
                                if (TabPages[i].Controls[0].Name.IndexOf("*") > 0 && TabPages[i].Controls[0].Name.Length > 5)
                                {
                                    DialogResult result = MessageBox.Show("Do you want to save \"" + TabPages[i].Controls[0].Name.Substring(0, TabPages[i].Controls[0].Name.Length - 5) + "\" file before you exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        StreamWriter writer = File.CreateText(TabPages[i].Controls[0].Name.Substring(0, TabPages[i].Controls[0].Name.Length - 5));
                                        writer.Write(TabPages[i].Controls[0].Text);
                                        writer.Close();
                                        CloseTab(i);
                                    }
                                    else if (result == DialogResult.Cancel)
                                    {
                                        break;
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        CloseTab(i);
                                    }
                                }
                                else
                                {
                                    CloseTab(i);
                                }
                            }
                            else
                            {
                                CloseTab(i);
                            }
                        }
                    }
                }
            }
        }

        private void CloseTab(int i)
        {
            if (TabPages.Count != 1)
            {
                if (PreRemoveTabPage != null)
                {
                    bool closeIt = PreRemoveTabPage(i);
                    if (!closeIt)
                        return;
                }
                TabPages.Remove(TabPages[i]);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
