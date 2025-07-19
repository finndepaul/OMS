using AutoMapper;
using Microsoft.AspNetCore.Http;
using OMS.Application.DataTransferObjs.Product;
using OMS.Application.Interfaces.Repositories.ReadWrite;
using OMS.Application.ValueObjs.ViewModels;
using OMS.Domain.Entities;
using OMS.Infrastructure.Database.AppDbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Infrastructure.Implements.Repositories.ReadWrite
{
    public class ProductReadWriteRepository : IProductReadWriteRepository
    {
        private readonly OMSReadWriteDbContext _dbContext;
        private readonly IMapper mapper;

        public ProductReadWriteRepository(OMSReadWriteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ResponseObject<ProductDto>> CreateProductAsync(ProductCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.ProductCode) || string.IsNullOrWhiteSpace(request.ProductName) || request.Price <= 0 || request.Unit <= 0)
                {
                    return new ResponseObject<ProductDto>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = $"Thông tin nhập liệu sai",
                    };
                }
                var product = mapper.Map<Product>(request);
                await _dbContext.Products.AddAsync(product, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var productDto = mapper.Map<ProductDto>(product);
                return new ResponseObject<ProductDto>
                {
                    Data = productDto,
                    Status = StatusCodes.Status201Created,
                    Message = $"Tạo sản phẩm thành công!!!",
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<ProductDto>
                {
                    Data = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Lỗi: {ex.Message}",
                };
            }
        }
    }
}
