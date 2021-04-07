using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GPSNotepad.Validation
{
    public static class Validation
    {
        public static bool IsName(string text)
        {
            return IsValidation(text, Constants.Namelen)
                && IsValidation(text, Constants.Start);
        }
        public static bool IsMail(string text)
        {
            return IsValidation(text, Constants.Maillen) 
                && IsValidation(text, Constants.Mail);
        }
        public static bool IsPassword(string text)
        {
            return IsValidation(text, Constants.Passwordlen)
                && IsValidation(text, Constants.Start);
        }

        public static bool IsValidation(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }
    }
}
