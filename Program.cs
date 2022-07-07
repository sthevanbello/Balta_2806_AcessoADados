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
            const string sql = "SELECT [Id], [Title] FROM [Category]";
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;
            var insertSql = @"INSERT INTO [Category] VALUES (
                                @Id, 
                                @Title, 
                                @Url, 
                                @Summary, 
                                @Order, 
                                @Description, 
                                @Featured)";

            using (var connection = new SqlConnection(connectionString))
            {
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });
                Console.WriteLine($"Rows: {rows}");
                var categories = connection.Query<Category>(sql);
                foreach (var item in categories)
                {
                    Console.WriteLine($"Id: {item.Id} - Title: {item.Title}");
                }
            }
        }

        
    }
}
