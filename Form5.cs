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
    public partial class Form5 : Form
    {
        public Form5()
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

        private void button2_Click(object sender, EventArgs e)
        {

           
                if (string.IsNullOrEmpty(textBox1.Text) ||
                    string.IsNullOrEmpty(textBox2.Text) ||
                    comboBox1.SelectedItem == null ||
                    string.IsNullOrEmpty(textBox4.Text) ||
                    dateTimePicker1.Value == dateTimePicker1.MinDate ||
                    string.IsNullOrEmpty(textBox3.Text) ||
                    string.IsNullOrEmpty(textBox5.Text) ||
                    string.IsNullOrEmpty(textBox6.Text) ||
                    string.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("Please fill in all information.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");
                connection.Open();

                string query = "INSERT INTO Doctors_Registration (name, age, gender, phoneno, dob, medicalno, spec, qualifi, hospital) " +
                               "VALUES (@name, @age, @gender, @phoneno, @dob, @medicalno, @spec, @qualifi, @hospital)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt32(textBox2.Text));
                    cmd.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@phoneno", textBox3.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@medicalno", textBox4.Text);
                    cmd.Parameters.AddWithValue("@spec", textBox5.Text);
                    cmd.Parameters.AddWithValue("@qualifi", textBox6.Text);
                    cmd.Parameters.AddWithValue("@hospital", textBox7.Text);

                    cmd.ExecuteNonQuery();
                }

                connection.Close();

                MessageBox.Show("Doctor data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedItem = null;
            textBox3.Clear();
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

            MessageBox.Show("All cleared..! Please Enter Next Doctor Registration Details..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;"))
                {
                    connection.Open();

                    string query = "IF EXISTS (SELECT 1 FROM Doctors_Registration WHERE phoneno = @phoneno) " +
                                   "UPDATE Doctors_Registration SET name = @name, age = @age, gender = @gender, dob = @dob, medicalno = @medicalno, spec = @spec, qualifi = @qualifi, hospital = @hospital WHERE phoneno = @phoneno " +
                                   "ELSE " +
                                   "INSERT INTO Doctors_Registration (name, age, gender, phoneno, dob, medicalno, spec, qualifi, hospital) VALUES (@name, @age, @gender, @phoneno, @dob, @medicalno, @spec, @qualifi, @hospital)";
                //Doctors_Registration is the table name (database)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@age", Convert.ToInt32(textBox2.Text));
                        cmd.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@phoneno", textBox3.Text);
                        cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@medicalno", textBox4.Text);
                        cmd.Parameters.AddWithValue("@spec", textBox5.Text);
                        cmd.Parameters.AddWithValue("@qualifi", textBox6.Text);
                        cmd.Parameters.AddWithValue("@hospital", textBox7.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Doctor data inserted or updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
