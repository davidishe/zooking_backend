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
using Microsoft.AspNetCore.Identity;
using Core.Identity;
using Core.Extensions;

namespace WebAPI.Controllers
{



  [AllowAnonymous]
  public class PhotoController : BaseApiController
  {


    private readonly IGenericRepository<Shelter> _sheltersRepo;
    private readonly IGenericRepository<Animal> _petsRepo;
    private readonly UserManager<HavenAppUser> _userManager;
    private readonly IMapper _mapper;


    public PhotoController(
      IGenericRepository<Animal> petsRepo,
      UserManager<HavenAppUser> userManager,
      IGenericRepository<Shelter> sheltersRepo,
      IMapper mapper)
    {
      _sheltersRepo = sheltersRepo;
      _petsRepo = petsRepo;
      _mapper = mapper;
      _userManager = userManager;
    }



    #region 3. Products CRUD functionality
    /* create, delete, update products */



    [AllowAnonymous]
    [HttpPost("{id}")]
    [Route("pets")]
    public async Task<ActionResult<Animal>> AddPhotoAnimal([FromQuery] int id)
    {
      await SetTimeOut();

      IFormFile file = Request.Form.Files[0];
      Animal item = await _petsRepo.GetByIdAsync(id);

      var deletePreviousFile = await DeleteFileFromServer(item.PictureUrl, "Users");
      if (!deletePreviousFile)
        return BadRequest("Что-то пошло не так при удалении файла");

      var pictureFileName = await SaveFileToServer(file, "Animals");
      item.PictureUrl = pictureFileName;
      _petsRepo.Update(item);
      item = await _petsRepo.GetByIdAsync(id);

      var productToReturn = _mapper.Map<Animal, AnimalToReturnDto>(item);
      return Ok(productToReturn);
    }


    [AllowAnonymous]
    [HttpPost("{id}")]
    [Route("shelters")]
    public async Task<ActionResult<Shelter>> AddPhotoShelter([FromQuery] int id)
    {

      await SetTimeOut();

      IFormFile file = Request.Form.Files[0];
      var item = await _sheltersRepo.GetByIdAsync(id);
      var deletePreviousFile = await DeleteFileFromServer(item.PictureUrl, "Users");
      if (!deletePreviousFile)
        return BadRequest("Что-то пошло не так при удалении файла");

      var pictureFileName = await SaveFileToServer(file, "Shelters");

      item.PictureUrl = pictureFileName;
      _sheltersRepo.Update(item);
      item = await _sheltersRepo.GetByIdAsync(id);

      var itemToReturn = _mapper.Map<Shelter, ShelterToReturnDto>(item);
      return Ok(itemToReturn);
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("user")]
    public async Task<ActionResult<UserToReturnDto>> AddPhotoUser()
    {
      var user = await _userManager.FindByClaimsCurrentUser(HttpContext.User);
      IFormFile file = Request.Form.Files[0];

      var deletePreviousFile = await DeleteFileFromServer(user.PictureUrl, "Users");
      if (!deletePreviousFile)
        return BadRequest("Что-то пошло не так при удалении файла");

      var pictureFileName = await SaveFileToServer(file, "Users");

      user.PictureUrl = pictureFileName;
      var result = await _userManager.UpdateAsync(user);

      if (!result.Succeeded)
        return BadRequest("Ошибка при обновлении пользователя");

      user = await _userManager.FindByClaimsCurrentUser(HttpContext.User);
      var userToReturn = _mapper.Map<HavenAppUser, UserToReturnDto>(user);
      return Ok(userToReturn);
    }


    private Task<string> SaveFileToServer(IFormFile file, string subPath)
    {
      var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
      var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", subPath, fileName);

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }

      return Task.FromResult(fileName);

    }

    private Task<bool> DeleteFileFromServer(string pictureFileName, string subPath)
    {
      var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", subPath, pictureFileName);
      if (System.IO.File.Exists(fullPath))
      {
        System.IO.File.Delete(fullPath);
      }

      return Task.FromResult(true);

    }


    private async Task<bool> SetTimeOut()
    {
      await Task.Delay(100);
      return true;
    }

    #endregion

  }
}