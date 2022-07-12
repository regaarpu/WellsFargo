using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entity
{
    public class Transactions
    {
        public int SecurityId { get; set; }
        public int PortfolioId { get; set; }
        public decimal Nominal { get; set; }
        public string OMS { get; set; }
        public string TransactionType { get; set; }

        

       
    }
}
