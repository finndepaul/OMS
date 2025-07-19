using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Application.DataTransferObjs.Order
{
    public class OrderCreateRequest
    {
        public string CustomerName { get; set; }
        public List<OrderCreateItemRequest> lstOrderDetails { get; set; } = new List<OrderCreateItemRequest>();
    }
    public class OrderCreateItemRequest
    {
        public Guid ProductId { get; set; }
        public int Unit { get; set; }
    }
}
