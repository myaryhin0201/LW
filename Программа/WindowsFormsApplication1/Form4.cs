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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
         string FileName = null;
     
         
     

        private void Form4_Load(object sender, EventArgs e)
        {
            FileName = "G:\\BAZA.accdb";
            //     textBox1.Text = FileName;

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

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
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
        }
    }

