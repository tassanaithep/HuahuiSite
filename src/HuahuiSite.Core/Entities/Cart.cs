﻿using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
