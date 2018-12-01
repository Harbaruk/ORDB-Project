using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class DriverEntity : WorkerEntity
    {
        public int ManagerId { get; set; }
    }
}