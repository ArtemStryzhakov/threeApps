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
    public class DifficultyGame : Form
    {
        Button easylD, normalID, hardD;
        public static int a, b, c;
        public DifficultyGame()
        {
            Size = new Size(200, 260);
            FormBorderStyle = FormBorderStyle.None;

            var screen = Screen.FromPoint(Cursor.Position);
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.Bounds.Left + 10;
            this.Top = screen.Bounds.Top + 10;

            easylD = new Button()
            {
                Text = "Tavaline raskusaste",
                AutoSize = true,
                Location = new Point((int)22.5, 20),
                Size = new Size(150, 50)
            };

            easylD.Click += (sender, args) =>
            {
                a = 3;
                b = 4;
                Game game = new Game();
                game.Show();
                this.Close();
            };

            normalID = new Button()
            {
                Text = "Normaalne raskus",
                AutoSize = true,
                Location = new Point((int)22.5, 100),
                Size = new Size(150, 50)
            };
            normalID.Click += (sender, args) =>
            {
                a = 4;
                b = 4;
                Game game = new Game();
                game.Show();
                this.Close();
            };

            hardD = new Button()
            {
                Text = "Raske raskus",
                AutoSize = true,
                Location = new Point((int)22.5, 180),
                Size = new Size(150, 50)
            };

            hardD.Click += (sender, args) =>
            {
                a = 4;
                b = 5;
                Game game = new Game();
                game.Show();
                this.Close();
            };

            object[] objects = new object[] { easylD, normalID, hardD };
            foreach (var item in objects) { Controls.Add((Control)item); }
        }
    }
    public partial class Game : Form
    {
        Random random = new Random();
        Timer timer;
        TableLayoutPanel tlp;
        Label firstClk = null;
        Label secondClk = null;
        
        List<string> icons = new List<string>() {};

        List<string> iconsCopy = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z", "p", "p", "?", "?"
        };

        public Game()
        {
            Text = "Leida sarnaseid pilte";
            Icon = Properties.Resources.puzzle_icon_149707;
            this.Size = new Size(600, 600);
            this.BackColor = Color.FromArgb(0, 153, 153);
            MaximizeBox = false;

            if (DifficultyGame.a == 3 && DifficultyGame.b == 4)
            {
                for (int i = 0; i < 12; i++) icons.Add(iconsCopy[i]);
            }
            else if (DifficultyGame.a == 4 && DifficultyGame.b == 4)
            {
                for (int i = 0; i < 16; i++) icons.Add(iconsCopy[i]);
            }
            else
            {
                for (int i = 0; i < 20; i++) icons.Add(iconsCopy[i]); 
            }

            string[] strings = new string[6] { "#3333FF", "#fa4670", "#8c0323", "#129e05", "#e6d307", "#fafaf7" };

            Random random = new Random();
            int rnd = random.Next(strings.Length);

            tlp = new TableLayoutPanel
            {
                BackColor = ColorTranslator.FromHtml(strings[rnd]),
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            for (int i = 0; i < DifficultyGame.a; i++)
            {
                for (int j = 0; j < DifficultyGame.b; j++)
                {
                    Label lb = new Label
                    {
                        BackColor = ColorTranslator.FromHtml(strings[rnd]),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", 40, FontStyle.Bold),
                        Size = new Size(40, 40),
                        Text = "c"
                    };
                    lb.Click += label1_Click;
                    tlp.Controls.Add(lb, i, j);
                }
            }
            timer = new Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;
            Controls.AddRange(new Control[] { tlp });
            AssignIconsToSquares();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            firstClk.ForeColor = firstClk.BackColor;
            secondClk.ForeColor = secondClk.BackColor;
            firstClk = null;
            secondClk = null;
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClk == null)
                {
                    firstClk = clickedLabel;
                    firstClk.ForeColor = Color.Black;
                    return;
                }

                secondClk = clickedLabel;
                secondClk.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClk.Text == secondClk.Text)
                {
                    firstClk = null;
                    secondClk = null;
                    return;
                }

                timer.Start();
            }
        }
        private void CheckForWinner()
        {
            foreach (Control control in tlp.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Sa sobitasid kõik ikoonid!", "Palju õnne");
            Close();
        }
    }
}
