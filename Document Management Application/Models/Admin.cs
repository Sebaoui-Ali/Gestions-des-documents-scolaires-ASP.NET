using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Document_Management_Application.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Email{ get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        [MaxLength(1000)]
        public string Password { get; set; }

    }
}
