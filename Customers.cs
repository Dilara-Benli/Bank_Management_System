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

namespace Bank_Management_System
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            DisplayCustomers();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-ODG3HU9;Initial Catalog=Bank_Management_System;Integrated Security=True");

        private void DisplayCustomers()
        {
            connection.Open();
            string query = "select * from Customer";
            /*
            string query2 = "SELECT C.CustomerID, CustomerName, CustomerLastName, CustomerGender, CustomerBirthDate, CustomerCity, " +
                "CustomerAddress, CustomerEmail, CustomerHomePhone, CustomerCellPhone, CustomerWorkPhone, " +
                "CustomerLoginUserName, CustomerLoginPassword  FROM Customer C JOIN CustomerLogin CL ON C.CustomerID = CL.CustomerID";
            */
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            AccountDGV.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void Reset()
        {
            CustomerNameTxt.Text = "";
            CustomerLastNameTxt.Text = "";
            CustomerBirthDateTxt.Text = "";
            CustomerAddressTxt.Text = "";
            CustomerGenderCbx.SelectedIndex = -1;
            CustomerEmailTxt.Text = "";
            CustomerCityTxt.Text = "";
            CustomerCellPhoneTxt.Text = "";
            CustomerWorkPhoneTxt.Text = "";
            CustomerHomePhoneTxt.Text = "";
            CustomerUserNameTxt.Text = "";
            CustomerPasswordTxt.Text = "";
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (CustomerNameTxt.Text == "" || CustomerLastNameTxt.Text == "" || CustomerBirthDateTxt.Text == "" || CustomerAddressTxt.Text == "" || CustomerGenderCbx.SelectedIndex == -1 || CustomerEmailTxt.Text == "" || CustomerCityTxt.Text == "" || CustomerCellPhoneTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customer(CustomerName, CustomerLastName, CustomerGender, CustomerBirthDate, CustomerCity, CustomerAddress, CustomerEmail, CustomerHomePhone, CustomerCellPhone, CustomerWorkPhone ) values(@CN, @CLN, @CG, @CBD, @CC, @CA, @CE, @CHP, @CCP, @CWP )", connection);
                    //SqlCommand cmd2 = new SqlCommand("insert into CustomerLogin(CustomerLoginUserName, CustomerLoginPassword) values (@CLU, @CLP)", connection);
                    //SqlCommand cmd3 = new SqlCommand("insert into CustomerLogin(CustomerID, CustomerLoginUserName, CustomerLoginPassword) values (@CusKey, @CLU, @CLP)", connection);
                    // @CN, @CLN, @CG, @CBD, @CC, @CA, @CE, @CHP, @CCP, @CWP
                    cmd.Parameters.AddWithValue("@CN", CustomerNameTxt.Text);  //varchar
                    cmd.Parameters.AddWithValue("@CLN", CustomerLastNameTxt.Text);  
                    cmd.Parameters.AddWithValue("@CG", CustomerGenderCbx.SelectedItem.ToString());  //cbx
                    cmd.Parameters.AddWithValue("@CBD", Convert.ToDateTime(CustomerBirthDateTxt.Text));  //date
                    cmd.Parameters.AddWithValue("@CC", CustomerCityTxt.Text);  
                    cmd.Parameters.AddWithValue("@CA", CustomerAddressTxt.Text);  
                    cmd.Parameters.AddWithValue("@CE", CustomerEmailTxt.Text);  
                    cmd.Parameters.AddWithValue("@CHP", CustomerHomePhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@CCP", CustomerCellPhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@CWP", CustomerWorkPhoneTxt.Text);


                    //cmd2.Parameters.AddWithValue("@CusKey", key);
                    //cmd2.Parameters.AddWithValue("CLU", CustomerUserNameTxt.Text);
                    //cmd2.Parameters.AddWithValue("CLP", CustomerPasswordTxt.Text);

                    cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();
                    MessageBox.Show("Müşteri Eklendi");

                    connection.Close();

                    
                    Reset();
                    DisplayCustomers();

                }
                catch (Exception exception)
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
                MessageBox.Show("Müşteri seçiniz");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("delete from Customer where CustomerID=@CusKey", connection);
                    //SqlCommand cmd2 = new SqlCommand("delete from CustomerLogin where CustomerID=@CusKey", connection);

                    cmd.Parameters.AddWithValue("@CusKey", key); 
                    //cmd2.Parameters.AddWithValue("@CusKey", key);

                    cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();
                    MessageBox.Show("Müşteri Silindi");

                    connection.Close();

                    Reset();
                    DisplayCustomers();
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
            CustomerNameTxt.Text = AccountDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustomerLastNameTxt.Text = AccountDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustomerGenderCbx.SelectedItem = AccountDGV.SelectedRows[0].Cells[3].Value.ToString();

            DateTime birthDate = Convert.ToDateTime(AccountDGV.SelectedRows[0].Cells[4].Value);
            CustomerBirthDateTxt.Text = birthDate.ToShortDateString();

            //CustomerBirthDateTxt.Text = AccountDGV.SelectedRows[0].Cells[4].Value.ToString();
            CustomerCityTxt.Text = AccountDGV.SelectedRows[0].Cells[5].Value.ToString();
            CustomerAddressTxt.Text = AccountDGV.SelectedRows[0].Cells[6].Value.ToString();
            CustomerEmailTxt.Text = AccountDGV.SelectedRows[0].Cells[7].Value.ToString();
            CustomerHomePhoneTxt.Text = AccountDGV.SelectedRows[0].Cells[8].Value.ToString();
            CustomerCellPhoneTxt.Text = AccountDGV.SelectedRows[0].Cells[9].Value.ToString();
            CustomerWorkPhoneTxt.Text = AccountDGV.SelectedRows[0].Cells[10].Value.ToString();
            //CustomerUserNameTxt.Text = AccountDGV.SelectedRows[0].Cells[11].Value.ToString();
            //CustomerPasswordTxt.Text = AccountDGV.SelectedRows[0].Cells[12].Value.ToString(); 

            if (CustomerNameTxt.Text == "")
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
            if (CustomerNameTxt.Text == "" || CustomerLastNameTxt.Text == "" || CustomerBirthDateTxt.Text == "" || CustomerAddressTxt.Text == "" || CustomerGenderCbx.SelectedIndex == -1 || CustomerEmailTxt.Text == "" || CustomerCityTxt.Text == "" || CustomerCellPhoneTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("update Customer set CustomerName=@CN, CustomerLastName=@CLN, CustomerGender=@CG, CustomerBirthDate=@CBD, CustomerCity=@CC, CustomerAddress=@CA, CustomerEmail=@CE, CustomerHomePhone=@CHP, CustomerCellPhone=@CCP, CustomerWorkPhone=@CWP where CustomerID=@CusKey", connection);
                    //SqlCommand cmd2 = new SqlCommand("update CustomerLogin set CustomerLoginUserName=@CLUN, CustomerLoginPassword=@CLP  where CustomerID=@CusKey", connection);

                    // @CN, @CLN, @CG, @CBD, @CC, @CA, @CE, @CHP, @CCP, @CWP
                    cmd.Parameters.AddWithValue("@CN", CustomerNameTxt.Text);  //varchar
                    cmd.Parameters.AddWithValue("@CLN", CustomerLastNameTxt.Text);
                    cmd.Parameters.AddWithValue("@CG", CustomerGenderCbx.SelectedItem.ToString());  //cbx
                    cmd.Parameters.AddWithValue("@CBD", Convert.ToDateTime(CustomerBirthDateTxt.Text));  //date
                    cmd.Parameters.AddWithValue("@CC", CustomerCityTxt.Text);
                    cmd.Parameters.AddWithValue("@CA", CustomerAddressTxt.Text);
                    cmd.Parameters.AddWithValue("@CE", CustomerEmailTxt.Text);
                    cmd.Parameters.AddWithValue("@CHP", CustomerHomePhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@CCP", CustomerCellPhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@CWP", CustomerWorkPhoneTxt.Text);
                    //cmd2.Parameters.AddWithValue("@CLUN", CustomerUserNameTxt.Text);
                    //cmd2.Parameters.AddWithValue("@CLP", CustomerPasswordTxt.Text);

                    cmd.Parameters.AddWithValue("@CusKey", key);
                    //cmd2.Parameters.AddWithValue("@CusKey", key);

                    cmd.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();
                    MessageBox.Show("Müşteri Güncellendi");

                    connection.Close();

                    Reset();
                    DisplayCustomers();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
