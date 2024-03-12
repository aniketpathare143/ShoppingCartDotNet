using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPIs.DataAccess;
using ShoppingCartAPIs.DTOs;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DatabaseLayer.ShoppingCart.Products
{
    public class ProductCRUD
    {
        private readonly ShoppingDbContext shoppingDbContext;
        private readonly IMapper mapper;
        private readonly IFormFile? imageFile;

        public ProductCRUD(ShoppingDbContext shoppingDbContext, IMapper mapper)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
        }

        public ProductCRUD(ShoppingDbContext shoppingDbContext, IMapper mapper, IFormFile imageFile)
        {
            this.shoppingDbContext = shoppingDbContext;
            this.mapper = mapper;
            this.imageFile = imageFile;
        }

        public Guid AddProduct(ProductDTO product)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                imageFile?.CopyTo(memoryStream);
                product.ProductImage = memoryStream.ToArray();
                var productRecord = mapper.Map<Product>(product);
                var insertedProduct = shoppingDbContext.Products.Add(productRecord);
                productRecord.ProductId = insertedProduct.Property(e => e.ProductId).CurrentValue;
                shoppingDbContext.SaveChanges();
                return productRecord.ProductId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveProduct(Guid productGuid)
        {
            try
            {
                var productRecord = shoppingDbContext.Products.Where(c => c.ProductId == productGuid).FirstOrDefault();
                if (productRecord != null)
                {
                    shoppingDbContext.Products.Remove(productRecord);
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

        public object GetProducts(Guid categoryGuid)
        {
            try
            {
                var products = shoppingDbContext.Products.Where(p => p.CategoryId == categoryGuid).ToList();
                var resultProducts = mapper.Map<List<ProductDTO>>(products);
                return resultProducts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetAllProducts()
        {
            try
            {
                var products = shoppingDbContext.Products.ToList();
                var resultProducts = mapper.Map<List<ProductDTO>>(products);
                return resultProducts;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
