using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quipu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //static CancellationTokenSource cts = new CancellationTokenSource();
        //CancellationToken ct = cts.Token;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        string path = @"C:\Users\lovel\source\repos\Quipu\File.txt";

        // Чтение файла и загрузка списка url
        
        public void FileRead()
        {
            StreamReader rdr = new StreamReader(path, Encoding.Default);
            while (!rdr.EndOfStream)
            {
                listBox1.Items.Add(rdr.ReadLine());
            }
            rdr.Close();
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            FileRead();
        }


        // Переход по ссылке

        int l = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(listBox1.Items[l].ToString());
            l++;



            if (l == listBox1.Items.Count)
            {
                l = 0;
            }
        }

       

        // ПОдсчет тегов, вывод количества, визуальное выделение url с наибольшим количеством тегов
        int f = 0;
        int h = 0;
        //bool need = true;
        
        
        void btn_fin_Click(object sender, RoutedEventArgs e)
        {
            WebClient wc = new WebClient();

            for (int m = 0; m < listBox1.Items.Count; m++)
            {
                string s = wc.DownloadString(listBox1.Items[f].ToString());

                var p = "</a>";
                int i = 0, count = 0;
                while ((i = s.IndexOf(p, i)) != -1)
                {
                    ++count;
                    i += p.Length;
                }
                listBox2.Items.Add(count.ToString());



                f++;

            }
            int count1 = listBox2.Items.Count;

            int largest = 0;

            foreach (object item in listBox2.Items)
            {
                if (Convert.ToInt32(item) > largest)
                {
                    largest = Convert.ToInt32(item);
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        if (listBox2.Items[i].ToString() == Convert.ToString(largest))
                        {

                            listBox2.SelectedIndex = i;

                        }

                    }

                }

            }


            string message = String.Format("Прочитано {0} url, наибольшее количество тегов <a> - {1}", count1, largest);
            MessageBox.Show(message);



            //if (ct.IsCancellationRequested)
            //    break;



        }




        // Прерывание процесса финального подсчета и вывода


        private void X_Click(object sender, RoutedEventArgs e)
        {
        //    cts.Cancel();
        }


    }
}
