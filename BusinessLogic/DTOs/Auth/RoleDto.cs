using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Auth
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }

}
