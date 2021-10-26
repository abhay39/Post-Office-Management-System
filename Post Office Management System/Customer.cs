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

namespace Post_Office_Management_System
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Post Office Management System\PostOfficeDB.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void savebtn_Click(object sender, EventArgs e)
        {
            
            if(cnnamelbl.Text==""|| phonelbl.Text==""|| addresslbl.Text == "")
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName, CustDOB, CustPhone, CustAdd) values(@CN,@CD,@CP,@CA)", Con);
                    cmd.Parameters.AddWithValue("@CN", cnnamelbl.Text);
                    cmd.Parameters.AddWithValue("@CD", date.Value.Date);
                    cmd.Parameters.AddWithValue("@CP", phonelbl.Text);
                    cmd.Parameters.AddWithValue("@CA", addresslbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully!!!");
                    Con.Close();
                    populate();
                    Reset();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void populate()
        {
            Con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AGENTGDV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int key = 0;

        private void AGENTGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cnnamelbl.Text = AGENTGDV.SelectedRows[0].Cells[1].Value.ToString();
            phonelbl.Text = AGENTGDV.SelectedRows[0].Cells[3].Value.ToString();
            addresslbl.Text = AGENTGDV.SelectedRows[0].Cells[4].Value.ToString();
            date.Text = AGENTGDV.SelectedRows[0].Cells[2].Value.ToString();
            if (cnnamelbl.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AGENTGDV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (cnnamelbl.Text == "" || phonelbl.Text == "" || addresslbl.Text == "")
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set CustName=@CN, CustDOB=@CD, CustPhone=@CP, CustAdd=@CA where CustNum=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", cnnamelbl.Text);
                    cmd.Parameters.AddWithValue("@CD", date.Value.Date);
                    cmd.Parameters.AddWithValue("@CP", phonelbl.Text);
                    cmd.Parameters.AddWithValue("@CA", addresslbl.Text);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated Successfully!!!");
                    Con.Close();
                    Reset();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Informations.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from CustomerTbl where CustNum = " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted Successfully.");
                    Con.Close();
                    Reset();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Reset()
        {
            cnnamelbl.Text = "";
            phonelbl.Text = "";
            addresslbl.Text = "";
            date.Text = "";
        }
    }
}
