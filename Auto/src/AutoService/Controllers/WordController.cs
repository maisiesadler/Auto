using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auto;
using Auto.Corrections;
using Auto.Corrections.ChangeLetter;
using Auto.Corrections.Suggestions;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    public class WordController : Controller
    {
        IFindWordManager _findWordManager;

        public WordController()
        { 
            _findWordManager = new FindWordManager();
            _findWordManager.AddTest(new AlternateOneClose());
            _findWordManager.AddTest(new GetSwitched());
            _findWordManager.AddTest(new RemoveDoubles(), 1);
            _findWordManager.AddTest(new KeepCommonCombinations());
            _findWordManager.AddTest(new AddDouble());
            //_findWordManager.AddTest(new KeepCommonCombinationsAll());
            //findWord.AddTest(new AlternateAllClose());
            //_findWordManager.AddTest(new AlternateOneAll());

            _findWordManager.AddTest(new SuggestCommon());
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Idea> Get()
        {
            yield return new Idea("Type to get suggestions...", 1000);
        }

        // GET api/values/5
        [HttpGet("{word}")]
        public IEnumerable<Idea> Get(string word)
        {
            return _findWordManager.Find(word);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
