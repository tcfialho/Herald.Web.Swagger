using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;

namespace Herald.Web.Swagger
{
    public class SwaggerOptions
    {
        public IEnumerable<string> Servers { get; set; }

        internal List<FilterDescriptor> OperationFilterDescriptors { get; set; } 

        public SwaggerOptions()
        {
            OperationFilterDescriptors = new List<FilterDescriptor>();
        }

        public void OperationFilter<TFilter>(params object[] arguments) where TFilter : IOperationFilter
        {
            OperationFilterDescriptors.Add(new FilterDescriptor
            {
                Type = typeof(TFilter),
                Arguments = arguments
            });
        }
    }
}
