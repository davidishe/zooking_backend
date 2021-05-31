using Core.Models;

namespace Core.Dtos
{
  public class AnimalToCreate
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TypeId { get; set; }
    public int RegionId { get; set; }
    // public string UserName { get; set; }
  }
}