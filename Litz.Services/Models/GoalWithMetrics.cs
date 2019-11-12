namespace Litz.Services.Models
{
    public class GoalWithMetrics
    {
        public string GoalName { get; set; }
        public string Color{ get; set; }
        public decimal ExpectedExpenses { get; set; }
        public decimal ActualExpenses{ get; set; }
    }
}
