using OneTimePadDLL;

namespace OneTimePadUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void CheckCypherShift10FromA()
        {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            char CypherShift = OTPO.PerformCypherShiftEncryption('A', 10);

            Assert.AreEqual('K', CypherShift);

        }

        [TestMethod]
        public void CheckCypherShift27FromA() {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            char CypherShift = OTPO.PerformCypherShiftEncryption('A',27);
            Assert.AreEqual('.', CypherShift);

        }

        //[TestMethod]
        public void CheckCypherShift35() {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            int CypherShift = OTPO.PerformCypherShiftEncryption('A',35); ;
            Assert.AreEqual(0, CypherShift);

        }

        //[TestMethod]
        public void CheckCypherShift2226()
        {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            int CypherShift = OTPO.PerformCypherShiftEncryption('A',226);
            Assert.AreEqual(9, CypherShift);

        }

  
    }
}