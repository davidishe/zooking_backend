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
using Core.Helpers;
using Core.Models;

namespace WebAPI.Controllers
{


  [AllowAnonymous]
  public class AnimalsController : BaseApiController
  {

    private readonly IGenericRepository<Animal> _itemsRepo;
    private readonly IGenericRepository<AnimalType> _itemTypeRepo;
    private readonly IGenericRepository<Region> _itemRegionRepo;
    private readonly IMapper _mapper;


    public AnimalsController(IGenericRepository<Animal> productsRepo, IGenericRepository<AnimalType> productTypeRepo, IGenericRepository<Region> productRegionRepo, IMapper mapper)
    {
      _itemsRepo = productsRepo;
      _itemTypeRepo = productTypeRepo;
      _itemRegionRepo = productRegionRepo;
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

      var specForCount = new AnimalForCountSpecification(userParams);
      var totalItems = await _itemsRepo.CountAsync(specForCount);

      var spec = new AnimalSpecification(userParams);
      var products = await _itemsRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Animal>, IReadOnlyList<AnimalToReturnDto>>(products);

      return Ok(new Pagination<AnimalToReturnDto>(userParams.PageIndex, userParams.PageSize, totalItems, data));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<Pagination<AnimalToReturnDto>>> GetAllClient([FromQuery] UserParams userParams)
    {

      userParams.IsAdmin = true;

      // count all items for pagination
      var specForCount = new AnimalForCountSpecification(userParams);
      var totalItems = await _itemsRepo.CountAsync(specForCount);

      // count all items for 
      var spec = new AnimalSpecification(userParams);
      var products = await _itemsRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Animal>, IReadOnlyList<AnimalToReturnDto>>(products);
      await SetTimeOut();
      return Ok(new Pagination<AnimalToReturnDto>(userParams.PageIndex, userParams.PageSize, totalItems, data));
    }


    // [Cached(10)]
    [AllowAnonymous]
    [HttpGet("{id}")]
    [Route("getbyid")]
    public async Task<ActionResult<AnimalToReturnDto>> GetProductByIdAsync([FromQuery] int id)
    {
      var spec = new AnimalSpecification(id);
      await SetTimeOut();
      var product = await _itemsRepo.GetEntityWithSpec(spec);
      return _mapper.Map<Animal, AnimalToReturnDto>(product);

    }

    [AllowAnonymous]
    [HttpGet("{guId}")]
    [Route("getproductid")]
    public async Task<ActionResult<AnimalToReturnDto>> GetProductIdByGuIdAsync([FromQuery] int guId)
    {
      var product = await _itemsRepo.GetByGuIdAsync(guId);
      await SetTimeOut();

      var productType = _itemTypeRepo.GetByIdAsync(product.AnimalTypeId).Result;
      var productRegion = _itemRegionRepo.GetByIdAsync(product.RegionId).Result;
      product.Type = productType;
      product.Region = productRegion;

      return _mapper.Map<Animal, AnimalToReturnDto>(product);
    }

    #endregion

    #region 2. Get regions & types functionality
    // [Cached(600)]
    [AllowAnonymous]
    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<IReadOnlyList<AnimalType>>> GetProductTypesByIdAsync()
    {
      var product = await _itemTypeRepo.ListAllAsync();
      await SetTimeOut();
      return Ok(product);
    }


    #endregion

    #region 3. Products CRUD functionality
    /* create, delete, update products */

    [AllowAnonymous]
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Animal>> CreateProduct(AnimalToCreate animalToCreate)
    {

      var animal = new Animal
      (
        animalToCreate.Name,
        "",
        animalToCreate.Description,
        animalToCreate.TypeId,
        await _itemTypeRepo.GetByIdAsync(animalToCreate.TypeId),
        animalToCreate.RegionId,
        await _itemRegionRepo.GetByIdAsync(animalToCreate.RegionId)
      );

      var newAnimal = _itemsRepo.AddEntityAsync(animal).Result;
      var animalToReturn = _mapper.Map<Animal, AnimalToReturnDto>(newAnimal);

      return Ok(animalToReturn);


    }


    [AllowAnonymous]
    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<Animal>> UpdateProduct(AnimalToCreate productForUpdate)
    {

      await SetTimeOut();

      var currentProduct = await _itemsRepo.GetByIdAsync((int)productForUpdate.Id);

      currentProduct.Name = productForUpdate.Name;
      currentProduct.Description = productForUpdate.Description;
      currentProduct.RegionId = productForUpdate.RegionId;
      currentProduct.Region = _itemRegionRepo.GetByIdAsync(productForUpdate.RegionId).Result;
      currentProduct.AnimalTypeId = productForUpdate.TypeId;
      currentProduct.Type = _itemTypeRepo.GetByIdAsync(productForUpdate.TypeId).Result;


      var updatedProduct = _itemsRepo.Update(currentProduct);
      return Ok(_mapper.Map<Animal, AnimalToReturnDto>(updatedProduct));

    }


    [AllowAnonymous]
    [HttpPatch]
    [Route("sale")]
    public async Task<ActionResult<Animal>> SaleSettings(int id)
    {

      await SetTimeOut();
      var product = await _itemsRepo.GetByIdAsync(id);
      product.IsSale = !product.IsSale;
      _itemsRepo.Update(product);
      return Ok(205);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<Animal>> DeleteProduct(int id)
    {

      await SetTimeOut();
      var product = await _itemsRepo.GetByIdAsync(id);
      _itemsRepo.Delete(product);
      return Ok(202);
    }


    #endregion

    #region 4. Regions & types CRUD functionality

    [AllowAnonymous]
    [HttpPost]
    [Route("create-type")]
    public async Task<ActionResult<IReadOnlyList<AnimalType>>> CreateProductTypeAsync(AnimalType productType)
    {
      var animal = await _itemTypeRepo.AddEntityAsync(productType);
      await SetTimeOut();
      return Ok(animal);
    }

    #endregion

    #region 5. Private methods for service functionaluty

    private async Task<bool> SetTimeOut()
    {
      await Task.Delay(100);
      return true;
    }

    #endregion

  }
}