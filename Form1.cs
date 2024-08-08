using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp12
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                passwordbox.UseSystemPasswordChar = false; // hide the password characters
            }
            else
            {
                passwordbox.UseSystemPasswordChar = true; // show the password characters
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = usernamebox.Text;
            string password = passwordbox.Text;

            //  logic here
            if (username == "p" && password == "p")
            {   
                // show the new form
                home mainForm = new home();
                mainForm.Show();

                // hide the current login form
                this.Hide();
            }
            else
            {   // Message show 
                MessageBox.Show("Login failed. Please check your Username and Password.", "Incorrect Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   // exit form the login from
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void passwordbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
