using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Post_Office_Management_System
{
    public partial class Parcel : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Post Office Management System\PostOfficeDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Parcel()
        {
            InitializeComponent();
            populate();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            /*
            if (sname.Text == "" || rname.Text == "" || rpone.Text == "")
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ParcelTbl (CustName, CustDOB, CustPhone, CustAdd) values(@CN,@CD,@CP,@CA)", Con);
                    cmd.Parameters.AddWithValue("@CN", cnnamelbl.Text);
                    cmd.Parameters.AddWithValue("@CD", date.Value.Date);
                    cmd.Parameters.AddWithValue("@CP", phonelbl.Text);
                    cmd.Parameters.AddWithValue("@CA", addresslbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully!!!");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            */
        }
        private void populate()
        {
            Con.Open();
            string Query = "select * from ParcelTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AGENTGDV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
