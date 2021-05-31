using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Contexts;
using Core.Helpers;
using Core.Models;

namespace Infrastructure.Data.Repos
{
  public class AnimalRepository : IAnimalRepository
  {

    private readonly DataContext _context;

    public AnimalRepository(DataContext context)
    {
      _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<Animal> GetAnimalByIdAsync(int id)
    {
      var Animal = await _context.Animals.Include(p => p.Id).FirstOrDefaultAsync();
      return Animal;
    }

    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PagedList<Animal>> GetAnimals(UserParams userParams)
    {
      var Animals = _context.Animals;
      return await PagedList<Animal>.CreateAsync(Animals, userParams.PageIndex, userParams.PageSize);
    }

    public async Task<IReadOnlyList<AnimalType>> GetAnimalTypesAsync()
    {
      return await _context.AnimalTypes.ToListAsync();
    }

    public async Task<IReadOnlyList<Region>> GetAnimalRegionsAsync()
    {
      return await _context.Regions.ToListAsync();
    }

    public async Task<IReadOnlyList<AnimalType>> CreateAnimalTypeAsync(AnimalType animalType)
    {
      var types = _context.AnimalTypes;
      _context.Add(animalType);
      _context.SaveChanges();
      return await _context.AnimalTypes.ToListAsync();
    }

    public async Task<IReadOnlyList<Region>> CreateAnimalRegionAsync(Region region)
    {
      var types = _context.Regions;
      _context.Add(region);
      _context.SaveChanges();
      return await _context.Regions.ToListAsync();
    }
  }
}