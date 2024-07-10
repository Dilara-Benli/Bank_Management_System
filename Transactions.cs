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
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-ODG3HU9;Initial Catalog=Bank_Management_System;Integrated Security=True");
        int balance;

        private void checkBalance()
        {
            connection.Open();
            string query = "select * from Account where AccountID="+checkBalanceTxt.Text+"";
            SqlCommand cmd = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                balanceLbl.Text = "Balance: " + dr["AccountBalance"].ToString();
                balance = Convert.ToInt32(dr["AccountBalance"].ToString());
            }
            connection.Close();
        }

        private void checkAvailableBalance()
        {
            //connection.Open();
            string query = "select * from Account where AccountID=" + transferFromTxt.Text + "";
            SqlCommand cmd = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                fromBalanceLbl.Text = "Balance: " + dr["AccountBalance"].ToString();
                balance = Convert.ToInt32(dr["AccountBalance"].ToString());
            }
            //connection.Close();
        }

        private void getNewBalance(string Account)
        {
            connection.Open();
            string query = "select * from Account where AccountID=" + Account + "";
            SqlCommand cmd = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                //balanceLbl.Text = "Balance: " + dr["AccountBalance"].ToString();
                balance = Convert.ToInt32(dr["AccountBalance"].ToString());
            }
            connection.Close();
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {
            if(checkBalanceTxt.Text == "")
            {
                MessageBox.Show("Hesap ID'nizi giriniz");
            }
            else
            {
                checkBalance();
                if(balanceLbl.Text == "Your Balance")
                {
                    MessageBox.Show("Hesap bulunamadı");
                    checkBalanceTxt.Text = "";
                }
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            balanceLbl.Text = "Your Balance";
        }

        private void Deposit()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Transactions(TransactionTypeID, AccountID, TransactionAmount, TransactionDate) values(@TT, @A, @TA, @TD)", connection);
                cmd.Parameters.AddWithValue("@TT", Convert.ToByte(1));
                cmd.Parameters.AddWithValue("@A", Convert.ToInt32(depositAccountIDTxt.Text));
                cmd.Parameters.AddWithValue("@TA", Convert.ToInt32(depositAmountTxt.Text));
                cmd.Parameters.AddWithValue("@TD", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void Withdraw()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Transactions(TransactionTypeID, AccountID, TransactionAmount, TransactionDate) values(@TT, @A, @TA, @TD)", connection);
                cmd.Parameters.AddWithValue("@TT", Convert.ToByte(2));
                cmd.Parameters.AddWithValue("@A", Convert.ToInt32(withdrawAccountIDTxt.Text));
                cmd.Parameters.AddWithValue("@TA", Convert.ToInt32(withdrawAmountTxt.Text));
                cmd.Parameters.AddWithValue("@TD", DateTime.Now);
                cmd.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void addBalance()
        {
            getNewBalance(transferToTxt.Text);
            int new_balance = balance + Convert.ToInt32(transferAmountTxt.Text);

            try
            {
                connection.Open();
                String query = "update Account set AccountBalance=@AB where AccountID=@AcKey";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@AB", new_balance);  //varchar
                cmd.Parameters.AddWithValue("@AcKey", Convert.ToInt32(transferToTxt.Text));

                cmd.ExecuteNonQuery();
                //MessageBox.Show("Para Yatırıldı");

                //depositAccountIDTxt.Text = "";
                //depositAmountTxt.Text = "";
                //balanceLbl.Text = "Your Balance";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void substractBalance()
        {
            getNewBalance(transferFromTxt.Text);
            int new_balance = balance - Convert.ToInt32(transferAmountTxt.Text);

            try
            {
                connection.Open();
                String query = "update Account set AccountBalance=@AB where AccountID=@AcKey";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@AB", new_balance);  //varchar
                cmd.Parameters.AddWithValue("@AcKey", Convert.ToInt32(transferFromTxt.Text));

                cmd.ExecuteNonQuery();
                //MessageBox.Show("Para Yatırıldı");

                //depositAccountIDTxt.Text = "";
                //depositAmountTxt.Text = "";
                //balanceLbl.Text = "Your Balance";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void depositBtn_Click(object sender, EventArgs e)
        {
            if (depositAccountIDTxt.Text == "" || depositAmountTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else
            {
                Deposit();
                //getNewBalance();
                getNewBalance(depositAccountIDTxt.Text);
                int new_balance = balance + Convert.ToInt32(depositAmountTxt.Text);

                try
                {
                    connection.Open();
                    String query = "update Account set AccountBalance=@AB where AccountID=@AcKey";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@AB", new_balance);  //varchar
                    cmd.Parameters.AddWithValue("@AcKey", Convert.ToInt32(depositAccountIDTxt.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Para Yatırıldı");

                    depositAccountIDTxt.Text = "";
                    depositAmountTxt.Text = "";
                    balanceLbl.Text = "Your Balance";
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void withdrawBtn_Click(object sender, EventArgs e)
        {
            if (withdrawAccountIDTxt.Text == "" || withdrawAmountTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else
            {
                
                //getNewBalance();
                getNewBalance(withdrawAccountIDTxt.Text);
                
                if (balance < Convert.ToInt32(withdrawAmountTxt.Text))
                {
                    MessageBox.Show("Yetersiz Bakiye");
                }
                else
                {
                    int new_balance = balance - Convert.ToInt32(withdrawAmountTxt.Text);
                    try
                    {
                        connection.Open();
                        String query = "update Account set AccountBalance=@AB where AccountID=@AcKey";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@AB", new_balance);  //varchar
                        cmd.Parameters.AddWithValue("@AcKey", Convert.ToInt32(withdrawAccountIDTxt.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Para Çekildi");

                        connection.Close();

                        Withdraw();

                        withdrawAccountIDTxt.Text = "";
                        withdrawAmountTxt.Text = "";
                        balanceLbl.Text = "Your Balance";

                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            if (transferFromTxt.Text == "")
            {
                MessageBox.Show("Hesap ID bilgisi eksik");
            }
            else
            {
                connection.Open();
                String query = "select count(*) from Account where AccountID='" + transferFromTxt.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    checkAvailableBalance();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Hesap ID bulunamadı");
                    transferFromTxt.Text = "";                  
                }
                connection.Close();
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            if (transferToTxt.Text == "")
            {
                MessageBox.Show("Hesap ID bilgisi eksik");
            }
            else
            {
                connection.Open();
                String query = "select count(*) from Account where AccountID='" + transferToTxt.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    //checkAvailableBalance();
                    MessageBox.Show("Hesap Bulundu");
                    connection.Close();
                    if(transferToTxt.Text == transferFromTxt.Text)
                    {
                        MessageBox.Show("Kaynak ve Hedef Hesaplar Aynı");
                        transferToTxt.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Hesap ID bulunamadı");
                    transferToTxt.Text = "";
                }
                connection.Close();
            }
        }

        private void Transfer()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Transfer(TransferSource, TransferDestination, TransferAmount, TransferDate) values(@TS, @TDE, @TA, @TDA)", connection);
                cmd.Parameters.AddWithValue("@TS", Convert.ToInt32(transferFromTxt.Text));
                cmd.Parameters.AddWithValue("@TDE", Convert.ToInt32(transferToTxt.Text));
                cmd.Parameters.AddWithValue("@TA", Convert.ToInt32(transferAmountTxt.Text));
                cmd.Parameters.AddWithValue("@TDA", DateTime.Now);
                MessageBox.Show("Transfer İşlemi Gerçekleştirildi");
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void transferBtn_Click(object sender, EventArgs e)
        {
            if(transferFromTxt.Text == "" || transferToTxt.Text == "" || transferAmountTxt.Text == "")
            {
                MessageBox.Show("Girilen bilgiler eksik");
            }
            else if (Convert.ToInt16(transferAmountTxt.Text) > balance)
            {
                MessageBox.Show("Yetersiz Bakiye");
            }
            else
            {
                Transfer();
                substractBalance();
                addBalance();

                transferToTxt.Text = "";
                transferFromTxt.Text = "";
                transferAmountTxt.Text = "";
            }
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            MainMenu obj = new MainMenu();
            obj.Show();
            this.Hide();
        }
    }
}
