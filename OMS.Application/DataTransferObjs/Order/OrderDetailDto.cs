using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.DataTransferObjs.Order
{
    public class OrderDetailDto
    {
        public string CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public double VAT { get; set; }
        public double GrandTotal { get; set; }
        public DateTime CreateTime { get; set; }
        public List<OrderDetailItemDto> Items { get; set; } = new List<OrderDetailItemDto>();
    }
    public class OrderDetailItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }
    }
}
