using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [Route("GetSamurai")]
        public ActionResult<Samurai> GetSamurai() // parse a type of our chossing in ActionResult method signature
        {
            //var samurais = _context.Samurais.ToList();
            // var samurais = _context.Samurais.Where(s => s.Name.Contains("Weeder")).ToList();
            var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "%r")).ToList();
            if (samurais == null)
            {
                return NotFound();
            }

            /*
            foreach (var s in samurais)
            {
                return Ok(s.Name);
            }
            */

            return Ok(samurais);
        }

        [HttpGet]
        [Route("GetFirstSamurai")]
        public ActionResult<string> GetFirstSamurai() // parse a type of our chossing in ActionResult method signature
        {
            //var samurais = _context.Samurais.Where(s => s.Id == 1);
            var samurais = _context.Samurais.LastOrDefault();
            if (samurais == null)
            {
                return NotFound();
            }

            return Ok(samurais);
        }
    }
}