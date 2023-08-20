namespace Odev1_NorthWind.Models
{
    public class EmployeeCreateModel
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Title { get; set; }
      //  public DateTime HireDate { get; set; }
        public int? ReportsTo { get; set; }

    }
}
