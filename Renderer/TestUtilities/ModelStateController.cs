using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhino.Mocks;

namespace IRR2.WebUI.UnitTests.TestUtilities
{
    /// <summary>
    /// Instance of a controller for testing things that use controller methods i.e. controller.TryValidateModel(model)
    /// </summary>
    public class ModelStateTestController : Controller
    {
        public ModelStateTestController()
        {
            ControllerContext = MockRepository.GenerateMock<ControllerContext>();
        }

        public bool TestTryValidateModel(object model)
        {
            return TryValidateModel(model);
        }

    }
}
