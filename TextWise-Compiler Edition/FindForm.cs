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
    public partial class FindForm : Form
    {
        RichTextBox rt;
        public FindForm(RichTextBox rt)
        {
            InitializeComponent();
            this.rt = rt;
        }
        public static void Find(RichTextBox rtb, String word, Color color)
        {
            if (word == "")
            {
                return;
            }
            int s_start = rtb.SelectionStart, startIndex = 0, index;
            while ((index = rtb.Text.IndexOf(word, startIndex)) != -1)
            {
                rtb.Select(index, word.Length);
                rtb.SelectionBackColor = Color.Yellow;
                startIndex = index + word.Length;
            }
            rtb.SelectionStart = s_start;
            rtb.SelectionLength = 0;
            rtb.SelectionColor = Color.Black;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Find(rt, textBox1.Text, Color.Gray);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
