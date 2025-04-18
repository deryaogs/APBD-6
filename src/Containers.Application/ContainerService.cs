using Containers.Models;
using Microsoft.Data.SqlClient;

namespace Containers.Application;

public class ContainerService : IContainerService
{
    private string _connectionString;

    public ContainerService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IEnumerable<Container> Containers()
    {
        List<Container> containers = [];
        const string queryString = "SELECT * FROM Containers";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var containerRow = new Container
                        {
                            ID = reader.GetInt32(0),
                            ContainerTypeId = reader.GetInt32(1),
                            isHazardious = reader.GetBoolean(2),
                            Name = reader.GetString(3)
                        };
                        containers.Add(containerRow);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return containers;
    }

    private int countRowsAdded = -1;
    public bool AddContainer(Container container)
    {
        const string insertString =
            "Insert into containers (ContainerTypeId, isHazardious, Name) values (@ContainerTypeId, @isHazardious, @Name)";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(insertString, connection);
            command.Parameters.AddWithValue("@ContainerTypeId", container.ContainerTypeId);
            command.Parameters.AddWithValue("@isHazardious", container.isHazardious);
            command.Parameters.AddWithValue("@Name", container.Name);
            
            connection.Open();
            countRowsAdded = command.ExecuteNonQuery();
        }

        return countRowsAdded != -1;
    }
}