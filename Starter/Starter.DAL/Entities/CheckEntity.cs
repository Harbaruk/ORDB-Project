using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class CheckEntity : AccountantDocumentationEntity
    {
        public int ClientId { get; set; }
        public double Address { get; set; }
    }
}