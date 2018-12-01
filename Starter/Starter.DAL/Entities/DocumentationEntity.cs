using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class DocumentationEntity : AbstractObjectEntity
    {
        public string ResponsiblePerson { get; set; }
        public DateTimeOffset Date { get; set; }
        public int SignId { get; set; }
    }
}