using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Auth
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
