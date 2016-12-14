﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Model.Entities
{
    public class Campaign : IEntityBase
    {
        public Campaign()
        {
            Calls = new List<Call>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ICollection<Call> Calls { get; set; }

    }
}
