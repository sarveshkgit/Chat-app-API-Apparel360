using Apperel360.Domain.Interfaces;
using Apperel360.Domain.Models;
using Apperel360.Infrastructure.Data.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Infrastructure.Data.Repositories
{

    public class ProductRepository : IProductRepository
    {
        IDapperDbContext _dapper;
        public ProductRepository(IDapperDbContext dapper)
        {
            _dapper = dapper;
        }

        public ProductMasterResponse Add(ProductMasterRequest viewModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Name", viewModel.Name, System.Data.DbType.String);
            dynamicParameters.Add("@Title", viewModel.Title, System.Data.DbType.String);
            dynamicParameters.Add("@Code", viewModel.Code, System.Data.DbType.String);
            dynamicParameters.Add("@Price", viewModel.Price, System.Data.DbType.Decimal);
            dynamicParameters.Add("@SalePrice", viewModel.SalePrice, System.Data.DbType.Decimal);
            dynamicParameters.Add("@ShortDetails", viewModel.ShortDetails, System.Data.DbType.String);
            dynamicParameters.Add("@Description", viewModel.Description, System.Data.DbType.String);
            dynamicParameters.Add("@Quantity", viewModel.Quantity, System.Data.DbType.Int32);
            dynamicParameters.Add("@Discount", viewModel.Discount, System.Data.DbType.Int32);
            dynamicParameters.Add("@IsNew", viewModel.IsNew, System.Data.DbType.Int32);
            dynamicParameters.Add("@IsSale", viewModel.IsSale, System.Data.DbType.Int32);
            dynamicParameters.Add("@CategoryId", viewModel.CategoryId, System.Data.DbType.Int32);
            dynamicParameters.Add("@ColorId", viewModel.ColorId, System.Data.DbType.Int32);
            dynamicParameters.Add("@SizeId", viewModel.SizeId, System.Data.DbType.Int32);
            dynamicParameters.Add("@TagId", viewModel.TagId, System.Data.DbType.Int32);
            dynamicParameters.Add("@CreatedBy", viewModel.CreatedBy, System.Data.DbType.Int32);

            return _dapper.ExecuteGet<ProductMasterResponse>("InsertProductMaster", dynamicParameters);
        }

        public long AddImageMapping(ProductPictureMappingRequest viewModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ProductId", viewModel.ProductId, System.Data.DbType.Int32);
            dynamicParameters.Add("@PictureName", viewModel.PictureName, System.Data.DbType.String);
            dynamicParameters.Add("@PicturePath", viewModel.PicturePath, System.Data.DbType.String);
            dynamicParameters.Add("@IsDelete", viewModel.IsDelete, System.Data.DbType.Int32);
            dynamicParameters.Add("@CreatedBy", viewModel.CreatedBy, System.Data.DbType.Int32);

            return _dapper.Execute("InsertProductPictureMapping", dynamicParameters);
        }

        public long Delete(int ID)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ID", ID, System.Data.DbType.Int32);
            return _dapper.Execute("DeleteProductMaster", dynamicParameters);
        }

        public IEnumerable<ProductMasterResponse> GetAll()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<ProductMasterResponse>("GetAllProductMaster", dynamicParameters);
        }

        public ProductMasterResponse GetbyId(int Id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Id, System.Data.DbType.Int32);
            return _dapper.ExecuteGet<ProductMasterResponse>("GetProductMasterbyId", dynamicParameters);
        }

        public List<ProductListColors> GetProductcolorsList()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<ProductListColors>("proc_GetProductcolorsList", dynamicParameters);
        }

        public List<ProductListResponse> GetProductList()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<ProductListResponse>("proc_GetProductList", dynamicParameters);
        }

        public List<ProductListPictures> GetProductPicturebyId(int Id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Id, System.Data.DbType.Int32);
            return _dapper.ExecuteGetAll<ProductListPictures>("GetProductPicturebyId", dynamicParameters);
        }

        public List<ProductListPictures> GetProductPicturesList()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<ProductListPictures>("proc_GetProductPicturesList", dynamicParameters);
        }

        public List<ProductListSizes> GetProductSizesList()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            return _dapper.ExecuteGetAll<ProductListSizes>("proc_GetProductSizesList", dynamicParameters);
        }

        public ProductMasterResponse Update(ProductMasterRequest viewModel)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", viewModel.Id, System.Data.DbType.Int32);
            dynamicParameters.Add("@Name", viewModel.Name, System.Data.DbType.String);
            dynamicParameters.Add("@Title", viewModel.Title, System.Data.DbType.String);
            dynamicParameters.Add("@Code", viewModel.Code, System.Data.DbType.String);
            dynamicParameters.Add("@Price", viewModel.Price, System.Data.DbType.Decimal);
            dynamicParameters.Add("@SalePrice", viewModel.SalePrice, System.Data.DbType.Decimal);
            dynamicParameters.Add("@ShortDetails", viewModel.ShortDetails, System.Data.DbType.String);
            dynamicParameters.Add("@Description", viewModel.Description, System.Data.DbType.String);
            dynamicParameters.Add("@Quantity", viewModel.Quantity, System.Data.DbType.Int32);
            dynamicParameters.Add("@Discount", viewModel.Discount, System.Data.DbType.Int32);
            dynamicParameters.Add("@IsNew", viewModel.IsNew, System.Data.DbType.Int32);
            dynamicParameters.Add("@IsSale", viewModel.IsSale, System.Data.DbType.Int32);
            dynamicParameters.Add("@CategoryId", viewModel.CategoryId, System.Data.DbType.Int32);
            dynamicParameters.Add("@ColorId", viewModel.ColorId, System.Data.DbType.Int32);
            dynamicParameters.Add("@SizeId", viewModel.SizeId, System.Data.DbType.Int32);
            dynamicParameters.Add("@TagId", viewModel.TagId, System.Data.DbType.Int32);
            dynamicParameters.Add("@ModifiedBy", viewModel.ModifiedBy, System.Data.DbType.Int32);

            return _dapper.ExecuteGet<ProductMasterResponse>("UpdateProductMaster", dynamicParameters);
        }
    }
}
