using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.DataTransferObjs.Order
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public double VAT { get; set; }
        public double GrandTotal { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
