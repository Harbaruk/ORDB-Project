﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class ClientDocumentationEntity : DocumentationEntity
    {
        public int ClientId { get; set; }
        public int Copies { get; set; }
    }
}