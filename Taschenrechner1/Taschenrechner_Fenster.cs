﻿using System;
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
        private const string V = "";
        decimal[] zahl = new decimal[999];
        int counter;
        int treffer;
        bool zugewiesen;
        string Zeichen = "";
        string Endergebnis;
        decimal Zwischenergebnis = 0;
        bool Funktionausführung = false;
        bool Ergebnisset = false;

        public void ClearA(bool hardReset = false)
        {
            if(hardReset == true)
            {
                Ergebnisset = false;
                txtAnzeige.Text = V;
                Setzeleertext();
                counter = 0;
            }
            Zwischenergebnis = 0;
        }
        public void rechnen(int c)
        {
            if (counter > 1)
            {
                string zahl_counterString = zahl[counter].ToString();
                int länge_Zahl_Counter = zahl_counterString.Length;
                string switchProof = txtAnzeige.Text.Substring(txtAnzeige.Text.Length - länge_Zahl_Counter - 2, 1);

                switch (switchProof)
                {
                    case "+":
                        if (counter == 2)
                        {
                            Zwischenergebnis = Zwischenergebnis + zahl[counter - 2] + zahl[counter - 1];
                        }
                        else if (counter > 2)
                        {
                            Zwischenergebnis = Zwischenergebnis + zahl[counter - 1];
                        }
                        break;
                    case "-":
                        if (counter == 2)
                        {
                            Zwischenergebnis = Zwischenergebnis + zahl[counter - 2] - zahl[counter - 1];
                        }
                        else if (counter > 2)
                        {
                            Zwischenergebnis = Zwischenergebnis - zahl[counter - 1];
                        }
                        break;

                }
            }
        }

        public int Find_ZE(int start, string[] ZE)
        {
            for (int i = start + 1; i <= ZE.Length - 1; i++)
            {

                if (ZE[i] != "")
                {
                    return i;
                }
            }

            return start + 1;
        }

        public void Structure_set()
        {
            txtAnzeige.Text = txtAnzeige.Text.Replace(".", ",");
           
            txtAnzeige.Text = txtAnzeige.Text.Replace(" ", "");
            txtAnzeige.Text = txtAnzeige.Text.Replace("+", " + ");
            txtAnzeige.Text = txtAnzeige.Text.Replace("-", " - ");
            txtAnzeige.Text = txtAnzeige.Text.Replace("*", " * ");
            txtAnzeige.Text = txtAnzeige.Text.Replace("/", " / ");
            for (int i = 0; i < txtAnzeige.Text.Length; i++)
            {
                if (txtAnzeige.Text.Substring(i, 1) == ",")
                {
                    for (int i2 = i + 1; i2
                        < txtAnzeige.Text.Length - Finde_Zeichen(" ", false, i2); i2++)
                    {
                        if (txtAnzeige.Text.Substring(i2, 1) == ",")
                        {
                            string txtAnzeigesave = txtAnzeige.Text.Substring(Finde_Zeichen(" ", true) + 1);
                            string txtAnzeigecopy = txtAnzeige.Text.Substring(i2, txtAnzeige.Text.Length - i2);
                            int txtAnzeigecopy_Length = txtAnzeigecopy.Length;
                            if (Finde_Zeichen("+", false, i2) != 0 || Finde_Zeichen("-", false, i2) != 0 || Finde_Zeichen("*", false, i2) != 0 || Finde_Zeichen("/", false, i2) != 0)
                            {
                                txtAnzeigecopy = txtAnzeigecopy.Remove(Finde_Zeichen(" ", true) - 1);
                            }
                            txtAnzeigecopy = txtAnzeigecopy.Replace(",", "");
                            if (Finde_Zeichen("+", false, i2) != 0 || Finde_Zeichen("-", false, i2) != 0 || Finde_Zeichen("*", false, i2) != 0 || Finde_Zeichen("/", false, i2) != 0)
                            {
                                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - txtAnzeigecopy_Length) + txtAnzeigecopy + txtAnzeigesave;
                            }
                            else
                            {
                                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - txtAnzeigecopy_Length) + txtAnzeigecopy;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public decimal Rechnen_n()
        {
            int Find_Ergebnis;
            Structure_set();
            string[] Nums_Ops = txtAnzeige.Text.Split(' ');
            string[] ZE = txtAnzeige.Text.Split(' ');
            for (int i = 0; i <= ZE.Length - 1; i++)
            {
                
                switch (Nums_Ops[i])
                {
                    case "*":
                        ZE[i + 1] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) * Convert.ToDecimal(ZE[i + 1]));
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        break;

                    case "/":
                        ZE[i + 1] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) / Convert.ToDecimal(ZE[i + 1]));
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        break;


                }

            }
            for (int i = 0; i <= ZE.Length - 1; i++)
            {
                switch (Nums_Ops[i])
                {
                    case "+":
                        Find_Ergebnis = Find_ZE(i, ZE);
                        ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) + Convert.ToDecimal(ZE[Find_Ergebnis]));
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        
                        break;

                    case "-":
                        Find_Ergebnis = Find_ZE(i, ZE);
                        ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) - Convert.ToDecimal(ZE[Find_Ergebnis]));
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        break;
                }

            }
            for (int i = ZE.Length - 1; i > 0; i--)
            {
                if(ZE[i] != "")
                {
                    return Convert.ToDecimal(ZE[i]);
                }
            }
            return 0;
        }
        public void addnum(string wert, bool Leerzeichen = false, bool lastNum = false)
        {
            btn_istgleich.Visible = true;
            Zeichen = "";

            if (Leerzeichen)
            {
                Zeichen = " ";
                btn_istgleich.Visible = false;

                switch (counter)
                {
                    case 0:
                        zugewiesen = true;
                        break;
                    default:
                        {

                            for (int i = txtAnzeige.Text.Length; i >= 0; i--)
                            {
                                if (txtAnzeige.Text.Substring(i-1, 1) == " ")
                                {
                                    treffer = i;
                                    break;
                                }
                            }

                            if(txtAnzeige.Text.Length - treffer > 0)
                            {
                                
                                zugewiesen = true;
                            }
                        }
                        break;
                }

                
                
               
                if (zugewiesen == true)
                {
                    counter++;
                    zugewiesen = false;
                }

                

            }

            if (txtAnzeige.Text.Length > 0)
            {
                if (txtAnzeige.Text.Substring(0,1)=="B")
                {
                    txtAnzeige.Text = "";
                }
            }

            if (Leerzeichen == true && txtAnzeige.Text.Substring(txtAnzeige.Text.Length-1, 1)==" ")
            {
                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - 3);
            } else if(lastNum == false)
            {
                txtAnzeige.Text = txtAnzeige.Text + Zeichen + wert + Zeichen;
            }
        }


        public void Setzeleertext()
        {
            txtAnzeige.Text = "Bitte geben sie Zahlen und Rechenoperationen ein!";
            Funktionausführung = true;
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
            addnum("+", true);
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            addnum("-", true);

        }

        private void btn_mal_Click(object sender, EventArgs e)
        {
            addnum("*", true);

        }

        private void btn_durch_Click(object sender, EventArgs e)
        {
            addnum("/", true);

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (txtAnzeige.Text.Length >0 && txtAnzeige.Text.Substring(txtAnzeige.Text.Length-1,1)==" ")
            {
                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - 3);

            }
            else if (txtAnzeige.Text.Length > 0)
            {

                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - 1);
            }

            if (txtAnzeige.Text.Length == 0)
            {
                Setzeleertext();
                counter = 0;
            }

        }

        private void btn_clearall_Click(object sender, EventArgs e)
        {
            ClearA(true);
        }

        public int Finde_Zeichen(string Zeichen, bool specialsearch = false, int startpoint = 0)
        {
            if(specialsearch == false)
            {
                for (int i = startpoint; i < txtAnzeige.Text.Length; i++)
                {
                    if (txtAnzeige.Text.Substring(i, 1) == Zeichen)
                    {
                        return i;
                    }
                }
            } else if(specialsearch == true)
            {
                for (int i = startpoint; i < txtAnzeige.Text.Length; i++)
                {
                    if (txtAnzeige.Text.Substring(i, 1) == Zeichen)
                    {
                        for(int i2 = i + 1; i < txtAnzeige.Text.Length; i2++)
                        {
                            if (txtAnzeige.Text.Substring(i2, 1) == Zeichen)
                            {
                                return i2;
                            }
                        }
                    }
                }
            }
            
            return 0;
        }

        private void txtAnzeige_TextChanged(object sender, EventArgs e)
        {
            if(Ergebnisset == true)
            {
                Ergebnisset = false;
                int istgleichichspot = Finde_Zeichen("=") + 1;
                txtAnzeige.Text = txtAnzeige.Text.Substring(istgleichichspot, txtAnzeige.Text.Length - istgleichichspot);
            }
        }

        private void txtAnzeige_Click(object sender, EventArgs e)
        {
            if(txtAnzeige.Text == "Bitte geben sie Zahlen und Rechenoperationen ein!")
            {
                txtAnzeige.Text = "";
            }
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            addnum("0");
        }

        private void btn_000_Click(object sender, EventArgs e)
        {
            addnum("000");
        }

        private void btn_komma_Click(object sender, EventArgs e)
        {
            addnum(",");
        }

        private void btn_istgleich_Click(object sender, EventArgs e)
        {
            Endergebnis = Zwischenergebnis.ToString();
            txtAnzeige.Text = txtAnzeige.Text + " = " + Rechnen_n().ToString();
            ClearA();
            Ergebnisset = true;

        }

        private void btn_00_Click(object sender, EventArgs e)
        {
            addnum("00");
        }
    }
}
