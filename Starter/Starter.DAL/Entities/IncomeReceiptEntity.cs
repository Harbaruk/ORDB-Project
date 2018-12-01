using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class IncomeReceiptEntity : AccountantDocumentationEntity
    {
        public int WorkerId { get; set; }
        public double Sum { get; set; }
    }
}