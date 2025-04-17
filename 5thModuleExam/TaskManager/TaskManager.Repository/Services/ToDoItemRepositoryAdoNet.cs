using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;
using TaskManager.Dal.Entities;
using TaskManager.Repository.Settings;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManager.Repository.Services;

public class ToDoItemRepositoryAdoNet : IToDoItemRepository
{
    private readonly string ConnectionString;

    public ToDoItemRepositoryAdoNet(SqlDBConnectionString sqlDBConnectionString)
    {
        ConnectionString = sqlDBConnectionString.ConnectionString;
    }

    // done add task
    public async Task<long> AddToDoItemAsync(ToDoItem toDoItem)
    {
        var sql = @"exec spAddItem @Title, @Description, @IsCompleted, @CreatedAt, @DueDate, @Id";

        using (var conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", toDoItem.Title);
                cmd.Parameters.AddWithValue("@Description", toDoItem.Description);
                cmd.Parameters.AddWithValue("@IsCompleted", toDoItem.IsCompleted);
                cmd.Parameters.AddWithValue("@CreatedAt", toDoItem.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);


                SqlParameter outputId = new SqlParameter("@Id", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                await cmd.ExecuteNonQueryAsync();
                return Convert.ToInt64(outputId.Value);
            }
        }
    }

    // delete item
    public async Task DeleteToDoItemByIdAsync(long id)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("spDeleteItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    // done select all
    public async Task<ICollection<ToDoItem>> SelectAllToDoItemsAsync(int skip, int take)
    {
        var sql = @"exec spSelectAllToDoItems @skip, @take";

        var toDoItems = new List<ToDoItem>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        toDoItems.Add(new ToDoItem()
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return toDoItems;
    }

    // select by due date
    public async Task<List<ToDoItem>> SelectByDueDateToDoItemsAsync(DateTime dueDate)
    {
        var sql = @"exec spSelectByDueDateToDoItems @DueDate";

        var toDoItems = new List<ToDoItem>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@DueDate", dueDate);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        toDoItems.Add(new ToDoItem()
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return toDoItems;
    }

    // select complited 
    public async Task<List<ToDoItem>> SelectCompletedToDoItemsAsync(int skip, int take)
    {
        var sql = @"exec spSelectCompletedToDoItems @skip, @take";

        var toDoItems = new List<ToDoItem>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        toDoItems.Add(new ToDoItem()
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return toDoItems;
    }

    public async Task<List<ToDoItem>> SelectIncompleteToDoItemsAsync(int skip, int take)
    {
        var sql = @"exec spSelectIncompleteToDoItems @skip, @take";

        var toDoItems = new List<ToDoItem>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        toDoItems.Add(new ToDoItem()
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        });
                    }
                }
            }
        }

        return toDoItems;

    }

    // select by id
    public async Task<ToDoItem> SelectToDoItemByIdAsync(long id)
    {
        var sql = @"spGetItemById";

        ToDoItem toDoItem = new();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("spGetItemById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        toDoItem = new ToDoItem()
                        {
                            Id = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            IsCompleted = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4),
                            DueDate = reader.GetDateTime(5),
                        };
                    }
                }
            }
        }

        return toDoItem;
    }

    // update item
    public async Task UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        var sql = @"exec spUpdateItem @Id, @Title, @Description, @IsComplited, @DueDate;";

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", toDoItem.Id);
                cmd.Parameters.AddWithValue("@Title", toDoItem.Title);
                cmd.Parameters.AddWithValue("@Description", toDoItem.Description);
                cmd.Parameters.AddWithValue("@IsComplited", toDoItem.IsCompleted);
                //cmd.Parameters.AddWithValue("@CreatedAt", toDoItem.CreatedAt);
                cmd.Parameters.AddWithValue("@DueDate", toDoItem.DueDate);

                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
