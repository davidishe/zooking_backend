using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data.Repos.GenericRepository;
using Infrastructure.Data.Spec;
using Core.Dtos;
using Core.Models;
using Core.Helpers;
using Core.Services;


namespace WebAPI.Controllers
{


  [AllowAnonymous]
  public class SheltersController : BaseApiController
  {

    private readonly IGenericRepository<Shelter> _sheltersRepo;
    private readonly IGenericRepository<Region> _regionRepo;
    private readonly IMapper _mapper;


    public SheltersController(IGenericRepository<Shelter> productsRepo, IGenericRepository<Region> productRegionRepo, IMapper mapper)
    {
      _sheltersRepo = productsRepo;
      _regionRepo = productRegionRepo;
      _mapper = mapper;
    }


    #region 1. Get products functionality

    [Authorize(Policy = "RequireModerator")]
    [HttpGet]
    [Route("all/admin")]
    public async Task<ActionResult> GetAllAdmin([FromQuery] UserParams userParams)
    {

      await SetTimeOut();
      userParams.IsAdmin = true;

      var specForCount = new ShelterForCountSpecification(userParams);
      var totalItems = await _sheltersRepo.CountAsync(specForCount);

      var spec = new ShelterSpecification(userParams);
      var products = await _sheltersRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Shelter>, IReadOnlyList<ShelterToReturnDto>>(products);
      await SetTimeOut();

      return Ok(new Pagination<ShelterToReturnDto>(userParams.PageIndex, userParams.PageSize, totalItems, data));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<Pagination<ShelterToReturnDto>>> GetAllClient([FromQuery] UserParams userParams)
    {

      userParams.IsAdmin = true;

      // count all items for pagination
      var specForCount = new ShelterForCountSpecification(userParams);
      var totalItems = await _sheltersRepo.CountAsync(specForCount);

      // count all items for 
      var spec = new ShelterSpecification(userParams);
      var products = await _sheltersRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Shelter>, IReadOnlyList<ShelterToReturnDto>>(products);
      await SetTimeOut();
      return Ok(new Pagination<ShelterToReturnDto>(userParams.PageIndex, userParams.PageSize, totalItems, data));
    }


    // [Cached(10)]
    // [AllowAnonymous]
    // [HttpGet("{id}")]
    // [Route("shelter")]
    // public async Task<ActionResult<ShelterToReturnDto>> GetProductByIdAsync([FromQuery] int id)
    // {
    //   var spec = new ShelterSpecification(id);
    //   var product = await _sheltersRepo.GetEntityWithSpec(spec);
    //   await SetTimeOut();
    //   return _mapper.Map<Shelter, ShelterToReturnDto>(product);
    // }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [Route("getbyid")]
    public async Task<ActionResult<ShelterToReturnDto>> GetProductIdByIdAsync([FromQuery] int id)
    {
      var item = await _sheltersRepo.GetByIdAsync(id);
      await SetTimeOut();

      var productRegion = _regionRepo.GetByIdAsync((int)item.Id).Result;
      item.Region = productRegion;
      return _mapper.Map<Shelter, ShelterToReturnDto>(item);
    }

    #endregion##############################################################################
    #region 2. Get regions functionality

    [AllowAnonymous]
    [HttpGet]
    [Route("regions")]
    public async Task<ActionResult<IReadOnlyList<AnimalType>>> GetProductRegionsByIdAsync()
    {
      var product = await _regionRepo.ListAllAsync();
      await SetTimeOut();
      return Ok(product);
    }
    #endregion

    #region 3. Products CRUD functionality
    /*
    create, delete, update products
    */

    [AllowAnonymous]
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Animal>> CreateProduct(ShelterToCreate animalToCreate)
    {

      await SetTimeOut();


      var productToReturn = new Shelter
      (
        animalToCreate.Name,
        "",
        animalToCreate.Description,
        (int)animalToCreate.RegionId,
        await _regionRepo.GetByIdAsync((int)animalToCreate.RegionId),
        animalToCreate.AnimalCount
      );

      var product = await _sheltersRepo.AddEntityAsync(productToReturn);
      return Ok(_mapper.Map<Shelter, ShelterToReturnDto>(product));

    }


    [AllowAnonymous]
    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<Shelter>> UpdateProduct(ShelterToCreate productForUpdate)
    {

      await SetTimeOut();

      var currentProduct = await _sheltersRepo.GetByIdAsync((int)productForUpdate.Id);

      currentProduct.Name = productForUpdate.Name;
      currentProduct.RegionId = (int)productForUpdate.RegionId;
      currentProduct.Description = productForUpdate.Description;
      currentProduct.AnimalCount = productForUpdate.AnimalCount;
      currentProduct.Region = _regionRepo.GetByIdAsync((int)productForUpdate.RegionId).Result;

      var updatedProduct = _sheltersRepo.Update(currentProduct);
      return Ok(_mapper.Map<Shelter, ShelterToReturnDto>(updatedProduct));

    }





    [AllowAnonymous]
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<Animal>> DeleteProduct(int id)
    {

      await SetTimeOut();
      var product = await _sheltersRepo.GetByIdAsync(id);
      _sheltersRepo.Delete(product);
      return Ok(202);
    }
    #endregion##############################################################################

    #region 4. Regions & types CRUD functionality

    [AllowAnonymous]
    [HttpPost]
    [Route("create-region")]
    public async Task<ActionResult<IReadOnlyList<AnimalType>>> CreateProductRegionAsync(Region region)
    {
      var product = await _regionRepo.AddEntityAsync(region);
      await SetTimeOut();
      return Ok(product);
    }

    #endregion##############################################################################

    #region 5. Private methods for service functionaluty
    private Task<string> SaveFileToServer(IFormFile file)
    {
      var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
      var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", "Products", fileName);

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }

      return Task.FromResult(fileName);

    }

    private async Task<bool> SetTimeOut()
    {
      await Task.Delay(100);
      return true;
    }

    #endregion##############################################################################

  }
}


