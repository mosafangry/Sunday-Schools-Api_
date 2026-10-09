using BusinessLogic.DTOs.MedDRA;
using BusinessLogic.IServicesBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class MedDRAService : IMedDRAService_GPT
    {
        private readonly MedDRAFileProcessor _processor;

        public MedDRAService(string medAsciiPath)
        {
            _processor = new MedDRAFileProcessor(medAsciiPath);
        }

        public List<MedDRAHierarchy> SearchTerms(string term)
            => _processor.SearchTerms(term);

        public MedDRAHierarchy GetTermByCode(string code, string level = "LLT")
            => _processor.GetTermByCode(code, level);

        public List<MedDRAHierarchy> GetTermsBySOC(string socCode)
            => _processor.GetTermsBySOC(socCode);

        public List<MedDRAHierarchy> GetAllHierarchy()
            => _processor.BuildCompleteHierarchy();
    }

}
