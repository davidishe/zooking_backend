using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace Infrastructure.Data.Repos
{
  public interface IAnimalRepository
  {
    void Add<T>(T entity) where T : class;
    Task<bool> SaveAll();
    void Delete<T>(T entity) where T : class;
    Task<Animal> GetAnimalByIdAsync(int id);
    Task<PagedList<Animal>> GetAnimals(UserParams userParams);
    Task<IReadOnlyList<AnimalType>> GetAnimalTypesAsync();
    Task<IReadOnlyList<AnimalType>> CreateAnimalTypeAsync(AnimalType animalType);
    Task<IReadOnlyList<Region>> GetAnimalRegionsAsync();
    Task<IReadOnlyList<Region>> CreateAnimalRegionAsync(Region animalRegion);

  }
}