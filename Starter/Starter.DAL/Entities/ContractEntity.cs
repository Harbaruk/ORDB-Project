using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class ContractEntity : ClientDocumentationEntity
    {
        public DateTimeOffset StartDate { get; set; }
    }
}