using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL;

namespace MyBlog.Models.Users
{
    public class UserModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Find { get; set; }

        public List<User> AllUsers { get; set; }

    }
}