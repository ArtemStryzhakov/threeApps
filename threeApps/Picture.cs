using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace threeApps
{
    public partial class Picture : Form
    {
        OpenFileDialog OpenFileD;
        ColorDialog cDialog;
        PictureBox pBox;
        CheckBox cBox;
        public Picture()
        {
            this.Size = new Size(600, 700);
            this.Text = "Pildi vaatamise programm";
            this.Icon = Properties.Resources.photoGallery;
            this.BackColor = Color.FromArgb(0, 153, 153);
            TableLayoutPanel tlp = new TableLayoutPanel { Dock = DockStyle.Fill };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

            pBox = new PictureBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.Fixed3D };
            pBox.DoubleClick += Pb_DoubleClick;

            tlp.Controls.Add(pBox);
            tlp.SetColumnSpan(pBox, 2);
            //-----------------------------------------------------------
            cBox = new CheckBox
            {
                Text = "Venitada"
            };
            cBox.CheckedChanged += Cb_CheckedChanged;
            //-----------------------------------------------------------
            FlowLayoutPanel flp = new FlowLayoutPanel { Dock = DockStyle.Fill };

            Button close = new Button { Text = "Sulge", AutoSize = true, BackColor = Color.FromArgb(153, 0, 0) };
            close.Click += Close_Click;

            Button clr = new Button { Text = "Tühjenda pilt", AutoSize = true, BackColor = Color.Yellow };
            clr.Click += Clr_Click;

            Button show = new Button { Text = "Näita pilti", AutoSize = true, BackColor = Color.Blue, ForeColor = Color.White };
            show.Click += Show_Click;

            List<object> itemList = new List<object>() { cBox, close, show, clr };
            foreach (object item in itemList) { flp.Controls.Add((Control)item); }
            tlp.Controls.Add(flp);

            OpenFileD = new OpenFileDialog
            {
                Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*",
                Title = "Valige pildifail"
            };
            cDialog = new ColorDialog();
            Controls.Add(tlp);
        }

        private void Pb_DoubleClick(object sender, EventArgs e)
        {
            if (cDialog.ShowDialog() == DialogResult.OK)
                pBox.BackColor = cDialog.Color;
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (cBox.Checked) {
                pBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else {
                pBox.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (OpenFileD.ShowDialog() == DialogResult.OK)
            {
                pBox.Load(OpenFileD.FileName);
            }
        }
        private void Clr_Click(object sender, EventArgs e)
        {
            pBox.Image = null;
        }
        private void Close_Click(object sender, EventArgs e) {
            Close();
        } 
    }
}

