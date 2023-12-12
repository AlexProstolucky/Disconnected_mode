using System.Data;
using System.Data.SQLite;

namespace Disconnected_mode.Data
{
    internal class DataSetCreator
    {
        private const string ConnectionString = "Data Source = storage.sqlite";
        private SQLiteDataAdapter StorageAdapter;
        private SQLiteDataAdapter ProviderAdapter;
        private SQLiteDataAdapter ProductAdapter;
        private SQLiteCommandBuilder commandBuilder;
        private static DataSetCreator instance;
        private static bool instanceCreated = false;
        protected DataSet storageSet;
        public DataTable storage;
        public DataTable providers;
        public DataTable product;
        protected DataSetCreator()
        {
            if (instanceCreated)
            {
                throw new InvalidOperationException("The instance has already been created.");
            }

            SQLiteFactory factory = new();
            SQLiteConnection.CreateFile("storage.sqlite");

            using SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.OpenAsync();

            using SQLiteCommand command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS Storage (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                ProductName TEXT NOT NULL,
                ProductType TEXT NOT NULL,
                Provider TEXT NOT NULL,
                Quantity INTEGER,
                CostPrice DECIMAL,
                SupplyDate DATETIME
            )
            ";

            command.ExecuteNonQuery();


            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Providers (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                ProviderName TEXT NOT NULL,
                ContactInfo TEXT
            )
            ";

            command.ExecuteNonQuery();

            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Product (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                TypeName TEXT NOT NULL
            )
            ";

            command.ExecuteNonQuery();

            Include();
            instanceCreated = true;
        }

        private void Include()
        {
            storageSet = new();
            StorageAdapter = new SQLiteDataAdapter("SELECT * FROM Storage", ConnectionString);
            storageSet.Clear();
            StorageAdapter.Fill(storageSet, ConnectionString);
            storage = storageSet.Tables[0];

            storageSet = new();
            ProviderAdapter = new SQLiteDataAdapter("SELECT * FROM Providers", ConnectionString);
            storageSet.Clear();
            ProviderAdapter.Fill(storageSet, ConnectionString);
            providers = storageSet.Tables[0];

            storageSet = new();
            ProductAdapter = new SQLiteDataAdapter("SELECT * FROM Product", ConnectionString);
            storageSet.Clear();
            ProductAdapter.Fill(storageSet, ConnectionString);
            product = storageSet.Tables[0];
        }
        public static DataSetCreator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataSetCreator();
                }
                return instance;
            }
        }

        public void SaveData()
        {
            commandBuilder = new SQLiteCommandBuilder(StorageAdapter);
            StorageAdapter.Update(storage);

            commandBuilder = new SQLiteCommandBuilder(ProviderAdapter);
            ProviderAdapter.Update(providers);

            commandBuilder = new SQLiteCommandBuilder(ProductAdapter);
            ProductAdapter.Update(product);
        }
    }
}
