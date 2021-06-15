using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Models
{
    public class IngressEgress : BaseEntity
    {
        public bool RetiredBefore { get; set; }
        public string DescriptionOut { get; set; } 
        public DateTime? IngressDateTime { get; set; }
        public DateTime? EgressDateTime { get; set; }
        //Se toma el ultimo QR Ordenado por decha Desc
        public long? DocumentEmployee { get; set; }
        public long? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        public long? DocumentForeman { get; set; }
        public long? ForemanId { get; set; }
        [ForeignKey("ForemanId")]
        public virtual Employee Foreman { get; set; }
        public long? LandId { get; set; }
        public virtual Land Land { get; set; }
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserEgressRegister { get; set; }
        public long? UserIdIngressRegister { get; set; }
        [ForeignKey("UserIdIngressRegister")]
        public virtual User UserIngressRegister { get; set; }
        public bool IsSyncToVisma { get; set; }
        public long? ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
        public long? TypeEmployeeId { get; set; }
        public virtual TypeEmployee TypeEmployee { get; set; }
        public long groupNumber { get; set; }
        public  bool isForeman { get; set; }
        public long? UserEditID { get; set; }
        [ForeignKey("UserEditID")]
        public virtual User UserEdit { get; set; }
        public DateTime? EditDateTime { get; set; }
        public bool isDeleted { get; set; }

        public int? ContractId { get; set; }
        public virtual Contract Contract { get; set; }

    }
}
