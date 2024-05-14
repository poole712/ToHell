using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class DatabaseHandler : MonoBehaviour
{
    private string _databaseName = "URI=file:ToHell.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
    }

    //CREATE THE DATABASE
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(_databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS toHellLeaderboard (name VARCHAR(30), score INT NOT NULL DEFAULT 0);";
                command.ExecuteNonQuery();
            }

            Debug.Log("Created toHellLeadboard Database.");
            connection.Close();
        }
    }

    //ADDING NEW SCORE TO LEADERBOARD
    public void SaveUserData(String userName, int score)
    {
        using (var connection = new SqliteConnection(_databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO toHellLeaderboard (name, score) VALUES (@userName, @score)";
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@score", score);

                command.ExecuteNonQuery();
            }

            Debug.Log("User " + userName + " succesfully saved.");
            connection.Close();
        }
    }

    //Retrieving Top 5 Names and Scores
    public List<(string userName, int score)> GetTopScores()
    {
        var topScores = new List<(string userName, int score)>();

        using (var connection = new SqliteConnection(_databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name, score FROM toHellLeaderboard ORDER BY score DESC LIMIT 5";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string userName = reader.GetString(0);
                        int score = reader.GetInt32(1);
                        topScores.Add((userName, score));
                    }
                }
            }
        }

        return topScores;
    }
}
