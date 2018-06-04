using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestfulApiJWT.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[Route("api/GetAll")]
		[HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "Yoda", "Darth Vadar", "Han Solo" };
        }

		[Route("api/Get")]
		[HttpGet("{id}")]
        public string Get(int id)
        {
			switch (id)
			{
				case 1:
					return "Yoda";
				case 2:
					return "Darth Vadar";
				case 3:
					return "Han Solo";
				default:
					return "nothing here";
			}
        }
    }
}
