﻿using Microsoft.AspNetCore.Identity;

namespace ERP.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;

        public string FirstAndLastName
        {
            get
            {
                return this.LastName + ", " + this.FirstName;
            }
        }
    }
}