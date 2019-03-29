using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegressionSearch.Classes;

namespace RegressionSearch.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class HeuristicaController : ControllerBase
    {
        public SlidingGame _slidingGame = new SlidingGame();
        [HttpPost]
        [Route("NewState")]
        public ActionResult<List<string>> GetNewState([FromBody] List<string> state )
        {
            return Ok(_slidingGame.NewState(state));
        }
    }
}