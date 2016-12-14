using CallCenter.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Api.ViewModels
{
    public class CallViewModel
    {
        public int Id { get; set; }

        public CallStatusEnum CallStatus { get; set; }

        public DateTime Time { get; set; }

        public string Note { get; set; }

        public int CustomerId { get; set; }

        public int CampaignId { get; set; }

        public string UserName { get; set; }
    }
}
