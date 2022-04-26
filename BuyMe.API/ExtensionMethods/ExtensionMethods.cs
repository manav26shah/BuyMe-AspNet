using BuyMe.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.ExtensionMethods
{
    public static class ExtensionMethods
    {
        
        public static int CountStars(this string s)
        {
             int count = 0;
            foreach (var item in s)
            {
                if (item == '*')
                {
                    count++;
                }
            }
            return count;
        }
    }
}
