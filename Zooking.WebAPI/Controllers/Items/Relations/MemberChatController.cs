using System.Threading.Tasks;
using Zooking.Core.Dtos;
using Zooking.Core.Models.Members;
using Core.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace Zooking.WebAPI.Controllers.Items
{

  [AllowAnonymous]
  public class MemberChatController : BaseApiController
  {

    private readonly IGenericRepository<MemberChat> _memberChatRepo;
    private readonly IGenericRepository<Chat> _chatRepo;
    private readonly IGenericRepository<Assistant> _assistantRepo;


    public MemberChatController(
        IGenericRepository<MemberChat> memberChatRepo,
        IGenericRepository<Chat> chatRepo,
        IGenericRepository<Assistant> assistantRepo
    )
    {
      _memberChatRepo = memberChatRepo;
      _chatRepo = chatRepo;
      _assistantRepo = assistantRepo;

    }


    [AllowAnonymous]
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<MemberChat>> Create(MemberChatDto itemDto)
    {

      var item = new MemberChat
      {
        ChatId = itemDto.ChatId,
        Chat = await _chatRepo.GetByIdAsync(itemDto.ChatId),
        AssistantId = itemDto.ChatId,
        Assistant = await _assistantRepo.GetByIdAsync(itemDto.Memberid)
      };

      var itemToReturn = await _memberChatRepo.AddEntityAsync(item);
      return Ok(itemToReturn);

    }





  }
}