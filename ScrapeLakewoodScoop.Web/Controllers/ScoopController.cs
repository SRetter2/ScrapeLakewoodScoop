using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrapeLakewoodScoop.Data;

namespace ScrapeLakewoodScoop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoopController : ControllerBase
    {
        [HttpGet]
        [Route("getall")]
        public IEnumerable<Scoop> GetAllScoops()
        {
            var repo = new ScoopRepository();
            return repo.GetScoops();
        }
    }
}