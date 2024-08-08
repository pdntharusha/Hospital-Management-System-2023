using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form7 : Form
    {
        private SqlConnection connection;
        private string connectionString = "Data Source=DESKTOP-AJ2IFIG\\SQLEXPRESS;Initial Catalog=Add Patients;Integrated Security=true;";
        private DataTable dataTable; // DataTable to store search results

        public Form7()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            connection = new SqlConnection(connectionString);
            dataTable = new DataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show the new form
            home mainForm = new home();
            mainForm.Show();

            // Hide the current login form
            this.Hide();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // Initialize DataGridView
            dataGridView1.AutoGenerateColumns = true;

            // Bind DataGridView to DataTable
            dataGridView1.DataSource = dataTable;

            // Populate the ComboBox with table names
            comboBox1.Items.Add("Appointment_Details"); // Add more table names as needed
            comboBox1.Items.Add("Doctors_Registration");
            comboBox1.Items.Add("Patient_Medical_History");
            comboBox1.Items.Add("Patient_Registration");
            // ...

            comboBox2.Items.Add("Appointment_Details"); // Add more table names as needed
            comboBox2.Items.Add("Doctors_Registration");
            comboBox2.Items.Add("Patient_Medical_History");
            comboBox2.Items.Add("Patient_Registration");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Perform a search based on user input
            string searchQuery = textBox1.Text; // Get the user's search criteria from a TextBox
            SearchData(searchQuery);
        }

        private void SearchData(string searchReason)
        {
            // Check if an item is selected in the ComboBox
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a table from the ComboBox.");
                return;
            }

            // Get the selected table name from the ComboBox
            string selectedTableName = comboBox2.SelectedItem.ToString();

            // Determine the column to search based on the selected table
            string searchColumn;
            switch (selectedTableName)
            {
                case "Appointment_Details":
                    searchColumn = "patientno"; // Replace 'patientno' with the actual column name for this table
                    break;
                case "Doctors_Registration":
                    searchColumn = "phoneno"; // Replace 'patientno' with the actual column name for this table
                    break;
                case "Patient_Medical_History":
                    searchColumn = "phoneno"; // Replace 'phoneno' with the actual column name for this table
                    break;
                case "Patient_Registration":
                    searchColumn = "phoneNO"; // Replace 'phoneNo' with the actual column name for this table
                    break;
                // Add more cases for other table names as needed
                default:
                    MessageBox.Show("Invalid table selected.");
                    return;
            }

            // Clear the previous data in the DataTable
            dataTable.Clear();

            // Use the selected table name and search column in your SQL query with a WHERE clause for the search
            string query = $"SELECT * FROM {selectedTableName} WHERE {searchColumn} LIKE @searchReason";

            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Add a parameter for the search criteria
                    cmd.Parameters.AddWithValue("@searchReason", "%" + searchReason + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable); // Fill the DataTable with data from the selected table and search criteria
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Refresh the DataGridView to display the updated data
            dataGridView1.DataSource = dataTable;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 mainForm = new Form7();
            mainForm.Show();

            // Hide the current login form
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select Information You Needed...!");
                return; // Exit the method early
            }

            // Get the selected table name from the ComboBox
            string selectedTableName = comboBox1.SelectedItem.ToString();

            // Clear the previous data in the DataTable
            dataTable.Clear();

            // Use the selected table name in your SQL query
            string query = $"SELECT * FROM {selectedTableName}";

            // Execute the query and update the DataGridView
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable); // Fill the DataTable with data from the selected table
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Perform a search based on user input
            string searchQuery = textBox1.Text; // Get the user's search criteria from a TextBox

            // Delete data from all tables based on the search criteria
            DeleteDataFromAllTables(searchQuery);
        }

        private void DeleteDataFromAllTables(string searchReason)
        {
            // Display a confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to delete data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                // User confirmed the deletion

                // Loop through the tables and delete data based on the search criteria in the specified column
                string[] tableNames = { "Appointment_Details", "Doctors_Registration", "Patient_Medical_History", "Patient_Registration" };

                foreach (string tableName in tableNames)
                {

                    string searchColumn;

                    switch (tableName)
                    {
                        case "Appointment_Details":
                            searchColumn = "patientno";
                            break;
                        case "Doctors_Registration":
                            searchColumn = "phoneno";
                            break;
                        case "Patient_Medical_History":
                            searchColumn = "phoneno";
                            break;
                        case "Patient_Registration":
                            searchColumn = "phoneNO";
                            break;
                        default:
                            continue; // Skip invalid table names
                    }

                    // Construct the SQL query to delete data from the table based on the search criteria in the specified column
                    string query = $"DELETE FROM {tableName} WHERE {searchColumn} LIKE @searchReason";

                    try
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            // Add a parameter for the search criteria
                            cmd.Parameters.AddWithValue("@searchReason", "%" + searchReason + "%");

                            cmd.ExecuteNonQuery(); // Execute the DELETE query
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting data from {tableName}: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                // Refresh the DataGridView to reflect the changes in data
                LoadData();
            }
            else if (result == DialogResult.Cancel)
            {
                // User canceled the deletion, no action required
            }


        }
        private void LoadData()
        {
            try
            {
                // Clear the previous data in the DataTable
                dataTable.Clear();

                // Get data from the database and fill the DataTable
                string selectedTableName = comboBox2.SelectedItem.ToString(); // Use the selected table name
                string query = $"SELECT * FROM {selectedTableName}";

                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable); // Fill the DataTable with data from the selected table
                }
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                connection.Close();
            }

            // Set the DataTable as the DataSource for the DataGridView
            dataGridView1.DataSource = dataTable;
        }


    }
}
