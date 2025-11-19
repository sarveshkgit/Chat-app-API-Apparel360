using Apperel360.Application.Interfaces;
using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
           _productRepository= productRepository;
        }

        public ProductMasterResponse Add(ProductMasterRequest viewModel)
        {
            return _productRepository.Add(viewModel);
        }

        public long AddImageMapping(ProductPictureMappingRequest viewModel)
        {
            return _productRepository.AddImageMapping(viewModel);
        }

        public long Delete(int ID)
        {
            return _productRepository.Delete(ID);   
        }

        public IEnumerable<ProductMasterResponse> GetAll()
        {
            return _productRepository.GetAll(); 
        }

        public ProductMasterResponse GetbyId(int Id)
        {
            return _productRepository.GetbyId(Id);
        }

        public List<ProductListColors> GetProductcolorsList()
        {
            return _productRepository.GetProductcolorsList();
        }

        public List<ProductListResponse> GetProductList()
        {
            return _productRepository.GetProductList();
        }

        public List<ProductListPictures> GetProductPicturebyId(int Id)
        {
           return _productRepository.GetProductPicturebyId(Id);
        }

        public List<ProductListPictures> GetProductPicturesList()
        {
            return _productRepository.GetProductPicturesList();
        }

        public List<ProductListSizes> GetProductSizesList()
        {
            return _productRepository.GetProductSizesList();
        }

        public ProductMasterResponse Update(ProductMasterRequest viewModel)
        {
            return _productRepository.Update(viewModel);
        }
    }
}
