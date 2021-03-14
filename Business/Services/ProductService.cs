using System;
using System.Collections.Generic;
using DAL.Entity;
using DAL.Helpers;
using DAL.Repositories;

namespace Business.Services
{
    public class ProductService
    {
        private ProductRepository Repo { get; set; }

        public ProductService()
        {
            Repo = new ProductRepository();
        }

        public List<Product> GetProducts()
        {
            var result = Repo.GetAll();
            return result;
        }
    }
}
