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
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        private ICallRepository _callRepository;
        private IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository,
            ICallRepository callRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _callRepository = callRepository;
            _mapper = mapper;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                //IEnumerable<Customer> _customers = _customerRepository.AllIncluding(t => t.Calls);
                IEnumerable<Customer> _customers = _customerRepository.GetAll();
                IEnumerable<CustomerViewModel> _customersViewModel = _mapper.Map<IEnumerable<CustomerViewModel>>(_customers);
                return new OkObjectResult(_customersViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured");
            }
        }

        [HttpGet("{id}", Name ="GetCustomer")]
        public IActionResult Get(int id)
        {
            Customer _customer = _customerRepository.GetSingle(id);
            if (_customer == null)
            {
                return NotFound();
            }
            CustomerViewModel _customerViewModel = _mapper.Map<CustomerViewModel>(_customer);
            return new OkObjectResult(_customerViewModel);
        }

        // GET api/Issues/special/5
        [HttpGet("{id}/calls")]
        public IActionResult GetDetail(int id)
        {
            Customer _customer = _customerRepository.GetSingle(id);
            if (_customer == null)
            {
                return NotFound();
            }
            IEnumerable<CallViewModel> _callsViewModel = _mapper.Map<IEnumerable<CallViewModel>>(_callRepository.FindBy(c => c.CustomerId == id));
            //IEnumerable<Call> _calls = _callRepository.GetAll();
            //IEnumerable<CallViewModel> _callsviewModel = _mapper.Map<IEnumerable<CallViewModel>>(_calls);
            return new OkObjectResult(_callsViewModel);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CustomerViewModel customerModel)
        {
            if (ModelState.IsValid)
            {
                Customer _newCustomer = _mapper.Map<CustomerViewModel, Customer>(customerModel);
                _customerRepository.Add(_newCustomer);

                if (await _customerRepository.Commit())
                {
                    customerModel = _mapper.Map<Customer, CustomerViewModel>(_newCustomer);
                    CreatedAtRouteResult result = CreatedAtRoute("GetCustomer", new { controller = "Customer", id = customerModel.Id }, customerModel);
                    return result;
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CustomerViewModel customerModel)
        {
            if (ModelState.IsValid)
            {
                Customer _customer = _customerRepository.GetSingle(id);

                _customer.Email = customerModel.Email;
                _customer.Name = customerModel.Name;
                _customer.Phone = customerModel.Phone;
                _customer.Surname = customerModel.Surname;

                _customerRepository.Update(_customer);
                if (await _customerRepository.Commit())
                {
                    return new NoContentResult();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Customer _customer = _customerRepository.GetSingle(id);


            if (_customer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(_customer);
            if (await _customerRepository.Commit())
            {
                return new NoContentResult();
            }
            return BadRequest();
        }
    }
}
