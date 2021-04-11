using GPSNotepad.Servises.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPSNotepad.Servises.AuthorizeService
{
    public class AuthorizeService: IAuthorizeService
    {
        private readonly IUserService userService;

        public AuthorizeService(IUserService userService)
        {
            this.userService = userService;
        }

        public bool Authorize(string Mail, string Password)
        {
            var result = userService.GetAllUsers().Where(u => u.Mail == Mail && u.Password == Password);
            if (result.Any())
            {
                userService.SetCurrentUser(result.First());
            }
            return result.Any();
        }

        public bool IsAuthorize()
        {
            return !(userService.GetCurrentUser() is null);
        }
    }
}
