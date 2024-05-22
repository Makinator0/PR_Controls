using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR_Controls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }
        private void InitializeCustomComponents()
        {
            TabControl tabControl1 = new TabControl();
            tabControl1.Size = new Size(400, 300);
            tabControl1.Location = new Point(this.ClientSize.Width - tabControl1.Width - 10, 10);
            tabControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            TabPage tabPage1 = new TabPage("Лекции");
            TabPage tabPage2 = new TabPage("Изображение");


            CheckedListBox checkedListBox1 = new CheckedListBox();
            checkedListBox1.Items.AddRange(new object[] {
                "Лекция 1",
                "Лекция 2",
                "Лекция 3",
                "Лекция 4"
            });
            checkedListBox1.Location = new Point(10, 10);

            Button buttonShowSelected = new Button();
            buttonShowSelected.Text = "Показать выбранные лекции";
            buttonShowSelected.Location = new Point(10, 120);
            buttonShowSelected.Click += ButtonShowSelected_Click;

            tabPage1.Controls.Add(checkedListBox1);
            tabPage1.Controls.Add(buttonShowSelected);

            StatusStrip statusStrip1 = new StatusStrip();
            statusStrip1.Name = "statusStrip1";
            ToolStripStatusLabel toolStripStatusLabel1 = new ToolStripStatusLabel();
            statusStrip1.Items.Add(toolStripStatusLabel1);
            statusStrip1.Location = new Point(0, this.ClientSize.Height - statusStrip1.Height);
            statusStrip1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Button buttonOpenFileDialog = new Button();
            buttonOpenFileDialog.Text = "Выбрать изображение";
            buttonOpenFileDialog.Location = new Point(10, 10);
            buttonOpenFileDialog.Click += ButtonOpenFileDialog_Click;

            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Size = new Size(300, 200);
            pictureBox1.Location = new Point(10, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            tabPage2.Controls.Add(buttonOpenFileDialog);
            tabPage2.Controls.Add(pictureBox1);

            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Add(tabPage2);

            this.Controls.Add(tabControl1);
            this.Controls.Add(statusStrip1);
        }

        private void ButtonShowSelected_Click(object sender, EventArgs e)
        {
            TabPage tabPage = (TabPage)((Button)sender).Parent;
            CheckedListBox checkedListBox1 = (CheckedListBox)tabPage.Controls[0];

            StatusStrip statusStrip1 = (StatusStrip)this.Controls["statusStrip1"];
            ToolStripStatusLabel toolStripStatusLabel1 = (ToolStripStatusLabel)statusStrip1.Items[0];

            toolStripStatusLabel1.Text = "Выбранные лекции: " + string.Join(", ", checkedListBox1.CheckedItems.OfType<string>().ToArray());
        }

        private void ButtonOpenFileDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TabPage tabPage = (TabPage)((Button)sender).Parent;
                PictureBox pictureBox1 = (PictureBox)tabPage.Controls[1];

                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void цветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            label1.ForeColor = colorDialog1.Color;
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            label1.Font = fontDialog1.Font;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void часыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += delegate { label2.Text = DateTime.Now.ToLongTimeString() + "далее время по частям:" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.ToString(); };
        }

        private void расчетОстаткаВремениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DateTime dt1 = dateTimePicker1.Value;
            label2.Text = "До даты " + dt1.ToLongDateString() + " осталось " + (dt1 - DateTime.Now).Days + " дней";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ListView lv1 = new ListView();
            lv1.Name = "lv1";
            lv1.Size = new Size(360, 160);
            lv1.Location = new Point(50, 200);
            lv1.View = View.Details;
            lv1.Columns.Add("тип элемента", 240);
            lv1.Columns.Add("имя элемента", 120);
            Controls.Add(lv1);
            foreach (Control c1 in Controls)
            {
                string[] s2 = new string[2];
                s2[0] = c1.GetType().ToString();
                s2[1] = c1.Name;
                ListViewItem lvi1 = new ListViewItem(s2);
                lv1.Items.Add(lvi1);
            }
        }
    }
}
