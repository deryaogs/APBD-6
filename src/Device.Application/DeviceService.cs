using Containers.Models;
using Microsoft.Data.SqlClient;

namespace Containers.Application;

public class DeviceService : IDeviceService
{
    private string _connectionString;

    public DeviceService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Devices> AllDevice()
    {
        List<Devices> containers = [];
        const string queryString = "SELECT * FROM devices";
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
                        var containerRow = new Devices
                        {
                            ID = reader.GetInt32(0),
                            isEnabled = reader.GetBoolean(2),
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

    private int countRowsAdded;

    public bool AddDevice(Devices devices)
    {
        countRowsAdded = -1;
        const string insertString =
            "Insert into devices (isEnabled, Name) values (@isEnabled, @Name)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(insertString, connection);
            command.Parameters.AddWithValue("@isEnabled", devices.isEnabled);
            command.Parameters.AddWithValue("@Name", devices.Name);

            connection.Open();
            countRowsAdded = command.ExecuteNonQuery();
        }

        return countRowsAdded != -1;
    }

    public bool RemoveDevice(Devices devices)
    {
        int countRowsDeleted = -1;
        const string deleteString = "DELETE FROM devices WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(deleteString, connection);
            command.Parameters.AddWithValue("@Id", devices.ID);

            connection.Open();
            countRowsDeleted = command.ExecuteNonQuery();
        }

        return countRowsDeleted != -1;
    }
    public bool UpdateDevice(Devices devices)
    {
        int countRowsUpdated = -1;
        const string updateString = "UPDATE devices SET isEnabled = @isEnabled, Name = @Name WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(updateString, connection);
            command.Parameters.AddWithValue("@isEnabled", devices.isEnabled);
            command.Parameters.AddWithValue("@Name", devices.Name);
            command.Parameters.AddWithValue("@Id", devices.ID);

            connection.Open();
            countRowsUpdated = command.ExecuteNonQuery();
        }

        return countRowsUpdated != -1;
    }
    
    public Devices? GetDeviceById(string id)
    {
        Devices? device = null;
        const string queryString = "SELECT Id, isEnabled, Name FROM devices WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    device = new Devices
                    {
                        ID = reader.GetInt32(0),
                        isEnabled = reader.GetBoolean(2),
                        Name = reader.GetString(3)
                    };
                }
            }
        }

        return device;
    }
}
