using System;
using System.ComponentModel.DataAnnotations;

namespace VideothekData.Models
{
    public class Customer
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Newsletter Option")]
        public bool IsSubscribedToNewsletter { get; set; }

        [Min18AsMember]
        public DateTime? Birthday { get; set; }

        
        [Display(Name ="Membership Type")]
        public byte MembershipId { get; set; }
        public MembershipType Membership { get; set; }
    }
}
