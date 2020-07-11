using System;
using System.Collections.Generic;
using System.Text;

namespace BANKEST2.Core.Entities
{
    public class TransactionRequest
    {
        public decimal Amount{ get; set; }
        public int AccountToTransfer { get; set; }
    }
}
