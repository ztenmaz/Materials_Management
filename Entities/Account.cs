﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account
    {
        public int idAccount { get; set; }
        public string username { get; set; }
        public string  password { get; set; }
        public int idRole { get; set; }
    }
}
