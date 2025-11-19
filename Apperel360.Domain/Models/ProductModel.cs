using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Domain.Models
{
    public class ProductMasterRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string ShortDetails { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int IsNew { get; set; }
        public int IsSale { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int TagId { get; set; }
    }

    public class ProductMasterResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string ShortDetails { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int IsNew { get; set; }
        public int IsSale { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int TagId { get; set; }
        public string ProductType { get; set; }
    }

    public class ProductListResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal salePrice { get; set; }
        public int discount { get; set; }
        public string shortDetails { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public int isNew { get; set; }
        public int isSale { get; set; }
        public string category { get; set; }
        [NotMapped]
        public List<string> pictures { get; set; }
        [NotMapped]
        public List<string> colors { get; set; }
        [NotMapped]
        public List<string> size { get; set; }
    }

    public class Variant
    {
        public string color { get; set; }
        public string images { get; set; }
    }

    public class ProductPictureMappingRequest : BaseRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string PictureName { get; set; }
        public string PicturePath { get; set; }
        public int IsDelete { get; set; }
    }

    public class ProductListPictures
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }

    public class Value
    {
        public int Id { get; set; }
    }
}
