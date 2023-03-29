using OneTimePadDLL;

namespace OneTimePadUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void CheckCypherShift10FromAEncryption()
        {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            char CypherShift = OTPO.PerformCypherShiftEncryption('A', 10);

            Assert.AreEqual('K', CypherShift);

        }

        [TestMethod]
        public void CheckCypherShift27FromAEncryption() {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            char CypherShift = OTPO.PerformCypherShiftEncryption('B',27);
            Assert.AreEqual('.', CypherShift);

        }

        [TestMethod]
        public void CheckCypherChift10DecryptK() {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            int CypherShift = OTPO.PerformCypherShiftDecryption('K',10); ;
            Assert.AreEqual('A', CypherShift);

        }

        [TestMethod]
        public void CheckCypherShiftDecrypt()
        {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            int CypherShift = OTPO.PerformCypherShiftDecryption('.',27);
            Assert.AreEqual('B', CypherShift);

        }

        [TestMethod]
        public void EncryptTextString()
        {
            string ToEncrypt = "CAT";  //Should Change CAT to DCW
            
            int[] ShiftArray = { 1, 2, 3 };

            OneTimePadOperations OTPO = new OneTimePadOperations();
            string? cyphertext = OTPO.EncryptMessage(ToEncrypt, ShiftArray);

            Assert.AreEqual("DCW", cyphertext);

        }
  
    }
}