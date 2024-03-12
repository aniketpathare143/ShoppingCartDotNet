using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart;
using ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Categories;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;

        public CategoryController(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult AddCategory([FromForm] CategoryDTO category)
        {
            var imageFile = Request.Form.Files[0];
            CategoryCRUD categoryCRUD = new CategoryCRUD(shoppingDbContext, mapper, imageFile);
            var categoryKey = categoryCRUD.AddCategory(category);
            return Ok(categoryKey);
        }

        [HttpDelete]
        [Route("{categoryGuid}")]
        public ActionResult DeleteCategory(Guid categoryGuid)
        {
            CategoryCRUD categoryCRUD = new CategoryCRUD(shoppingDbContext, mapper);
            bool isDeleted = categoryCRUD.RemoveCategory(categoryGuid);
            if (isDeleted)
                return Ok("Category deleted successfully");
            else
                return NotFound();
        }

        [HttpGet]
        [Route("{categoryGuid?}")]
        public ActionResult GetCategory(Guid? categoryGuid)
        {
            CategoryCRUD categoryCRUD = new CategoryCRUD(shoppingDbContext, mapper);
            var category = categoryCRUD.GetCategories(categoryGuid);
            return Ok(category);
        }
    }
}
