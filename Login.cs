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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-ODG3HU9;Initial Catalog=Bank_Management_System;Integrated Security=True");

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            userNameTxt.Text = "";
            passwordTxt.Text = "";
            RoleCbx.SelectedIndex = -1;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if(RoleCbx.SelectedIndex == -1)
            {
                MessageBox.Show("Rolünüzü seçiniz");
            }else if(RoleCbx.SelectedIndex == 1)
            {
                if(userNameTxt.Text == "" || passwordTxt.Text == "")
                {
                    MessageBox.Show("Kullanıcı adı ve şifreyi giriniz");
                }
                else
                {
                    connection.Open();
                    String query = "select count(*) from AdminLogin where AdminLoginUserName='"+userNameTxt.Text+"' and AdminLoginPassword='"+passwordTxt.Text+"'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query,connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if(dt.Rows[0][0].ToString() == "1")
                    {
                        Customers obj = new Customers();
                        obj.Show();
                        this.Hide();
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Girilen kullanıcı adı veya şifre yanlış");
                        userNameTxt.Text = "";
                        passwordTxt.Text = "";
                    }
                    connection.Close();
                }
            }
            else
            {
                if (userNameTxt.Text == "" || passwordTxt.Text == "")
                {
                    MessageBox.Show("Kullanıcı adı ve şifreyi giriniz");
                }
                else
                {
                    connection.Open();
                    String query = "select count(*) from CustomerLogin where CustomerLoginUserName='" + userNameTxt.Text + "' and CustomerLoginPassword='" + passwordTxt.Text + "'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MainMenu obj = new MainMenu();
                        obj.Show();
                        this.Hide();
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Girilen kullanıcı adı veya şifre yanlış");
                        userNameTxt.Text = "";
                        passwordTxt.Text = "";
                    }
                    connection.Close();
                }
            }
        }
    }
}
