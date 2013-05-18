using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Arcana
{
    //outline courtesty http://www.codeproject.com/Articles/10213/Create-an-SQL-Server-Database-Using-Csharp
    public class DatabaseLink
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

    //Constructor
    public DatabaseLink()
    {
        Initialize();
    }

    //Initialize values
    private void Initialize()
    {

        server = "127.0.0.1";
        database = "Arcana";
        uid = "ArcanaGame";
        password = "password";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
		database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";"
        + "PORT=" + "3306" + ";";

        connection = new MySqlConnection(connectionString);
        if (connection == null) Console.Out.WriteLine("Connection failed!");
    }

    //open connection to database
    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            Console.Out.WriteLine("Connection successful!");
            return true;
        }
        catch (MySqlException ex)
        {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            Debug.WriteLine(ex.ToString());
            switch (ex.Number)
            {
                case 0:
                    MessageBox.Show("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    MessageBox.Show("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
    }

    //Close connection
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
            return false;
        }
    }

    //Insert statement
    public void Insert(int playerNum)
    {
        string playerName = "Player" + playerNum;
        string query = "INSERT INTO " + playerName + " VALUES('Angel', '5')";

        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Update statement
    public void Update()
    {

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            //cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Delete statement
    public void Delete(string query)
    {

        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }

    //Select statement
    public List <string> [] Select(int playerNumber)
    {
        string query = "SELECT * FROM player" + playerNumber;

        //Create a list to store the result
        List<string>[] list = new List<string>[2];
        list[0] = new List<string>();
        list[1] = new List<string>();

        //Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["name"] + "");
                list[1].Add(dataReader["power"] + "");
            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();

            //return list to be displayed
            return list;
        }
        else
        {
            return list;
        }
    }

    //Count statement
    public int Count(string query)
    {
        int Count = -1;

        //Open Connection
        if (this.OpenConnection() == true)
        {
            //Create Mysql Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar() + "");

            //close Connection
            this.CloseConnection();

            return Count;
        }
        else
        {
            return Count;
        }
    }

    //Backup
    public void Backup()
    {
    }

    //Restore
    public void Restore()
    {
    }
    }
}
