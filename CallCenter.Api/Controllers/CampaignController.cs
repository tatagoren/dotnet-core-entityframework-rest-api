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
    public class CampaignController : Controller
    {
        private ICampaignRepository _campaignRepository;
        private IMapper _mapper;

        public CampaignController(ICampaignRepository campaignRepository,
            IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Campaign> _campaigns = _campaignRepository.GetAll();
                IEnumerable<CampaignViewModel> _campaignsViewModel = _mapper.Map<IEnumerable<CampaignViewModel>>(_campaigns);
                return new OkObjectResult(_campaignsViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured");
            }
        }

        [HttpGet("{id}", Name = "GetCampaign")]
        public IActionResult Get(int id)
        {
            Campaign _campaign = _campaignRepository.GetSingle(id);
            if (_campaign == null)
            {
                return NotFound();
            }
            CampaignViewModel _campaignViewModel = _mapper.Map<CampaignViewModel>(_campaign);
            return new OkObjectResult(_campaignViewModel);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CampaignViewModel campaignModel)
        {
            if (ModelState.IsValid)
            {
                Campaign _newCampaign = _mapper.Map<CampaignViewModel, Campaign>(campaignModel);
                _campaignRepository.Add(_newCampaign);

                if (await _campaignRepository.Commit())
                {
                    campaignModel = _mapper.Map<Campaign, CampaignViewModel>(_newCampaign);
                    CreatedAtRouteResult result = CreatedAtRoute("GetCampaign", new { controller = "Campaign", id = campaignModel.Id }, campaignModel);
                    return result;
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CampaignViewModel campaignModel)
        {
            if (ModelState.IsValid)
            {
                Campaign _campaign = _campaignRepository.GetSingle(id);

                _campaign.CreationDate = campaignModel.CreationDate;
                _campaign.Name = campaignModel.Name;
                _campaign.Description = campaignModel.Description;
                _campaign.ExpirationDate = campaignModel.ExpirationDate;

                _campaignRepository.Update(_campaign);
                if (await _campaignRepository.Commit())
                {
                    return new NoContentResult();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Campaign _campaign = _campaignRepository.GetSingle(id);


            if (_campaign == null)
            {
                return NotFound();
            }

            _campaignRepository.Delete(_campaign);
            if (await _campaignRepository.Commit())
            {
                return new NoContentResult();
            }
            return BadRequest();
        }
    }
}
