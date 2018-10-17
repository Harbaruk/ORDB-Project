using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class DispatcherEntity : WorkerEntity
    {
        public int Manager { get; set; }
    }
}