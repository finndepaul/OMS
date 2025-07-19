using OMS.Application.DataTransferObjs.Product;
using OMS.Application.ValueObjs.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.Interfaces.Repositories.ReadOnly
{
    public interface IProductReadOnlyRepository
    {
        Task<PageList<ProductDto>> GetAllProductsAsync(PagingParameters pagingParameters, CancellationToken cancellationToken);
    }
}
