namespace Disconnected_mode.Repository
{
    internal interface IDataSet
    {
        void AddItem(int id, string productName, string productType, string provider, int quantity, decimal costPrice, DateTime supplyDate);
        void AddProvider(int id, string providerName, string contactInfo);
        void AddProduct(int id, string typeName);

        void RemoveItem(int id);
        void RemoveProvider(int id);
        void RemoveProduct(int id);

        void UpdateItem(int id, string productName = null, string productType = null, string provider = null, int? quantity = null, decimal? costPrice = null, DateTime? supplyDate = null);
        void UpdateProvider(int id, string providerName = null, string contactInfo = null);
        void UpdateProduct(int id, string typeName = null);

        // Показати інформацію про постачальника з максимальною кількістю товарів на складі
        void ShowProviderWithMaxQuantity();

        // Показати інформацію про постачальника з мінімальною кількістю товарів на складі
        void ShowProviderWithMinQuantity();

        // Показати інформацію про тип товару з максимальною кількістю одиниць на складі
        void ShowProductTypeWithMaxQuantity();

        // Показати інформацію про тип товару з мінімальною кількістю товарів на складі
        void ShowProductTypeWithMinQuantity();

        // Показати товари, з постачання яких минула задана кількість днів
        void ShowProductsSuppliedLastDays(int days);
    }
}
