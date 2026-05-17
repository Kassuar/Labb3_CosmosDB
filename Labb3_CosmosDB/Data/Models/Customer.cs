namespace Labb3_CosmosDB.Data.Models
{
    public class Customer
    {
      public string id { get; set; }

      public string Name { get; set; } = string.Empty;

      public string Title { get; set; } = string.Empty;

      public string Telephone { get; set; } = string.Empty;

      public string Email { get; set; } = string.Empty;

      public string Address { get; set; } = string.Empty;

      public Seller Seller { get; set; }  
        
    }
}
