using System;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Models;
using Shopping.Api.Data;

namespace Shopping.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
	{
		private readonly ILogger<ProductController> _logger;

		public ProductController(ILogger<ProductController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<Product> GetProducts()
		{
			return ProductContext.Products;
		}
	}
}

