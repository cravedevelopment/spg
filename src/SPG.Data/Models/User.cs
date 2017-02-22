﻿using SPG.Data.EF;

namespace SPG.Model
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }
    }
}