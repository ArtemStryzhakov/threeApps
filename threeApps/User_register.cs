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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace threeApps
{
    public partial class User_register : Form
    {
        Label lbl, username_lbl, password_lbl, email_lbl, sugu_lbl, vanus_lbl;
        TextBox username, password, email, sugu, vanus;
        Button btn;
        public User_register()
        {
            Size = new Size(450, 400);
            Icon = Properties.Resources.singinIcon;
            Text = "Sign in form";

            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - this.Width / 2;
            this.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - this.Height / 2;

            FormBorderStyle = FormBorderStyle.None;

            lbl = new Label()
            {
                Text = "Sisestage oma andmed:",
                Location = new Point(155, 30),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            username_lbl = new Label()
            {
                Text = "Kasutajanimi:",
                Location = new Point((int)102.5, (int)62.5),
                AutoSize = true
            };

            username = new TextBox()
            {
                Location = new Point(180, 60),
            };

            password_lbl = new Label()
            {
                Text = "Parool:",
                Location = new Point(130, (int)112.5),
                AutoSize = true
            };

            password = new TextBox()
            {
                Location = new Point(180, 112),
                PasswordChar = '●',
            };

            email_lbl = new Label()
            {
                Text = "Email:",
                Location = new Point(130, (int)162.5),
                AutoSize = true
            };

            email = new TextBox()
            {
                Location = new Point(180, 162),

            };

            sugu_lbl = new Label()
            {
                Text = "Sugu:",
                Location = new Point(130, (int)212.5),
                AutoSize = true
            };

            sugu = new TextBox()
            {
                Location = new Point(180, 210),
            };

            vanus_lbl = new Label()
            {
                Text = "Vanus:",
                Location = new Point(130, (int)262.5),
                AutoSize = true
            };

            vanus = new TextBox()
            {
                Location = new Point(180, 260),
            };

            btn = new Button()
            {
                Text = "Jätka",
                Location = new Point((int)192.5, 300)
            };

            Button btn_back = new Button()
            {
                Text = "Mul on konto",
                AutoSize = true,
                BackColor = Color.LightBlue,
            };
            btn_back.Click += Btn_back_Click;

            btn.Click += Btn_Click;

            List<object> list = new List<object>() { lbl, username_lbl, username, password_lbl, password, email_lbl, email, sugu_lbl, sugu, vanus_lbl, vanus, btn, btn_back };
            foreach (object item in list) { this.Controls.Add((Control)item); }
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            User_login login = new User_login();
            this.Close();
            login.Show();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string xmlPath = "@..\\..\\..\\..\\users.xml";
            
            if (username.Text != "" && password.Text != "" && email.Text != "" && sugu.Text != "" && vanus.Text != "")
            {
                if (email.Text.Contains("@") && email.Text.Contains("."))
                {
                    Append(xmlPath, username.Text, password.Text, email.Text, sugu.Text, vanus.Text);
                }
                else
                {
                    MessageBox.Show("Kas e-post või sugu on vale");
                }
            }
            else
            {
                MessageBox.Show("Ei, täitke see tekstiväli!");
            }

            User_login login = new User_login();
            this.Close();
            login.Show();
        }

        public static void Append(string filename, string username, string password, string email, string sugu, string vanus)
        {
            var username_ob = new XElement("User", new XAttribute("id", email), new XElement("username", username), new XElement("password", password), new XElement("email", email),
                new XElement("sugu", sugu), new XElement("vanus", vanus)); // username = kasutaja

            var doc = new XDocument();

            if (File.Exists(filename))
            {
                doc = XDocument.Load(filename);
                doc.Element("Profile").Add(username_ob);
            }
            else
            {
                doc = new XDocument(new XElement("Profile", username_ob));
            }
            doc.Save(filename);
        }
    }
}
