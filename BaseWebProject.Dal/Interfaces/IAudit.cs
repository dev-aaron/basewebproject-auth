using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseWebProject.DAL.Interfaces
{
    public interface IAudit
    {
        DateTime? CreatedOn { get; set; }
        int? CreatedBy_Member_Id { get; set; }
        DateTime? LastModifiedOn { get; set; }
        int? ModifiedBy_Member_Id { get; set; }
    }
}
