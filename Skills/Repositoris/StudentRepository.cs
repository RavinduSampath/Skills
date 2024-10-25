using Skills.Models;
using System;
using System.Collections.Generic; 
using Microsoft.Data.SqlClient;

namespace Skills.Repositoris
{
    public class StudentRepository
    {
        private readonly string connectionString = "Data Source=DESKTOP-TD1B2SN\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Encrypt=False";

        // Get all students
        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Registration";
                    using (SqlCommand command = new SqlCommand(query, connection))

                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new Student
                                {
                                    regNo = reader["regNo"] != DBNull.Value ? (int)reader["regNo"] : 0, // Handle nullable field
                                    firstName = reader["firstName"]?.ToString() ?? string.Empty, // Handle null
                                    lastName = reader["lastName"]?.ToString() ?? string.Empty,
                                    dateOfBirth = reader["dateOfBirth"] != DBNull.Value ? (DateTime)reader["dateOfBirth"] : DateTime.MinValue, // Handle nullable field
                                    gender = reader["gender"]?.ToString() ?? string.Empty,
                                    address = reader["address"]?.ToString() ?? string.Empty,
                                    email = reader["email"]?.ToString() ?? string.Empty,
                                    mobilePhone = reader["mobilePhone"]?.ToString() ?? string.Empty,
                                    homePhone = reader["homePhone"]?.ToString() ?? string.Empty,
                                    parentName = reader["parentName"]?.ToString() ?? string.Empty,
                                    nic = reader["nic"]?.ToString() ?? string.Empty,
                                    contactNo = reader["contactNo"]?.ToString() ?? string.Empty
                                });
                            }
                                
                            
                        } 
                    } 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while retrieving students: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            return students;
        }

        // Get student by registration number
        public Student GetStudentByRegNo(int regNo)
        {
            Student student = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM [Registration] WHERE regNo = @regNo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@regNo", regNo);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student = new Student
                                {
                                    regNo = (int)reader["regNo"],
                                    firstName = reader["firstName"].ToString(),
                                    lastName = reader["lastName"].ToString(),
                                    dateOfBirth = (DateTime)reader["dateOfBirth"],
                                    gender = reader["gender"].ToString(),
                                    address = reader["address"].ToString(),
                                    email = reader["email"].ToString(),
                                    mobilePhone = reader["mobilePhone"].ToString(),
                                    homePhone = reader["homePhone"].ToString(),
                                    parentName = reader["parentName"].ToString(),
                                    nic = reader["nic"].ToString(),
                                    contactNo = reader["contactNo"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while retrieving the student: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            return student;
        }

        // Create a new student
        public bool CreateStudent(Student student)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO [Registration] (firstName, lastName, dateOfBirth, gender, address, email,mobilePhone, homePhone, parentName, nic, contactNo) " +
                                   "VALUES (@firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", student.firstName);
                        cmd.Parameters.AddWithValue("@lastName", student.lastName);
                        cmd.Parameters.AddWithValue("@dateOfBirth", student.dateOfBirth);
                        cmd.Parameters.AddWithValue("@gender", student.gender);
                        cmd.Parameters.AddWithValue("@address", student.address);
                        cmd.Parameters.AddWithValue("@email", student.email);
                        cmd.Parameters.AddWithValue("@mobilePhone", student.mobilePhone);
                        cmd.Parameters.AddWithValue("@homePhone", student.homePhone);
                        cmd.Parameters.AddWithValue("@parentName", student.parentName);
                        cmd.Parameters.AddWithValue("@nic", student.nic);
                        cmd.Parameters.AddWithValue("@contactNo", student.contactNo);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true; // Indicate success
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while creating the student: " + ex.Message);
                return false; // Indicate failure
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return false; // Indicate failure
            }
        }

        // Update an existing student
        public bool UpdateStudent(Student student)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE [Registration] SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, " +
                                   "gender = @gender, address = @address, email = @email, mobilePhone = @mobilePhone, homePhone = @homePhone, " +
                                   "parentName = @parentName, nic = @nic, contactNo = @contactNo WHERE regNo = @regNo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@regNo", student.regNo);
                        cmd.Parameters.AddWithValue("@firstName", student.firstName);
                        cmd.Parameters.AddWithValue("@lastName", student.lastName);
                        cmd.Parameters.AddWithValue("@dateOfBirth", student.dateOfBirth);
                        cmd.Parameters.AddWithValue("@gender", student.gender);
                        cmd.Parameters.AddWithValue("@address", student.address);
                        cmd.Parameters.AddWithValue("@email", student.email);
                        cmd.Parameters.AddWithValue("@mobilePhone", student.mobilePhone);
                        cmd.Parameters.AddWithValue("@homePhone", student.homePhone);
                        cmd.Parameters.AddWithValue("@parentName", student.parentName);
                        cmd.Parameters.AddWithValue("@nic", student.nic);
                        cmd.Parameters.AddWithValue("@contactNo", student.contactNo);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true; // Indicate success
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while updating the student: " + ex.Message);
                return false; // Indicate failure
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return false; // Indicate failure
            }
        }

        // Delete a student
        public bool DeleteStudent(int regNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM [Registration] WHERE regNo = @regNo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@regNo", regNo);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true; // Indicate success
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred while deleting the student: " + ex.Message);
                return false; // Indicate failure
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return false; // Indicate failure
            }
        }
        public int Count()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Registration";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count;
                }

        }   }
    }
}
