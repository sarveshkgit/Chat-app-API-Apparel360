using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Models
{
    internal class SizeMasterModel
    {
    }
    public class SizeMasterRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductListSizes
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
}
