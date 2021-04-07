using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Servises.AuthorizeService
{
    public interface IAuthorizeService
    {
        bool IsAuthorize();
        bool Authorize(string Mail, string Password);
    }
}
