using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain
{
    public class BaseRequest
    {
        public BaseRequest()
        {
            CreatedBy = 1;
            ModifiedBy = 1;
            IsActive = 1;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }

        public int IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
