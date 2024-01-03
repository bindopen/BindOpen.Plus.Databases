using BindOpen.Scoping;
using NUnit.Framework;

namespace BindOpen.Plus.Databases.Tests
{
    [TestFixture, Order(1)]
    public class FirstTest
    {
        IBdoScope _scope;

        [SetUp]
        public void Setup()
        {
            _scope = GlobalVariables.Scope;
        }
    }
}
