using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
namespace threeApps
{
    public class DifficultyMath : Form
    {
        Button easylD, normalID, hardD;
        public static int a;
        public DifficultyMath()
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
                Math.timeLeft = 18;
                Math math = new Math();
                math.Show();
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
                Math.timeLeft = 18;
                Math math = new Math();
                math.Show();
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
                a = 5;
                Math.timeLeft = 15;
                Math math = new Math();
                math.Show();
                this.Close();
            };
            
            object[] objects = new object[] { easylD, normalID, hardD };
            foreach (var item in objects) {Controls.Add((Control)item);}
        }
    }
    public partial class Math : Form
    {
        //private Difficulty diff = new Difficulty();
        
        Random rnd = new Random();
        char[] symbols = new char[] { '+', '-', '*', '/' };
        int firstPlus, secondPlus, firstMl, secondMl, firstD, secondD, firstMn, secondMn;
        public static int timeLeft = 30;
        TableLayoutPanel tlp;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Label lb;
        Button start;
        public Math()
        {    
           // diff.Show();
            Text = "Matemaatika viktoriin";
            Icon = Properties.Resources.math_book_school_study_icon_209279;
            Size = new Size(700, 500);
            this.BackColor = Color.FromArgb(0, 153, 153);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            lb = new Label
            {
                Font = new Font(Font.FontFamily, 18),
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(200, 30),
                Location = new Point(180, 40),
                BackColor = Color.White
            };
            Label label = new Label
            {
                Font = new Font(Font.FontFamily, (float)15.75),
                Text = "Aega jäänud",
                AutoSize = true,
                Location = new Point(140, 40)
            };
            start = new Button
            {
                Text = "Alustage viktoriini",
                Font = new Font(Font.FontFamily, 14),
                AutoSize = true,
                TabIndex = 0,
                BackColor = Color.FromArgb(153, 0, 0),
                Location = new Point(100, 315),
            };
            start.Click += Start_Click;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            for (int i = 1; i < DifficultyMath.a; i++)
            {
                Label num1 = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = "?",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                Label sign = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = symbols[i - 1].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                Label num2 = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = "?",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                Label equals = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = "=",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                NumericUpDown Numer = new NumericUpDown
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Width = 100,
                    TabIndex = i + 1,
                };

                tlp.Controls.Add(num1, 0, i);
                tlp.Controls.Add(sign, 1, i);
                tlp.Controls.Add(num2, 2, i);
                tlp.Controls.Add(equals, 3, i);
                tlp.Controls.Add(Numer, 4, i);
            }
            tlp.Controls.Add(lb, 3, 0);
            tlp.SetColumnSpan(label, 2);
            tlp.SetColumnSpan(lb, 2);
            tlp.Controls.Add(label, 1, 0);
            tlp.SetColumnSpan(start, 2);
            tlp.Controls.Add(start, 2, 5);
            Controls.Add(tlp);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NumericUpDown Numer = (NumericUpDown)tlp.GetControlFromPosition(4, 1);
            NumericUpDown minN = (NumericUpDown)tlp.GetControlFromPosition(4, 2);
            NumericUpDown mulN = (NumericUpDown)tlp.GetControlFromPosition(4, 3);
            NumericUpDown divN = (NumericUpDown)tlp.GetControlFromPosition(4, 4);
            if (CheckTheAnswer())
            {
                timer.Stop();
                MessageBox.Show("Sa said kõik vastused õiged!",
                                "Palju õnne!");
                start.Enabled = true;
                this.Close();
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                lb.Text = timeLeft + " sekundit";
            }
            else
            {
                timer.Stop(); 
                lb.Text = "Aeg on läbi!";
                MessageBox.Show("Sa ei lõpetanud õigeks ajaks.", "Vabandust!");
                if (DifficultyMath.a == 3)
                {
                    Numer.Value = firstPlus + secondPlus;
                    minN.Value = firstMn - secondMn;
                }
                else if (DifficultyMath.a == 4)
                {
                    Numer.Value = firstPlus + secondPlus;
                    minN.Value = firstMn - secondMn;
                    mulN.Value = firstMl * secondMl;
                }
                else
                {
                    Numer.Value = firstPlus + secondPlus;
                    minN.Value = firstMn - secondMn;
                    mulN.Value = firstMl * secondMl;
                    divN.Value = firstD / secondD;
                }
                
                start.Enabled = true;

                this.Close();
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            start.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            NumericUpDown N = (NumericUpDown)tlp.GetControlFromPosition(4, 1);
            NumericUpDown minN = (NumericUpDown)tlp.GetControlFromPosition(4, 2);
            NumericUpDown mulN = (NumericUpDown)tlp.GetControlFromPosition(4, 3);
            NumericUpDown divN = (NumericUpDown)tlp.GetControlFromPosition(4, 4);
            if (DifficultyMath.a == 3)
            {
                if ((firstPlus + secondPlus == N.Value) && (firstMn - secondMn == minN.Value))
                    return true;
                else
                    return false;

                return true;
            }
            else if (DifficultyMath.a == 4)
            {
                if ((firstPlus + secondPlus == N.Value) && (firstMn - secondMn == minN.Value) && (firstMl * secondMl == mulN.Value))

                    return true;

                else
                    return false;

                    return true;
            }

            else if (DifficultyMath.a == 5)
            {
                if ((firstPlus + secondPlus == N.Value) && (firstMn - secondMn == minN.Value) && (firstMl * secondMl == mulN.Value) && (firstD / secondD == divN.Value))
                    return true;
                else
                    return false;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartTheQuiz()
        {
            for (int row = 1; row < DifficultyMath.a; row++)
            {
                Label num1 = (Label)tlp.GetControlFromPosition(0, row);
                Label symbol = (Label)tlp.GetControlFromPosition(1, row);
                Label num2 = (Label)tlp.GetControlFromPosition(2, row);
                NumericUpDown N = (NumericUpDown)tlp.GetControlFromPosition(4, row);
                int[] thing = getNums(symbol.Text);
                num1.Text = thing[0].ToString();
                num2.Text = thing[1].ToString();
                N.Value = 0;
            }
            lb.Text = $"{timeLeft} sekundit";
            timer.Start();
        }

        public int[] getNums(string sym)
        {
            int num1 = 0;
            int num2 = 0;

            if (sym == "+")
            {
                num1 = rnd.Next(51);
                num2 = rnd.Next(51);
                firstPlus = num1;
                secondPlus = num2;
            }
            else if (sym == "-")
            {
                num1 = rnd.Next(1, 101);
                num2 = rnd.Next(1, num1);
                firstMn = num1;
                secondMn = num2;
            }
            else if (sym == "/")
            {
                num2 = rnd.Next(2, 11);
                int temporaryQuotient = rnd.Next(2, 11);
                num1 = num2 * temporaryQuotient;
                firstD = num1;
                secondD = num2;
            }
            else if (sym == "*")
            {
                num1 = rnd.Next(2, 11);
                num2 = rnd.Next(2, 11);
                firstMl = num1;
                secondMl = num2;
            }

            return new int[2] { num1, num2 };
        }
    }
}