using System.Collections.Generic;
using System.Threading.Tasks;
using Zooking.Core.Dtos;
using Zooking.Infrastructure.Specifications;
using Core.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using Zooking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebAPI.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class BaseMapFilterController<TEntity, TDto> : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
  {

    private readonly IDbRepository<TEntity> _repo;
    private readonly ILogger<BaseMapFilterController<TEntity, TDto>> _logger;
    private readonly IMapper _mapper;

    public BaseMapFilterController(IDbRepository<TEntity> repo, ILogger<BaseMapFilterController<TEntity, TDto>> logger, IMapper mapper)
    {
      _repo = repo;
      _logger = logger;
      _mapper = mapper;
    }


    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
      var entitis = await _repo.GetAll().ToListAsync();
      var mappedEntitis = _mapper.Map<IReadOnlyList<TEntity>, IReadOnlyList<TDto>>(entitis);

      return Ok(mappedEntitis);
    }


    [HttpGet]
    [Route("getbyid")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> GetById([FromQuery] int id)
    {
      var entity = await _repo.GetByIdAsync(id);
      return Ok(entity);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> Create(TEntity entity)
    {
      if (entity == null)
        return BadRequest("Вы отправили пустой объект");

      await _repo.AddAsync(entity as TEntity);
      return Ok(200);
    }


    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> Update(TEntity entity)
    {
      if (entity == null)
        return BadRequest("Вы отправили нам пустой объект");

      if (!(entity.Id >= 1))
        return BadRequest("У объекта должен быть id");

      await _repo.UpdateAsync(entity);
      _logger.LogInformation("значение было обновлено через update");
      return Ok(entity);
    }


    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> Delete([FromQuery] int id)
    {
      var entity = await _repo.GetByIdAsync(id);
      await _repo.DeleteAsync(entity);
      return Ok(entity);
    }

    [HttpGet("backend")]
    public async Task<IActionResult> BackendAsync(
            [FromQuery(Name = "s")] string? s,
            [FromQuery(Name = "sort")] string? sort,
            [FromQuery(Name = "page")] int? page
        )
    {
      return Ok(await QueryAsync(s, sort, page));
    }


    public async Task<object> QueryAsync(string s, string sort, int? queryPage)
    {

      var query = _repo.GetAll();

      // if (!string.IsNullOrEmpty(s))
      // {
      //   query = query.Where(p => p.Title.Contains(s) || p.Description.Contains(s));
      // }

      // if (sort == "asc")
      // {
      //   query = query.OrderBy(p => p.Price);
      // }
      // else if (sort == "desc")
      // {
      //   query = query.OrderByDescending(p => p.Price);
      // }


      int perPage = 3;
      int page = queryPage.GetValueOrDefault(1) == 0 ? 1 : queryPage.GetValueOrDefault(1);
      var total = query.Count();

      return new
      {
        data = query.Skip((page - 1) * perPage).Take(perPage),
        total,
        page,
        last_page = total / perPage
      };
    }
  }





}