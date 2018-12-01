using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class StorageEntity : MaterialEntity
    {
        public int ClientId { get; set; }
        public string StorageAddress { get; set; }
    }
}