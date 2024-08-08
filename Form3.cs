using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox5.Text) ||
                comboBox1.SelectedItem == null ||
                string.IsNullOrEmpty(textBox6.Text) ||
                dateTimePicker1.Value == dateTimePicker1.MinDate)
            {
                MessageBox.Show("Please fill in all information.");
                return;
            }

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Patient_Registration VALUES (@Name, @Age, @Address, @Email, @Gender, @PhoneNo, @Details)", con);

            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(textBox3.Text)); 
            cmd.Parameters.AddWithValue("@Address", textBox2.Text);
            cmd.Parameters.AddWithValue("@Email", textBox5.Text);
            cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox6.Text); 
            cmd.Parameters.AddWithValue("@Details", dateTimePicker1.Value);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // show the new form
            home mainForm = new home();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedItem = null;

            MessageBox.Show("All cleared..! Please Enter next patient Details..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");

                string phoneNumber = textBox6.Text;
                int age;

                if (!int.TryParse(textBox3.Text, out age))
                {
                    MessageBox.Show("Invalid Age. Please enter a valid number.");
                    return;
                }

                string query = "IF EXISTS (SELECT 1 FROM Patient_Registration WHERE PhoneNo = @PhoneNo) " +
                               "UPDATE Patient_Registration SET Name = @Name, Age = @Age, Address = @Address, Email = @Email, Gender = @Gender, Details = @Details WHERE PhoneNo = @PhoneNo " +
                               "ELSE " +
                               "INSERT INTO Patient_Registration (Name, Age, Address, Email, Gender, PhoneNo, Details) VALUES (@Name, @Age, @Address, @Email, @Gender, @PhoneNo, @Details)";
               
                                //Patient_Registration is the database table name

            using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Address", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Gender", comboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("@PhoneNo", phoneNumber);
                    cmd.Parameters.AddWithValue("@Details", dateTimePicker1.Text);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Data inserted or updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
           


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
