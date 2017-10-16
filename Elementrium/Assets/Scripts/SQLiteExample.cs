using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;

namespace Assets.Scripts
{


    public class SQLiteExample
    {
        public void example()
        {

            // This is our path to the database
            string connectionPath = "URI=file:" + Application.dataPath + "/Assets/Elementrium.db";

            // Database Connection object we will use to interact with the database
            // We can do these on the same line, they are separated to show the process
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(connectionPath);

            // This opens the connection to the database
            dbconn.Open();

            // This is a Database command object we use to run queries
            // We use the DB connection to generate this command object
            IDbCommand dbcmd = dbconn.CreateCommand();

            // This is an example query. It obtains all Trium IDs, names, and formulas

            string sqlQuery = "SELECT ID, Name, Formula " +  /* What we want to get */
                              "FROM Trium";                  /* What table is it in? */

            // Set the next command's text to the query we made above
            dbcmd.CommandText = sqlQuery;

            // Create a reader object that executes the command
            // This will make the reader obtain a stream of information we will read
            IDataReader reader = dbcmd.ExecuteReader();

            // reader.Read() will return True or False. If true, we will execute what is in the while() loop
            while (reader.Read())
            {
                // Because our Query asks for ID, Name, Formula, we must access them in that order
                // In each iteration of the loop, the reader will have one row of data from the database
                int id = reader.GetInt32(0);            /* the ID column of the Trium */
                string name = reader.GetString(1);      /* the Name column of the Trium */
                string formula = reader.GetString(2);   /* the Formula column of the Trium */

                // Notice how these are local variables. If you want to access them
                // outside of the while loop, you would want to save them in 
                // variables declared outside of the while loop. 

            }

            // These are cleanup operations to avoid memory leakage
            // Do this after you are done executing queries against the database.
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }


        /**
         * makeQuery
         * 
         * This method will create a query and return a data reader that 
         * allows the caller to access database information
         *
         *
         */
        public IDataReader makeQuery(string query)
        {

            // TODO: IMPLEMENT GENERALIZED METHOD

            return null;

        }
    }
}
