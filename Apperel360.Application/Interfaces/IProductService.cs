using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Interfaces
{
    public interface IProductService
    {
        ProductMasterResponse Add(ProductMasterRequest viewModel);
        ProductMasterResponse Update(ProductMasterRequest viewModel);
        long Delete(int ID);
        ProductMasterResponse GetbyId(int Id);
        List<ProductListPictures> GetProductPicturebyId(int Id);
        IEnumerable<ProductMasterResponse> GetAll();
        long AddImageMapping(ProductPictureMappingRequest viewModel);
        //For forntend data
        //ResultDto<IEnumerable<ProductListResponse>> GetProductList();
        List<ProductListResponse> GetProductList();
        List<ProductListColors> GetProductcolorsList();
        List<ProductListPictures> GetProductPicturesList();
        List<ProductListSizes> GetProductSizesList();
    }
}
