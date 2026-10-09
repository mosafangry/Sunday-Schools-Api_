using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Company
{
    public class CompanyUpdateDto : CompanyCreateDto
    {
        public int CompanyId { get; set; }
    }

}
