using System;
using Core.Helpers;
using Core.Models;

namespace Infrastructure.Data.Spec
{
  public class AnimalSpecification : BaseSpecification<Animal>
  {
    public AnimalSpecification(UserParams userParams)
    : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.Name.ToLower().Contains(userParams.Search.ToLower())) &&
          (!userParams.regionId.HasValue || x.RegionId == userParams.regionId) &&
          (!userParams.typeId.HasValue || x.AnimalTypeId == userParams.typeId)
        )
    {
      AddInclude(x => x.Type);
      AddInclude(x => x.Region);
      ApplyPaging((userParams.PageSize * (userParams.PageIndex)), userParams.PageSize);
      AddOrderByDescending(x => x.EnrolledDate);

      if (!string.IsNullOrEmpty(userParams.sort))
      {
        switch (userParams.sort)
        {
          case "name":
            AddOrderByAscending(s => s.Name);
            break;
          default:
            AddOrderByAscending(x => x.EnrolledDate);
            break;
        }
      }
    }

    public AnimalSpecification(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.Type);
      AddInclude(x => x.Region);
    }
  }


}