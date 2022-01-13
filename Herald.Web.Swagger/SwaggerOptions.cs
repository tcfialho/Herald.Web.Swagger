using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;

namespace Herald.Web.Swagger
{
    public class SwaggerOptions
    {
        internal List<FilterDescriptor> OperationFilterDescriptors { get; }
        public IEnumerable<string> Servers { get; set; }

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
