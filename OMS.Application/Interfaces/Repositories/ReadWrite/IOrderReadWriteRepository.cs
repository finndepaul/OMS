using OMS.Application.DataTransferObjs.Order;
using OMS.Application.ValueObjs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.Interfaces.Repositories.ReadWrite
{
    public interface IOrderReadWriteRepository
    {
        Task<ResponseObject<OrderDetailDto>> CreateOrderAsync(OrderCreateRequest request, CancellationToken cancellationToken);
    }
}
