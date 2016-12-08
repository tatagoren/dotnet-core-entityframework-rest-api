using AutoMapper;
using CallCenter.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Api.ViewModels.Mappings
{
    public class TwoWayRegularMappingProfile : Profile
    {
        public TwoWayRegularMappingProfile()
        {
            CreateMap<CustomerViewModel, Customer>().ReverseMap();
            CreateMap<CampaignViewModel, Campaign>().ReverseMap();
            CreateMap<CallViewModel, Call>().ReverseMap();
        }
    }
}
