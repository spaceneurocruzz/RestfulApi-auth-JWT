using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
		[HttpPost]
		public IActionResult CreateToken([FromBody]LoginModel login)
		{
			
		}

		private string BuildToken(UserModel user)
		{
		
		}

	}
}