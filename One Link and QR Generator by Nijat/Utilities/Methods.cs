using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace One_Link_and_QR_Generator_by_Nijat.Utilities
{
    public static class Methods
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
