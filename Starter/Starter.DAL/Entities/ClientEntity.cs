using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class ClientEntity : HumanEntity
    {
        public string Agreement { get; set; }
        public string Address { get; set; }
    }
}