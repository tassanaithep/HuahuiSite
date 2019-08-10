using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class LoginViewModel
    {
        public bool IsLogin { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleName { get; set; }
        public int RoleId { get; set; }

        public string Name { get; set; }
    }
}
