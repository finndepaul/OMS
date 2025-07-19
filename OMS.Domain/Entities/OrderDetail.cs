using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Domain.Entities
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }

        private OrderDetail()
        {
            
        }
        public OrderDetail(Guid orderId, Guid productId, string productCode, string productName, decimal price, int unit)
        {
            OrderDetailId = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            Price = price;
            Unit = unit;
        }
    }
}
