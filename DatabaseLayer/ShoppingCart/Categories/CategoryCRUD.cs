using AutoMapper;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Categories
{
    public class CategoryCRUD
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;
        private readonly IFormFile? imageFile;

        public CategoryCRUD(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        public CategoryCRUD(ShoppingDbContext shoppingDbContext, IMapper mapper, IFormFile imageFile)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
            this.imageFile = imageFile;
        }

        public Guid AddCategory(CategoryDTO category)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                imageFile?.CopyTo(memoryStream);
                category.CategoryImage = memoryStream.ToArray();

                var categoryRecord = mapper.Map<CategoryDTO, Category>(category);
                var insertedCategory = shoppingDbContext.Categories.Add(categoryRecord);
                categoryRecord.CategoryId = insertedCategory.Property(e => e.CategoryId).CurrentValue;
                shoppingDbContext.SaveChanges();
                return categoryRecord.CategoryId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveCategory(Guid categoryGuid)
        {
            try
            {
                var categoryRecord = shoppingDbContext.Categories.Where(c => c.CategoryId == categoryGuid).FirstOrDefault();
                if (categoryRecord != null)
                {
                    shoppingDbContext.Categories.Remove(categoryRecord);
                    shoppingDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetCategories(Guid? categoryGuid)
        {
            try
            {
                if (categoryGuid == null)
                {
                    var categories = shoppingDbContext.Categories.ToList();
                    var allCategories = mapper.Map<List<Category>, List<CategoryDTO>>(categories);
                    return allCategories;
                }
                else
                {
                    var category= shoppingDbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryGuid);
                    var resultCategory = mapper.Map<CategoryDTO>(category);
                    return resultCategory;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
