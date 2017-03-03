using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SPG.Data.EventSource.Command;
using SPG.EventSourcing.CommandBus;
//using SPG.Data.CQRS.Commands;
using SPG.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPG.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommandsController : Controller
    {
        private readonly ICommandBus _bus;
        public CommandsController(ICommandBus bus)
        {
            _bus = bus;
            //_bus = ServiceLocator.Bus;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {

            _bus.ExecuteAsync(new CreateEventCommand(Guid.NewGuid(), Guid.NewGuid(), -1, DateTime.Now, RandomNumberGenerator.NumberValue(1, 250), RandomNumberGenerator.NumberValue(0, 20),
                 RandomNumberGenerator.ByteArrayValue(160000), RandomNumberGenerator.NumberValue(1, 1000), RandomNumberGenerator.NumberValue(1, 5000000))
                );

            //_bus.Send(new CreateSampleCommand(Guid.NewGuid(), DateTime.Now,
            //        RandomNumberGenerator.NumberValue(1, 1000),
            //        RandomNumberGenerator.NumberValue(1, 5000000),
            //        RandomNumberGenerator.NumberValue(1, 250),
            //        RandomNumberGenerator.NumberValue(0, 20),
            //        RandomNumberGenerator.ByteArrayValue(160000)));
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
