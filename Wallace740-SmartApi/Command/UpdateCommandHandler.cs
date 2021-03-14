using BusinessCore.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Wallace740_SmartApi.Command
{
    public class UpdateCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private IProductService _productService;
        public UpdateCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = _productService.UpdateProduct(request.Product);
            return Task.FromResult(Unit.Value);
        }
    }
}


