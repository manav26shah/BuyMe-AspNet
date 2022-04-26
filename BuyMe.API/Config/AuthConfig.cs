using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.Config
{
    public class AuthConfig
    {
        public int BlockAfterHowManyLoginAttempts { get; set; }
        public int AfterHowManySecondsRetrySendingOTP { get; set; }
    }
}
