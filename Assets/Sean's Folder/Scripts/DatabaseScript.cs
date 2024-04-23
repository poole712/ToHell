using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class DatabaseScript : MonoBehaviour
{
    private string dbName = "URI=file:ToHell.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
    }

    //CREATE THE DATABASE
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS users (name VARCHAR(30));";
                command.ExecuteNonQuery();
            }

            Debug.Log("Database Created" + connection.ConnectionString);
            connection.Close();
        }
    }

    //INSERT INTO DATABASE
    public void AddUser(String userName)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO users (name) VALUES ('" + userName + "');";
                command.ExecuteNonQuery();
            }

            Debug.Log("User " + userName + " succesfully added.");
            connection.Close();
        }
    }

    //READ INTO THE DATABASE
    public void DisplayNames()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM users;";
                //REST OF THE CODE HERE
            }

            connection.Close();
        }
    }

    public Boolean checkUserExist(String userName)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM users WHERE name = ('" + userName + "');";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // User exists in the database
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        // User does not exist, add them to the database
                        reader.Close(); // Close reader before executing another command
                        AddUser(userName);
                        connection.Close();
                        return false;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
