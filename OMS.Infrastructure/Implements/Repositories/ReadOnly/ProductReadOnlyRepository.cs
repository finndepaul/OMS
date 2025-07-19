using Microsoft.EntityFrameworkCore;
using OMS.Application.DataTransferObjs.Product;
using OMS.Application.Interfaces.Repositories.ReadOnly;
using OMS.Application.ValueObjs.Paginations;
using OMS.Infrastructure.Database.AppDbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Infrastructure.Implements.Repositories.ReadOnly
{
    public class ProductReadOnlyRepository : IProductReadOnlyRepository
    {
        private readonly OMSReadOnlyDbContext _db;

        public ProductReadOnlyRepository(OMSReadOnlyDbContext context)
        {
            _db = context;
        }

        public async Task<PageList<ProductDto>> GetAllProductsAsync(PagingParameters pagingParameters, CancellationToken cancellationToken)
        {
            var query = _db.Products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductCode = p.ProductCode,
                ProductName = p.ProductName,
                Price = p.Price,
                Unit = p.Unit,
            }).AsNoTracking();
            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync(cancellationToken);

            return new PageList<ProductDto>(items, count, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
