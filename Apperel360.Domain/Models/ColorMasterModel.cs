using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Models
{
    public class ColorMasterModel
    {
    }

    public class ColorMasterRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class ProductListColors
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
}
