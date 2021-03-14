using System;
using System.Collections.Generic;
using DALCore.Entity;

namespace BusinessCore.Interfaces
{
    public interface IProductService
    {
        Product GetItemById(string id);
        List<Product> GetProducts();
        Product InsertProduct(Product product);
        Product UpdateProduct(Product product);
    }
}
