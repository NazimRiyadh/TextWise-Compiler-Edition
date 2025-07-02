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

namespace TextWise_Compiler_Edition
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void signup_Click(object sender, EventArgs e)
        {
            signup s = new signup();
            s.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

            if (txtid.Text == "" || txtpass.Text == "")
            {
                MessageBox.Show("username or password is blank!");
                return;
            }

            string error;
            string query = "Select * from userInfo where email='" + txtid.Text + "'";
            DataTable dt = database_Access.getData(query, out error);
            if (String.IsNullOrEmpty(error))
            {
                if (dt.Rows.Count == 0)

                {
                    MessageBox.Show("Invalid E-mail");
                    return;
                }

                if (txtpass.Text == dt.Rows[0][2].ToString())
                {
                    Form1 f = new Form1(dt.Rows[0][0].ToString());
                    f.ShowDialog();
                    this.Hide();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            signup s = new signup();
            s.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "" || txtpass.Text == "")
            {
                MessageBox.Show("username or password is blank!");
                return;
            }

            string error;
            string query = "Select * from userInfo where email='" + txtid.Text + "'";
            DataTable dt = database_Access.getData(query, out error);
            if (String.IsNullOrEmpty(error))
            {
                if (dt.Rows.Count == 0)

                {
                    MessageBox.Show("Invalid E-mail");
                    return;
                }

                if (txtpass.Text == dt.Rows[0][2].ToString())
                {
                    Form1 f = new Form1(dt.Rows[0][0].ToString());
                    f.ShowDialog();
                    this.Hide();
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
