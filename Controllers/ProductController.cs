using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Categories;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Products;
using ShoppingCartAPIs.DTOs;

namespace ShoppingCartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public ProductController(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult AddProduct([FromForm] ProductDTO product)
        {
            var imageFile = Request.Form.Files[0];
            ProductCRUD productCRUD = new ProductCRUD(shoppingDbContext, mapper, imageFile);
            var categoryKey = productCRUD.AddProduct(product);
            return Ok(categoryKey);
        }

        [HttpDelete]
        [Route("{productGuid}")]
        public ActionResult DeleteProduct(Guid productGuid)
        {
            ProductCRUD productCRUD = new ProductCRUD(shoppingDbContext, mapper);
            bool isDeleted = productCRUD.RemoveProduct(productGuid);
            if (isDeleted)
                return Ok("Product deleted successfully");
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{categoryGuid}")]
        public ActionResult GetProductByCategories(Guid categoryGuid)
        {
            ProductCRUD productCRUD = new ProductCRUD(shoppingDbContext, mapper);
            var product = productCRUD.GetProducts(categoryGuid);
            return Ok(product);
        }

        [HttpGet]       
        public ActionResult GetAllProducts(Guid categoryGuid)
        {
            ProductCRUD productCRUD = new ProductCRUD(shoppingDbContext, mapper);
            var product = productCRUD.GetAllProducts();
            return Ok(product);
        }
    }
}
