using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Model.Entities
{
    public class Customer : IEntityBase
    {
        public Customer()
        {
            Calls = new List<Call>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Note { get; set; }

        public ICollection<Call> Calls { get; set; }
    }
}
