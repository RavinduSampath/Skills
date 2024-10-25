using Skills.Models;
using Skills.Repositoris;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Skills
{
    public partial class Home : Form
    {
        public int RegNo { get; set; }

        public Home()
        {
            InitializeComponent();
        }
        private int regNo;
        public void EditStudent(Student student)
        {
           this.Text = "Edit Student";

           textBox1.Text = student.firstName;
            textBox2.Text = student.lastName;
            dateTimePicker1.Value = student.dateOfBirth;
            textBox3.Text = student.address;
            textBox4.Text = student.email;
            textBox5.Text = student.mobilePhone;
            textBox6.Text = student.homePhone;
            textBox7.Text = student.parentName;
            textBox8.Text = student.nic;
            textBox9.Text = student.contactNo;
            if (student.gender == "Male")
            {
                radioButtonMale.Checked = true;
            }
            else if (student.gender == "Female")
            {
                radioButtonFemale.Checked = true;
            }

            this.RegNo = student.regNo;
        }


        private void label4_Click(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e) { }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new Student object
            Student student = new Student();

            // Input validation
            if (!ValidateInputs())
            {
                MessageBox.Show("Please fill in all required fields correctly.");
                return;
            }

            // Get selected gender
            string selectedGender = GetSelectedGender();

            // Populate student properties
            student.regNo = this.RegNo;
            student.firstName = textBox1.Text;
            student.lastName = textBox2.Text;
            student.dateOfBirth = dateTimePicker1.Value;
            student.gender = selectedGender;
            student.address = textBox3.Text;
            student.email = textBox4.Text;
            student.mobilePhone = textBox5.Text;
            student.homePhone = textBox6.Text;
            student.parentName = textBox7.Text;
            student.nic = textBox8.Text;
            student.contactNo = textBox9.Text;

            var repo = new StudentRepository();

            try
            {
                if (RegNo == 0) // New student
                {
                    repo.CreateStudent(student);
                    MessageBox.Show("Student added successfully!");
                }
                else // Existing student update
                {
                    repo.UpdateStudent(student);
                    MessageBox.Show("Student updated successfully!");
                }
                ShowDetailsForm(); // Navigate to the Home form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving student: " + ex.Message);
            }
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrWhiteSpace(textBox1.Text) &&
                   !string.IsNullOrWhiteSpace(textBox2.Text) &&
                   int.TryParse(textBox9.Text, out _) &&
                   int.TryParse(textBox8.Text, out _) &&
                   !string.IsNullOrWhiteSpace(textBox7.Text) &&
                   !string.IsNullOrWhiteSpace(textBox6.Text) &&
                   !string.IsNullOrWhiteSpace(textBox5.Text) &&
                   !string.IsNullOrWhiteSpace(textBox4.Text);
        }

        private string GetSelectedGender()
        {
            if (radioButtonMale.Checked) return radioButtonMale.Text;
            if (radioButtonFemale.Checked) return radioButtonFemale.Text;
            return string.Empty; // Handle case if no gender is selected
        }

        private void ShowDetailsForm()
        {
            Details detailsForm = new Details();
            detailsForm.Show();
            this.Hide();
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox1.Focus();
        }
    }
}
