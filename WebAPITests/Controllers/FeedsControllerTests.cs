using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Tests
{
    [TestClass()]
    public class FeedsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var controller = new FeedsController();
            var result = controller.Get(new Models.RequestQueryParams() {
                q = "159968055@N03"
            });

            Assert.IsNotNull(result);
        }
    }
}