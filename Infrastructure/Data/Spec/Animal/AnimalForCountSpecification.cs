using System;
using Core.Helpers;
using Core.Models;

namespace Infrastructure.Data.Spec
{
  public class AnimalForCountSpecification : BaseSpecification<Animal>
  {
    public AnimalForCountSpecification(UserParams userParams)
          : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.Name.ToLower().Contains(userParams.Search.ToLower())) &&
          (!userParams.regionId.HasValue || x.RegionId == userParams.regionId) &&
          (!userParams.typeId.HasValue || x.AnimalTypeId == userParams.typeId)

        )
    {

    }

  }

}