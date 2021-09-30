using System.Collections.Generic;
using System.Threading.Tasks;
using Zooking.Core.Dtos;
using Zooking.Infrastructure.Specifications;
using Core.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace WebAPI.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class BaseMapController<TEntity, TDto> : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
  {

    private readonly IGenericRepository<TEntity> _repo;
    private readonly ILogger<BaseMapController<TEntity, TDto>> _logger;
    private readonly IMapper _mapper;

    public BaseMapController(IGenericRepository<TEntity> repo, ILogger<BaseMapController<TEntity, TDto>> logger, IMapper mapper)
    {
      _repo = repo;
      _logger = logger;
      _mapper = mapper;
    }


    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
      await SetTimeOut();
      var spec = new BaseSpecification<TEntity>();
      var entitis = await _repo.ListAsync(spec);
      // var mappedEntitis = _mapper.Map<IReadOnlyCollection<TEntity>, IReadOnlyCollection<TDto>>(entitis);
      var mappedEntitis = _mapper.Map<IReadOnlyList<TEntity>, IReadOnlyList<TDto>>(entitis);

      return Ok(mappedEntitis);
    }


    [HttpGet]
    [Route("getbyid")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> GetById([FromQuery] int id)
    {
      await SetTimeOut();
      var entity = await _repo.GetByIdAsync(id);
      return Ok(entity);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> Create(TEntity entity)
    {
      await SetTimeOut();
      if (entity == null)
        return BadRequest("Вы отправили пустой объект");

      var entityToReturn = await _repo.AddEntityAsync(entity as TEntity);
      return Ok(entityToReturn);
    }


    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> Update(TEntity entity)
    {
      await SetTimeOut();
      if (entity == null)
        return BadRequest("Вы отправили нам пустой объект");

      if (!(entity.Id >= 1))
        return BadRequest("У объекта должен быть id");

      _repo.Update(entity);
      _logger.LogInformation("значение было обновлено через update");
      return Ok(entity);
    }


    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> Delete([FromQuery] int id)
    {
      await SetTimeOut();
      var entity = await _repo.GetByIdAsync(id);
      _repo.Delete(entity);
      return Ok(entity);
    }


    private async Task<bool> SetTimeOut()
    {
      await Task.Delay(10);
      return true;
    }


  }
}