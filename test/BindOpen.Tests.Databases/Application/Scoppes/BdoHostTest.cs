using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Tests.Databases;
using NUnit.Framework;

namespace BindOpen.Tests.Application.Scopes
{
    [TestFixture, Order(1)]
    public class BdoHostTest
    {
        IBdoHost _appHost;

        [SetUp]
        public void Setup()
        {
            _appHost = GlobalVariables.AppHost;
        }

        [Test, Order(1)]
        public void TestBdoHost()
        {
            string xml = "";
            if (_appHost.Log.HasErrorsOrExceptions())
            {
                xml = _appHost.ToXml();
            }
            Assert.That(_appHost.IsLoaded, "Host not loaded. Result was '" + xml);
        }
    }
}
