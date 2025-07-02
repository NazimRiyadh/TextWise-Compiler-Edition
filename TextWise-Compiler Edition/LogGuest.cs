using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextWise_Compiler_Edition
{
    public partial class LogGuest : Form
    {
        public LogGuest()
        {
            InitializeComponent();
        }

        private void Guest_Click(object sender, EventArgs e)
        {
            GuestUsers f = new GuestUsers();
            f.Show();
            this.Hide();
            
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            


        }

        private void LogGuest_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            perks p=new perks();
            p.Show();
        }
    }
}
