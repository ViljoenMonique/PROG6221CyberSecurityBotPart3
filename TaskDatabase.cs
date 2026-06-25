using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class TaskDatabase
    {
        private readonly string _connectionString = "Data Source=cyber_tasks.db";

        public TaskDatabase()
        {
            CreateTable();
        }

        private void CreateTable()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            // Create table with all columns
            string sql = @"CREATE TABLE IF NOT EXISTS Tasks (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Description TEXT NOT NULL,
                            DateAdded TEXT NOT NULL,
                            ReminderDate TEXT,
                            Status TEXT DEFAULT 'Active')";
            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();

            // Add ReminderDate column if it doesn't exist (for existing databases)
            try
            {
                string alterSql = "ALTER TABLE Tasks ADD COLUMN ReminderDate TEXT";
                using var alterCommand = new SqliteCommand(alterSql, connection);
                alterCommand.ExecuteNonQuery();
            }
            catch
            {
                // Column already exists - ignore error
            }
        }

        public void AddTask(string description, string? reminderDate = null)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string sql = "INSERT INTO Tasks (Description, DateAdded, ReminderDate) VALUES (@desc, @date, @reminder)";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@desc", description);
            command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            command.Parameters.AddWithValue("@reminder", reminderDate ?? (object)DBNull.Value);

            command.ExecuteNonQuery();
        }

        public List<string> GetAllTasks()
        {
            var tasks = new List<string>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            string sql = "SELECT Id, Description, DateAdded, ReminderDate FROM Tasks WHERE Status = 'Active' ORDER BY Id DESC";
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string desc = reader.GetString(1);
                string date = reader.GetString(2);
                string reminder = reader.IsDBNull(3) ? "" : reader.GetString(3);

                string line = $"[{id}] [{date}] {desc}";
                if (!string.IsNullOrEmpty(reminder))
                    line += $" (Reminder: {reminder})";
                tasks.Add(line);
            }
            return tasks;
        }

        public bool MarkTaskComplete(int taskId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            string sql = "UPDATE Tasks SET Status = 'Completed' WHERE Id = @id";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", taskId);
            return command.ExecuteNonQuery() > 0;
        }

        public bool DeleteTask(int taskId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            string sql = "DELETE FROM Tasks WHERE Id = @id";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", taskId);
            return command.ExecuteNonQuery() > 0;
        }
    }
}
