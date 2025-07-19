using OMS.Application.DataTransferObjs.Product;
using OMS.Application.ValueObjs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.Interfaces.Repositories.ReadWrite
{
    public interface IProductReadWriteRepository
    {
        Task<ResponseObject<ProductDto>> CreateProductAsync(ProductCreateRequest request, CancellationToken cancellationToken);
    }
}
