using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.Common;
///

namespace konstruivania_grapf_test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///  

    public partial class MainWindow : Window
    {
      
        main_control final; //
        save_to_db save;
        public MainWindow()
        {
            InitializeComponent(); //ініціалізація форми
            final = new main_control();//створення екземпляра класу управління
            final.editing = false;
            comboBox1.IsEnabled = false;
            button2.IsEnabled = false;
            checkBox1.IsEnabled = false;
            button3.IsEnabled = false;

        }
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)//обробка двойного щелчка на формі, створення дочірніх елементів форми
        {
            if (final.editing == true) 
            {
                Point currPos = e.GetPosition(grid_main_window);
                int x = (int)currPos.X;
                int y = (int)currPos.Y;
                final.add_vershuna(x, y);
                grid_main_window.Children.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle);
                comboBox1.Items.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle.Name);
                comboBox1.SelectedIndex = final.elipsu.Count - 1;
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e) //запуск знаходження найкоротшого шляху
        {
            bool no_select_verchuna = true;
            for (int ia = 0; ia < final.elipsu.Count; ia++) 
            {
                if (final.elipsu[ia].blueRectangle.StrokeThickness ==4)
                {
                    no_select_verchuna = false;
                }
            }

            if (comboBox1.Items.Count==0)
            {
                MessageBox.Show("Сначала нужно сделать хотя бы 1 вершину","Error",MessageBoxButton.OK,MessageBoxImage.Error);

            }
            else if(no_select_verchuna==false)
            {
                MessageBox.Show("Освободите поле от выделенных вершин");
            }

             else if(final.editing==true)
            {
                MessageBox.Show("Сперва нужно выйти с редактирования");
             }
            else
            {

                final.first = comboBox1.SelectedIndex;
                final.deikstra_run();
                for (int i = 0; i < final.elipsu.Count;i++ )
                {
                grid_main_window.Children.Add(final.elipsu[i].lb_vershunu);
                }
                button4.IsEnabled=false;
                button2.IsEnabled = false;

            }

        }

        private void button5_Click(object sender, RoutedEventArgs e)//очищення вікна для створення графів, активація кнопок загрузки, створення ребер
        {
            button2.IsEnabled = true;
            button4.IsEnabled = true;
            grid_main_window.Children.RemoveRange(0,grid_main_window.Children.Count);
            final = null;
            final = new main_control();
            comboBox1.Items.Clear();
          
        }
        private void checkBox1_Click(object sender, RoutedEventArgs e) //включення виключення режиму редагування
        {
            if (checkBox1.IsChecked==true)
            {
                groupBox2.IsEnabled=true;
                final.editing = true;
            }
            else
            {
                groupBox2.IsEnabled = false ;
                final.editing = false;
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)//збереження графа у файл
        {
            save = new save_to_db();
            save.x = final.elipsu.Count;
            save.x_reber = final.rebra.Count;
            save.save_to_DB();
            //vnosum vershunu
            for (int ui = 0; ui < final.elipsu.Count; ui++)
            {
                save.Ax[ui] = final.elipsu[ui].x;
                save.Ay[ui] = final.elipsu[ui].y;
            }
            //vnosum rebra
            for (int ui = 0; ui < final.rebra.Count; ui++)
            {
                save.from[ui] = final.rebra[ui].from;
                save.to[ui] = final.rebra[ui].to;
            }

                Nullable<bool> rez;
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Deikstra_graph (.volfar)|*.volfar";
            sf.FileName = "Deikstra_graph";
            rez = sf.ShowDialog();
            if (rez == true)
            {
                FileStream fils = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fils, save);
                fils.Close();
            }
        }   

        private void button4_Click(object sender, RoutedEventArgs e) //загрузка графа з файла
        {
            

            Nullable<bool> rez;
            OpenFileDialog sf = new OpenFileDialog();
            sf.Filter = "Deikstra_graph (.volfar)|*.volfar";
            sf.FileName = "Deikstra_graph";
            rez = sf.ShowDialog();
            try
            {
                if (rez == true)
                {
                    FileStream fils = new FileStream(sf.FileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryFormatter bf = new BinaryFormatter();
                    save = (save_to_db)bf.Deserialize(fils);
                    fils.Close();
                }
           
            //clear all
            grid_main_window.Children.RemoveRange(0, grid_main_window.Children.Count);
            final = null;
            final = new main_control();
            comboBox1.Items.Clear();
            //vostanovlenia vershun
            for (int opi = 0; opi < save.x; opi++)
            {
                final.add_vershuna(save.Ax[opi], save.Ay[opi]);
                grid_main_window.Children.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle);
                comboBox1.Items.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle.Name);
            }
            comboBox1.SelectedIndex = 0;
            //vostanovlenia reber
            for (int opi = 0; opi < save.x_reber; opi++)
            {
                final.from = save.from[opi];
                final.to = save.to[opi];
                final.add_rebro();
                grid_main_window.Children.Add(final.rebra[final.rebra.Count - 1].rebro);
                Grid.SetZIndex(final.rebra[final.rebra.Count - 1].rebro, -1);
                final.elipsu[final.rebra[final.rebra.Count - 1].from].blueRectangle.StrokeThickness = 1;
                final.elipsu[final.rebra[final.rebra.Count - 1].to].blueRectangle.StrokeThickness = 1;
                final.enable_conect = true;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                final.editing = true;
                comboBox1.IsEnabled = true;
                button2.IsEnabled = true;
                checkBox1.IsEnabled = true;
                button3.IsEnabled = true;
                button4.IsEnabled = true;
                System.Windows.Media.ImageBrush myBrush = new System.Windows.Media.ImageBrush();
                Image image = new Image();
                image.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri("C:/Users/Maksimka/Desktop/konstruivania_grapf_test2/ukraine_74.jpg"));
                myBrush.ImageSource = image.Source;
                grid_main_window.Background = myBrush; 
             }
            if (comboBox2.SelectedIndex == 1)
            {
                final.editing = true;
                comboBox1.IsEnabled = true;
                button2.IsEnabled = true;
                checkBox1.IsEnabled = true;
                button4.IsEnabled = true; 
                button3.IsEnabled = true;
                System.Windows.Media.ImageBrush myBrush = new System.Windows.Media.ImageBrush();
                Image image = new Image();
                image.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri("C:/Users/Maksimka/Desktop/konstruivania_grapf_test2/map_rivers_razdel.jpg"));
                myBrush.ImageSource = image.Source;
                grid_main_window.Background = myBrush;
            }
         }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Space(object sender, KeyEventArgs e)
        {
            bool no_select_verchuna = false;
            for (int ia = 0; ia < final.elipsu.Count; ia++)
            {
                if (final.elipsu[ia].blueRectangle.StrokeThickness == 6)
                {
                    no_select_verchuna = true;
                }
            }
            if (no_select_verchuna)
            {
                grid_main_window.Children.Add(final.rebra[final.rebra.Count - 1].rebro);
                Grid.SetZIndex(final.rebra[final.rebra.Count - 1].rebro, -1);
                final.elipsu[final.rebra[final.rebra.Count - 1].from].blueRectangle.StrokeThickness = 1;
                final.elipsu[final.rebra[final.rebra.Count - 1].to].blueRectangle.StrokeThickness = 1;
                final.enable_conect = true;
            }
            else
            {
                MessageBox.Show("Нужно что бы было выделенно 2 города");
            }

        }
    }
}
