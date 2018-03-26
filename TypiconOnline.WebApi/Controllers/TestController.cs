using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFSQLite;
using TypiconOnline.AppServices.Implementations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var uof = new EFSQLiteUnitOfWork("Data\\SQLiteDB.db");

            var typicon = uof.Repository<TypiconEntity>().Get(c => c.Id == 1);

            IKernel kernel = new StandardKernel();

            var ruleSerializer = kernel.Get<IRuleSerializerRoot>();

            var rule = typicon.GetModifiedRuleHighestPriority(DateTime.Now, ruleSerializer);

            var easters = uof.Repository<EasterItem>().GetAll();

            var arr = easters.Select(c => c.Date.ToShortDateString());

            return arr;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return Directory.GetCurrentDirectory();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
