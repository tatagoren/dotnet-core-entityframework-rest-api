using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Api.ViewModels.Mappings
{
    public class AutoMapperConfiguration
    {

        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TwoWayRegularMappingProfile());
            });
        }
    }
}
