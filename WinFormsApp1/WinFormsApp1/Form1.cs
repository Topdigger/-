using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using System;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public string[] Chips;
        public string ch;
        public string mas;
        public int sum;
        public int k;
        public int prom;
        public string k1;
        public bool prover;
        public int max;
        public int maxi;
        public int len;
        public int flag;
        public int flagres;
        public bool all;
        public Form1()
            {
                InitializeComponent();
            }
        public void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            int a = new int();
            a = ((int)numericUpDown1.Value);
            int[] Chips = new int[a];
            len = Chips.Length;
            flagres = 0;
            all = false;
            for (int i = 0; i < len; i++)
            {
                string table = Microsoft.VisualBasic.Interaction.InputBox("¬ведите количество фишек на " + (i + 1) + " столе:");
                int table1 = Int32.Parse(table);
                Chips.SetValue(table1, i);
                int fis = int.Parse(table);
                sum = sum + fis;
            }
            for (int i = 0; i < Chips.Length; i++)
            {
                textBox2.Text += Chips.GetValue(i);
            }

            do
            {

                k = sum / Chips.Length;
                max = (int)Chips.GetValue(0);
                for (int b = 0; b < (Chips.Length - 1); b++)
                {
                    maxi = (int)Chips.GetValue(b + 1);
                    if (maxi > max)
                    {
                        max = (int)Chips.GetValue((b + 1));
                        flag = b + 1;

                    }
                    else
                    {
                        max = max;
                    }

                }
                int men = flag - 1;
                if (men < 0)
                {
                    men = a - 1;
                }
                else
                {
                    men = men;
                }
                int bol = flag + 1;
                if (bol >= a)
                {
                    bol = 0;
                }
                else
                {
                    bol = bol;
                }
                if ((int)Chips.GetValue(men) < (int)Chips.GetValue(bol))
                {
                    int norm = (int)Chips.GetValue(flag);
                    while (norm > k)
                    {
                        int per = (int)Chips.GetValue(flag);
                        per--;
                        int per2 = (int)Chips.GetValue(men);
                        per2++;

                        Chips.SetValue(per2, men);
                        Chips.SetValue(per, flag);
                        norm--;
                        flagres++;
                    }

                }
                else
                {
                    if ((int)Chips.GetValue(men) > (int)Chips.GetValue(bol))
                    {
                        int norm = (int)Chips.GetValue(flag);
                        while (norm > k)
                        {
                            int per = (int)Chips.GetValue(flag);
                            per--;
                            int per2 = (int)Chips.GetValue(bol);
                            per2++;

                            Chips.SetValue(per2, bol);
                            Chips.SetValue(per, flag);
                            norm--;
                            flagres++;
                        }
                    }
                }
                all = Chips.All(x => x == k);
            }
            while (all != true);

            for (int i = 0; i < Chips.Length; i++)
                {
                    textBox1.Text += Chips.GetValue(i);
                }

            flag++;
            textBox4.Text = flagres.ToString();
        }
        }


    }

