using System;
using System.Collections.Generic;
using Litz.Services;

namespace Litz.Models
{
    public class TransactionIndexViewModel
    {
        public DateTime PreviousMonth { get; set; }
        public DateTime CurrentPeriod { get; set; }
        public DateTime NextMonth { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}