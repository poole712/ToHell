using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class DatabaseHandler : MonoBehaviour
{
    private string databaseName = "URI=file:ToHell.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
    }

    //CREATE THE DATABASE
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS toHellUsers (name VARCHAR(30), coins INT NOT NULL DEFAULT 0);";
                command.ExecuteNonQuery();
            }

            Debug.Log("Succesfuly Connected to the Database");
            connection.Close();
        }
    }

    //INSERT INTO DATABASE
    public void AddUser(String userName)
    {
        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO toHellUsers (name) VALUES ('" + userName + "');";
                command.ExecuteNonQuery();
            }

            Debug.Log("User " + userName + " succesfully updated.");
            connection.Close();
        }
    }

    public void SaveUserData(String userName, int coins)
    {
        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE toHellUsers SET coins = " + coins + " WHERE name = '" + userName + "'";
                command.ExecuteNonQuery();
            }

            Debug.Log("User " + userName + " succesfully saved.");
            connection.Close();
        }
    }

    public Boolean CheckUserExist(String userName)
    {
        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM toHellUsers WHERE name = ('" + userName + "');";
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
                        reader.Close();
                        AddUser(userName);
                        connection.Close();
                        return false;
                    }
                }
            }
        }
    }

    public int GetUserCoins(string userName)
    {
        int coinsToReturn = 0;
        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT coins FROM toHellUsers WHERE name = '" + userName + "'";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {  // If a record is found
                        coinsToReturn = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);  // Get the coins value
                    }
                }
            }

            connection.Close();
        }

        return coinsToReturn;  // Return the retrieved coins value
    }

    // Update is called once per frame
    void Update()
    {

    }
}
