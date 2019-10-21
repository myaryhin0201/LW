using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string FileName = null;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            FileName = "G:\\BAZA.accdb";
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileName = fd.FileName;
                textBox1.Text = FileName;

                treeView1.Nodes.Clear();
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + FileName);
                con.Open();

                DataTable tableNames = con.GetSchema("Tables");
                DataTable columnNames = con.GetSchema("Columns");

                int p = 0;
                foreach (DataRow dr in tableNames.Rows)
                {
                    string tableName = (string)dr["TABLE_NAME"];
                    if (!tableName.Contains("MSys"))
                    {
                        treeView1.Nodes.Add(tableName);
                        foreach (DataRow dr1 in columnNames.Rows)
                        {
                            if (tableName == (string)dr1["TABLE_NAME"])
                            {
                                treeView1.Nodes[p].Nodes.Add((string)dr1["COLUMN_NAME"]);
                            }
                        }
                        p++;

                    }
                }
                con.Close();
            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FileName = "G:\\BAZA.accdb";
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + FileName);
            con.Open();

            if (treeView1.SelectedNode.Level == 0)
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from " + treeView1.SelectedNode.Text, con);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

                DataSet ds = new DataSet();

                da.Fill(ds, treeView1.SelectedNode.Text);
                dataGridView1.DataSource = ds.Tables[0];

            }
            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from " + treeView1.SelectedNode.Parent.Text, con);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                DataSet ds = new DataSet();

                da.Fill(ds, treeView1.SelectedNode.Parent.Text);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
                FileName = "C:\\Users\\Maksimka\\documents\\visual studio 2013\\Projects\\WindowsFormsApplication1\\kursovoy.accdb";
                textBox1.Text = FileName;

                treeView1.Nodes.Clear();
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + FileName);
                con.Open();

                DataTable tableNames = con.GetSchema("Tables");
                DataTable columnNames = con.GetSchema("Columns");

                int p = 0;
                foreach (DataRow dr in tableNames.Rows)
                {
                    string tableName = (string)dr["TABLE_NAME"];
                    if (!tableName.Contains("MSys"))
                    {
                        treeView1.Nodes.Add(tableName);
                        foreach (DataRow dr1 in columnNames.Rows)
                        {
                            if (tableName == (string)dr1["TABLE_NAME"])
                            {
                                treeView1.Nodes[p].Nodes.Add((string)dr1["COLUMN_NAME"]);
                            }
                        }
                        p++;

                    }
                }
                con.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
      /*      OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + FileName);
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from " + treeView1.SelectedNode.Text, con);
            DataSet ds = new DataSet();
            da.Update(ds.Tables[0]);
            con.Close();*/

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Form1 f1 = new Form1();
          //  this.Hide();
            f1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            FileName = "G:\\BAZA.accdb";
            textBox1.Text = FileName;

            treeView1.Nodes.Clear();
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + FileName);
            con.Open();

            DataTable tableNames = con.GetSchema("Tables");
            DataTable columnNames = con.GetSchema("Columns");

            int p = 0;
            foreach (DataRow dr in tableNames.Rows)
            {
                string tableName = (string)dr["TABLE_NAME"];
                if (!tableName.Contains("MSys"))
                {
                    treeView1.Nodes.Add(tableName);
                    foreach (DataRow dr1 in columnNames.Rows)
                    {
                        if (tableName == (string)dr1["TABLE_NAME"])
                        {
                            treeView1.Nodes[p].Nodes.Add((string)dr1["COLUMN_NAME"]);
                        }
                    }
                    p++;

                }
            }
            con.Close();
        }

    }
}

