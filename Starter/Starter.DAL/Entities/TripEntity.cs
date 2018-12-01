using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class TripEntity : AbstractObjectEntity
    {
        public int ManagerId { get; set; }
        public int DispatcherId { get; set; }
        public int DriverId { get; set; }
        public int CarId { get; set; }
    }
}