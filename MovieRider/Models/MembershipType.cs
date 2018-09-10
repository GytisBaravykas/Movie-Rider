using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRider.Models
{
    public class MembershipType
    {
        /*private List<string> MbTypes = new List<string>() {"Pay as you go","Monthly","Season", "Year"};
        public string GetType
        {
            get
            {
                return MbTypes[Id-1];
            }
        }*/
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}
