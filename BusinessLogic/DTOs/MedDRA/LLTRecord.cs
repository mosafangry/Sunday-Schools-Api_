using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.MedDRA
{
    public class LLTRecord
    {
        public string LLT_Code { get; set; }
        public string LLT_Name { get; set; }
        public string PT_Code { get; set; }
        public string LLT_Whoart_Code { get; set; }
        public string LLT_Harts_Code { get; set; }
        public string LLT_Costart_Sym { get; set; }
        public string LLT_Icd9_Code { get; set; }
        public string LLT_Icd9cm_Code { get; set; }
        public string LLT_Icd10_Code { get; set; }
        public string LLT_Currency { get; set; }
        public string LLT_Jart_Code { get; set; }
    }

}
