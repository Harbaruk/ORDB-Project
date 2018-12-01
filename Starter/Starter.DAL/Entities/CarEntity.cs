using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class CarEntity : MaterialEntity
    {
        public string Number { get; set; }
        public string CarPermission { get; set; }
        public double MaxWeight { get; set; }
        public int CarTypeId { get; set; }
    }
}