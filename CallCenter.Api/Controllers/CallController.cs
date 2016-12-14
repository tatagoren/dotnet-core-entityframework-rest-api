using AutoMapper;
using CallCenter.Api.ViewModels;
using CallCenter.Data.Repository;
using CallCenter.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Api.Controllers
{
    [Route("api/[controller]")]
    public class CallController : Controller
    {
        private ICallRepository _callRepository;
        private ICustomerRepository _customerRepository;
        private ICampaignRepository _campaignRepository;
        private IMapper _mapper;

        public CallController(ICallRepository callRepository,
            ICustomerRepository customerRepository,
            ICampaignRepository campaignRepository,
            IMapper mapper)
        {
            _callRepository = callRepository;
            _customerRepository = customerRepository;
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Call> _calls = _callRepository.GetAll().ToList();
                IEnumerable<CallViewModel> _callsViewModel = _mapper.Map<IEnumerable<CallViewModel>>(_calls);


                return new OkObjectResult(_callsViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured");
            }
        }

        [HttpGet("{id}", Name = "GetCall")]
        public IActionResult Get(int id)
        {
            Call _call = _callRepository.GetSingle(id);
            if (_call == null)
            {
                return NotFound();
            }
            CallViewModel _callViewModel = _mapper.Map<CallViewModel>(_call);

            return new OkObjectResult(_callViewModel);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CallViewModel callViewModel)
        {
            if (ModelState.IsValid)
            {
                //Save To Db
                Call _call = _mapper.Map<CallViewModel, Call>(callViewModel);
                _call.Campaign = null;
                _call.Customer = null;
                _callRepository.Add(_call);

                if (await _callRepository.Commit())
                {
                    callViewModel = _mapper.Map<Call, CallViewModel>(_call);
                    //return Created($"api/customer/{customerModel.Name}", Mapper.Map<CustomerViewModel>(_customer));
                    CreatedAtRouteResult result = CreatedAtRoute("GetCustomer", new { controller = "Call", id = callViewModel.Id }, callViewModel);
                    return result;
                }
            }
            return BadRequest("Failed To Save the Call");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CallViewModel callModel)
        {
            if (ModelState.IsValid)
            {
                Call _call = _callRepository.GetSingle(id);

                _call.CallStatus = callModel.CallStatus;
                _call.Note = callModel.Note;
                _call.Time = DateTime.Now;
                _call.UserName = callModel.UserName;
                _call.CustomerId = callModel.CustomerId;
                _call.CampaignId = callModel.CampaignId;


                _callRepository.Update(_call);
                if (await _callRepository.Commit())
                {
                    return new NoContentResult();
                }
            }
            return BadRequest("Failed To Update Call");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Call _call = _callRepository.GetSingle(id);
            if (_call == null)
            {
                return NotFound();
            }

            _callRepository.Delete(_call);
            if (await _callRepository.Commit())
            {
                return new NoContentResult();
            }
            return BadRequest();
        }
    }
}
