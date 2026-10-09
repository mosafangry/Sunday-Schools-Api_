using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class SafeXmlModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Only use this binder for the IchIcsrXMLModel type
            if (context.Metadata.ModelType == typeof(IchIcsrXMLModel))
                return new SafeXmlModelBinder();

            return null;
        }
    }
}
