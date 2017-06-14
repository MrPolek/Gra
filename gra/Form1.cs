using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gra
{
    public partial class Form1 : Form
    {
        bool idzlewo;
        bool idzprawo;
        int szybkosc = 5;
        int punkty = 0;
        bool jestnacisniety;
        int ileprzeciwnikow = 12;
        int szybkoscgracz = 6;

        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();

        }

        private void klawiszdol(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left)
            {
                idzlewo = true;
            }
            if(e.KeyCode==Keys.Right)
            {
                idzprawo = true;
            }
            if(e.KeyCode==Keys.Space && !jestnacisniety)
            {
                jestnacisniety = true;
                robpocisk();
            }
            if(e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
            foreach (Control g in this.Controls)
            {
                if (g is PictureBox && g.Tag == "gracz")
                {
                    if (((PictureBox)g).Left > 400)
                    {
                        idzlewo = false;
                    }
                    if (((PictureBox)g).Left < 400)
                    {
                        idzlewo = true;
                    }
                }
            }

        }

        private void klawiszgora(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left)
            {
                idzlewo = false;
            }
            if(e.KeyCode==Keys.Right)
            {
                idzprawo = false;
            }
            if(jestnacisniety)
            {
                jestnacisniety = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (idzlewo)
            {
                gracz.Left -= szybkoscgracz;
            }
            else if (idzprawo)
            {
                gracz.Left += szybkoscgracz;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "wrog")
                {
                    if
                        (((PictureBox)x).Bounds.IntersectsWith(gracz.Bounds))
                    {
                        koniecgry();
                        MessageBox.Show("lol przegrales O.O");
                    }
                    ((PictureBox)x).Left += szybkosc;
                    if (((PictureBox)x).Left > 500)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;
                    }
                }
            }

            foreach (Control y in this.Controls)
            {
                if(y is PictureBox && y.Tag == "pocisk")
                {
                    y.Top -= 20;
                    if(((PictureBox)y).Top<this.Height-490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
            foreach(Control i in this.Controls)
            {
                foreach(Control j in this.Controls)
                {
                    if(i is PictureBox && i.Tag == "wrog")
                    {
                        if(j is PictureBox && j.Tag=="pocisk")
                        {
                            if(i.Bounds.IntersectsWith(j.Bounds))
                            {
                                punkty++;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }



            label1.Text = "Punkty: " + punkty;
            if(punkty>ileprzeciwnikow -1)
            {
                koniecgry();
                MessageBox.Show("Wygrales!!!");
            }



         }
        private void robpocisk()
        {
            PictureBox pocisk = new PictureBox();
            pocisk.Image = Properties.Resources.bullet;
            pocisk.Size = new Size(5, 20);
            pocisk.Tag = "pocisk";
            pocisk.Left = gracz.Left + gracz.Width / 2;
            pocisk.Top = gracz.Top - 20;
            this.Controls.Add(pocisk);
            pocisk.BringToFront();
        }

        


        private void koniecgry()
        {
            timer1.Stop();
            
        }
    }
}
