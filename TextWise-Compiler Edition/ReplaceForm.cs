﻿using System;
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
    public partial class ReplaceForm : Form
    {

        RichTextBox rt;
        public ReplaceForm(RichTextBox rt)
        {
            InitializeComponent();
            this.rt = rt;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            rt.Text = rt.Text.Replace(textBox1.Text, textBox2.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
