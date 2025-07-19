using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Application.DataTransferObjs.Product;
using OMS.Application.Interfaces.Repositories.ReadOnly;
using OMS.Application.Interfaces.Repositories.ReadWrite;
using OMS.Application.ValueObjs.Paginations;

namespace OMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadWriteRepository _productReadWriteRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;

        public ProductsController(IProductReadWriteRepository productReadWriteRepository, IProductReadOnlyRepository productReadOnlyRepository)
        {
            _productReadWriteRepository = productReadWriteRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
        }
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<ProductDto>> GetAllProductsAsync([FromQuery] PagingParameters pagingParameters, CancellationToken cancellationToken)
        {
            var response = await _productReadOnlyRepository.GetAllProductsAsync(pagingParameters, cancellationToken);
            return Ok(response);
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult> CreateProductAsync([FromBody] ProductCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _productReadWriteRepository.CreateProductAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
