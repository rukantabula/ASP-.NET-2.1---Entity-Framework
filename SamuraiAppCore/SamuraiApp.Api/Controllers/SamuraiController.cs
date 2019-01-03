using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.Api.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class SamuraiController : ControllerBase
    {
        private readonly SamuraiContext _context;

        public SamuraiController(SamuraiContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddSamurai")]
        public ActionResult<Samurai> AddSamurai(Samurai samurai) // parse a type of our chossing in ActionResult method signature
        {

            if (samurai == null)
            {
                return NotFound();
            }
            else
            {
                _context.Samurais.Add(samurai);
                _context.SaveChanges();

            }

            return Ok(samurai.Name + " is added successully!");
        }
    }
}