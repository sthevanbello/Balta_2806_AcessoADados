using System;
using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=DB_Balta;Trusted_Connection=False; TrustServerCertificate=True;User ID=sa;Password=1q2w3e4r@#$";
            //const string connectionString = "Server=localhost,1433;Database=DB_Balta;Encrypt=False;Trusted_Connection=False;User ID=sa;Password=1q2w3e4r@#$";
            // 1q2w3e4r@#$
            // Encrypt=False - Falta isso para funcionar depois Microsoft.Data.SqlClient versão 4.0.0;
            // Trusted_Connection=False; TrustServerCertificate=True
            // Microsoft.Data.SqlClient (NUGET)

            //var connection = new SqlConnection(connectionString);
            //connection.Open();

            //connection.Close();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT [Id], [Title] FROM [Category]";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                    }
                }
            }
        }
    }
}
