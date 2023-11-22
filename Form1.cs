using System.Drawing.Printing;

namespace TextEditorApplication
{
    public partial class Form1 : Form
    {
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
        }

        public int getWidth()
        {
            int w = 25;
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }
            return w;
        }

        public void AddLineNumbers()
        {
            Point pt = new Point(0, 0);
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.Text = "";
            richTextBox2.Width = getWidth();

            for (int i = First_Line; i <= Last_Line; i++)
            {
                richTextBox2.Text += (i + 1) + "\n";
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Microsoft Sans Serif", 14);
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
            this.BackColor = Color.White;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Open a file...";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "Text Files (.txt)|*.txt";
            svf.Title = "Save a file...";

            if (svf.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                sw.Write(richTextBox1.Text);
                sw.Close();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Print
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void fontStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fd.Font;
            }
        }

        private void fontColorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = cd.Color;
            }
        }

        private void lightModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
            this.BackColor = Color.White;

            /* if (richTextBox1.ForeColor != Color.White)
            {
                richTextBox1.BackColor = Color.White;
            }
            else
            {
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
            } */
        }

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.DimGray;
            richTextBox1.ForeColor = Color.White;
            this.BackColor = Color.DimGray;

            /* if (richTextBox1.ForeColor != Color.Black) 
            {
                richTextBox1.BackColor = Color.DimGray;
            }
            else
            {
                richTextBox1.BackColor = Color.DimGray;
                richTextBox1.ForeColor = Color.White;
            } */
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a simple Notepad Application made using Visual Studio.", "Notepad Application");
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void lineCountToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int lineCount = richTextBox1.Lines.Count();
            /* char[] separator = { ' ' };
            int wordCount = richTextBox1.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length; */

            MessageBox.Show($"Line Count: {lineCount}", "Word Count");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox2.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            AddLineNumbers();
            richTextBox2.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            richTextBox2.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void richTextBox2_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.Select();
            richTextBox2.DeselectAll();
        }
    }
}