using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CompanyId { get; set; }
        public int? roleId { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
