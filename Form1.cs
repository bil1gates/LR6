using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace органайзер
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "123")
            {
                Form2 form = new Form2();
                form.Show();
                Hide();
                Form1 form1 = new Form1();
                form1.Hide();
            }
            else
            {
                MessageBox.Show("Неверный код");
            }
            

        }

    }
}
