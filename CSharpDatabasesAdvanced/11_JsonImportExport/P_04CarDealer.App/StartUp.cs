using System;

namespace P_04CarDealer.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var manager = new CarDealerManager();

            manager.ResetDatabase();

            manager.ImportData();

            manager.OrderedCustomersExport();

            manager.FerrariCarsByDistanceExport();

            manager.LocalSuppliersExport();

            manager.CarsWithTheirListOfPartsExport();

            manager.TotalSalesByCustomerExport();

            manager.SalesWithAppliedDiscountExport();
        }
    }
}
