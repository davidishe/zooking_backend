using System;
using Core.Helpers;
using Core.Models;

namespace Infrastructure.Data.Spec
{
  public class ShelterSpecification : BaseSpecification<Shelter>
  {
    public ShelterSpecification(UserParams userParams)
    : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.Name.ToLower().Contains(userParams.Search.ToLower())) &&
          (!userParams.regionId.HasValue || x.RegionId == userParams.regionId) &&
          (userParams.IsAdmin == true)
        )
    {
      AddInclude(x => x.Region);
      ApplyPaging((userParams.PageSize * (userParams.PageIndex)), userParams.PageSize);

      if (!string.IsNullOrEmpty(userParams.sort))
      {
        switch (userParams.sort)
        {
          case "animalcount":
            AddOrderByDescending(s => s.AnimalCount);
            break;
          case "name":
            AddOrderByAscending(s => s.Name);
            break;
          default:
            AddOrderByDescending(x => x.EnrolledDate);
            break;
        }
      }
    }

    public ShelterSpecification(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.Region);
    }
  }


}