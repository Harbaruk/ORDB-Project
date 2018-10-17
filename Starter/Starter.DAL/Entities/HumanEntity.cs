using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class HumanEntity : AbstractObjectEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}