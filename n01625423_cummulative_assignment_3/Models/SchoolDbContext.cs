using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace n01625423_cummulative_assignment_3.Models
{
    public class SchoolDbContext
    {
        /// <summary>
        ///     Database Connection Properties, only for read only 
        ///     Only Access to SchoolDbContext -> To access school database
        /// </summary>
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        /// <summary>
        ///     Create a variable that finally connects the database
        /// </summary
        protected static string ConnectionString
        {
            get
            {
                /// convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00
                /// this can allow C# to have an easier interpretation of the date (no date instead of 0 BCE)

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }

        /// <summary>
        /// Create a database connection for school database
        /// </summary>
        /// <returns>
        /// database connection object as an output
        /// </returns>

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}