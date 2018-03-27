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
using TypiconOnline.Repository.EFCore;
using TypiconOnline.AppServices.Implementations;
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        IRuleSerializerRoot _ruleSerializer;
        IMemoryCache _cache;

        public TestController(IRuleSerializerRoot ruleSerializer, IMemoryCache cache)
        {
            _ruleSerializer = ruleSerializer ?? throw new ArgumentNullException("IRuleSerializerRoot in TestController");
            _cache = cache ?? throw new ArgumentNullException("cache");
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var uof = new SQLiteUnitOfWork("Data\\SQLiteDB.db");

            var easters = uof.Repository<EasterItem>().GetAll();

            var arr = easters.Select(c => c.Date.ToShortDateString());

            return arr;
        }

        // GET api/<controller>/2015-05-13
        [HttpGet("{str}")]
        public string Get(string str)
        {
            if (DateTime.TryParseExact(str, "yyyy-MM-dd", new CultureInfo("ru-RU"), DateTimeStyles.None, out DateTime date))
            {
                var uof = new SQLiteUnitOfWork("Data\\SQLiteDB.db");

                var typicon = uof.Repository<TypiconEntity>().Get(c => c.Id == 1);

                var rule = typicon.GetModifiedRuleHighestPriority(date, _ruleSerializer);

                return rule.RuleEntity.Name;
            }
            else
            {
                return "Измененное Правило отсутствует";
            }
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
