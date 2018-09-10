using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRider.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter customer's name.")] //Custom message
        [StringLength(30)]
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Name
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        [Display(Name="Date of birth")]
        [DisplayFormat(DataFormatString ="{0:dd/MMM/yyyy}")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscibedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }

        [Display(Name="Membership type")]
        public byte MembershipTypeId { get; set; }
    }
}
