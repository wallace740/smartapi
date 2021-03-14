using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessCore;
using BusinessCore.Services;
using DALCore.Entity;
using MediatR;
using Wallace740_SmartApi.Query;
using Wallace740_SmartApi.Command;

namespace Wallace740_SmartApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IMediator _med;

        public ApiController(IMediator mediator)
        {
            _med = mediator;
        }

        /// <summary>
        /// Starting route to see API is alive. To change default route -- launchsettings.json
        /// </summary>
        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                Message = "Hello, API is staying alive! Go try other endpoints :)"
            });
        }

        /// <summary>
        /// Get specific Product
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("get-product")]
        [Authorize("read:products")]
        public IActionResult GetProductById(string id)
        {
            var tResult = _med.Send(new ProductQuery(id));

            return new JsonResult(tResult.Result);
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        [HttpGet("get-products")]
        [Authorize("read:products")]
        public IActionResult GetProducts()
        {
            var tResult = _med.Send(new GetProductsQuery());

            return new JsonResult(tResult.Result);
        }

        [HttpPost("insert")]
        [Authorize("write:products")]
        public IActionResult Insert(Product insertProduct)
        {
            _med.Send(new InsertProductCommand(insertProduct));
            return new JsonResult(insertProduct);
        }

        [HttpPost("update")]
        [Authorize("write:products")]
        public IActionResult Update(Product updateProduct)
        {
            _med.Send(new UpdateProductCommand(updateProduct));
            return new JsonResult(updateProduct);
        }

        #region Test endpoint public / private region

        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Ok(new
            {
                Message = "Success, OK from private endpoint! You are authenticated to see this."
            });
        }
        #endregion

    }
}


