using System;
using System.Collections.Generic;
using BusinessCore.Interfaces;
using DALCore;
using DALCore.Entity;
using DALCore.Repositories;

namespace BusinessCore.Services
{
    public class ProductService : IProductService
    {
        //private ProductRepository Repo { get; set; }
        private IDbRepository<Product> _dbRepository;

        public ProductService(IDbRepository<Product> dbRepository)
        {
            //Repo = new ProductRepository();
            _dbRepository = dbRepository;
        }


        public List<Product> GetProducts()
        {
            var result = _dbRepository.GetAll();//Repo.GetAll();
            return result;
        }

        public Product InsertProduct(Product product)
        {
            var result = _dbRepository.Insert(product);
            return result;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _dbRepository.Update(product);
            return result;
        }

    }
}
