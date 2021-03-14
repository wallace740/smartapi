using DALCore.Entity;
using MediatR;

namespace Wallace740_SmartApi.Command
{
    public class UpdateProductCommand : IRequest
    {
        public Product Product { get; set; }
        public UpdateProductCommand(Product product)
        {
            Product = product;
        }
    }
}


