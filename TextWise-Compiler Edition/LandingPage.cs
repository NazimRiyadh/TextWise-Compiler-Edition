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
    public partial class LandingPage : Form
    {
        public LandingPage()
        {
            InitializeComponent();
            // Set the form properties to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Fixed Single
            this.MaximizeBox = false; // Disable maximize box
        }

        private void LandingPage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of Form1
            LogGuest l = new LogGuest();

            // Hide the current (landing) form
            this.Hide();
               
            // Show Form1 as a dialog (modal)
            l.ShowDialog();


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            
            LogGuest l = new LogGuest();
            this.Hide();

            l.Show();

            

        }

        private void LandingPage_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}

