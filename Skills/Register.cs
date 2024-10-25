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

namespace Skills
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-TD1B2SN\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Encrypt=False");
        private void button2_Click(object sender, EventArgs e)
        {
            String UserName = textBox1.Text;
            String Password = textBox2.Text;
            String ConfirmPassword = textBox3.Text;


            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
                textBox2.Focus();
                return;
            }
            else
            {
                try
                {
                    
                    conn.Open();

                   
                    String query = "INSERT INTO [Login] (UserName, Password) VALUES (@UserName, @Password)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Add parameters to the query
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 1) // Single match found
                    {
                        // Successful login
                        MessageBox.Show("Sign Up Successful");
                        this.Hide();

                        // Load next page (Home)
                        Login f2 = new Login();
                        f2.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Failed login
                        MessageBox.Show("Sign Up Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
