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
            using (var connection = new SqlConnection(connectionString))
            {
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //ListCategories(connection);
                //DeleteCategory(connection);
                ListCategories(connection);
                //CreateCategory(connection);
                //ListCategories(connection);
                CreateManyCategory(connection);
                ListCategories(connection);
            }
        }

        static void CreateCategory(SqlConnection connection)
        {
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
        }
        static void ListCategories(SqlConnection connection)
        {
            const string sql = "SELECT [Id], [Title] FROM [Category]";
            var categories = connection.Query<Category>(sql);
            foreach (var item in categories)
            {
                Console.WriteLine($"Id: {item.Id} - Title: {item.Title}");
            }
        }
        static void UpdateCategory(SqlConnection connection)
        {
            var updateSql = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
            var rows = connection.Execute(updateSql, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2022"
            });
            Console.WriteLine($"Records updated: {rows}");
        }
        static void DeleteCategory(SqlConnection connection)
        {
            var deleteSql = "DELETE FROM [Category] WHERE [Title] = @Title";
            //var deleteSql = "DELETE FROM [Category] WHERE [Id] = @id";
            var rows = connection.Execute(deleteSql, new
            {
                //id = new Guid("de1181ab-94ee-467b-9d13-72d7257967b9"),
                Title = "Amazon AWS"
            });
            Console.WriteLine($"Records updated: {rows}");
        }
        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria nova";
            category2.Featured = false;
            var insertSql = @"INSERT INTO [Category] VALUES (
                                @Id, 
                                @Title, 
                                @Url, 
                                @Summary, 
                                @Order, 
                                @Description, 
                                @Featured)";
            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
                },
                new
                {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
                }
            });
            Console.WriteLine($"Rows: {rows}");
        }

    }

}

