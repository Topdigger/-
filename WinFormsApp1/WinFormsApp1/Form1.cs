using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using System;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public string[] Chips;//массив фишек
        public int sum;//сумма элементов массива
        public int k;//количество фишек, которое должно быть на каждом месте
        public int bol;
        public int max;
        public int men;
        public int flag;//выбранный элемент массива
        public int flagres;//переменная для хранения количества шагов
        public bool all;//принимает true, если все элементы массива равны
        public int se;
        public int rast;//хранит расстояние для суммирования элементов справа и слева от текущего элемента 
        public int len;//хранит длину массива
        public Form1()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            int a;
            a = ((int)numericUpDown1.Value);
            int[] Chips = new int[a];
            len = Chips.Length;
            flagres = 0;
            all = false;
            sum = 0;
            string table = textBox3.Text;
            Chips = table.Split(",").Select(int.Parse).ToArray();
                for (int i = 0; i < Chips.Length; i++)
                    {  
                        sum += Chips[i];
                    }
                for (int i = 0; i < Chips.Length; i++)
                    {
                        textBox2.Text += Chips.GetValue(i) + " ";
                    }
            max = Chips[0];
            k = sum / Chips.Length;
                if (a % 2 == 1)
                    {
                        rast = (a - 1)/2;
                    }
                else 
                    {
                        rast = (a - 2)/2;   
                    }
            do
               {
                flag = -1;
                se = 0;
                int summen = 0;//сумма элементов слева
                int sumbol = 0;//сумма элементов справа

                do//выбор первого значения >k из массива 
                    {
                        int per = Chips[se];
                        if (per > k)
                            {
                                flag = se;
                                break;
                            }
                        else
                            {
                                se++;
                            }
                    }
                    while (flag < 0); 
                        for (int i = 1; i <= rast; i++)
                            {
                                if (flag - i < 0)
                                    {
                                        summen += Chips[a - i +1];
                                    }
                                else
                                    {
                                        summen += Chips[flag - i];
                                    }
                            }
                        for (int i = 1; i <= rast; i++)
                            {
                                if (flag + i > (a-1))
                                    {
                                        sumbol += Chips[-1+((flag+i)-(a-1))];
                                    }
                                else
                                    {
                                        sumbol += Chips[flag + i];
                                    }
                            }
                if (sumbol == summen)
                {
                    if (flag + 1 < a & flag - 1 >= 0)
                    {
                        if (Chips[flag - 1] > Chips[flag + 1])
                        {
                            Chips[flag]--;
                            Chips[flag + 1]++;
                            flagres++;
                        }
                        else
                        {
                            Chips[flag]--;
                            Chips[flag - 1]++;
                            flagres++;
                        }
                    }
                    else
                    {
                        if (flag - 1 < 0)
                        {
                            men = a - 1;
                        }
                        else
                        {
                            men = flag - 1;
                        }
                        if (flag + 1 >= a)
                        {
                            bol = 0;
                        }
                        else
                        {
                            bol = flag + 1;
                        }
                        if (Chips[men] < Chips[bol])
                        {
                            Chips[flag]--;
                            Chips[men]++;
                            flagres++;
                        }
                        else
                        {
                            Chips[flag]--;
                            Chips[bol]++;
                            flagres++;
                        }
                    }
                }
                else
                {

                    if (sumbol > summen)
                    {
                        if (flag - 1 >= 0)
                        {
                            Chips[flag]--;
                            Chips[flag - 1]++;
                            flagres++;
                        }
                        else
                        {
                            Chips[flag]--;
                            Chips[a - 1]++;
                            flagres++;
                        }
                    }
                    else
                    {
                        if (flag + 1 <= a - 1)
                        {
                            Chips[flag]--;
                            Chips[flag + 1]++;
                            flagres++;
                        }
                        else
                        {
                            Chips[flag]--;
                            Chips[0]++;
                            flagres++;
                        }
                    }
                }
                 all = Chips.All(x => x == k);
                 se = 0;
                 flag = -1;
                
                 }
                 while (all != true);

                for (int i = 0; i < Chips.Length; i++)//вывод получившегося массива
                    {
                        textBox1.Text += Chips.GetValue(i) + " ";
                    }

               
                textBox4.Text = flagres.ToString();//вывод количества шагов
            
        }

    }
}

