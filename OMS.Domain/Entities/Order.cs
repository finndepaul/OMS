using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Domain.Entities
{
    public class Order : IAggregateRoot
    {
        public Guid OrderId { get; private set; }
        public string CustomerName { get; private set; }
        public double TotalAmount { get; private set; }
        public double VAT { get; private set; }
        public double GrandTotal { get; private set; }
        public DateTime CreateTime { get; private set; }

        private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();
        public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

        private Order()
        {
        }

        public Order(string customerName)
        {
            OrderId = Guid.NewGuid();
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            CreateTime = DateTime.UtcNow;
        }

        public void AddOrderDetail(Guid productId, string productCode, string productName, decimal price, int unit)
        {
            if (price < 0) throw new ArgumentException("Giá sản phẩm không được âm.", nameof(price));
            if (unit <= 0) throw new ArgumentException("Số lượng phải lớn hơn 0.", nameof(unit));
            if (string.IsNullOrWhiteSpace(productCode)) throw new ArgumentNullException(nameof(productCode));
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentNullException(nameof(productName));

            var orderDetail = new OrderDetail(OrderId, productId, productCode, productName, price, unit);
            _orderDetails.Add(orderDetail);
            UpdateTotals();
        }
        private void UpdateTotals()
        {
            TotalAmount = _orderDetails.Sum(od => (double)(od.Price * od.Unit));
            VAT = TotalAmount * 0.1; // Giả sử thuế VAT là 10%
            GrandTotal = TotalAmount +  VAT;
        }


    }
}
