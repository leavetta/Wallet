﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletAspNetCore.Auth
{
    public class JwtOptions
    {
        public string Secretkey { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}
