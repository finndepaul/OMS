using AutoMapper;
using Microsoft.AspNetCore.Http;
using OMS.Application.DataTransferObjs.Order;
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
    public class OrderReadWriteRepository : IOrderReadWriteRepository
    {
        private readonly OMSReadOnlyDbContext _db;

        public OrderReadWriteRepository(OMSReadOnlyDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseObject<OrderDetailDto>> CreateOrderAsync(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.CustomerName))
                {
                    return new ResponseObject<OrderDetailDto>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Tên khách hàng không được để trống",
                    };
                }
                if (request.lstOrderDetails == null || !request.lstOrderDetails.Any())
                {
                    return new ResponseObject<OrderDetailDto>
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Đơn hàng phải có ít nhất một chi tiết đơn hàng"
                    };
                }
                var order = new Order(request.CustomerName);
                foreach (var item in request.lstOrderDetails)
                {
                    if (item.Unit <= 0)
                    {
                        return new ResponseObject<OrderDetailDto>
                        {
                            Data = null,
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Số lượng và giá phải lớn hơn 0"
                        };
                    }
                    var product = await _db.Products.FindAsync(item.ProductId, cancellationToken);
                    if (product == null)
                    {
                        return new ResponseObject<OrderDetailDto>
                        {
                            Data = null,
                            Status = StatusCodes.Status404NotFound,
                            Message = $"Sản phẩm với ID {item.ProductId} không tồn tại"
                        };
                    }
                    if (item.Unit > product.Unit)
                    {
                        return new ResponseObject<OrderDetailDto>
                        {
                            Data = null,
                            Status = StatusCodes.Status400BadRequest,
                            Message = $"Sản phẩm với ID {item.ProductId} số lượng trong kho không đủ"
                        };
                    }
                    order.AddOrderDetail(item.ProductId, product.ProductCode, product.ProductName, product.Price, item.Unit);

                    product.Unit -= item.Unit; // Giảm số lượng sản phẩm trong kho
                    _db.Products.Update(product); // Cập nhật sản phẩm trong kho
                }
                await _db.AddAsync(order, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                var orderDto = new OrderDetailDto
                {
                    CustomerName = order.CustomerName,
                    TotalAmount = order.TotalAmount,
                    VAT = order.VAT,
                    GrandTotal = order.GrandTotal,
                    CreateTime = order.CreateTime,
                    Items = order.OrderDetails.Select(od => new OrderDetailItemDto
                    {
                        ProductId = od.ProductId,
                        ProductCode = od.ProductCode,
                        ProductName = od.ProductName,
                        Price = od.Price,
                        Unit = od.Unit
                    }).ToList()
                };
                return new ResponseObject<OrderDetailDto>
                {
                    Data = orderDto,
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo đơn hàng thành công!!!",
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<OrderDetailDto>
                {
                    Data = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Lỗi: {ex.Message}",
                };
            }
        }
    }
}
