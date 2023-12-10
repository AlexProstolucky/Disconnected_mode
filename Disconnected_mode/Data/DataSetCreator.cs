using System.Data;

namespace Disconnected_mode.Data
{
    internal class DataSetCreator
    {
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
            storageSet = new DataSet("StorageSet");

            storage = new DataTable("Storage");
            providers = new DataTable("Providers");
            product = new DataTable("ProductType");
            create_table_colums();
            instanceCreated = true;
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
        private void create_table_colums()
        {
            storage.Columns.Add("ID", typeof(int));
            storage.Columns.Add("ProductName", typeof(string));
            storage.Columns.Add("ProductType", typeof(string));
            storage.Columns.Add("Provider", typeof(string));
            storage.Columns.Add("Quantity", typeof(int));
            storage.Columns.Add("CostPrice", typeof(decimal));
            storage.Columns.Add("SupplyDate", typeof(DateTime));
            storageSet.Tables.Add(storage);


            providers.Columns.Add("ID", typeof(int));
            providers.Columns.Add("ProviderName", typeof(string));
            providers.Columns.Add("ContactInfo", typeof(string));
            storageSet.Tables.Add(providers);

            product.Columns.Add("ID", typeof(int));
            product.Columns.Add("TypeName", typeof(string));
            storageSet.Tables.Add(product);
        }
    }
}
