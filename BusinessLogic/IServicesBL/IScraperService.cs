using BusinessLogic.ServicesBL;
using DataAccess.Entities.FdaValidation;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IScraperService
    {
        Task<FDAValidationResult> ScrapeAndValidateAsync(IchIcsrXMLModel model);
    }

}
