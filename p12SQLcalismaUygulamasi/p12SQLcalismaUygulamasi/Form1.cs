using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace p12SQLcalismaUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void db()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select name from sys.databases",con);
            SqlDataReader dr= cmd.ExecuteReader();

            while (dr.Read())
            {
                cmbDatabases.Items.Add(dr[0].ToString());
            }
            con.Close();
        }
        
        void table()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=" + cmbDatabases.Text + ";Integrated Security=True");
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select * from sys.Tables", conn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                cmbTables.Items.Add(dr1[0].ToString());
            }
            conn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            db();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=" + cmbDatabases.Text + ";Integrated Security=True");
        }

        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTables.Items.Clear();
            table();
        }
        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            rchQuery.Text="select * from "+ cmbTables.Text;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=" + cmbDatabases.Text + ";Integrated Security=True");
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(rchQuery.Text, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                SqlDataAdapter da = new SqlDataAdapter("select * from " + " " + cmbTables.Text, sqlConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {

                MessageBox.Show("Please Check Your Query And Try Again!", "Operation Failed!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://en.wikipedia.org/wiki/Microsoft_SQL_Server");
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/sql-server/sql-server-downloads");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbDatabases.Text = "";
            cmbTables.Text = "";
            rchQuery.Text = "";
        }
    }
}
