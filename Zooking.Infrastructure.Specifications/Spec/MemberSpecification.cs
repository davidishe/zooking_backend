using System;
using Core.Models;
using Core.Helpers;

namespace Zooking.Infrastructure.Specifications
{
  public class AssistantSpecification : BaseSpecification<Assistant>
  {
    public AssistantSpecification(UserParams userParams)
    : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.Name.ToLower().Contains(userParams.Search.ToLower())))
    {
      AddInclude(x => x.MemberChats);

      ApplyPaging(userParams.PageSize * userParams.PageIndex, userParams.PageSize);

      if (!string.IsNullOrEmpty(userParams.sort))
      {
        switch (userParams.sort)
        {
          case "name":
            AddOrderByAscending(s => s.Name);
            break;
          default:
            AddOrderByAscending(x => x.IsEnabled);
            break;
        }
      }
    }


    public AssistantSpecification()
    : base()
    {
      AddInclude(x => x.MemberChats);
    }



    public AssistantSpecification(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.MemberChats);
    }
  }


}