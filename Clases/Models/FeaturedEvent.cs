using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
    public class FeaturedEvent : BaseEntity
    { 
        public string type { get; set; }
     
        public string Description { get; set; }
        public long? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserRegister { get; set; }

        public long? LastForemanId { get; set; }
        [ForeignKey("LastForemanId")]
        public virtual Employee LastForeman { get; set; }

         
        public long? IdCurrentForeman { get; set; }
        [ForeignKey("IdCurrentForeman")]
        public virtual Employee CurrentForeman { get; set; }

        public long? LastContractorId { get; set; }
        [ForeignKey("LastContractorId")]
        public virtual Contractor LastContractor { get; set; }

        public long? CurrentContractorId { get; set; }
        [ForeignKey("CurrentContractorId")]
        public virtual Contractor CurrentContractor { get; set; }


        public long? LastTypeEmployeeId { get; set; }
        [ForeignKey("LastTypeEmployeeId")]
        public virtual TypeEmployee LastTypeEmployee { get; set; }


        public long? CurrentTypeEmployeeId { get; set; }
        [ForeignKey("CurrentTypeEmployeeId")]
        public virtual TypeEmployee CurrentTypeEmployee { get; set; }




        public DateTime? IngressDateTime { get; set; }
         
    }
}
