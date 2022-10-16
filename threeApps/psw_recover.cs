using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace threeApps
{
    public partial class psw_recover : Form
    {
        Label title, email_lbl, password_lbl;
        TextBox email, password;
        Button btn, btn_back;
        User_login login = new User_login();
        public psw_recover()
        {
            Size = new Size(450, 350);
            Icon = Properties.Resources.recycle_recover_document_restore_icon_220428;
            Text = "Recover password form";

            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - this.Width / 2;
            this.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - this.Height / 2;

            FormBorderStyle = FormBorderStyle.None;

            title = new Label()
            {
                Text = "Sisestage oma andmed:",
                Location = new Point(155, 60),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            email_lbl = new Label()
            {
                Text = "Email:",
                Location = new Point((int)135, (int)112.5),
                AutoSize = true
            };

            email = new TextBox()
            {
                Location = new Point(180, 110),
            };

            password_lbl = new Label()
            {
                Text = "Parool:",
                Location = new Point(130, (int)162.5),
                AutoSize = true
            };

            password = new TextBox()
            {
                Location = new Point(180, 160),
                PasswordChar = '●',
            };

            btn = new Button()
            {
                Text = "Jätka",
                Location = new Point((int)192.5, 200)
            };
            btn.Click += Btn_Click;

            btn_back = new Button()
            {
                Text = "Ma mõtlesin ümber",
                AutoSize = true,
                BackColor = Color.LightBlue,
            };
            btn_back.Click += Btn_back_Click;
            
            List<object> list = new List<object>() { title, email_lbl, password_lbl, email, password, btn, btn_back };
            foreach (object item in list)
            {
                this.Controls.Add((Control)item);
            }
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            User_login login = new User_login();
            login.Show();
            this.Close();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\users.xml");
            XmlNode tgtnode = doc.SelectSingleNode($"Profile/User[@id='{email.Text}']/password");
            tgtnode.InnerText = password.Text;

            doc.Save(@"..\..\users.xml");
        }
    }
}
