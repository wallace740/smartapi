using System;
using DALCore.Entity;
using MediatR;

namespace Wallace740_SmartApi.Query
{
    public class ProductQuery : IRequest<Product>
    {
        public string Id { get; set; }

        public ProductQuery(string id)
        {
            Id = id;
        }
    }
}
