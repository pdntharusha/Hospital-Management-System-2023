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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp12
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            // show the new form
            home mainForm = new home();
            mainForm.Show();

            // hide the current login form
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                comboBox1.SelectedItem == null ||
                comboBox2.SelectedItem == null ||
                dateTimePicker1.Value == dateTimePicker1.MinDate ||
                dateTimePicker2.Value.TimeOfDay == TimeSpan.Zero)
            {
                MessageBox.Show("Please fill in all information.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Appointment_Details VALUES (@doctor, @date, @time, @reason, @payment, @Patientno)", con);

            cmd.Parameters.AddWithValue("@doctor", comboBox1.Text);
            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@time", dateTimePicker2.Value.TimeOfDay);
            cmd.Parameters.AddWithValue("@reason", textBox1.Text);
            cmd.Parameters.AddWithValue("@payment", comboBox2.SelectedItem);
            cmd.Parameters.AddWithValue("@Patientno", textBox2.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
            textBox1.Clear();
            textBox2.Clear();
            comboBox2.SelectedIndex = -1;

            MessageBox.Show("All cleared..! Please Enter Next Appointment Details..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;");

                string Number = textBox2.Text;

                string query = "IF EXISTS (SELECT 1 FROM Appointment_Details WHERE Patientno = @Patientno) " +
                               "UPDATE Appointment_Details SET doctor = @doctor, date = @date, time = @time, reason = @reason, payment = @payment WHERE Patientno = @Patientno " +
                               "ELSE " +
                               "INSERT INTO Appointment_Details (doctor, date, time, reason, payment, Patientno) VALUES (@doctor, @date, @time, @reason, @payment, @Patientno)";
           
            // Appointment_Details is the database table name



                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@doctor", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@time", dateTimePicker2.Value.TimeOfDay);
                    cmd.Parameters.AddWithValue("@reason", textBox1.Text);
                    cmd.Parameters.AddWithValue("@payment", comboBox2.SelectedItem);
                    cmd.Parameters.AddWithValue("@Patientno", Number);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Data inserted or updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
                }
            

        }
    }
}
