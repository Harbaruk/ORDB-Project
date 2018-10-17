using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class WorkerEntity : HumanEntity
    {
        public int Salary { get; set; }
        public string Status { get; set; }
    }
}