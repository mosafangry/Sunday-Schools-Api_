using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.MedDRA
{
    public class MedDRAHierarchy
    {
        public string SOC_Code { get; set; }
        public string SOC_Name { get; set; }
        public string HLGT_Code { get; set; }
        public string HLGT_Name { get; set; }
        public string HLT_Code { get; set; }
        public string HLT_Name { get; set; }
        public string PT_Code { get; set; }
        public string PT_Name { get; set; }
        public string LLT_Code { get; set; }
        public string LLT_Name { get; set; }
        public bool IsPrimarySOC { get; set; }
    }

}
