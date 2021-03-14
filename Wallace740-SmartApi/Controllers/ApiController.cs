using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessCore;
using BusinessCore.Services;
using BusinessCore.Interfaces;
using DALCore.Entity;

namespace Wallace740_SmartApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IProductService _productService;

        public ApiController(IProductService service)
        {
            _productService = service;
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

        [HttpPost("get-products")]
        [Authorize]  //("read:products")
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();

            return new JsonResult(products);
        }

        [HttpPost("insert")]
        [Authorize]
        public IActionResult Insert(Product insertProduct)
        {
            var newProduct = _productService.InsertProduct(insertProduct);

            return new JsonResult(newProduct);
        }

        [HttpPost("update")]
        [Authorize]
        public IActionResult Update(Product updateProduct)
        {
            var product = _productService.UpdateProduct(updateProduct);

            return new JsonResult(product);
        }

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



        [HttpPost("private-scoped")]
        [Authorize]  //("read:products")
        public IActionResult Scoped()
        {
            var products = _productService.GetProducts();

            return new JsonResult(products);
        }

        // This is a helper action. It allows you to easily view all the claims of the token.
        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }
    }
}
