using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Mailing_System
{
    class Validation
    {
        
        internal static bool validateUserName(string text)
        {
            bool isEmail = Regex.IsMatch(text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                return true;
            }
            return false;
        }

        internal static bool validateLastName(string text)
        {
            if (text.Length < 5)
                return false;
            else
                return true;
        }

        internal static bool validateFirstName(string text)
        {
            if (text.Length < 5)
                return false;
            else
                return true;
        }

        internal static bool validatePassword(string text)
        {
            if (text.Length < 5)
                return false;
            else
                return true;
        }

        internal static bool validatePasswordconfirm(string text1, string text2)
        {
            if (text1 != text2)
            {
                return false;
            }
            return true;
        }

        internal static bool validatePhone(string text)
        {
            bool isPhone = Regex.IsMatch(text, @"\A([0-9]{10,15})\Z", RegexOptions.IgnoreCase);
            if (isPhone)
            {
                return true;
            }

           return false;
        }

       

        internal static bool validateDate(DateTime value)
        {

            int d = DateTime.Now.CompareTo(value);
            if (d > 1000)
            {
                return true;
            }
            return false;
            
        }

        internal static bool validateOldPassword(string text, string password)
        {
            if (text == password )
            {
                return true;
            }
            else {
                return false;
            }
        }

        internal static bool validateDeletePhone(string text, List<string> list)
        {
            foreach (string s in list)
            {
                if (text == s)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
