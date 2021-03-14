using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessCore.Interfaces;
using DALCore.Entity;
using MediatR;
using Wallace740_SmartApi.Query;

namespace Wallace740_SmartApi.Query
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery, Product>
    {
        private IProductService _productService;
        public ProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<Product> Handle(ProductQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productService.GetItemById(query.Id));
        }
    }
}
