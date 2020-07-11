using System;
using System.Collections.Generic;
using System.Text;

namespace BANKEST2.Core.Entities
{
    public static class TransactionTypes
    {
        public static string WireTransfer { get; private set; } = "WireTransfer";
        public static string Deposit { get; private set; } = "Deposit";
        public static string WithDrawal { get; private set; } = "WithDrawal";
        public static string Query { get; private set; } = "Query";
    }
}
