using BusinessLogic.DTOs.MedDRA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IMedDRAService_GPT
    {
        List<MedDRAHierarchy> SearchTerms(string term);
        MedDRAHierarchy GetTermByCode(string code, string level = "LLT");
        List<MedDRAHierarchy> GetTermsBySOC(string socCode);
        List<MedDRAHierarchy> GetAllHierarchy();
    }

}
