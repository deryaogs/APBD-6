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
                            isEnabled = reader.GetBoolean(1),
                            Name = reader.GetString(2)
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
    public IEnumerable<Embedded> AllEmbedded() {
        List<Embedded> containers = [];
        const string queryString = "SELECT * FROM embedded";
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
                        var containerRow = new Embedded()
                        {
                            ID = reader.GetInt32(0),
                            DeviceID = reader.GetInt32(1),
                            NetworkName = reader.GetString(2),
                            IpAddress = reader.GetString(3)
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
    public IEnumerable<PersonalComputer> AllPersonalComputer() {
        List<PersonalComputer> containers = [];
        const string queryString = "SELECT * FROM personalcomputer";
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
                        var containerRow = new PersonalComputer()
                        {
                            ID = reader.GetInt32(0),
                            DeviceID = reader.GetInt32(1),
                            OperationSystem = reader.GetString(2)
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
    
    public IEnumerable<SmartWatch> AllSmartWatch() {
        List<SmartWatch> containers = [];
        const string queryString = "SELECT * FROM smartwatch";
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
                        var containerRow = new SmartWatch()
                        {
                            ID = reader.GetInt32(0),
                            DeviceID = reader.GetInt32(1),
                            BatteryPercentage = reader.GetInt32(2)
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
    public bool AddEmbedded(Embedded embedded)
    {
        countRowsAdded = -1;
        
        if (!System.Net.IPAddress.TryParse(embedded.IpAddress, out _))
        {
            throw new ArgumentException("Invalid IP address format.");
        }

        if (string.IsNullOrWhiteSpace(embedded.NetworkName))
        {
            throw new ArgumentException("Network name cannot be empty or whitespace.");
        }
        
        const string insertString =
            "Insert into Embedded (DeviceId, NetworkName, IpAddress) values (@DeviceId, @NetworkName, @IpAddress)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(insertString, connection);
            command.Parameters.AddWithValue("@DeviceId", embedded.DeviceID);
            command.Parameters.AddWithValue("@NetworkName", embedded.NetworkName);
            command.Parameters.AddWithValue("@IpAddress", embedded.IpAddress);

            connection.Open();
            countRowsAdded = command.ExecuteNonQuery();
        }

        return countRowsAdded != -1;
    }
    public bool AddPersonalComputer(PersonalComputer personalComputer)
    {
        countRowsAdded = -1;
        const string insertString =
            "Insert into PersonalComputer (DeviceId, OperationSystem) values (@DeviceId, @OperationSystem)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(insertString, connection);
            command.Parameters.AddWithValue("@DeviceId", personalComputer.DeviceID);
            command.Parameters.AddWithValue("@OperationSystem", personalComputer.OperationSystem);

            connection.Open();
            countRowsAdded = command.ExecuteNonQuery();
        }

        return countRowsAdded != -1;
    }
    public bool AddSmartWatch(SmartWatch smartWatch)
    {
        countRowsAdded = -1;
        if (smartWatch.BatteryPercentage > 100 || smartWatch.BatteryPercentage < 0)
        {
            throw new ArgumentException("Invalid Battery Percentage.");
        }
        const string insertString =
            "Insert into SmartWatch (DeviceId, BatteryPercentage) values (@DeviceId, @BatteryPercentage)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(insertString, connection);
            command.Parameters.AddWithValue("@DeviceId", smartWatch.DeviceID);
            command.Parameters.AddWithValue("@BatteryPercentage", smartWatch.BatteryPercentage);

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
    public bool RemoveSmartWatch(SmartWatch smartWatch)
    {
        int countRowsDeleted = -1;
        const string deleteString = "DELETE FROM SmartWatch WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(deleteString, connection);
            command.Parameters.AddWithValue("@DeviceId", smartWatch.ID);

            connection.Open();
            countRowsDeleted = command.ExecuteNonQuery();
        }

        return countRowsDeleted != -1;
    }
    public bool RemovePersonalComputer(PersonalComputer personalComputer)
    {
        int countRowsDeleted = -1;
        const string deleteString = "DELETE FROM PersonalComputer WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(deleteString, connection);
            command.Parameters.AddWithValue("@ID", personalComputer.ID);

            connection.Open();
            countRowsDeleted = command.ExecuteNonQuery();
        }

        return countRowsDeleted != -1;
    }
    public bool RemoveEmbedded(Embedded embedded)
    {
        int countRowsDeleted = -1;
        const string deleteString = "DELETE FROM Embedded WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(deleteString, connection);
            command.Parameters.AddWithValue("@ID", embedded.ID);

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
    public bool UpdateSmartWatch(SmartWatch smartWatch)
    {
        int countRowsUpdated = -1;
        const string updateString = "UPDATE SmartWatch SET BatteryPercentage = @BatteryPercentage WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(updateString, connection);
            command.Parameters.AddWithValue("@BatteryPercentage", smartWatch.BatteryPercentage);
            command.Parameters.AddWithValue("@ID", smartWatch.ID);

            connection.Open();
            countRowsUpdated = command.ExecuteNonQuery();
        }

        return countRowsUpdated != -1;
    }
    public bool UpdatePersonalComputer(PersonalComputer personalComputer)
    {
        int countRowsUpdated = -1;
        const string updateString = "UPDATE PersonalComputer SET OperationSystem = @OperationSystem WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(updateString, connection);
            command.Parameters.AddWithValue("@OperationSystem", personalComputer.OperationSystem);
            command.Parameters.AddWithValue("@ID", personalComputer.ID);

            connection.Open();
            countRowsUpdated = command.ExecuteNonQuery();
        }

        return countRowsUpdated != -1;
    }
    public bool UpdateEmbedded(Embedded embedded)
    {
        int countRowsUpdated = -1;
        const string updateString = "UPDATE Embedded SET NetworkName = @NetworkName, IpAddress = @IpAddress WHERE ID = @ID";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(updateString, connection);
            command.Parameters.AddWithValue("@NetworkName", embedded.NetworkName);
            command.Parameters.AddWithValue("@IpAddress", embedded.IpAddress);
            command.Parameters.AddWithValue("@ID", embedded.ID);

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
                        isEnabled = reader.GetBoolean(1),
                        Name = reader.GetString(2)
                    };
                }
            }
        }

        return device;
    }
}
