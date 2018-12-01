using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class SpecialistEntity : WorkerEntity
    {
        public int SpecializationId { get; set; }
        public string StationAddress { get; set; }
    }
}