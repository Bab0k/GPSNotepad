using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Model.Tables.User
{
    public class User: RealmObject
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Mail { get; set; } = "";
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
