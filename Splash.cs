using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_Management_System
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        int startP = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            myProgress.Value = startP;
            if (myProgress.Value == 100)
            {
                myProgress.Value = 0;
                timer1.Stop();
                Login obj = new Login();
                obj.Show();
                this.Hide();
            }
        }

        private void Splash1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

       
    }
}
