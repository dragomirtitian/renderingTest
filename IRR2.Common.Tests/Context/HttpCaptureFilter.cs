using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace IRR2.Common.Tests.Context
{
    public class HttpCaptureFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            var x = Serializer.Serialize(System.Web.HttpContext.Current);
            //System.IO.File.WriteAllText(@"C:\Users\drago\Desktop\renderingTest\context.json", x);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
