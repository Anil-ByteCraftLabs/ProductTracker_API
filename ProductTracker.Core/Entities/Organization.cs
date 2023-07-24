﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Core.Entities
{
    public class Organization: Entity
    {
        public string OrgName { get; set; }
        public string AliasName { get; set; }
        public byte[] Logo { get; set; }
        public string DBPath { get; set; }
        public bool IsActive { get; set; }
        public DateTime DeActivationDate { get; set; }

    }
}
