using System;
using System.Collections.Generic;
using DALCore.Constants;
using DALCore.Entity;
using DALCore.Helpers;

namespace DALCore.Repositories
{
    public class ProductRepository : IDbRepository<Product>
    {
        private string TableName { get; set; }
        public static FireBaseHelper DHelper { get => dHelper; set => dHelper = value; }

        private static FireBaseHelper dHelper = null;


        public ProductRepository()
        {
            TableName = FireBaseConstants.Tables.Products;
            DHelper = new FireBaseHelper(TableName);
        }

        public List<Product> GetAll()
        {
            var result = DHelper.GetAll<Product>();
            return result;
        }

        public Product GetItemById(string id)
        {
            var result = DHelper.GetItemById<Product>(id);
            return result;
        }

        public Product Insert(Product item)
        {
            var result = DHelper.InsertItemToDB(item);
            return result;
        }

        public Product Update(Product item)
        {
            var result = DHelper.UpdateItemToDB(item);
            return result;
        }

    }
}
