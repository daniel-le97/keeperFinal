namespace keeperFinal.Models;

public class Tag : IHasCreator
{
  public int Id { get; set; }
  public string name { get; set; }
  public int KeepId { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}