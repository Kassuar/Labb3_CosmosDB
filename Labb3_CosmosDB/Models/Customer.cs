namespace Labb3_CosmosDB.Models
{
    public class Customer
    {
      public string id { get; set; }

      public string Name { get; set; }

      public string Title { get; set; }

      public string Telephone { get; set; }

      public string Email { get; set; }

      public string Address { get; set; }

      public Seller Seller { get; set; }  
        
    }
}
