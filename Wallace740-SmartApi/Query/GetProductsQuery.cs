using DALCore.Entity;
using MediatR;
using System.Collections.Generic;

namespace Wallace740_SmartApi.Query
{
    public class GetProductsQuery : IRequest<List<Product>> { }


}


