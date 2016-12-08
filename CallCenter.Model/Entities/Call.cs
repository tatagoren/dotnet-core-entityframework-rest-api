using CallCenter.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Model.Entities
{
    public class Call : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public CallStatusEnum CallStatus { get; set; }

        public DateTime Time { get; set; }

        public string Note { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int CampaignId { get; set; }

        public Campaign Campaign { get; set; }

        public string UserName { get; set; }
    }
}
