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

    public IEnumerable<Device> AllDevice()
    {
        List<Device> containers = [];
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
                        var containerRow = new Device
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

    public bool AddDevice(Device device)
    {
        countRowsAdded = -1;
        const string insertString =
            "Insert into devices (isEnabled, Name) values (@isEnabled, @Name)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(insertString, connection);
            command.Parameters.AddWithValue("@isEnabled", device.isEnabled);
            command.Parameters.AddWithValue("@Name", device.Name);

            connection.Open();
            countRowsAdded = command.ExecuteNonQuery();
        }

        return countRowsAdded != -1;
    }

    public bool RemoveDevice(Device device)
    {
        int countRowsDeleted = -1;
        const string deleteString = "DELETE FROM devices WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(deleteString, connection);
            command.Parameters.AddWithValue("@Id", device.ID);

            connection.Open();
            countRowsDeleted = command.ExecuteNonQuery();
        }

        return countRowsDeleted != -1;
    }
    public bool UpdateDevice(Device device)
    {
        int countRowsUpdated = -1;
        const string updateString = "UPDATE devices SET isEnabled = @isEnabled, Name = @Name WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(updateString, connection);
            command.Parameters.AddWithValue("@isEnabled", device.isEnabled);
            command.Parameters.AddWithValue("@Name", device.Name);
            command.Parameters.AddWithValue("@Id", device.ID);

            connection.Open();
            countRowsUpdated = command.ExecuteNonQuery();
        }

        return countRowsUpdated != -1;
    }

}
