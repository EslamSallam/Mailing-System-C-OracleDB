using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailing_System
{
    public class User
    {
        public static string email { get; internal set; }
        public static string firstname { get; internal set; }
        public static string lastname { get; internal set; }
        public static string password { get; internal set; }
        public static string birthdate { get; internal set; }
        public static string gender { get; internal set; }
        public static string age { get; internal set; }

        private static List<string> phone1;

        internal static List<string> Getphone()
        {
            return phone1;
        }

        internal static void Setphone(List<string> value)
        {
            phone1 = value;
        }
    }
}
