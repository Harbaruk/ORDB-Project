﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }

        public ICollection<RefreshTokenEntity> Tokens { get; set; }
    }
}