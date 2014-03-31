using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ut
{
    [TestClass]
    public class UnitTest1
    {
        private Data.Store _store;
        [TestInitialize]
        public void TestInit()
        {
            _store = new Data.Store();
        }

        [TestMethod]
        public void TestMethod1()
        {
            _store.Add(new Data.IssueItem { 
                Title = "Test",
                Content = "Hej",
                WKT = "POINT(0 0)",
                Created = DateTime.Now
            });
        }
    }
}
