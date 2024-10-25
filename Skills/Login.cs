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
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-TD1B2SN\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Encrypt=False");
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String UserName = textBox1.Text;
            String Password = textBox2.Text;

            try
            {
                
                conn.Open();

                
                String query = "SELECT * FROM [Login] WHERE UserName = @UserName AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);

                
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1) 
                {
                    
                    MessageBox.Show("Login Successful");
                    this.Hide();

                    
                    Details f2 = new Details();
                    f2.Show();
                    this.Hide();
                }
                else
                {
                    // Failed login
                    MessageBox.Show("Invalid Login Credentials,Please Check UserName and Password and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                
                conn.Close();
            }
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
    }
}
