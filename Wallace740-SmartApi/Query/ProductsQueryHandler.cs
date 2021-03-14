using BusinessCore.Interfaces;
using DALCore.Entity;
using MediatR;
using Wallace740_SmartApi.Query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wallace740_SmartApi.Query
{
    public class ProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private IProductService _productService;
        public ProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public System.Threading.Tasks.Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productService.GetProducts());
        }
    }
}


