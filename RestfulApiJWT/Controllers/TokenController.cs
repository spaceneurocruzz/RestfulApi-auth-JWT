using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using RestfulApiJWT.Models;

namespace RestfulApiJWT.Controllers
{
	[Route("api/[controller]")]
	public class TokenController : Controller
	{
		private IConfiguration _config;

		public TokenController(IConfiguration config)
		{
			_config = config;
		}

		[AllowAnonymous]
		[Route("api/Authenticate/{project}")]
		[HttpPost]
		public IActionResult
			Authenticate([FromBody]LoginModel inputModel)
		{
			IActionResult response = Unauthorized();

			if (inputModel.Username == null || inputModel.Password == null)
				return BadRequest(new { result = "incorrect user or password" });

			if (inputModel.Username != "ADMIN" || inputModel.Password != "ADMIN")
				return BadRequest(new { result = "incorrect user or password" });

			UserModel user = new UserModel();
			user.Name = "ADMIN";
			user.Email = "ADMIN@admin.com";

			var tokenString = BuildToken(user);
			response = Ok(new { token = tokenString });

			return response;
		}

		private string BuildToken(UserModel user)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.Sub, user.Name),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			   };

			JwtSecurityToken token = new JwtSecurityToken(	_config["Jwt:Issuer"],
															_config["Jwt:Issuer"],
															claims,
															expires: DateTime.Now.AddMinutes(30),
															signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}