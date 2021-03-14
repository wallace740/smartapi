using System;
using DALCore.Entity;
using MediatR;

namespace Wallace740_SmartApi.Command
{
    public class InsertProductCommand : IRequest
    {
        public Product Product { get; set; }
        public InsertProductCommand(Product product)
        {
            Product = product;
        }
    }
}
