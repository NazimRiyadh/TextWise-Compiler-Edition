using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TextWise_Compiler_Edition;

namespace To_Do_List_App
{
    public partial class ToDoList : Form
    {
        public string myyid;
        private DataTable todoList = new DataTable();
        private bool isEditing = false;

        private string connectionString = "Data Source=DESKTOP-VGSIRKN\\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";

        public ToDoList()
        {
            InitializeComponent();
            InitializeToDoList();
        }

        public ToDoList(string id)
        {
            myyid = id;

            InitializeComponent();
            InitializeToDoList();
        }

        private void InitializeToDoList()
        {
            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");
            todoList.Columns.Add("Time");

            toDoListView.DataSource = todoList;

            LoadTasksFromDatabase();
        }

        private void toDoListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                titleTextBox.Text = toDoListView.Rows[e.RowIndex].Cells[0].Value.ToString();
                descriptionTextbox.Text = toDoListView.Rows[e.RowIndex].Cells[1].Value.ToString();
                //txttime.Text = toDoListView.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void ToDoList_Load(object sender, EventArgs e)
        {
            string error;
            try
            {
                // Use parameterized query to prevent SQL injection
                string query = "SELECT title, description, time FROM todo WHERE userid = @userid";

                // Use parameters to avoid SQL injection and improve security
                SqlParameter[] parameters = { new SqlParameter("@userid", SqlDbType.Int) };
                parameters[0].Value = int.Parse(myyid);

                DataTable dt = database_Access.getData(query, parameters, out error);

                if (String.IsNullOrEmpty(error))
                {
                    toDoListView.AutoGenerateColumns = false;
                    toDoListView.DataSource = dt;
                    toDoListView.Refresh();
                    toDoListView.ClearSelection();
                }
                else
                {
                    MessageBox.Show($"Error loading tasks: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that the required fields are not empty
                if (string.IsNullOrEmpty(titleTextBox.Text) || string.IsNullOrEmpty(descriptionTextbox.Text))
                {
                    MessageBox.Show("Please fill in all fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Construct the insert query with parameters
                string insertQuery = "INSERT INTO todo (userid, title, description, time) VALUES (@userid, @title, @description, @time)";

                // Open a new database connection
                using (SqlConnection connection = new SqlConnection(database_Access.connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand with parameters
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userid", myyid);
                        command.Parameters.AddWithValue("@title", titleTextBox.Text);
                        command.Parameters.AddWithValue("@description", descriptionTextbox.Text);

                        // Automatically set the time to the current date and time
                        command.Parameters.AddWithValue("@time", DateTime.Now);

                        // Execute the insert query
                        command.ExecuteNonQuery();
                    }

                    // Display success message
                    MessageBox.Show("Task saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Refresh the tasks in the DataGridView
                LoadTasksFromDatabase();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (toDoListView.SelectedRows.Count > 0)
            {
                string selectedTitle = toDoListView.SelectedRows[0].Cells[0].Value.ToString();
                string error;

                string deleteQuery = $"DELETE FROM todo WHERE userid={myyid} AND title='{selectedTitle}'";
                database_Access.InsertData(deleteQuery, out error);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Task deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error deleting task: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadTasksFromDatabase();
                ClearFields();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (toDoListView.SelectedRows.Count > 0)
            {
                isEditing = true;
                saveButton.Text = "Update";
            }
            else
            {
                MessageBox.Show("Please select a task to edit.", "No Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadTasksFromDatabase()
        {
            string error;
            string query = $"SELECT title, description, time FROM todo WHERE userid={myyid}";
            DataTable dt = database_Access.getData(query, out error);

            if (string.IsNullOrEmpty(error))
            {
                toDoListView.AutoGenerateColumns = false;
                toDoListView.DataSource = dt;
                toDoListView.Refresh();
                toDoListView.ClearSelection();
            }
            else
            {
                MessageBox.Show($"Error loading tasks: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            titleTextBox.Clear();
            descriptionTextbox.Clear();
            //txttime.Clear();

            isEditing = false;
            saveButton.Text = "Save";
        }

        private void search_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that the search field is not empty
                if (string.IsNullOrEmpty(searchTextBox.Text))
                {
                    MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Construct the search query
                string searchQuery = $"SELECT title, description, time FROM todo WHERE userid={myyid} AND (title LIKE '%{searchTextBox.Text}%' OR description LIKE '%{searchTextBox.Text}%')";

                // Load the search results from the database
                string error;
                DataTable dt = database_Access.getData(searchQuery, out error);

                if (string.IsNullOrEmpty(error))
                {
                    toDoListView.AutoGenerateColumns = false;
                    toDoListView.DataSource = dt;
                    toDoListView.Refresh();
                    toDoListView.ClearSelection();

                    // Display a message if no results were found
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching tasks found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show($"Error searching tasks: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
