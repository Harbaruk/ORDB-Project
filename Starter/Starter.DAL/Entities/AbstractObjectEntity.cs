using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class AbstractObjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Major { get; set; }
        public int Parent { get; set; }
        public string Type { get; set; }
    }
}