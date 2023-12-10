using Disconnected_mode.Data;
using System.Data;

namespace Disconnected_mode.Repository
{
    internal class DataWorking : IDataSet
    {
        DataSetCreator DB;
        public DataWorking()
        {
            DB = DataSetCreator.Instance;
        }
        public void AddItem(int id, string productName, string productType, string provider, int quantity, decimal costPrice, DateTime supplyDate)
        {
            DataRow newRow = DB.storage.NewRow();
            newRow["ID"] = id;
            newRow["ProductName"] = productName;
            newRow["ProductType"] = productType;
            newRow["Provider"] = provider;
            newRow["Quantity"] = quantity;
            newRow["CostPrice"] = costPrice;
            newRow["SupplyDate"] = supplyDate;
            DB.storage.Rows.Add(newRow);
        }

        public void AddProduct(int id, string typeName)
        {
            DataRow newRow = DB.product.NewRow();
            newRow["ID"] = id;
            newRow["TypeName"] = typeName;
            DB.product.Rows.Add(newRow);
        }

        public void AddProvider(int id, string providerName, string contactInfo)
        {
            DataRow newRow = DB.providers.NewRow();
            newRow["ID"] = id;
            newRow["ProviderName"] = providerName;
            newRow["ContactInfo"] = contactInfo;
            DB.providers.Rows.Add(newRow);
        }

        public void RemoveItem(int id)
        {
            DataRow[] rows = DB.storage.Select($"ID = {id}");
            foreach (DataRow row in rows)
            {
                DB.storage.Rows.Remove(row);
            }
        }

        public void RemoveProduct(int id)
        {
            DataRow[] rows = DB.product.Select($"ID = {id}");
            foreach (DataRow row in rows)
            {
                DB.product.Rows.Remove(row);
            }
        }

        public void RemoveProvider(int id)
        {
            DataRow[] rows = DB.providers.Select($"ID = {id}");
            foreach (DataRow row in rows)
            {
                DB.providers.Rows.Remove(row);
            }
        }

        public void UpdateItem(int id, string productName = null, string productType = null, string provider = null, int? quantity = null, decimal? costPrice = null, DateTime? supplyDate = null)
        {
            DataRow[] rows = DB.storage.Select($"ID = {id}");

            if (rows.Length > 0)
            {
                DataRow row = rows[0];

                if (productName != null)
                    row["ProductName"] = productName;

                if (productType != null)
                    row["ProductType"] = productType;

                if (provider != null)
                    row["Provider"] = provider;

                if (quantity.HasValue)
                    row["Quantity"] = quantity.Value;

                if (costPrice.HasValue)
                    row["CostPrice"] = costPrice.Value;

                if (supplyDate.HasValue)
                    row["SupplyDate"] = supplyDate.Value;
            }
        }

        public void UpdateProvider(int id, string providerName = null, string contactInfo = null)
        {
            DataRow[] rows = DB.providers.Select($"ID = {id}");

            if (rows.Length > 0)
            {
                DataRow row = rows[0];

                if (providerName != null)
                    row["ProviderName"] = providerName;

                if (contactInfo != null)
                    row["ContactInfo"] = contactInfo;
            }
        }

        public void UpdateProduct(int id, string typeName = null)
        {
            DataRow[] rows = DB.product.Select($"ID = {id}");

            if (rows.Length > 0)
            {
                DataRow row = rows[0];

                if (typeName != null)
                    row["TypeName"] = typeName;
            }
        }


        public void ShowProviderWithMaxQuantity()
        {
            var maxQuantity = DB.storage.AsEnumerable().Max(row => row.Field<int>("Quantity"));
            DataRow[] rows = DB.storage.Select($"Quantity = {maxQuantity}");
            DisplayProviderInfo(rows, "Provider with the maximum quantity:");
        }

        public void ShowProviderWithMinQuantity()
        {
            var minQuantity = DB.storage.AsEnumerable().Min(row => row.Field<int>("Quantity"));
            DataRow[] rows = DB.storage.Select($"Quantity = {minQuantity}");
            DisplayProviderInfo(rows, "Provider with the minimum quantity:");
        }

        private void DisplayProviderInfo(DataRow[] rows, string message)
        {
            Console.WriteLine(message);
            foreach (DataRow row in rows)
            {
                var providerName = row.Field<string>("Provider");
                DataRow[] providerRows = DB.providers.Select($"ProviderName = '{providerName}'");
                foreach (DataRow providerRow in providerRows)
                {
                    Console.WriteLine($"ID: {providerRow["ID"]}, Name: {providerRow["ProviderName"]}, Contact: {providerRow["ContactInfo"]}");
                }
            }
            Console.WriteLine();
        }


        public void ShowProductTypeWithMaxQuantity()
        {
            var maxQuantity = DB.storage.AsEnumerable().Max(row => row.Field<int>("Quantity"));
            DataRow[] rows = DB.storage.Select($"Quantity = {maxQuantity}");
            DisplayProductTypeInfo(rows, "Product type with the maximum quantity:");
        }
        public void ShowProductTypeWithMinQuantity()
        {
            var minQuantity = DB.storage.AsEnumerable().Min(row => row.Field<int>("Quantity"));
            DataRow[] rows = DB.storage.Select($"Quantity = {minQuantity}");
            DisplayProductTypeInfo(rows, "Product type with the minimum quantity:");
        }

        private void DisplayProductTypeInfo(DataRow[] rows, string message)
        {
            Console.WriteLine(message);
            foreach (DataRow row in rows)
            {
                var productType = row.Field<string>("ProductType");
                DataRow[] productTypeRows = DB.product.Select($"TypeName = '{productType}'");
                foreach (DataRow productTypeRow in productTypeRows)
                {
                    Console.WriteLine($"ID: {productTypeRow["ID"]}, Type Name: {productTypeRow["TypeName"]}");
                }
            }
            Console.WriteLine();
        }

        public void ShowProductsSuppliedLastDays(int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days);
            DataRow[] rows = DB.storage.Select($"SupplyDate >= '{startDate.ToString("yyyy-MM-dd")}'");
            DisplayDataRow(rows, $"Products supplied in the last {days} days:");
        }

        public void InfoStorage()
        {
            Console.WriteLine("\nAll Data in Storage Table:");

            foreach (DataRow row in DB.storage.Rows)
            {
                foreach (DataColumn column in DB.storage.Columns)
                {
                    Console.WriteLine($"{column.ColumnName}: {row[column]}");
                }
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("\n\n");
        }
        private void DisplayDataRow(DataRow[] rows, string message)
        {
            Console.WriteLine(message);
            foreach (DataRow row in rows)
            {
                foreach (DataColumn column in row.Table.Columns)
                {
                    Console.WriteLine($"{column.ColumnName}: {row[column]}");
                }
            }
            Console.WriteLine("\n\n");
        }
    }
}
