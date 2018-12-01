using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class OrderEntity : AbstractObjectEntity
    {
        public int ClientId { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public double Path { get; set; }
        public string ClientAddress { get; set; }
        public string RecepientAddress { get; set; }
        public DateTimeOffset FinishingDate { get; set; }
    }
}