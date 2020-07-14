using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        int Vorzeichen_Minus = 0;

        public void ClearA(bool hardReset = false)
        {
            if(hardReset == true)
            {
                Ergebnisset = false;
                txtAnzeige.Text = V;
                Setzeleertext();
                counter = 0;
                txtInfoKlammern.Text = "";
                txtInfoKlammern.Visible = false;
            }
            Zwischenergebnis = 0;
            webTRGoogle.Visible = false;                                                                                                        //Webseite abschalten
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
                Vorzeichen_Minus = i;
                if (ZE[i] == "-")
                {
                    return i + 1;
                } else if(ZE[i] != "")
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
            txtAnzeige.Text = txtAnzeige.Text.Replace("^", " ^ ");
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


        public string Structure_set_mitKlammern(string Textzumbearbeiten)                                                               // Gleiche Funktion wie "Structure_set" nur mit Parameter
        {                                                                                                                               // Umbau zu der Funktion ist sinnvoll.
            Textzumbearbeiten = Textzumbearbeiten.Replace(".", ",");
            Textzumbearbeiten = Textzumbearbeiten.Replace(" ", "");
            Textzumbearbeiten = Textzumbearbeiten.Replace("+", " + ");
            Textzumbearbeiten = Textzumbearbeiten.Replace("-", " - ");
            Textzumbearbeiten = Textzumbearbeiten.Replace("*", " * ");
            Textzumbearbeiten = Textzumbearbeiten.Replace("/", " / ");
            Textzumbearbeiten = Textzumbearbeiten.Replace("^", " ^ ");
            for (int i = 0; i < Textzumbearbeiten.Length; i++)
            {
                if (Textzumbearbeiten.Substring(i, 1) == ",")
                {
                    for (int i2 = i + 1; i2
                        < Textzumbearbeiten.Length - Finde_Zeichen(" ", false, i2); i2++)
                    {
                        if (Textzumbearbeiten.Substring(i2, 1) == ",")
                        {
                            string txtAnzeigesave = Textzumbearbeiten.Substring(Finde_Zeichen(" ", true) + 1);
                            string txtAnzeigecopy = Textzumbearbeiten.Substring(i2, Textzumbearbeiten.Length - i2);
                            int txtAnzeigecopy_Length = txtAnzeigecopy.Length;
                            if (Finde_Zeichen("+", false, i2) != 0 || Finde_Zeichen("-", false, i2) != 0 || Finde_Zeichen("*", false, i2) != 0 || Finde_Zeichen("/", false, i2) != 0)
                            {
                                txtAnzeigecopy = txtAnzeigecopy.Remove(Finde_Zeichen(" ", true) - 1);
                            }
                            txtAnzeigecopy = txtAnzeigecopy.Replace(",", "");
                            if (Finde_Zeichen("+", false, i2) != 0 || Finde_Zeichen("-", false, i2) != 0 || Finde_Zeichen("*", false, i2) != 0 || Finde_Zeichen("/", false, i2) != 0)
                            {
                                Textzumbearbeiten = Textzumbearbeiten.Substring(0, Textzumbearbeiten.Length - txtAnzeigecopy_Length) + txtAnzeigecopy + txtAnzeigesave;
                            }
                            else
                            {
                                Textzumbearbeiten = Textzumbearbeiten.Substring(0, Textzumbearbeiten.Length - txtAnzeigecopy_Length) + txtAnzeigecopy;
                            }
                            break;
                        }
                    }
                }
            }

            return Textzumbearbeiten;
        }


        public decimal Rechnen_n()                                                                                                                  // Empfehlung Umbau zu "Rechnen_nmitKlammern" klingt auch gut.
        {
            int Find_Ergebnis;
            Structure_set();
            string[] Nums_Ops = txtAnzeige.Text.Split(' ');
            string[] ZE = txtAnzeige.Text.Split(' ');
            for (int i = 0; i <= ZE.Length - 1; i++)
            {
                switch (Nums_Ops[i])
                {
                    case "^":
                          
                        decimal Zwischenergebnis2 = 0;
                        for (int e = 1; e < Convert.ToDecimal(ZE[i + 1]); e++)
                        {
                            if(e == 1)
                            {
                                Zwischenergebnis2 = Convert.ToDecimal(ZE[i - 1]) * Convert.ToDecimal(ZE[i - 1]);
                            } else
                            {
                                Zwischenergebnis2 = Zwischenergebnis2 * Convert.ToDecimal(ZE[i - 1]);
                            }
                        }
                        ZE[i + 1] = Convert.ToString(Zwischenergebnis2);
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        break;
                }
            }
            for (int i = 0; i <= ZE.Length - 1; i++)
            {
                switch (Nums_Ops[i])
                {
                    case "*":
                        Find_Ergebnis = Find_ZE(i, ZE);
                        if (Find_Ergebnis > Vorzeichen_Minus)
                        {
                            ZE[Find_Ergebnis - 1] = "";
                            string v = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) * Convert.ToDecimal(value: "-" + ZE[Find_Ergebnis]));
                            ZE[Find_Ergebnis] = v;
                        }
                        else
                        {
                            ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) * Convert.ToDecimal(ZE[Find_Ergebnis]));
                        }
                       
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        break;

                    case "/":

                        Find_Ergebnis = Find_ZE(i, ZE);
                        if (Find_Ergebnis > Vorzeichen_Minus)
                        {
                            ZE[Find_Ergebnis - 1] = "";
                            string v = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) / Convert.ToDecimal(value: "-" + ZE[Find_Ergebnis]));
                            ZE[Find_Ergebnis] = v;
                        }
                        else
                        {
                            ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) / Convert.ToDecimal(ZE[Find_Ergebnis]));
                        }
                        
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
                        if(Find_Ergebnis > Vorzeichen_Minus)
                        {
                            ZE[Find_Ergebnis - 1] = "";
                            string v = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) + Convert.ToDecimal(value: "-" + ZE[Find_Ergebnis]));
                            ZE[Find_Ergebnis] = v;
                        }
                        else
                        {
                            ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) + Convert.ToDecimal(ZE[Find_Ergebnis]));
                        }
                        
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        
                        break;

                    case "-":
                        Find_Ergebnis = Find_ZE(i, ZE);
                        if(ZE[i - 1] == "")
                        {
                            ZE[Find_Ergebnis] = ZE[i] + ZE[Find_Ergebnis];
                        }
                        else
                        {
                            Find_Ergebnis = Find_ZE(i, ZE);
                            if (Find_Ergebnis > Vorzeichen_Minus)
                            {
                                ZE[Find_Ergebnis - 1] = "";
                                string v = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) - Convert.ToDecimal(value: "-" + ZE[Find_Ergebnis]));
                                ZE[Find_Ergebnis] = v;
                            }
                            else
                            {
                                ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) - Convert.ToDecimal(ZE[Find_Ergebnis]));
                            }
                            
                            ZE[i] = "";
                            ZE[i - 1] = "";
                        }
                        
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


        public decimal Rechnen_nmitKlammern(string Textzumberechnen)                                                        // Neue Funkion, eigentlich eine Kopie von "Rechnen_n" nur mit Parameter
        {                                                                                                                   // Es wurde nur txtAnzeige.Text durch Textzumberechnen ersetzt und Variablen eingefügt
            int Find_Ergebnis;
            Textzumberechnen= Structure_set_mitKlammern(Textzumberechnen);                                                  // Neue Mainrechenfunktion ist sinnvoll.
            string[] Nums_Ops = Textzumberechnen.Split(' ');
            string[] ZE = Textzumberechnen.Split(' ');

            for (int i = 0; i <= ZE.Length - 1; i++)
            {
                    switch (Nums_Ops[i])
                    {
                        case "^":                                                                                           // Das reine " Karret " erzeugte Fehler auch in Rechnen_n, jetzt aber auch noch!!

                            decimal Zwischenergebnis2 = 0;
                            for (int e = 1; e < Convert.ToDecimal(ZE[i + 1]); e++)
                            {
                                if (e == 1)
                                {
                                    Zwischenergebnis2 = Convert.ToDecimal(ZE[i - 1]) * Convert.ToDecimal(ZE[i - 1]);
                                }
                                else
                                {
                                    Zwischenergebnis2 = Zwischenergebnis2 * Convert.ToDecimal(ZE[i - 1]);
                                }
                            }
                            ZE[i + 1] = Convert.ToString(Zwischenergebnis2);
                            ZE[i] = "";
                            ZE[i - 1] = "";
                            break;
                    }
            }
            for (int i = 0; i <= ZE.Length - 1; i++)
            {
                switch (Nums_Ops[i])
                {
                    case "*":
                        Find_Ergebnis = Find_ZE(i, ZE);
                        ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) * Convert.ToDecimal(ZE[Find_Ergebnis]));
                        ZE[i] = "";
                        ZE[i - 1] = "";
                        break;

                    case "/":
                        Find_Ergebnis = Find_ZE(i, ZE);
                        ZE[Find_Ergebnis] = Convert.ToString(Convert.ToDecimal(ZE[i - 1]) / Convert.ToDecimal(ZE[Find_Ergebnis]));
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
            for (int i = ZE.Length - 1; i > 0; i--)                                                                                                 // Achtung Fehler bei nur einem Zeichen?
            {                                                                                                                                       // Zeichenlänge 1 dann ZE[] = "" ToDecimal schlägt fehl! Prüfen - Darwin?
                if (ZE[i] != "")
                {
                    return Convert.ToDecimal(ZE[i]);                                                                                                
                }                                                                                                                                   
            }
            return 0;
        }





        public void addnum(string wert, bool Leerzeichen = false, bool lastNum = false, bool Vorzeichen = false, bool firstNum = false)
        {
            btn_istgleich.Visible = true;
            btn_durch.Visible = true;
            btn_plus.Visible = true;
            btn_minus.Visible = true;
            btn_mal.Visible = true;
            btn_hoch.Visible = true;
            Zeichen = "";

            if (Vorzeichen)
            {
                Leerzeichen = false;
            }


            if (Leerzeichen)
            {
                Zeichen = " ";
                btn_istgleich.Visible = false;
                btn_durch.Visible = false;
                btn_plus.Visible = false;
                btn_mal.Visible = false;
                btn_hoch.Visible = false;

                switch (counter)
                {
                    case 0:
                        zugewiesen = true;
                        break;
                    default:
                        {

                            for (int i = txtAnzeige.Text.Length; i >= 0; i--)
                            {
                                if (txtAnzeige.Text.Substring(i-1, 1) == " ")  //Das verstehe ich nicht.. wenn Länge = 1 dann Error?
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
            if (lastNum == false || firstNum)
            {
                txtAnzeige.Text = txtAnzeige.Text + Zeichen + wert + Zeichen;
            }
            else if (Leerzeichen == true && txtAnzeige.Text.Substring(txtAnzeige.Text.Length-1, 1)==" ")
            {
                txtAnzeige.Text = txtAnzeige.Text.Substring(0, txtAnzeige.Text.Length - 3);
            } 

            SetzeInfoKlammernText();


        }

        private void SetzeInfoKlammernText()                                                                                                    // Neue Funktion zum befüllen des Info Textes Klammernprüfung (nur fürs Auge)
        {                                                                                                                                       // Eigentlich nur die Prüfung sind genug Klammern da, Google hat das besser gelöst
            int AnzahlKlammerauf=0, AnzahlKlammerzu=0;
            
            txtInfoKlammern.Visible = false;                                                                                                    // Textfeld ausblenden und leeren
            txtInfoKlammern.Text = "";

            txtAnzeige.Text = txtAnzeige.Text.Replace("()", "");                                                                                // Hier schon einmal (völlig falsche Stelle) "()" ersetzen durch "" - woanders hin=? JA

            if (txtAnzeige.Text.Length >0)
            {
                txtInfoKlammern.Visible = (txtAnzeige.Text.IndexOf("(") > -1 || txtAnzeige.Text.IndexOf(")") > -1);                             // Textfeld sichtbar machen, wenn "(" oder ")" vorhanden ist.
            }

            if (!(txtInfoKlammern.Visible = true))                                                                                              // Prüfung ist sichtbar?
            {
            }
            else
            {
                AnzahlKlammerauf = txtAnzeige.Text.Length - txtAnzeige.Text.Replace("(", "").Length;                                            // Anzahl Klammern errechnen "(" - könnte anders gelöst werden
                AnzahlKlammerzu = txtAnzeige.Text.Length - txtAnzeige.Text.Replace(")", "").Length;                                             // Anzahl Klammern errechnen ")"

                txtInfoKlammern.Text = "Es gibt " + Convert.ToString(AnzahlKlammerauf) + "x ( und " + Convert.ToString(AnzahlKlammerzu) + "x )";// Text ausgeben, den man möchte
            }


            if (AnzahlKlammerauf==AnzahlKlammerzu)                                                                                              // Ist das Klammerpaar OK?
            {
                txtInfoKlammern.BackColor = Color.FromKnownColor(KnownColor.GreenYellow);                                                       // Dann mach grün
            } else
            {
                txtInfoKlammern.BackColor = Color.FromKnownColor(KnownColor.Red);                                                               // oder Rot
            }

            txtInfoKlammern.Visible = (AnzahlKlammerauf > 0);                                                                                   // Info Text nur zeigen, wenn "(" da ist -- kann man besser machen 
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
            btn_istgleich.Visible = false;
            btn_durch.Visible = false;
            btn_plus.Visible = false;
            btn_minus.Visible = true;
            btn_mal.Visible = false;
            btn_hoch.Visible = false;
            Setzeleertext();
            webTRGoogle.Navigate("https://www.google.com/search?q=taschenrechner+google&oq=taschenrechner+google&aqs=chrome..69i57j0l7.2968j0j4&sourceid=chrome&ie=UTF-8");

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
            bool VZeichen_Set = false;

            if (txtAnzeige.Text == "" || txtAnzeige.Text.Substring(txtAnzeige.Text.Length - 1, 1) == " " || txtAnzeige.Text.Substring(txtAnzeige.Text.Length - 1, 1) == "+" || txtAnzeige.Text.Substring(txtAnzeige.Text.Length - 1, 1) == "-" || txtAnzeige.Text.Substring(txtAnzeige.Text.Length - 1, 1) == "*" || txtAnzeige.Text.Substring(txtAnzeige.Text.Length - 1, 1) == "/" || txtAnzeige.Text.Substring(txtAnzeige.Text.Length - 1, 1) == "^")
            {
                VZeichen_Set = true;
            }

            if (VZeichen_Set)
            {
                addnum("-", false, false, VZeichen_Set, true);
            }
            else
            {
                addnum("-", true, false, VZeichen_Set, true);
            }
           

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
                webTRGoogle.Visible = false;
            }
            
        }

        private void txtAnzeige_Click(object sender, EventArgs e)
        {
            webTRGoogle.Visible = false;

            if (txtAnzeige.Text == "Bitte geben sie Zahlen und Rechenoperationen ein!")
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

            int first=0, last;                                                                                                                      // Ab hier eingefügt für Klammer Rechnung
            string Teilstring;                                                                                                                      // Laufzeitvariablen Start und Endpunkt der innersten Klammer berechnen.
            decimal Ergebnis;                                                                                                                       // Das Ergebnis als Debug Var
            string Textmerken;                                                                                                                      // Die Debug Var kann später weg
            string WebSuchURL=txtAnzeige.Text;                                                                                                      // String für Websuche

            if (txtAnzeige.Text.IndexOf("(") > 0 || txtAnzeige.Text.IndexOf(")") > 0)                                                               // Ist ein "(" oder ")" im Original Text
            {
                //txtAnzeige.Text= txtAnzeige.Text + "   Hier ist etwas anders zu machen!";
                                                                                                                                                     // Dann beginne
                do
                {
                    last = txtAnzeige.Text.IndexOf(")");                                                                                            // Ermittle das ersteVorkommen von ")"
                    for (int i = last; i>= 0; i--)                                                                                                  // Suche von da Rückwärts die erste "("
                    {
                        if (txtAnzeige.Text.Substring(i, 1) == "(")
                        {
                            first = i;                                                                                                              // Treffer
                            break;
                        }
                    }

                    Teilstring = txtAnzeige.Text.Substring(first+1, last - first-1);                                                                // Ermittle den String zwischen den beiden innersten Klammern

                    Ergebnis = Rechnen_nmitKlammern(Teilstring);                                                                                    // Errechne das Ergebnis aus Teilstring
                                                                                                                                                    // Achtung 2+3+(8*9) ist OK ((2+3) + (7*8) / (9/9) auch OK?

                    Textmerken = txtAnzeige.Text;                                                                                                   // Nur zu Debug Zwecken
                    txtAnzeige.Text = txtAnzeige.Text.Substring(0,first - 1) + Convert.ToString(Ergebnis) + txtAnzeige.Text.Substring(last + 1);    // Original String manipulieren


                } while (txtAnzeige.Text.IndexOf("(") > 0);                                                                                         // und von vorne, bis keine Klammer mehr da ist!

                txtInfoKlammern.Visible = false;                                                                                                    // Info Feld aus.... da in Changed was passiert? Was?

                txtAnzeige.Text = txtAnzeige.Text + " = " + Rechnen_n().ToString();                                                                 // und den letzten String (also 5+56/1) errechnen...
                                                                                                                                                    // Achtung die Funktion "Rechnen_n" wird gebraucht, bis Umbau wie Rechnen_mitKlammern

            }
            else
            {
                txtAnzeige.Text = txtAnzeige.Text + " = " + Rechnen_n().ToString();                                                                 // Normale Berechnung...
            }

            ClearA();
            Ergebnisset = true;

            webTRGoogle.Visible = true;
            webTRGoogle.Navigate("https://www.google.com/search?q=taschenrechner+" + WebSuchURL.Replace("+", "%2B").Replace(" ", " +").Replace("/", "%2F"));
                
        }

        private void btn_00_Click(object sender, EventArgs e)
        {
            addnum("00");
        }

        private void btn_klammerauf_Click(object sender, EventArgs e)
        {
            addnum("(");
        }

        private void btn_klammerzu_Click(object sender, EventArgs e)
        {
            addnum(")");
        }

        private void btn_wurzel_Click(object sender, EventArgs e)
        {
            addnum("");
        }

        private void btn_hoch_Click(object sender, EventArgs e)
        {
            addnum(" ^ ");
        }

        private void webTRGoogle_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

    }
}
