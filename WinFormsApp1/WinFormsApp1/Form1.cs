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
        public decimal rast;//хранит расстояние для суммирования элементов справа и слева от текущего элемента 
        public int len;//хранит длину массива
        public int sc1;
        public int sc2;
        public int sch;
        public Form1()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";//обнуление текстбоксов для новых значений
            int a;
            a = (int)numericUpDown1.Value;//переменная хранит в себе количество столов
            int[] Chips = new int[a];//массив столов
            len = Chips.Length;//длинна массива
            flagres = 0;//переменная хранит количество перемещений
            all = false;//переменная-условие для основного цикла 
            sum = 0;//сумма элементов массива
            string table = textBox3.Text;//текстбокс для ввода фишек
            Chips = table.Split(",").Select(int.Parse).ToArray();
            if (Chips.Length != a)
                {
                    MessageBox.Show("Количество введенных элементов не соответствует указанному количеству столов");
                }
            else
                {
                    for (int i = 0; i < Chips.Length; i++)
                        {
                            sum += Chips[i];//подсчёт суммы элементов
                        }
                    for (int i = 0; i < Chips.Length; i++)
                        {
                            textBox2.Text += Chips.GetValue(i) + " ";//вывод начального массива
                        }
                    max = Chips[0];//начало поиска первого элемента массива,с которого начнутся перемещения
                    k = sum / Chips.Length;//значение фишек, которое должно быть на каждом столе
                    if (a % 2 == 1)
                        {
                            rast = (a - 1) / 2;//расстояние - количество столов для рассчета справа и слева от рассматриваемого элемента
                        }
                    else
                        {
                            rast = (a - 2) / 2;
                        }
                    do//начало основного цикла
                    {
                        flag = -1;//переменная для хранения индекса текущего анализируемого элемента
                        se = 0;
                        sc1 = 0;
                        sc2 = 0;
                        sch = -1;
                        decimal summen = 0;//сумма элементов слева
                        decimal sumbol = 0;//сумма элементов справа

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
                                    sc1++;
                                    summen += Chips[a - sc1];
                                }
                            else
                                {
                                    summen += Chips[flag - i];
                                }
                        }
                        for (int i = 1; i <= rast; i++)
                        {
                            if (flag + i > (a - 1))
                                {
                                    sc2++;
                                    sumbol += Chips[-1 + sc2];
                                }
                            else
                                {
                                    sumbol += Chips[flag + i];
                                }
                        }
                        //ЗДЕСЬ НАЧАЛО ПРОВЕРКИ НА СУММУ
                        if (sumbol / k == rast)//если все элементы справа уже равны k, то перекидываем налево
                        {
                            if (flag - 1 >= 0)
                            {
                                Chips[flag]--;
                                Chips[flag - 1]++;
                                flagres++;
                                if (Chips[flag - 1] == k + 1)//если фишку перекинули на стол, где уже было k фишек, то переносим её дальше
                                {
                                    flag--;
                                    while (Chips[flag] == k + 1)
                                    {
                                        if (flag - 1 < 0)
                                            {
                                                sch++;
                                                Chips[a - 1 - sch]++;
                                                Chips[flag]--;
                                                flag = a - 1;
                                                flagres++;
                                            }
                                        else
                                            {
                                                Chips[flag]--;
                                                Chips[flag - 1]++;
                                                flag--;
                                                flagres++;
                                            }
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (summen / k == rast)//если все элементы слева уже равны k, то перекидываем направо
                            {
                                if (flag + 1 <= a - 1)
                                {
                                    Chips[flag]--;
                                    Chips[flag + 1]++;
                                    flagres++;
                                    if (Chips[flag + 1] == k + 1)
                                    {
                                        flag++;
                                        do
                                        {
                                            if (flag + 1 >= a)
                                                {
                                                    sch++;
                                                    Chips[sch]++;
                                                    Chips[flag]--;
                                                    flag = 0;
                                                    flagres++;
                                                }
                                            else
                                                {
                                                    Chips[flag]--;
                                                    Chips[flag + 1]++;
                                                    flag++;
                                                    flagres++;
                                                }
                                        }
                                        while (Chips[flag] == k + 1);
                                    }
                                }
                            }
                            else
                        if (sumbol == summen)// если сумма слева равна сумме справа, то выбираем направление движения по соседним элементам
                            {
                                if (flag + 1 < a & flag - 1 >= 0)
                                {
                                    if (Chips[flag - 1] > Chips[flag + 1])
                                    {
                                        Chips[flag]--;
                                        Chips[flag + 1]++;
                                        flagres++;
                                        if (Chips[flag + 1] == k + 1)
                                        {
                                            flag--;
                                            do
                                            {
                                                if (flag + 1 >= a)
                                                {
                                                    sch++;
                                                    Chips[sch]++;
                                                    Chips[flag]--;
                                                    flag = 0;
                                                    flagres++;
                                                }
                                                else
                                                {
                                                    Chips[flag]--;
                                                    Chips[flag + 1]++;
                                                    flag++;
                                                    flagres++;
                                                }
                                            }
                                            while (Chips[flag] == k + 1);
                                        }
                                    }
                                    else
                                    {
                                        Chips[flag]--;
                                        Chips[flag - 1]++;
                                        flagres++;
                                        if (Chips[flag - 1] == k + 1)
                                        {
                                            flag--;
                                            do
                                                {
                                                    if (flag - 1 < 0)
                                                        {
                                                            sch++;
                                                            Chips[a - 1 - sch]++;
                                                            Chips[flag]--;
                                                            flag = a - 1;
                                                            flagres++;
                                                        }
                                                    else
                                                        {
                                                            Chips[flag]--;
                                                            Chips[flag - 1]++;
                                                            flag--;
                                                            flagres++;
                                                        }
                                                }
                                            while (Chips[flag] == k + 1);
                                        }
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

                                if (sumbol > summen)// если сумма справа больше суммы слева, то идём влево
                                    {
                                        if (flag - 1 >= 0)
                                            {
                                                Chips[flag]--;
                                                Chips[flag - 1]++;
                                                flagres++;
                                                if (Chips[flag - 1] == k + 1)
                                                    {
                                                        flag--;
                                                        while (Chips[flag] == k + 1)
                                                            {
                                                                if (flag - 1 < 0)
                                                                    {
                                                                        sch++;
                                                                        Chips[a - 1 - sch]++;
                                                                        Chips[flag]--;
                                                                        flag = a - 1;
                                                                        flagres++;
                                                                    }
                                                                else
                                                                    {
                                                                        Chips[flag]--;
                                                                        Chips[flag - 1]++;
                                                                        flag--;
                                                                        flagres++;
                                                                    }
                                                            }

                                                    }

                                            }
                                        else
                                            {
                                                Chips[flag]--;
                                                Chips[a - 1]++;
                                                flagres++;
                                                if (Chips[a - 1] == k + 1)
                                                    {
                                                        flag = a - 1;
                                                        while (Chips[flag] == k + 1)
                                                            {
                                                                if (flag - 1 < 0)
                                                                    {
                                                                        sch++;
                                                                        Chips[a - 1 - sch]++;
                                                                        Chips[flag]--;
                                                                        flag = a - 1;
                                                                        flagres++;
                                                                    }
                                                                else
                                                                    {
                                                                        Chips[flag]--;
                                                                        Chips[flag - 1]++;
                                                                        flag--;
                                                                        flagres++;
                                                                    }

                                                            }
                                                    }
                                            }
                                    }
                                else
                                {

                                    if (flag + 1 <= a - 1)
                                    {
                                        Chips[flag]--;
                                        Chips[flag + 1]++;
                                        flagres++;
                                        if (Chips[flag + 1] == k + 1)
                                        {
                                            flag++;
                                            do
                                            {
                                                if (flag + 1 >= a)
                                                    {
                                                        sch++;
                                                        Chips[sch]++;
                                                        Chips[flag]--;
                                                        flag = 0;
                                                        flagres++;
                                                    }
                                                else
                                                    {
                                                        Chips[flag]--;
                                                        Chips[flag + 1]++;
                                                        flag++;
                                                        flagres++;
                                                    }
                                            }
                                            while (Chips[flag] == k + 1);
                                        }
                                    }
                                    else
                                        {
                                            Chips[flag]--;
                                            Chips[0]++;
                                            flagres++;
                                            if (Chips[0] == k + 1)
                                            {
                                                flag = 0;
                                                do
                                                    {
                                                        if (flag + 1 >= a)
                                                            {
                                                                sch++;
                                                                Chips[sch]++;
                                                                Chips[flag]--;
                                                                flag = 0;
                                                                flagres++;
                                                            }
                                                        else
                                                            {
                                                                Chips[flag]--;
                                                                Chips[flag + 1]++;
                                                                flag++;
                                                                flagres++;
                                                            }
                                                    }
                                                while (Chips[flag] == k + 1);
                                            }
                                        }
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
}

