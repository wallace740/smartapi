using System.Collections.Generic;
using BusinessCore.Interfaces;
using DALCore;
using DALCore.Entity;

namespace BusinessCore.Services
{
    public class ProductService : IProductService
    {
        private IDbRepository<Product> _dbRepository;

        public ProductService(IDbRepository<Product> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public Product GetItemById(string id)
        {
            return _dbRepository.GetItemById(id);
        }

        public List<Product> GetProducts()
        {
            return _dbRepository.GetAll();
        }

        public Product InsertProduct(Product product)
        {
            return _dbRepository.Insert(product);
        }

        public Product UpdateProduct(Product product)
        {
            return _dbRepository.Update(product);
        }

    }
}
