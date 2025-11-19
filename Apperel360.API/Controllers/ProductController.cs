using Apperel360.Application.Interfaces;
using Apperel360.Application.Logic.Interfaces;
using Apperel360.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace Apperel360.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private IProductService _productService;
        private IJwtToken _jwtToken;
        private IAppUtility _appUtility;
        public ProductController(IProductService productService, IJwtToken jwtToken, IAppUtility appUtility, IWebHostEnvironment env)
        {
            _productService = productService;
            _jwtToken = jwtToken;
            _appUtility = appUtility;
            _env = env;
        }

        //[HttpGet, Route("GetProductList")]
        [HttpGet]
        [ActionName("GetProductList")]
        public IActionResult GetProductList()
        {
            try
            {
                var resDetails = _productService.GetProductList();
                var resColor = _productService.GetProductcolorsList();
                var resSize = _productService.GetProductSizesList();
                var resPicture = _productService.GetProductPicturesList();


                List<string> lstList;

                List<ProductListColors> lstProductListColors;
                List<ProductListSizes> lstProductListSizes;
                List<ProductListPictures> lstProductListPictures;


                foreach (var row in resDetails)
                {
                    //For Colors
                    lstList = new List<string>();
                    lstProductListColors = resColor.ToList();
                    foreach (var color in lstProductListColors.FindAll(o => o.ProductId == row.id))
                    {
                        lstList.Add(color.Name);
                    }
                    row.colors = lstList;

                    //For Sizes
                    lstList = new List<string>();
                    lstProductListSizes = resSize.ToList();
                    foreach (var size in lstProductListSizes.FindAll(o => o.ProductId == row.id))
                    {
                        lstList.Add(size.Name);
                    }
                    row.size = lstList;
                   

                    //For Pictures
                    lstList = new List<string>();
                    lstProductListPictures = resPicture.ToList();
                    foreach (var tag in lstProductListPictures.FindAll(o => o.ProductId == row.id))
                    {
                        lstList.Add(tag.Name);
                    }
                    row.pictures = lstList;
                    
                }

                if (resDetails != null)
                {
                    return Ok(new { Type = "success", Code = "001", Message = "", Data = resDetails });
                }
                else
                {
                    return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = ex.Message });
            }

        }

        [HttpPost, DisableRequestSizeLimit]
        [ActionName("Save")]
        public IActionResult UploadImage()
        {
            ProductMasterRequest viewModel = new ProductMasterRequest
            {
                Name = Request.Form["Name"][0],
                Title = Request.Form["Title"][0],
                Code = Request.Form["Code"][0],
                ShortDetails = Request.Form["ShortDetails"][0],
                Description = Request.Form["Description"][0],               
                Price = Convert.ToDecimal(Request.Form["Price"][0]),
                SalePrice = Convert.ToDecimal(Request.Form["SalePrice"][0]),
                Quantity = Convert.ToInt16(Request.Form["Quantity"][0]),
                Discount = Convert.ToInt16(Request.Form["Discount"][0]),
                IsNew = Convert.ToInt16(Request.Form["IsNew"][0]),
                IsSale = 1,//Convert.ToInt16(Request.Form["IsSale"][0] == "true" ? 1 : 0),
                CategoryId = 1,//Convert.ToInt16(Request.Form["CategoryId"][0]),
                ColorId = 1,//Convert.ToInt16(Request.Form["ColorId"][0]),
                SizeId = 1,// Convert.ToInt16(Request.Form["SizeId"][0]),
                TagId = 1,//Convert.ToInt16(Request.Form["TagId"][0])
            };
            var res = _productService.Add(viewModel);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // Allowed extensions
            var folderName = "Images";
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var files = Request.Form.Files;
            int count = 1;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var postedFile = Image;
                    if (postedFile.Length > 0)
                    {
                        var originalFileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition).FileName.Trim('"');
                        var extension = Path.GetExtension(originalFileName).ToLower(); // Always lowercase for comparison

                        var newfileName = $"{res.Id}_{count}{extension}";

                        var fullPath = Path.Combine(pathToSave, newfileName);
                        var dbPath = Path.Combine(folderName, newfileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }

                        ProductPictureMappingRequest vm = new ProductPictureMappingRequest
                        {
                            ProductId = res.Id,
                            PictureName = newfileName,
                            PicturePath = newfileName,
                            IsDelete = count
                        };
                        var res1 = _productService.AddImageMapping(vm);
                        count++;
                    }
                }
            }
            if (res != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = res });
            }
            else
            {
                return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
            }
            //return Ok(res);
        }


        [HttpPost, DisableRequestSizeLimit]
        [ActionName("Update")]
        public IActionResult UpdateProduct()
        {
            ProductMasterRequest viewModel = new ProductMasterRequest
            {
                Id = Convert.ToInt32(Request.Form["Id"][0]),
                Name = Request.Form["Name"][0],
                Title = Request.Form["Title"][0],
                Code = Request.Form["Code"][0],                
                ShortDetails = Request.Form["ShortDetails"][0],
                Description = Request.Form["Description"][0],
                Price = Convert.ToDecimal(Request.Form["Price"][0]),
                SalePrice = Convert.ToDecimal(Request.Form["SalePrice"][0]),
                Quantity = Convert.ToInt16(Request.Form["Quantity"][0]),
                Discount = Convert.ToInt16(Request.Form["Discount"][0]),
                IsNew = Convert.ToInt16(Request.Form["IsNew"][0]),
                //IsSale = Convert.ToInt16(Request.Form["IsSale"][0] == "true" ? 1 : 0),
                //CategoryId = Convert.ToInt16(Request.Form["CategoryId"][0]),
                //ColorId = Convert.ToInt16(Request.Form["ColorId"][0]),
                //SizeId = Convert.ToInt16(Request.Form["SizeId"][0]),
                //TagId = Convert.ToInt16(Request.Form["TagId"][0])
            };
            var res = _productService.Update(viewModel);


            var folderName = "Images";
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var files = Request.Form.Files;
            int count = 1;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var postedFile = Image;
                    if (postedFile.Length > 0)
                    {
                        var originalFileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition).FileName.Trim('"');
                        var extension = Path.GetExtension(originalFileName).ToLower(); // Always lowercase for comparison

                        var newfileName = $"{res.Id}_{count}{extension}";
                        var fullPath = Path.Combine(pathToSave, newfileName);
                        var dbPath = Path.Combine(folderName, newfileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }

                        ProductPictureMappingRequest vm = new ProductPictureMappingRequest
                        {
                            ProductId = res.Id,
                            PictureName = newfileName,
                            PicturePath = newfileName,
                            IsDelete = count
                        };
                        var res1 = _productService.AddImageMapping(vm);
                        count++;
                    }
                }
            }
            if (res != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = res });
            }
            else
            {
                return Ok(new { Type = "fail", Code = HttpStatusCode.BadRequest.ToString(), Message = MessageStream.SomethingWentWrong });
            }
            // return Ok(res);
        }

        [HttpGet]
        [ActionName("GetAll")]
        public IActionResult GetAll()
        {
            var resDetails = _productService.GetAll();
            if (resDetails != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = resDetails });
            }
            else
            {
                return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
            }
        }

        [HttpGet("{Id}")]
        [ActionName("GetbyId")]
        public IActionResult GetbyId(int Id)
        {
            var resDetails = _productService.GetbyId(Id);
            if (resDetails != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = resDetails });
            }
            else
            {
                return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
            }
        }

        [HttpGet("{Id}")]
        [ActionName("GetProductPicturebyId")]
        public IActionResult GetProductPicturebyId(int Id)
        {
            var resDetails = _productService.GetProductPicturebyId(Id);
            if (resDetails != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = resDetails });
            }
            else
            {
                return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete([FromBody] Value viewModel)
        {
            var resDetails = _productService.Delete(viewModel.Id);
            if (resDetails != null)
            {
                return Ok(new { Type = "success", Code = "001", Message = "", Data = resDetails });
            }
            else
            {
                return Ok(new { Type = "fail", Code = "002", Message = MessageStream.SomethingWentWrong });
            }
        }
    }
}
