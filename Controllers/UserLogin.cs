using LearningSystem.DbContexts;
using LearningSystem.functions;
using LearningSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace LearningSystem.Controllers
{
 
  [Route("api/User")]
  [ApiController]
  
  public class UserLogin : ControllerBase
  {
    private IConfiguration _config;
    private static Claim[] AllClaim;
    private string ConnStr;
    private string UserID;
    private string Role;
    private string RoleID;
    private DBUser DBUser;
    public UserLogin(IConfiguration Configuration)
    {
      _config = Configuration;
      ConnStr = _config.GetConnectionString("MySqlConnection");
    }


    private string GerarTokenJWT(string username)
    {
      var issuer = _config["Jwt:Issuer"];
      var audience = _config["Jwt:Audience"];
      var expiry = DateTime.Now.AddMinutes(120);
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(issuer: issuer, audience: audience, AllClaim,
      expires: expiry, signingCredentials: credentials);
      var tokenHandler = new JwtSecurityTokenHandler();
      var stringToken = tokenHandler.WriteToken(token);
    
      return stringToken;
    }



    private static void setAllclaim(string userID, string username)
    {

      var claims = new[]
        {
         new Claim(ClaimTypes.Name, username),
         new Claim(ClaimTypes.NameIdentifier, userID)
    // Add more claims as needed
      };
         AllClaim = claims;
    }




    private bool ValidarUsuario(User loginDetalhes)
    {
      DBUser = new DBUser();
      var UserInfo = DBUser.GetUserToValidation(loginDetalhes.UserName, ConnStr);
      

     if(UserInfo.Count > 0)
     {
        var password = UserInfo[0].password;
        var UserId = UserInfo[0].UserID.ToString();
        UserID = UserInfo[0].UserID.ToString();
        Role = UserInfo[0].Role;
        RoleID = UserInfo[0].RoleID;
        if (loginDetalhes.password == password) 
        {
          setAllclaim(UserId, loginDetalhes.UserName);
          return true;
        }

     }
      return false;
    }

    
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] User loginDetalhes)
    {
    //  return new HttpResponseMessage()
    //  {
    //    Content = new StringContent(json, Encoding.UTF8, "application/json"),
    //    StatusCode = HttpStatusCode.OK
    //  };
    //}
    //  else
    //  {

    //    return new HttpResponseMessage()
    //{
    //  StatusCode = HttpStatusCode.Unauthorized
    //    };



    bool resultado = ValidarUsuario(loginDetalhes);
      string username = loginDetalhes.UserName;
      if (resultado)
      {
        var tokenString = GerarTokenJWT(username);

        return Ok(new { token = tokenString,
        UserName = loginDetalhes.UserName,
        UserId= UserID,
        Role=Role,
        RoleID= RoleID
        });
      }
      else
      {
        return Unauthorized();
      }
    }



    [HttpGet("profile")]
    [Authorize]
    public IActionResult GetProfileForMe()
    {
      // Access user claims
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var username = User.Identity.Name;
      var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

      // You can also access custom claims
      var customClaim = User.FindFirst("CustomClaimName")?.Value;

       var LogginedUser = DBUser.GetUserByID(userId, ConnStr);
      // Return user information
      var userProfile = new
      {
        UserId = userId,
        Username = username,
        Roles = LogginedUser[0].Role,
        FirstName = LogginedUser[0].FirstName,
        LastName = LogginedUser[0].LastName,
        Email = LogginedUser[0].Email,
        Active_Status = LogginedUser[0].ActiveStatus,
        Personal_pic = LogginedUser[0].Picture
      };

      return Ok(userProfile);
    }




    [HttpPost("logout")]

    public IActionResult logout()
    {
      // Get the user's token from the request (e.g., from headers)
      var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

      // Revoke the user's token
      TokenRevocationService.RevokeToken(token);

      return Ok("Logged out successfully");
    }
  }




}

