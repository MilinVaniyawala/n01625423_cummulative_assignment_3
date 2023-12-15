﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using n01625423_cummulative_assignment_3.Models;

namespace n01625423_cummulative_assignment_3.Controllers
{
    ///  Controller for access teacher table data from school database
    ///  A WebAPI Controller which allows you to access information about teachers
    public class TeacherDataController : ApiController
    {
        // Context Class - Which allows us to access MYSQL Database
        private SchoolDbContext School = new SchoolDbContext();

        // --- Get All Teachers Data ---

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            // Create an instance variable for database connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open Connection between web server and database
            Conn.Open();

            // Create a variable to handle the query Command
            MySqlCommand cmd =  Conn.CreateCommand();

            // SQL Query for get all the teacher data from the teachers datatable
            cmd.CommandText = "Select * from teacher where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an emplty list of teachers [Intialize Everytime]
            List<Teacher> Teachers = new List<Teacher>{ };

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Access Column as per the datatable Column name
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string Salary = ResultSet["salary"].ToString();

                // Created Teacher Object
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherID = TeacherId;
                NewTeacher.TeacherFName = TeacherFname;
                NewTeacher.TeacherLName = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                // Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }
            // Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            // Return the final list of teacher names
            return Teachers;
        }

        // --- Find Teacher --- Need This during updating and deleting specific Teacher from the teachers datatable
        [HttpGet]
       public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            // Create an instance variable for database connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open Connection between web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY to find a teacher data using teacher id from the teacher datatable
            cmd.CommandText = "Select * from teacher where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string Salary = ResultSet["salary"].ToString();

                NewTeacher.TeacherID = TeacherId;
                NewTeacher.TeacherFName = TeacherFname;
                NewTeacher.TeacherLName = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
            // Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            // Return a teacher object
            return NewTeacher;
        }
        // --- Add Teacher ---
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            // Create an instance variable for database connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open Connection between web server and database
            Conn.Open();

            // Create a variable to handle the query Command
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query to insert a teacher info teacher datatable 
            cmd.CommandText = "insert into teacher (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close the connection between the MySQL Database and the WebServer
            Conn.Close();
        }
        // --- Update Teacher ---
        [HttpPost]
        public int UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            // Server Side Validation
            try
            {
                // Validate input parameters
                if (string.IsNullOrWhiteSpace(TeacherInfo.TeacherFName) ||
                    string.IsNullOrWhiteSpace(TeacherInfo.TeacherLName) ||
                    string.IsNullOrWhiteSpace(TeacherInfo.EmployeeNumber) ||
                    string.IsNullOrWhiteSpace(TeacherInfo.Salary))
                {
                    // Return 0 if any required field is missing
                    return 0;
                }
                // Create an instance variable for database connection
                MySqlConnection Conn = School.AccessDatabase();

                // Open Connection between web server and database
                Conn.Open();

                // Create a variable to handle the query Command
                MySqlCommand cmd = Conn.CreateCommand();

                // SQL Query for update particular teacher data by teacher id
                cmd.CommandText = "update teacher set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, salary=@Salary  where teacherid=@TeacherId";
                cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFName);
                cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLName);
                cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
                cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
                cmd.Parameters.AddWithValue("@TeacherId", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                // Close the connection between the MySQL Database and the WebServer
                Conn.Close();

                // Return 1 when the operation was successful
                return 1;
            }
            catch (Exception)
            {
                // Return -1 when an error occurs
                return -1;
            }
        }
        // --- Remove[Delete] Teacher ---
        [HttpPost]
        public void RemoveTeacher(int id)
        {
            // Create an instance variable for database connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open Connection between web server and database
            Conn.Open();

            // Create a variable to handle the query Command
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query for delete particular teacher data by teacher id
            cmd.CommandText = "Delete from teacher where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close the connection between the MySQL Database and the WebServer
            Conn.Close();
        }
    }
}
