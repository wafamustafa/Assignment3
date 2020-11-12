using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using the MySql database: SchoolDb
using MySql.Data.MySqlClient;

namespace Assignment3_N01450753_WafaMustafa_5101B.Models
{
    public class SchoolDbContext
    {
        //USING YOUR WAY OF CONNECTING TO THE DATABASE
        // DATE USED: November 09/2020
        // git repo Blog1.


        //These are readonly "secret" properties. 
        //Only the SchoolDbContext class can use them.
        //everything is a get method because we will only be accessing info from the database not updating/adding it. 
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is connecting to the SchoolDb on MySql provided on MAMP.
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        //this will use the database to access info on the school database
        /// <summary>
        /// returns a connection to School database.
        /// </summary>
        /// <example> 
        /// private SchoolDbContext school = new SchoolDbContext();
        /// MySqlConnection Conn = school.AccessDatabase();
        /// </example>
        /// <returns>MySql connection object</returns>
        public MySqlConnection AccessDatabase()
        {
            //
            return new MySqlConnection(ConnectionString);

        }

    }

}



