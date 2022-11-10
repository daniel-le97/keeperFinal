namespace keeperFinal.Models;


public class Vault : IHasCreator
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  
  public string CoverImg { get; set; } = "https://images.unsplash.com/photo-1609358905581-e5381612486e?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1011&q=80";
  public bool? IsPrivate { get; set; } = false;

  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

}