using System.Xml;
using System.Xml.Serialization;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;



namespace BusinessLogic.Helpers
{


    public class SafeXmlModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            // Get the raw XML from the request body
            string xmlContent;
            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                xmlContent = await reader.ReadToEndAsync();
            }

            try
            {
                // Configure XML reader settings to ignore DTD
                var settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Ignore,
                    ValidationType = ValidationType.None,
                    XmlResolver = null  // Prevents external entity resolution
                };

                // Create an XmlSerializer for the model type
                var serializer = new XmlSerializer(bindingContext.ModelType);

                // Deserialize using the safe reader settings
                using (var stringReader = new StringReader(xmlContent))
                using (var xmlReader = XmlReader.Create(stringReader, settings))
                {
                    var result = serializer.Deserialize(xmlReader);
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"XML deserialization error: {ex.Message}");
            }
        }
    }
}
