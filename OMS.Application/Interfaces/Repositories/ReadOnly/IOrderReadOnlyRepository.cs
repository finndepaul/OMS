using OMS.Application.DataTransferObjs.Order;
using OMS.Application.ValueObjs.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.Interfaces.Repositories.ReadOnly
{
    public interface IOrderReadOnlyRepository
    {
        Task<PageList<OrderDto>> GetAllOrderAsync(DateTime fromDate, DateTime toDate, PagingParameters pagingParameters, CancellationToken cancellationToken);
        Task<OrderDetailDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
