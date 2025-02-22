using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain
{
    public class BaseResponse
    {
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
    }
}
