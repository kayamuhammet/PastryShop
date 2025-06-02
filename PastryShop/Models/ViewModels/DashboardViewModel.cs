namespace PastryShop.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int Last7DaysOrders { get; set; }
        public List<string> OrderChartLabels { get; set; } = new();
        public List<int> OrderChartData { get; set; } = new();
    }
}