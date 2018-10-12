using GameLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game15.App
{
    public partial class MainForm : Form
    {
        private int time=0;
        private string Name;
        Game game = new Game();
        private List<PlayerScore> Records = new List<PlayerScore>() { new PlayerScore() { Name = "Vasya", Time = 1000 }, new PlayerScore() { Name = "Petya", Time = 100 }, new PlayerScore() { Name = "Zina", Time = 20 } };
        int btnSize;
        int btnMargin = 10;
        public MainForm()
        {
            InitializeComponent();
            btnSize = ClientSize.Width > ClientSize.Height ?
                (ClientSize.Height - btnMargin) / 4 - btnMargin :
                (ClientSize.Width - btnMargin) / 4 - btnMargin;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            StartForm dlg = new StartForm();
            if (dlg.ShowDialog(this) == DialogResult.Cancel)
            {
                Close();
                return;
            }
            Name = dlg.textBoxName.Text;
            GameMode mode = GameMode.Horizontal;
            if (dlg.radioButton2.Checked)
            {
                mode = GameMode.Vertical;
            }
            else if (dlg.radioButton3.Checked)
            {
                mode = GameMode.Mixed;
            }
            time = 0;
            timer1.Enabled = true;
            game.InitGame(mode);

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (game[j, i] != 0)
                        new Button
                        {
                            Parent = this,
                            Text = Convert.ToString(game[j, i], 10),
                            Tag = game[j, i],
                            Width = btnSize,
                            Height = btnSize,
                            Location = new Point
                            {
                                X = btnMargin + (btnSize + btnMargin) * i,
                                Y = btnMargin + (btnSize + btnMargin) * j
                            }
                        }.Click += btnClick;
                }
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null) return;
            int value = (int)btn.Tag;
            int x = game.zeroX;
            int y = game.zeroY;

            if (game.CheckAndGo(value))
            {
                btn.Location = new Point
                {
                    X = btnMargin + (btnSize + btnMargin) * x,
                    Y = btnMargin + (btnSize + btnMargin) * y
                };
            }

            if (game.IsWin())
            {
                timer1.Enabled = false;
                Records.Add(new PlayerScore() { Name = this.Name, Time = time });
                Records.Sort();
                string s =$"Имя \t Очки \n";

                for (int i = 0; i < Records.Count; i++)
                {
                    s+= $"{Records[i].Name}\t{Records[i].Time}\n";
                }
                
                MessageBox.Show(s);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label1.Text = time.ToString();
        }
        


    }
}
