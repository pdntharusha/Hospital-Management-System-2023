using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            // show the new form
            home mainForm = new home();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Patient_Medical_History values (@phoneno,@reason,@coditions,@allergies,@habits,@smoking,@alcohol,@history)", conn);
            cmd.Parameters.AddWithValue("@phoneno", textBox1.Text);
            cmd.Parameters.AddWithValue("@reason", textBox2.Text);
            cmd.Parameters.AddWithValue("@coditions", textBox3.Text);
            cmd.Parameters.AddWithValue("@allergies", textBox4.Text);
            cmd.Parameters.AddWithValue("@habits", textBox5.Text);
            cmd.Parameters.AddWithValue("@smoking", comboBox1.Text);
            cmd.Parameters.AddWithValue("@alcohol", comboBox2.Text);
            cmd.Parameters.AddWithValue("@history", textBox6.Text);
            cmd.ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("Data insert Successfully");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();  
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;

            MessageBox.Show("All cleared..! Please Enter next patient Details..!");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");

            string phoneNumber = textBox1.Text;

            string query = "IF EXISTS (SELECT 1 FROM Patient_Medical_History WHERE phoneno = @phoneno) " +
                           "UPDATE Patient_Medical_History SET reason = @reason, conditions = @conditions, allergies = @allergies, habits = @habits, smoking = @smoking, alcohol = @alcohol, history = @history " +
                           "ELSE " +
                           "INSERT INTO Patient_Medical_History (phoneno, reason, conditions, allergies, habits, smoking, alcohol, history) " +
                           "VALUES (@phoneno, @reason, @conditions, @allergies, @habits, @smoking, @alcohol, @history)";
            //Patient_Medical_History is the datebase table name
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@phoneno", phoneNumber);
                cmd.Parameters.AddWithValue("@reason", textBox2.Text);
                cmd.Parameters.AddWithValue("@conditions", textBox3.Text);
                cmd.Parameters.AddWithValue("@allergies", textBox4.Text);
                cmd.Parameters.AddWithValue("@habits", textBox5.Text);
                cmd.Parameters.AddWithValue("@smoking", comboBox1.Text);
                cmd.Parameters.AddWithValue("@alcohol", comboBox2.Text);
                cmd.Parameters.AddWithValue("@history", textBox6.Text);

                connection.Open();
                cmd.ExecuteNonQuery();  
                connection.Close();


                MessageBox.Show("Data inserted or updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
