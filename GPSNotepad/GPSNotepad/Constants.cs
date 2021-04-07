using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad
{
    public static class Constants
    {

        public const string Uppercase = @"\w*[A-Z]+\w";
        public const string Lovercase = @"\w*[a-z]+\w*";
        public const string Number = @"\w*[0-9]+\w*";
        public const string Namelen = @"^\w{4,16}";
        public const string Maillen = @"^\w{4,20}";
        public const string Mail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string Passwordlen = @"^\w{6,16}";
        public const string Start = @"^[a-zA-Z]+\w*";
    }
}
