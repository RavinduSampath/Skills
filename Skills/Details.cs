using Skills.Repositoris;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Threading;

namespace Skills
{
    public partial class Details : Form
    {
        // Constructor
        public Details()
        {
            InitializeComponent();
            ReadStudents(); // Load students on form initialization
            label2.Text = Count().ToString(); // Display the number of students
            
        }

        // Method to read and display students in the DataGridView
        private void ReadStudents()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNo");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("DateOfBirth");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Address");
            dt.Columns.Add("Email");
            dt.Columns.Add("mobilePhone");
            dt.Columns.Add("HomePhone");
            dt.Columns.Add("ParentName");
            dt.Columns.Add("NIC");
            dt.Columns.Add("ContactNo");
             // Create a DataTable for students

            var repo = new StudentRepository();
            var students = repo.GetAllStudents(); // Fetch all students from the repository

            // Populate the DataTable with student data
            foreach (var student in students)
            {
                var row = dt.NewRow();
                row["RegNo"] = student.regNo;
                row["FirstName"] = student.firstName;
                row["LastName"] = student.lastName;
                row["DateOfBirth"] = student.dateOfBirth.ToShortDateString(); // Format DateTime
                row["Gender"] = student.gender;
                row["Address"] = student.address;
                row["Email"] = student.email;
                row["mobilePhone"] = student.mobilePhone;
                row["HomePhone"] = student.homePhone;
                row["ParentName"] = student.parentName;
                row["NIC"] = student.nic;
                row["ContactNo"] = student.contactNo;
                dt.Rows.Add(row);
            }

            // Bind the DataTable to the DataGridView
           this.dataGridView1.DataSource = dt;
        }

        

        // Navigate to the Home form
        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        // Navigate to the Login form
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        // Exit the application
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Edit selected student
        private void button2_Click(object sender, EventArgs e)
        {
            var val = dataGridView1.SelectedRows[0].Cells["RegNo"].Value;
            if (val == null || val.ToString().Length == 0) return;

            int regNo = int.Parse(val.ToString());
            
            var repo = new StudentRepository();
            var student = repo.GetStudentByRegNo(regNo);

            if (student == null) return;

            Home home = new Home();
            home.EditStudent(student);
            this.Hide();
            if (home.ShowDialog() == DialogResult.OK)
            {
                ReadStudents(); // Refresh the student list after editing
            }



        }

        // Delete selected student
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return; // Check if a row is selected

            var val = dataGridView1.SelectedRows[0].Cells["RegNo"].Value;
            if (val == null || val.ToString().Length == 0) return;

            if (int.TryParse(val.ToString(), out int regNo))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Student?", "Delete Student", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var repo = new StudentRepository();
                    if (repo.DeleteStudent(regNo)) // Check if delete operation was successful
                    {
                        MessageBox.Show("Student deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error deleting student.");
                    }
                    ReadStudents(); // Refresh the student list after deletion
                }
            }
        }
        public int Count()
        {
            var repo = new StudentRepository();
            return repo.Count();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
