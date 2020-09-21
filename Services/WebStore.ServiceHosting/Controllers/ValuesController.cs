using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value-{i}")
            .ToList();

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => values;
        
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            if (id >= values.Count)
            {
                return NotFound();
            }

            return values[id];
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, string value)
        {
            if (id < 0 || id >= values.Count)
            {
                return BadRequest();
            }

            values[id] = value;

            return Ok();
        }

        [HttpPost]
        public ActionResult Post(string value)
        {
            values.Add(value);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            if (id >= values.Count)
            {
                return NotFound();
            }

            values.RemoveAt(id);

            return Ok();
        }
    }
}
