using BusinessCore.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Wallace740_SmartApi.Command
{
    public class InsertCommandHandler : IRequestHandler<InsertProductCommand>
    {
        private IProductService _productService;
        public InsertCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<Unit> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var result = _productService.InsertProduct(request.Product);
            return Task.FromResult(Unit.Value);
        }
    }
}


