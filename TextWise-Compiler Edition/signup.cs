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
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
        }

        private void signup_Load(object sender, EventArgs e)
        {

        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            if(txtname.Text=="" || txtusername.Text == "" || txtpass.Text == ""|| txtconpass.Text == ""||
                txtconpass.Text == ""|| txtphone.Text == "")
                    {
                MessageBox.Show("Fill up all the blanks!");
                return;
                    }
             if(txtpass.Text!=txtconpass.Text)
            {
                MessageBox.Show("Password doesn't match");
                return;
            }
            string error;
            string query = "Select * from userinfo where email=' " + txtemail.Text + "'";
            DataTable dt = database_Access.getData(query,out error);
            if(String.IsNullOrEmpty(error))
            {
                if(dt.Rows.Count==0)
                {
                    string q = "Insert into userInfo (username, password, email, name, phone) values " +
                        "('" + txtusername.Text + "','" + txtpass.Text + "','" + txtemail.Text + "','" + txtname.Text + "','" + txtphone.Text + "')";
                    database_Access.InsertData(q,out error);
                    if(String.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("Account created");
                    }
                    else
                    {
                        MessageBox.Show(error);
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnback_Click(object sender, EventArgs e)
        {

        }
    }
}
