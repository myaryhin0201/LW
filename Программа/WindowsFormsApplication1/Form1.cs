using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string queryString  = ("SELECT Name FROM RISKS WHERE ID_CAT=1");
           // string query_String = ("SELECT Vero FROM RISKS WHERE ID_CAT=1");
            string queryString2 = ("SELECT Name FROM RISKS WHERE ID_CAT=2");
           // string query_String2 = ("SELECT Vero FROM RISKS WHERE ID_CAT=2");
            string queryString3 = ("SELECT Name FROM RISKS WHERE ID_CAT=3");
          //  string query_String3 = ("SELECT Vero FROM RISKS WHERE ID_CAT=3");
            string queryString4 = ("SELECT Name FROM RISKS WHERE ID_CAT=4");
           // string query_String4 = ("SELECT Vero FROM RISKS WHERE ID_CAT=4");
            string queryString5 = ("SELECT Name FROM RISKS WHERE ID_CAT=5");
           // string query_String5 = ("SELECT Vero FROM RISKS WHERE ID_CAT=5");
            string queryString6 = ("SELECT Name FROM RISKS WHERE ID_CAT=6");
          //  string query_String6 = ("SELECT Vero FROM RISKS WHERE ID_CAT=6");
            string queryString7 = ("SELECT Name FROM RISKS WHERE ID_CAT=7");
        //    string query_String7 = ("SELECT Vero FROM RISKS WHERE ID_CAT=7");
            string queryString8 = ("SELECT Name FROM RISKS WHERE ID_CAT=8");
       //     string query_String8 = ("SELECT Vero FROM RISKS WHERE ID_CAT=8");
            string queryString9 = ("SELECT Name FROM RISKS WHERE ID_CAT=9");
        //    string query_String9 = ("SELECT Vero FROM RISKS WHERE ID_CAT=9");

            string l1 = ("SELECT Name FROM CATEGORY WHERE ID=1");
            string l2 = ("SELECT Name FROM CATEGORY WHERE ID=2");
            string l3 = ("SELECT Name FROM CATEGORY WHERE ID=3");
            string l4 = ("SELECT Name FROM CATEGORY WHERE ID=4");
            string l5 = ("SELECT Name FROM CATEGORY WHERE ID=5");
            string l6 = ("SELECT Name FROM CATEGORY WHERE ID=6");
            string l7 = ("SELECT Name FROM CATEGORY WHERE ID=7");
            string l8 = ("SELECT Name FROM CATEGORY WHERE ID=8");
            string l9 = ("SELECT Name FROM CATEGORY WHERE ID=9");
            string ll = ("SELECT Name FROM SOSTAV WHERE ID=1");
            string lll = ("SELECT Name FROM SOSTAV WHERE ID=2");
         


            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\\Users\\Maksimka\\documents\\visual studio 2013\\Projects\\WindowsFormsApplication1\\BAZA.accdb "))
            {
                connection.Open();
                //Cостав 1
                OleDbCommand lll1 = new OleDbCommand(ll, connection);
                OleDbDataReader llll = lll1.ExecuteReader();
                while (llll.Read())
                {
                    label3.Text = llll[0].ToString();
                }
                llll.Close();
                //Состав 2
                OleDbCommand llll1 = new OleDbCommand(lll, connection);
                OleDbDataReader lllll = llll1.ExecuteReader();
                while (lllll.Read())
                {
                    label4.Text = lllll[0].ToString();
                }
                lllll.Close();

                //1
                OleDbCommand command = new OleDbCommand(queryString, connection);
                
                OleDbCommand l11 = new OleDbCommand(l1, connection);
                OleDbDataReader l111=l11.ExecuteReader();
                while(l111.Read())
                {
                label5.Text = l111[0].ToString();
                }
                l111.Close();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    checkedListBox1.Items.Add(reader[0].ToString());
                }
              reader.Close();

         /*     OleDbCommand com = new OleDbCommand(query_String, connection);
              OleDbDataReader cm = com.ExecuteReader();
              while (cm.Read()) 
              {
                  checkedListBox10.Items.Add(cm[0].ToString());
              }
              cm.Close();
                */
                //2
              OleDbCommand command2 = new OleDbCommand(queryString2, connection);
            OleDbDataReader reader2 = command2.ExecuteReader();
            
                OleDbCommand l22 = new OleDbCommand(l2, connection);
            OleDbDataReader l222 = l22.ExecuteReader();
            while (l222.Read())
            {
                label6.Text = l222[0].ToString();
            }
            l222.Close();
              while (reader2.Read())
              {
                  checkedListBox2.Items.Add(reader2[0].ToString());
              }
              reader2.Close();

         /*     OleDbCommand com2 = new OleDbCommand(query_String2, connection);
              OleDbDataReader cm2 = com2.ExecuteReader();
              while (cm2.Read())
              {
                  checkedListBox11.Items.Add(cm2[0].ToString());
              }
              cm2.Close();*/
                //3
              OleDbCommand command3 = new OleDbCommand(queryString3, connection);
            
                OleDbCommand l33 = new OleDbCommand(l3, connection);
              OleDbDataReader l333 = l33.ExecuteReader();
              while (l333.Read())
              {
                  label7.Text = l333[0].ToString();
              }
              l333.Close();
          OleDbDataReader reader3 = command3.ExecuteReader();
              while (reader3.Read())
              {
                  checkedListBox3.Items.Add(reader3[0].ToString());
              }
              reader3.Close();
          /*    OleDbCommand com3 = new OleDbCommand(query_String3, connection);
              OleDbDataReader cm3 = com3.ExecuteReader();
              while (cm3.Read())
              {
                  checkedListBox12.Items.Add(cm3[0].ToString());
              }
              cm3.Close();*/
             //4
              OleDbCommand command4 = new OleDbCommand(queryString4, connection);
              OleDbCommand l44 = new OleDbCommand(l4, connection);
              OleDbDataReader l444 = l44.ExecuteReader();
              while (l444.Read())
              {
                  label8.Text = l444[0].ToString();
              }
              l444.Close();
             OleDbDataReader reader4 = command4.ExecuteReader();
              while (reader4.Read())
              {
                  checkedListBox4.Items.Add(reader4[0].ToString());
              }
              reader4.Close();
           /*   OleDbCommand com4 = new OleDbCommand(query_String4, connection);
              OleDbDataReader cm4 = com4.ExecuteReader();
              while (cm4.Read())
              {
                  checkedListBox13.Items.Add(cm4[0].ToString());
              }
              cm4.Close();*/
                //5
              OleDbCommand command5 = new OleDbCommand(queryString5, connection);
              OleDbCommand l55 = new OleDbCommand(l5, connection);
              OleDbDataReader l555 = l55.ExecuteReader();
              while (l555.Read())
              {
                  label9.Text = l555[0].ToString();
              }
              l555.Close();
             OleDbDataReader reader5 = command5.ExecuteReader();
              while (reader5.Read())
              {
                  checkedListBox5.Items.Add(reader5[0].ToString());
              }
              reader5.Close();
          /*    OleDbCommand com5 = new OleDbCommand(query_String5, connection);
              OleDbDataReader cm5 = com5.ExecuteReader();
              while (cm5.Read())
              {
                  checkedListBox14.Items.Add(cm5[0].ToString());
              }
              cm5.Close();*/
                //6
              OleDbCommand command6 = new OleDbCommand(queryString6, connection);
              OleDbCommand l66 = new OleDbCommand(l6, connection);
              OleDbDataReader l666 = l66.ExecuteReader();
              while (l666.Read())
              {
                  label10.Text = l666[0].ToString();
              }
              l666.Close();
             OleDbDataReader reader6 = command6.ExecuteReader();
              while (reader6.Read())
              {
                  checkedListBox6.Items.Add(reader6[0].ToString());
              }
              reader6.Close();
          /*    OleDbCommand com6 = new OleDbCommand(query_String6, connection);
              OleDbDataReader cm6 = com6.ExecuteReader();
              while (cm6.Read())
              {
                  checkedListBox15.Items.Add(cm6[0].ToString());
              }
              cm6.Close();*/
                //7
              OleDbCommand command7 = new OleDbCommand(queryString7, connection);
              OleDbCommand l77 = new OleDbCommand(l7, connection);
              OleDbDataReader l777 = l77.ExecuteReader();
              while (l777.Read())
              {
                  label11.Text = l777[0].ToString();
              }
              l777.Close();
              OleDbDataReader reader7 = command7.ExecuteReader();
              while (reader7.Read())
              {
                  checkedListBox7.Items.Add(reader7[0].ToString());
              }
              reader7.Close();
             /* OleDbCommand com7 = new OleDbCommand(query_String7, connection);
              OleDbDataReader cm7 = com7.ExecuteReader();
              while (cm7.Read())
              {
                  checkedListBox16.Items.Add(cm7[0].ToString());
              }
              cm7.Close();*/
                //8
              OleDbCommand command8 = new OleDbCommand(queryString8, connection);
              OleDbCommand l88 = new OleDbCommand(l8, connection);
              OleDbDataReader l888 = l88.ExecuteReader();
              while (l888.Read())
              {
                  label12.Text = l888[0].ToString();
              }
              l888.Close();
              OleDbDataReader reader8 = command8.ExecuteReader();
              while (reader8.Read())
              {
                  checkedListBox8.Items.Add(reader8[0].ToString());
              }
              reader8.Close();
           /*   OleDbCommand com8 = new OleDbCommand(query_String8, connection);
              OleDbDataReader cm8 = com8.ExecuteReader();
              while (cm8.Read())
              {
                  checkedListBox17.Items.Add(cm8[0].ToString());
              }
              cm8.Close();*/
                //9
              OleDbCommand command9 = new OleDbCommand(queryString9, connection);
              OleDbCommand l99 = new OleDbCommand(l9, connection);
              OleDbDataReader l999 = l99.ExecuteReader();
              while (l999.Read())
              {
                  label13.Text = l999[0].ToString();
              }
              l999.Close();
              OleDbDataReader reader9 = command9.ExecuteReader();
              while (reader9.Read())
              {
                  checkedListBox9.Items.Add(reader9[0].ToString());
              }
              reader9.Close();
          /*    OleDbCommand com9 = new OleDbCommand(query_String9, connection);
              OleDbDataReader cm9 = com9.ExecuteReader();
              while (cm9.Read())
              {
                  checkedListBox18.Items.Add(cm9[0].ToString());
              }
              cm9.Close();*/
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form2 f2 = new Form2();
            Form1 f1 = new Form1();
            //  this.Hide();
            f2.Show();
            /*

            double ko = 0;
            string[] tu1 = new string[listBox1.Items.Count];
            string[] tutu1 = new string[listBox3.Items.Count];
            double u, tur;
            string[] tu2 = new string[listBox2.Items.Count];
            string[] tutu2 = new string[listBox4.Items.Count];
        
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                tutu1[i] = listBox3.Items[i].ToString();
            }
            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                tutu2[i] = listBox4.Items[i].ToString();
            }

            double kof = 0.4;

            Random rnd = new Random();
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    int qwer = int.Parse(textBox2.Text);
                    if (qwer != 0)
                    {
                        double wer = 0;
                        double wer2 = 0;
                        tur = tu1.Length;

                           for (int i = 0; i < tur; i++)
                            {
                           
                                u = Convert.ToDouble( tutu1[i])*kof;
                                if (u <= 0.04) { wer = wer; }//незначительные риски
                                if (u > 0.04 && u < 0.12) { }//умеренные риски
                                if (u > 0.12) { }//Критические риски
                                wer += u;
                            }
                        
                        wer = wer / (tur);
                        listBox2.Items.CopyTo(tu2, 0);
                       
                        
                            for (int j = 0; j < tu2.Length; j++)
                            {
                           
                                u = Convert.ToDouble(tutu2[j])*kof; 
                                wer2 += u;
                            }
                       
                        wer2 = wer2 / (tu2.Length);


             
                     ko = wer + wer2;
                        
                        double ty = int.Parse(textBox2.Text);
                        double hu = (ko + ty);
                        //Вывод в базу данных
                        using (var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=C:\\Users\\Maksimka\\documents\\visual studio 2013\\Projects\\WindowsFormsApplication1\\kursovoy.accdb"))
                        {
                                connection.Open();
                                using (OleDbCommand command = connection.CreateCommand())
                                {
                                    string str = "";
                              /*     if (listBox3.Items.Count != 0)
                                    {
                                        foreach (string st in listBox1.Items)
                                        {
                                            str += st + "; " + "\r\n";
                                        }
                                        command.CommandText = "INSERT INTO [Kurs] ([Name], [Days], [Additional days], [Result], [Risks]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + ko.ToString() + "','" + hu.ToString() + "','" + str + "');";
                                        command.ExecuteNonQuery();
                                    }

                                    if (listBox4.Items.Count != 0)
                                    {
                                        str = "";
                                        foreach (string stt in listBox2.Items)
                                        {
                                            str += stt + "; " + "\r\n";
                                        }

                                        command.CommandText = "INSERT INTO [Kurs] ([Name], [Days], [Additional days], [Result], [Risks]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + ko.ToString() + "','" + hu.ToString() + "','" + str + "');";
                                        command.ExecuteNonQuery();
                                    }
                                    if ((listBox3.Items.Count != 0) && (listBox4.Items.Count != 0))
                                    {
                                        foreach (string st in listBox1.Items)
                                        {
                                            str += st + "; " + "\r\n";
                                        }
                                        foreach (string stt in listBox2.Items)
                                        {
                                            str += stt + "; " + "\r\n";
                                        }

                                        command.CommandText = "INSERT INTO [Kurs] ([Name], [Days], [Additional days], [Result], [Risks]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + ko.ToString() + "','" + hu.ToString() + "','" + str + "');";
                                        command.ExecuteNonQuery();
                                    }

                                }
                                MessageBox.Show("Данные удачно сохранены в БД!");
                                connection.Close();
                            }
                  //      }

                        /*
                                            //        }
                                                //Maybe 2
                                           //        if (checkBox2.Checked)
                                          //         {
                                                        listBox1.Items.CopyTo(tu1, 0);
                                                        tur = tu1.Length;

                                                        for (int cop = 0; cop < 500; cop++)
                                                        {
                                                            for (int i = 0; i < tur; i++)
                                                            {
                                                                int rew = rnd.Next(40, 70);
                                                                u = (int.Parse(textBox2.Text) * rew) / 100;
                                                                wer += u;
                                                            }
                                                        }
                                                        wer = wer / (500 * tur);
                                                        listBox2.Items.CopyTo(tu2, 0);
                                                        for (int cop = 0; cop < 500; cop++)
                                                        {
                                                            for (int j = 0; j < tu2.Length; j++)
                                                            {
                                                                int rew = rnd.Next(40, 70);
                                                                u = (int.Parse(textBox2.Text) * rew) / 100;
                                                                wer2 += u;
                                                            }
                                                        }
                                                        wer2 = wer2 / (500*tu2.Length);
                                                        ko = wer + wer2;
                                                        double ty = int.Parse(textBox2.Text);
                                                        double hu = (ko + ty);
                                                        using (var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
                                            @"Data Source=C:\\Users\\Maksimka\\documents\\visual studio 2013\\Projects\\WindowsFormsApplication1\\kursovoy.accdb"))
                                                        {
                                                            connection.Open();
                                                            using (OleDbCommand command = connection.CreateCommand())
                                                            {
                                                                string str = "";
                                                                foreach (string st in listBox1.Items)
                                                                {
                                                                    str += st + "; " + "\r\n";
                                                                }
                                                                foreach (string stt in listBox2.Items)
                                                                {
                                                                    str += stt + "; " + "\r\n";
                                                                }

                                                                command.CommandText = "INSERT INTO [Kurs] ([Name], [Days], [Probability], [Additional days], [Result], [Risks]) values ('" + textBox1.Text + "'," + textBox2.Text + ",'" + checkBox2.Text + "','" + ko.ToString() + "','" + hu.ToString() + "','" + str + "');";
                                                                command.ExecuteNonQuery();
                                                            }
                                                            MessageBox.Show("Данные удачно сохранены в БД!");
                                                            connection.Close();
                                                        }

                                         //          }
                                                 //Maybe3
                                                //      if (checkBox3.Checked)
                                               //     {
                                                        listBox1.Items.CopyTo(tu1, 0);
                                                        tur = tu1.Length;
                                                        for (int cop = 0; cop < 500; cop++)
                                                        {
                                                            for (int i = 0; i < tur; i++)
                                                            {
                                                                int rew = rnd.Next(70, 100);
                                                                u = (int.Parse(textBox2.Text) * rew) / 100; 
                                                                wer += u;
                                                            }
                                                        }
                                                        wer = wer / (500 * tur);

                                                        listBox2.Items.CopyTo(tu2, 0);
                                                        for (int cop = 0; cop < 500; cop++)
                                                        {
                                                            for (int j = 0; j < tu2.Length; j++)
                                                            {
                                                                int rew = rnd.Next(70, 100);
                                                                u = (int.Parse(textBox2.Text) * rew) / 100;
                                                                wer2 += u;
                                                            }
                                                        }
                                                        wer2 = wer2 / (500*tu2.Length);
                                                        ko = wer + wer2;
                                                        double ty = int.Parse(textBox2.Text);
                                                        double hu = (ko + ty);
                                                        using (var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
                                            @"Data Source=C:\\Users\\Maksimka\\documents\\visual studio 2013\\Projects\\WindowsFormsApplication1\\kursovoy.accdb"))
                                                        {
                                                            connection.Open();
                                                            using (OleDbCommand command = connection.CreateCommand())
                                                            {
                                                                string str = "";
                                                                foreach (string st in listBox1.Items)
                                                                {
                                                                    str += st + "; " + "\r\n";
                                                                }
                                                                foreach (string stt in listBox2.Items)
                                                                {
                                                                    str += stt + "; " + "\r\n";
                                                                }

                                                                command.CommandText = "INSERT INTO [Kurs] ([Name], [Days], [Probability], [Additional days], [Result], [Risks]) values ('" + textBox1.Text + "'," + textBox2.Text + ",'" + checkBox3.Text + "','" + ko.ToString() + "','" + hu.ToString() + "','" + str + "');";
                                                                command.ExecuteNonQuery();
                                                            }
                                                            MessageBox.Show("Данные удачно сохранены в БД!");
                                                            connection.Close();
                                                        }

                                           //         }*/

                        /*        if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
                                {
                                    checkBox1.Checked = false;
                                    checkBox2.Checked = false;
                                    checkBox3.Checked = false;
                                    MessageBox.Show(" Выберите одну вероятность ", "Вы не выбрали", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                if (((checkBox1.Checked) && (checkBox2.Checked)) ||
                                    ((checkBox1.Checked) && (checkBox3.Checked)) ||
                                    (((checkBox2.Checked) && (checkBox3.Checked))))
                                {
                                    checkBox1.Checked = false;
                                    checkBox2.Checked = false;
                                    checkBox3.Checked = false;
                                    MessageBox.Show(" Выберите только одну вероятность ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                    }
                    else { MessageBox.Show("Вы ввели 0, введите реально значение", "Введите количетво дней", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                }
                else { MessageBox.Show("Вы не заполнили поле, сколько дней выделено под проeкт", "Введите количество дней", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
            else { MessageBox.Show("Вы не заполнили поле, название проeкта", "Введите название", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            */
        }
            
            
        




        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/*listBox3.Items.Add( checkedListBox10.Items[e.Index]);*/ listBox1.Items.Add(checkedListBox1.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox1.Items.Count - 1)); i++)
                {
                    if ((listBox1.Items[i] == checkedListBox1.Items[e.Index]))
                    {
                        listBox1.Items.RemoveAt(i);
                        listBox3.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }


        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) { /*listBox3.Items.Add(checkedListBox11.Items[e.Index]);*/ listBox1.Items.Add(checkedListBox2.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox1.Items.Count - 1)); i++)
                {
                    if ((listBox1.Items[i] == checkedListBox2.Items[e.Index]))
                    {
                        listBox1.Items.RemoveAt(i);
                        listBox3.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox3.Items.Add(checkedListBox12.Items[e.Index]);*/ listBox1.Items.Add(checkedListBox3.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox1.Items.Count - 1)); i++)
                {
                    if ((listBox1.Items[i] == checkedListBox3.Items[e.Index]))
                    {
                        listBox1.Items.RemoveAt(i);
                        listBox3.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox4_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox3.Items.Add(checkedListBox13.Items[e.Index]);*/ listBox1.Items.Add(checkedListBox4.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox1.Items.Count - 1)); i++)
                {
                    if ((listBox1.Items[i] == checkedListBox4.Items[e.Index]))
                    {
                        listBox1.Items.RemoveAt(i);
                        listBox3.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox5_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox4.Items.Add(checkedListBox14.Items[e.Index]);*/ listBox2.Items.Add(checkedListBox5.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox2.Items.Count - 1)); i++)
                {
                    if ((listBox2.Items[i] == checkedListBox5.Items[e.Index]))
                    {
                        listBox2.Items.RemoveAt(i);
                        listBox4.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox6_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox4.Items.Add(checkedListBox15.Items[e.Index]);*/ listBox2.Items.Add(checkedListBox6.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox2.Items.Count - 1)); i++)
                {
                    if ((listBox2.Items[i] == checkedListBox6.Items[e.Index]))
                    {
                        listBox2.Items.RemoveAt(i);
                        listBox4.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox7_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox4.Items.Add(checkedListBox16.Items[e.Index]);*/ listBox2.Items.Add(checkedListBox7.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox2.Items.Count - 1)); i++)
                {
                    if ((listBox2.Items[i] == checkedListBox7.Items[e.Index]))
                    {
                        listBox2.Items.RemoveAt(i);
                        listBox4.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox8_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox4.Items.Add(checkedListBox17.Items[e.Index]);*/ listBox2.Items.Add(checkedListBox8.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox2.Items.Count - 1)); i++)
                {
                    if ((listBox2.Items[i] == checkedListBox8.Items[e.Index]))
                    {
                        listBox2.Items.RemoveAt(i);
                        listBox4.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void checkedListBox9_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int i;
            if ((e.NewValue == CheckState.Checked)) {/* listBox4.Items.Add(checkedListBox18.Items[e.Index]);*/ listBox2.Items.Add(checkedListBox9.Items[e.Index]); }
            else
            {
                for (i = 0; (i <= (listBox2.Items.Count - 1)); i++)
                {
                    if ((listBox2.Items[i] == checkedListBox9.Items[e.Index]))
                    {
                        listBox2.Items.RemoveAt(i);
                        listBox4.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            Form1 f1 = new Form1();
          //  this.Hide();
            f5.Show();
       /*     int i;
            for (i = 0; i <= checkedListBox1.Items.Count - 1; i++) { checkedListBox1.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox2.Items.Count - 1; i++) { checkedListBox2.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox3.Items.Count - 1; i++) { checkedListBox3.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox4.Items.Count - 1; i++) { checkedListBox4.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox5.Items.Count - 1; i++) { checkedListBox5.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox6.Items.Count - 1; i++) { checkedListBox6.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox7.Items.Count - 1; i++) { checkedListBox7.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox8.Items.Count - 1; i++) { checkedListBox8.SetItemChecked(i, false); }
            for (i = 0; i <= checkedListBox9.Items.Count - 1; i++) { checkedListBox9.SetItemChecked(i, false); }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i <= checkedListBox1.Items.Count - 1; i++) { checkedListBox1.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox2.Items.Count - 1; i++) { checkedListBox2.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox3.Items.Count - 1; i++) { checkedListBox3.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox4.Items.Count - 1; i++) { checkedListBox4.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox5.Items.Count - 1; i++) { checkedListBox5.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox6.Items.Count - 1; i++) { checkedListBox6.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox7.Items.Count - 1; i++) { checkedListBox7.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox8.Items.Count - 1; i++) { checkedListBox8.SetItemChecked(i, true); }
            for (i = 0; i <= checkedListBox9.Items.Count - 1; i++) { checkedListBox9.SetItemChecked(i, true); }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Максим Ярыгин" + " ", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Рекомендации стандартов управления проектами не следует принимать как догму." +
               "Каждый конкретный проект уникален и требует адаптации теоретических положений к его условиям.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

                      Form2 f2 = new Form2();
                      Form1 f1 = new Form1();
                      this.Hide();
                      f2.Show();

        }

      
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            Form1 f1 = new Form1();
           // this.Hide();
            f4.Show();
        }
    }
}
