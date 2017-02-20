using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using STG;
using STG.Controllers;

namespace STG.Tests.Controllers
{
    [TestClass]
    public class SchoolControllerTest
    {
        [TestMethod]
        public void TestGenerating()
        {
            // Arrange
            SchoolsController controller = new SchoolsController();

            // Act
            ViewResult result = controller.Generate(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
