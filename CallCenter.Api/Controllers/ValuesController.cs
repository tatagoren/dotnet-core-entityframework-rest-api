using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CallCenter.Model.Entities;
using CallCenter.Api.ViewModels;

namespace CallCenter.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IMapper _mapper;

        public ValuesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Call c = _mapper.Map<Call>(new CallViewModel{ CallStatus = Model.Enums.CallStatusEnum.ANSWERED_SUCCESS});
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
