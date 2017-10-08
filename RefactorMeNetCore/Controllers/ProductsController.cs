using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactorMeNetCore.Services;
using RefactorMeNetCore.Models;

namespace RefactorMeNetCore.Controllers
{
    [Produces("application/json")]
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsAsync([FromQuery] string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var products = await _productService.GetProductsByNameAsync(name);
                    return Ok(new
                    {
                        Items = products
                    });
                }
                else
                {
                    var allProducts = await _productService.GetAllProductsAsync();
                    return Ok(new
                    {
                        Items = allProducts
                    });
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id:Guid}", Name = "Get")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatProductAsync([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productCreated = await _productService.CreateAsync(product);

            return CreatedAtAction(
                nameof(GetProductByIdAsync),
                new
                {
                    id = productCreated.Id
                },
                productCreated
            );
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productUpdated = await _productService.UpdateAsync(id, product);
            return Ok(productUpdated);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductAndOptions(Guid id)
        {
            try
            {
                var productDeleted = await _productService.DeleteAsync(id);
                return Ok(productDeleted);
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpGet("{id:Guid}/options")]
        [ProducesResponseType(typeof(List<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllOptionsByProductIdAsync(Guid id)
        {
            var options = await _productService.GetAllOptionsByProductIdAsync(id);
            return Ok(new
            {
                Items = options
            });
        }

        [HttpGet("{id:Guid}/options/{optionId:Guid}")]
        [ProducesResponseType(typeof(List<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            try
            {
                var options = await _productService.GetOptionByProductIdAndOptionIdAsync(id, optionId);
                return Ok(options);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("{id:Guid}/options")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreatOptionByProductIdAsync(Guid id, [FromBody]ProductOption option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var optionCreated = await _productService.CreatOptionByProductIdAsync(id, option);

            return CreatedAtAction(
                nameof(GetOptionByProductIdAndOptionIdAsync),
                new
                {
                    id = optionCreated.ProductId,
                    optionId = optionCreated.Id
                },
                optionCreated
            );
        }

        [HttpPut("{id:Guid}/options/{optionId:Guid}")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId, [FromBody] ProductOption option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var optionUpdated = await _productService.UpdateOptionByProductIdAndOptionIdAsync(id, optionId, option);

            return Ok(optionUpdated);
        }


        [HttpDelete("{id:Guid}/options/{optionId:Guid}")]
        [ProducesResponseType(typeof(ProductOption), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            var optionDeleted = await _productService.DeleteOptionByProductIdAndOptionIdAsync(id, optionId);
            return Ok(optionDeleted);
        }
    }
}