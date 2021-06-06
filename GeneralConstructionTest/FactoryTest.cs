using System.Diagnostics;
using GeneralConstruction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneralConstructionTest
{
    [TestClass]
    public class FactoryTest
    {
        private readonly Factory _factory = new Factory();
        [TestMethod]
        public void TestGetTypeNames()
        {
            var names = _factory.GetTypeNames<IImplementor>();
            Assert.AreEqual(3,names.Count);
            Assert.IsTrue(names.Contains("First"));
            Assert.IsTrue(names.Contains("Second"));
            Assert.IsTrue(names.Contains("DoesNotFollowNameConvention"));
        }

        [TestMethod]
        public void TestCreateInstance()
        {
            var first = _factory.CreateInstance<IImplementor>("First");
            var second = _factory.CreateInstance<IImplementor>("Second");
            var doesNotFollowNameConvention = _factory.CreateInstance<IImplementor>("DoesNotFollowNameConvention");
            Assert.IsNotNull(first);
            Assert.IsNotNull(second);
            Assert.IsNotNull(doesNotFollowNameConvention);
            Assert.IsTrue(first is FirstImplementor);
            Assert.IsTrue(second is SecondImplementor);
            Assert.IsTrue(doesNotFollowNameConvention is DoesNotFollowNameConvention);
        }
    }
}
