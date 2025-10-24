using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities.Order
{
    public enum Status
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
}
