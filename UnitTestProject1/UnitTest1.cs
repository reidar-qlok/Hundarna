using GreyhoundRace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Punter newPunter = new Emely();

        [TestMethod]
        public void TestInstantiation() // Tests to make sure the Instantiation has worked by making sure the cash amounts are correct
        {
            int ExpectedCash = 50;
            int ActualCash = newPunter.Cash;
            Assert.AreEqual(ExpectedCash, ActualCash);
        }
    }
}
