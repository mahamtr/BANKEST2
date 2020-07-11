using System;
using System.Collections.Generic;
using System.Text;

namespace BANKEST2.Core.Entities
{
    public class TransactionInfo
    {
        
        public int PrimaryAccount { get; set; }
        public int SecondaryAccount{ get; set; }
        public string TransactionType { get; set; }
        public decimal PrimaryAccountBalance { get; set; }
        public Exception TransactionException { get; set; } = null;
    }
}
