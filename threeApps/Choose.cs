using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace threeApps
{
    public partial class Choose : Form
    {
        Button f_app, s_app, t_app, closeApp;
        Label text, namePl;
        User_login login = new User_login();

        public Choose()
        {
            this.Icon = Properties.Resources.adobe_indesign_software_computer_app_design_software_icon_191061;
            this.Text = "Menu App";
            this.Size = new Size(555, 375);
            this.BackColor = Color.FromArgb(0, 153, 153);
            
            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - this.Width / 2;
            this.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - this.Height / 2;

            FormBorderStyle = FormBorderStyle.None; 

            text = new Label()
            {
                Text = "Valige mis tahes rakendus:",
                Location = new Point(165, 80),
                Font = new Font("Arial", 15),
                AutoSize = true
            };

            namePl = new Label()
            {
                Location = new Point(0, 1),
                AutoSize = true,
                Text = $"Tere, {User_login.FromXML_user}!",
                Font = new Font("Comic Sans MS", 12)
            };
            //--------------------------------------------
            f_app = new Button();
            f_app.Size = new Size(150, 70);
            f_app.Location = new Point(20, 120);
            f_app.Text = "Piltide vaatamise rakendus";
            f_app.BackColor = Color.FromArgb(153, 0, 0);
            f_app.Font = new Font("Arial", 10);
            f_app.ForeColor = Color.White;
            f_app.Click += F_app_Click;
            //--------------------------------------------
            s_app = new Button();
            s_app.Size = new Size(150, 70);
            s_app.Location = new Point(200, 120);
            s_app.Text = "Matemaatiline äraarvamismäng";
            s_app.BackColor = Color.FromArgb(153, 0, 0);
            s_app.Font = new Font("Arial", 10);
            s_app.ForeColor = Color.White;
            s_app.Click += S_app_Click;
            //--------------------------------------------
            t_app = new Button();
            t_app.Size = new Size(150, 70);
            t_app.Location = new Point(380, 120);
            t_app.Text = "Mäng sarnaste piltide leidmiseks";
            t_app.BackColor = Color.FromArgb(153, 0, 0);
            t_app.Font = new Font("Arial", 10);
            t_app.ForeColor = Color.White;
            t_app.Click += T_app_Click;

            closeApp = new Button()
            {
                Text = "Sulge",
                Location = new Point(240, 330),
                Size = new Size(80, 30),
                Font = new Font("Arial", 10),
                ForeColor = Color.White,
                BackColor = Color.Brown,
                FlatStyle = FlatStyle.Flat,
            };
            closeApp.FlatAppearance.BorderColor = Color.Brown;
            closeApp.FlatAppearance.BorderSize = 1;

            closeApp.Click += (sender, args) =>
            {
                this.Close();
                login.Show();
            };

            //Display objects in the program
            object[] varib = new object[] { text, f_app, s_app, t_app, namePl, closeApp };

            foreach (object items in varib) { this.Controls.Add((Control)items); }
        }

            private void F_app_Click(object sender, EventArgs e)
        {
            Picture first_app = new Picture();
            first_app.Show();
        }
        private void S_app_Click(object sender, EventArgs e)
        {  
            DifficultyMath second_app = new DifficultyMath();
            second_app.Show();
        }

        private void T_app_Click(object sender, EventArgs e)
        {
            DifficultyGame third_app = new DifficultyGame();
            third_app.Show();
        }  
    }
}
