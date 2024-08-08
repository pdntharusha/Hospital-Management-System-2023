using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 mainForm = new Form3();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 mainForm = new Form4();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 mainForm = new Form5();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 mainForm = new Form6();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // show the new form
            Form1 mainForm = new Form1();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // show the new form
           Form7 mainForm = new Form7();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
