using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyAppBack.Data;
using MyAppBack.Dtos;
using MyAppBack.Models;

namespace MyAppBack.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;

    public AuthController(IAuthRepository repo, IConfiguration config)
    {
      _config = config;
      _repo = repo;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
      // validate request
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

      if (await _repo.UserExists(userForRegisterDto.Username))
        return BadRequest("Username already exists");

      var userToCreate = new User
      {
        Username = userForRegisterDto.Username,
        FirstName = userForRegisterDto.FirstName,
        LastName = userForRegisterDto.LastName,
        PhoneNumber = userForRegisterDto.PhoneNumber,
        City = userForRegisterDto.City,
        Items = userForRegisterDto.UserLinks,
      };

      var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
      return Ok(201);

    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {

      var userFromRepo = await _repo.Login(
        userForLoginDto.username.ToLower(),
        userForLoginDto.password);

      // validate request
      if (userFromRepo == null)
        return Unauthorized();

      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
        new Claim(ClaimTypes.Name, userFromRepo.Username)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8
      .GetBytes(_config.GetSection("AppKeys:Token").Value));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return Ok(new
      {
        token = tokenHandler.WriteToken(token),
        userFromRepo.UserId
      });
    }

  }
}