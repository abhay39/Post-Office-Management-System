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
    public partial class Money_Transfer : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\C#\gui\Post Office Management System\PostOfficeDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Money_Transfer()
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
            if (sendernametb.Text == "" || receivernabembt.Text == "" || raddress.Text == ""||saddress.Text=="")
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MoneyTrTbl (TDate, SName, RName,SAdd,SPhone,RPhone,TrCode) values(@TD,@SN,@RN,@SA,@SP,@RP,@T)", Con);
                    cmd.Parameters.AddWithValue("@TD", tdate.Value.Date);
                    cmd.Parameters.AddWithValue("@SN", sendernametb.Text);
                    cmd.Parameters.AddWithValue("@RN", receivernabembt.Text);
                    cmd.Parameters.AddWithValue("@SA", saddress.Text);
                    cmd.Parameters.AddWithValue("@SP", sendrphone.Text);
                    cmd.Parameters.AddWithValue("@RP", reseverphone.Text);
                    cmd.Parameters.AddWithValue("@T", seccode.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transfer Added Successfully!!!");
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
            string Query = "select * from MoneyTrTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            transfergdv.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            saddress.Text = "";
            raddress.Text = "";
            sendrphone.Text = "";
            reseverphone.Text = "";
            sendernametb.Text = "";
            receivernabembt.Text = "";
            seccode.Text = "";
            reseverphone.Text = "";
        }
        int key = 0;
        private void editbtn_Click(object sender, EventArgs e)
        {
            if (sendernametb.Text == "" || receivernabembt.Text == "" || saddress.Text == "")
            {
                MessageBox.Show("Missing Informations!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update MoneyTrTbl set TDate=@TD, SName=@SN, RName=@RN, SAdd=@SA, SPhone=@SP, RPhone=@RP, TrCode=@T where TrNo=@Key", Con);
                    cmd.Parameters.AddWithValue("@TD", tdate.Value.Date);
                    cmd.Parameters.AddWithValue("@SN", sendernametb.Text);
                    cmd.Parameters.AddWithValue("@RN", receivernabembt.Text);
                    cmd.Parameters.AddWithValue("@SA", saddress.Text);
                    cmd.Parameters.AddWithValue("@SP", sendrphone.Text);
                    cmd.Parameters.AddWithValue("@RP", reseverphone.Text);
                    cmd.Parameters.AddWithValue("@T", seccode.Text);
                    cmd.Parameters.AddWithValue("@Key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transfer Updated Successfully!!!");
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

        private void transfergdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sendernametb.Text = transfergdv.SelectedRows[0].Cells[2].Value.ToString();
            tdate.Text = transfergdv.SelectedRows[0].Cells[1].Value.ToString();
            receivernabembt.Text = transfergdv.SelectedRows[0].Cells[3].Value.ToString();
            //raddress.Text = transfergdv.SelectedRows[0].Cells[].Value.ToString();
            saddress.Text = transfergdv.SelectedRows[0].Cells[4].Value.ToString();
            reseverphone.Text = transfergdv.SelectedRows[0].Cells[5].Value.ToString();
            sendrphone.Text = transfergdv.SelectedRows[0].Cells[6].Value.ToString();
            seccode.Text = transfergdv.SelectedRows[0].Cells[7].Value.ToString();
            if (sendernametb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(transfergdv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void deletbtn_Click(object sender, EventArgs e)
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
                    string Query = "delete from MoneyTrTbl where TrNo = " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transfer Deleted Successfully.");
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
