using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taschenrechner1
{
    public partial class Taschenrechner_Fenster : Form
    {

        decimal[] zahl = new decimal[999];
        int counter;
        int treffer;

        public void addnum(string wert, int Leerzeichen=0)
        {
            string Zeichen="";
            if(Leerzeichen==1)
            {
                Zeichen = " ";

                if (counter == 0)
                {
                    zahl[counter] = Convert.ToDecimal(txtAnzeige.Text);
                } else
                {

                for(int i = txtAnzeige.Text.Length;i>= 0; i--)
                {
                        if (txtAnzeige.Text.Substring(i, 1) == " ")
                        {
                            treffer = i;
                            break;
                        }
                }

                    zahl[counter] = Convert.ToDecimal(txtAnzeige.Text.Substring(treffer + 1, txtAnzeige.Text.Length - treffer));

                }
                counter++;


            }

            if(txtAnzeige.Text.Length>0)
            {
                if (txtAnzeige.Text.Substring(0,1)=="B")
                {
                    txtAnzeige.Text = "";
                }
            }

            txtAnzeige.Text = txtAnzeige.Text + Zeichen + wert + Zeichen;
        }


        public void Setzeleertext()
        {
            txtAnzeige.Text = "Bitte geben sie Zahlen und Rechenoperationen ein!";
        }


        public Taschenrechner_Fenster()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Setzeleertext();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            addnum("1");
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            addnum("2");

        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            addnum("3");

        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            addnum("4");

        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            addnum("5");

        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            addnum("6");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            addnum("7");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            addnum("8");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            addnum("9");

        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            addnum("+", 1);
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            addnum("-", 1);

        }

        private void btn_mal_Click(object sender, EventArgs e)
        {
            addnum("*", 1);

        }

        private void btn_durch_Click(object sender, EventArgs e)
        {
            addnum("/", 1);

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if(txtAnzeige.Text.Length>0 && txtAnzeige.Text.Substring(txtAnzeige.Text.Length-1,1)==" ")
            {
                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - 3);

            } else
            {
                if(txtAnzeige.Text.Length > 0)
                {

                    txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - 1);
                }

            }

            if (txtAnzeige.Text.Length == 0)
            {
                Setzeleertext();
            }

        }

    private void btn_clearall_Click(object sender, EventArgs e)
        {
            txtAnzeige.Text = "";
            Setzeleertext();
        }
    }
}
