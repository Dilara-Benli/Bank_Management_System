using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Bank_Management_System
{
    public partial class Accounts : Form
    {
        public Accounts()
        {
            InitializeComponent();
            DisplayAccounts();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-ODG3HU9;Initial Catalog=Bank_Management_System;Integrated Security=True");

        private void DisplayAccounts()
        {
            connection.Open();
            //string query = "select * from Account";
            string query2 = "SELECT AccountID, A.CustomerID, CustomerName, CustomerLastName, A.AccountTypeID, AccountTypeName, AccountBalance " +
                "FROM Account A JOIN Customer C ON C.CustomerID = A.CustomerID JOIN AccountType AcT ON AcT.AccountTypeID = A.AccountTypeID";
            SqlDataAdapter adapter = new SqlDataAdapter(query2, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            AccountDGV.DataSource = ds.Tables[0];
            //AccountDGV.Columns[0].HeaderCell.Style.BackColor = Color.FromArgb(64,0,64);
            //AccountDGV.Columns[2].HeaderCell.Style.BackColor = Color.SkyBlue;
            //AccountDGV.Columns[1].HeaderCell.Style.BackColor = Color.SkyBlue;
            //AccountDGV.Columns[3].HeaderCell.Style.BackColor = Color.SkyBlue;
            connection.Close();
        }

        private void Reset()
        {
            CustomerIDTxt.Text = "";
            AccountTypeIDTxt.Text = "";
            //AccountBalanceTxt.Text = "";

        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if(CustomerIDTxt.Text == "" || AccountTypeIDTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into Account(CustomerID, AccountBalance, AccountTypeID) values(@CI, @AB, @AT)", connection);
                    cmd.Parameters.AddWithValue("@CI", Convert.ToInt32(CustomerIDTxt.Text));  //int
                    cmd.Parameters.AddWithValue("@AB", 0);  //int
                    cmd.Parameters.AddWithValue("@AT", Convert.ToByte(AccountTypeIDTxt.Text));  //tinyint
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hesap Eklendi");

                    connection.Close();

                    Reset();
                    DisplayAccounts();
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Hesap Seçiniz");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("delete from Account where AccountID=@AcKey", connection);
                    cmd.Parameters.AddWithValue("@AcKey", key);  
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hesap Silindi");

                    connection.Close();

                    Reset();
                    DisplayAccounts();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
            }
        }

        int key = 0;
        private void AccountDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerIDTxt.Text = AccountDGV.SelectedRows[0].Cells[1].Value.ToString();
            //AccountBalanceTxt.Text = AccountDGV.SelectedRows[0].Cells[6].Value.ToString();
            AccountTypeIDTxt.Text = AccountDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(CustomerIDTxt.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AccountDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustomerIDTxt.Text == "" || AccountTypeIDTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("update Account set CustomerID=@CI, AccountTypeID=@AT where AccountID=@AcKey", connection);
                    cmd.Parameters.AddWithValue("@CI", Convert.ToInt32(CustomerIDTxt.Text));  //int 
                    cmd.Parameters.AddWithValue("@AT", Convert.ToByte(AccountTypeIDTxt.Text));  //tinyint
                    cmd.Parameters.AddWithValue("@AcKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hesap Güncellendi");

                    connection.Close();

                    Reset();
                    DisplayAccounts();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }
    }
}
