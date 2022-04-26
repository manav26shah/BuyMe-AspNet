﻿using System.Collections.Generic;

namespace BuyMe.API.Config
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Error { get; set; }
    }
}
