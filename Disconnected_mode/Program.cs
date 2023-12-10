using Disconnected_mode.Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        DataWorking dataWorking = new DataWorking();
        dataWorking.AddItem(1, "Laptop", "Electronics", "TechProvider", 50, 800.00m, DateTime.Now.AddDays(-30));
        dataWorking.AddItem(2, "Smartphone", "Electronics", "TechProvider", 100, 500.00m, DateTime.Now.AddDays(-20));
        dataWorking.AddItem(3, "Desk Chair", "Furniture", "FurnitureCo", 20, 150.00m, DateTime.Now.AddDays(-15));

        dataWorking.AddProvider(1, "TechProvider", "+4909053690");
        dataWorking.AddProvider(2, "FurnitureCo", "+89090536958");

        dataWorking.AddProduct(1, "Electronics");
        dataWorking.AddProduct(2, "Furniture");
        dataWorking.InfoStorage();

        dataWorking.ShowProductsSuppliedLastDays(20);

        dataWorking.ShowProviderWithMaxQuantity();
        dataWorking.ShowProviderWithMinQuantity();

        dataWorking.ShowProductTypeWithMaxQuantity();
        dataWorking.ShowProductTypeWithMinQuantity();

        Console.WriteLine("\nUpdate DATA");
        dataWorking.UpdateItem(1, productName: "SOSISON");

        dataWorking.InfoStorage();
        Console.WriteLine("\nRemove DATA");
        dataWorking.RemoveItem(1);

        dataWorking.InfoStorage();

        Console.ReadLine();

    }
}