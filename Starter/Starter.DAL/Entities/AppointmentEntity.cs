using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class AppointmentEntity : HrDocumentationEntity
    {
        public int WorkerId { get; set; }
    }
}