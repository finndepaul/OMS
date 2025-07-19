using Microsoft.EntityFrameworkCore;
using OMS.Application.DataTransferObjs.Order;
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
    public class OrderReadOnlyRepository : IOrderReadOnlyRepository
    {
        private readonly OMSReadOnlyDbContext _db;

        public OrderReadOnlyRepository(OMSReadOnlyDbContext db)
        {
            _db = db;
        }

        public async Task<PageList<OrderDto>> GetAllOrderAsync(DateTime fromDate, DateTime toDate, PagingParameters pagingParameters, CancellationToken cancellationToken)
        {
            DateTime today = DateTime.Today;
            if (fromDate == DateTime.MinValue)
                fromDate = today; // Đầu ngày hiện tại
            if (toDate == DateTime.MinValue)
                toDate = today.AddDays(1).AddTicks(-1); // Cuối ngày hiện tại

            // Đảm bảo toDate bao gồm cả ngày cuối cùng
            toDate = toDate.Date.AddDays(1).AddTicks(-1);
            var query = _db.Orders
                .Where(o => o.CreateTime >= fromDate && o.CreateTime <= toDate)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    CustomerName = o.CustomerName,
                    TotalAmount = o.TotalAmount,
                    VAT = o.VAT,
                    GrandTotal = o.GrandTotal,
                    CreateTime = o.CreateTime,
                }).AsNoTracking();
            var count = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync(cancellationToken);
            return new PageList<OrderDto>(items, count, pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public async Task<OrderDetailDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _db.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderDetailDto
                {
                    CustomerName = o.CustomerName,
                    TotalAmount = o.TotalAmount,
                    VAT = o.VAT,
                    GrandTotal = o.GrandTotal,
                    CreateTime = o.CreateTime,
                    Items = o.OrderDetails.Select(od => new OrderDetailItemDto
                    {
                        ProductId = od.ProductId,
                        ProductName = od.ProductName,
                        Unit = od.Unit,
                        Price = od.Price,
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            return order ?? throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID {orderId}.");
        }
    }
}
