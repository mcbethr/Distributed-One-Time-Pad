using OneTimePadDLL;

namespace OneTimePadUnitTests
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod] 
        public void CheckLinkedListCreation()
        {
            OneTimePadOperations OTPO = new OneTimePadOperations();
            char FoundCharacter = OTPO.PerformCypherShiftEncryptionLL('N',14);
            Assert.AreEqual('b', FoundCharacter);
        }

        
        [TestMethod]
        public void CheckCypherShift88FromAEncryption() {

            OneTimePadOperations OTPO = new OneTimePadOperations();
            char CypherShift = OTPO.PerformCypherShiftEncryptionLL('A',90);
            Assert.AreEqual('A', CypherShift);

        }
        /*
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
        public void TestEncryptTextString()
        {
            string ToEncrypt = "CAT";  //Should Change CAT to DCW
            
            int[] ShiftArray = { 1, 2, 3 };

            OneTimePadOperations OTPO = new OneTimePadOperations();
            string? cyphertext = OTPO.EncryptMessage(ToEncrypt, ShiftArray);

            Assert.AreEqual("DCW", cyphertext);

        }

        [TestMethod]
        public void TestGenerateKeyDefault()
        {
            OneTimePadOperations OTPO = new OneTimePadOperations();
            int[] Key = OTPO.GenerateKey();
            Assert.AreEqual(500, Key.Length);
        }

        [TestMethod]
        public void TestGenerateKey1000()
        {
            OneTimePadOperations OTPO = new OneTimePadOperations();
            int[] Key = OTPO.GenerateKey(1000);
            Assert.AreEqual(1000, Key.Length);
        }
        */


    }
}