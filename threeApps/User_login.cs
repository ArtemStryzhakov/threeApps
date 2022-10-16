using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Web;

namespace threeApps
{
    public partial class User_login : Form
    {
        Label lbl, username_lbl, password_lbl, showTime;
        TextBox username, password;
        Button btn, newUser, recover_psw, close_btn;
        public static string FromXML_user = "";
        public string FromXML_pwd = "";
        private Timer currentTime;
        public User_login()
        {
            Size = new Size(450, 400);
            Icon = Properties.Resources.user;
            Text = "Login form";

            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - this.Width / 2;
            this.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - this.Height / 2;

            FormBorderStyle = FormBorderStyle.None;
            lbl = new Label()
            {
                Text = "Sisestage oma andmed:",
                Location = new Point(155, 75),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            username_lbl = new Label()
            {
                Text = "Kasutajanimi:",
                Location = new Point((int)102.5, (int)112.5),
                AutoSize = true
            };

            currentTime = new Timer();
            currentTime.Enabled = true;
            currentTime.Interval = 1000;
            currentTime.Tick += Timer_tick;
            
            showTime = new Label()
            {
                AutoSize = true,
                Location = new Point(375, (int)7.5),
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Arial", 10)
            };

            username = new TextBox()
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

            newUser = new Button()
            {
                Text = "Uus kasutaja...",
                Location = new Point(300, 200),
                BackColor = Color.LightBlue,
                AutoSize = true
            };

            newUser.Click += NewUser_Click;

            btn = new Button()
            {
                Text = "Jätka",
                Location = new Point((int)192.5, 200)
            };
            btn.Click += Btn_Click;

            recover_psw = new Button()
            {
                Text = "Parooli taastamine",
                Location = new Point(60, 200),
                AutoSize = true,
                BackColor = Color.LightGreen
            };
            recover_psw.Click += Recover_psw_Click;

            close_btn = new Button() {
                Text = "Sulge",
                BackColor = Color.LightPink,
                Location = new Point((int)177.5, 350),
                Size = new Size(100, 30)
            };
            close_btn.Click += (sender, args) => System.Environment.Exit(1);

            List<object> list = new List<object>() { lbl, username_lbl, username, password_lbl, password, newUser, btn, recover_psw, close_btn, showTime };
            foreach (object item in list) { this.Controls.Add((Control)item); }
        }

        // Clock which shows real time
        private void Timer_tick(object sender, EventArgs e) => showTime.Text = DateTime.Now.ToString("HH:mm:ss");

        private void Recover_psw_Click(object sender, EventArgs e)
        {
            psw_recover recover = new psw_recover();
            recover.Show();
            this.Hide();
        }

        private void NewUser_Click(object sender, EventArgs e)
        {
            User_register register = new User_register();
            this.Hide();
            register.Show();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string user = username.Text;
            string pass = password.Text;

            XDocument doc = XDocument.Load(Application.StartupPath.ToString() + "@..\\..\\..\\users.xml");


            var selected_user = from x in doc.Descendants("User").Where
                                (x => (string)x.Element("username") == username.Text)
                                select new
                                {
                                    XMLuser = x.Element("username").Value,
                                    XMLpud = x.Element("password").Value 
                                };

                foreach (var x in selected_user)
                {
                    FromXML_user = x.XMLuser;
                    FromXML_pwd = x.XMLpud;
                }

            if (username.Text != "" || password.Text != "")
            {
                if (user == FromXML_user)
                {
                    if (pass == FromXML_pwd)
                    {
                        Choose choose = new Choose();
                    
                        this.Hide();
                        choose.Show();                  
                    }
                    else
                    {
                        MessageBox.Show("Wrong password");
                        ClearBoxes();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong username!");
                    ClearBoxes();
                }
            }
            else
            {
                MessageBox.Show("Täitke kõik tekstikastid.");
            }
            
        }

        private void ClearBoxes()
        {
            username.Clear();
            password.Clear();
        }
    }
}
