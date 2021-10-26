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
    public partial class Agents : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Post Office Management System\PostOfficeDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Agents()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (agenttb.Text == "" || phonetb.Text == "" || passwordtb.Text == ""||gedercb.SelectedIndex==-1)
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AgentTbl (AgName,AgDOB, AgAdd, AgPhone,AgGen, AgPass) values(@AN,@AD,@AA,@AP,@AG,@APP)", Con);
                    cmd.Parameters.AddWithValue("@AN", agenttb.Text);
                    cmd.Parameters.AddWithValue("@AD", date.Value.Date);
                    cmd.Parameters.AddWithValue("@AA", adresstb.Text);
                    cmd.Parameters.AddWithValue("@AP", phonetb.Text);
                    cmd.Parameters.AddWithValue("@APP", passwordtb.Text);
                    cmd.Parameters.AddWithValue("@AG", gedercb.SelectedItem.ToString());
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

        }
        private void populate()
        {
            Con.Open();
            string Query = "select * from AgentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AGENTGDV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int key =0;
        private void AGENTGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            agenttb.Text = AGENTGDV.SelectedRows[0].Cells[1].Value.ToString();
            phonetb.Text = AGENTGDV.SelectedRows[0].Cells[4].Value.ToString();
            adresstb.Text = AGENTGDV.SelectedRows[0].Cells[3].Value.ToString();
            date.Text = AGENTGDV.SelectedRows[0].Cells[2].Value.ToString();
            gedercb.Text = AGENTGDV.SelectedRows[0].Cells[5].Value.ToString();
            passwordtb.Text = AGENTGDV.SelectedRows[0].Cells[6].Value.ToString();
            if (agenttb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AGENTGDV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
            agenttb.Text = "";
            phonetb.Text = "";
            adresstb.Text = "";
            date.Text = "";
            passwordtb.Text = "";
            gedercb.SelectedIndex =-1;
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (agenttb.Text == "" || passwordtb.Text == "" || phonetb.Text == "" || adresstb.Text == "" || gedercb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update AgentTbl set AgName=@AN,AgDOB=@AD, AgAdd=@AA, AgPhone=@AP,AgGen=@AG, AgPass=@APP where AgNum=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", agenttb.Text);
                    cmd.Parameters.AddWithValue("@AD", date.Value.Date);
                    cmd.Parameters.AddWithValue("@AA", adresstb.Text);
                    cmd.Parameters.AddWithValue("@AP", phonetb.Text);
                    cmd.Parameters.AddWithValue("@APP", passwordtb.Text);
                    cmd.Parameters.AddWithValue("@AG", gedercb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent updated Successfully!!!");
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
                    string Query = "delete from AgentTbl where AgNum = " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Deleted Successfully.");
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
    }
}
