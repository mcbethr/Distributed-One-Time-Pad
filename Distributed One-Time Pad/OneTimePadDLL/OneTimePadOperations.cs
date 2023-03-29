using System.Security.Cryptography;

namespace OneTimePadDLL
{
    public class OneTimePadOperations
    {
        static int MaxLength = 38;
        const int MaxNumberOfCharacters = 500;
        private readonly char[] CharacterLibrary = new char[MaxLength];

        /// <summary>
        /// In the future we should use Hexidecimal
        /// </summary>
        public OneTimePadOperations()
        {
            CharacterLibrary[0] = '.';
            CharacterLibrary[1] = '1';
            CharacterLibrary[2] = '2';
            CharacterLibrary[3] = '3';
            CharacterLibrary[4] = '4';
            CharacterLibrary[5] = '5';
            CharacterLibrary[6] = '6';
            CharacterLibrary[7] = '7';
            CharacterLibrary[8] = '8';
            CharacterLibrary[9] = '9';
            CharacterLibrary[10] = '0';
            CharacterLibrary[11] = 'A';
            CharacterLibrary[12] = 'B';
            CharacterLibrary[13] = 'C';
            CharacterLibrary[14] = 'D';
            CharacterLibrary[15] = 'E';
            CharacterLibrary[16] = 'F';
            CharacterLibrary[17] = 'G';
            CharacterLibrary[18] = 'H';
            CharacterLibrary[19] = 'I';
            CharacterLibrary[20] = 'J';
            CharacterLibrary[21] = 'K';
            CharacterLibrary[22] = 'L';
            CharacterLibrary[23] = 'M';
            CharacterLibrary[24] = 'N';
            CharacterLibrary[25] = 'O';
            CharacterLibrary[26] = 'P';
            CharacterLibrary[27] = 'Q';
            CharacterLibrary[28] = 'R';
            CharacterLibrary[29] = 'S';
            CharacterLibrary[30] = 'T';
            CharacterLibrary[31] = 'U';
            CharacterLibrary[32] = 'V';
            CharacterLibrary[33] = 'W';
            CharacterLibrary[34] = 'X';
            CharacterLibrary[35] = 'Y';
            CharacterLibrary[36] = 'Z';
            CharacterLibrary[37] = ' ';


        }

        public string? EncryptMessage(string message, int[] ShiftArray)
        {
            string? cypherText = null;
            int i = 0;
            foreach (char c in message)
            {
                char EncryptedText = EnryptCypher(c, ShiftArray[i]);
                cypherText = cypherText + EncryptedText;
                i++;
            }

            return cypherText;
        }

        //public string DecryptMessage

        public char EnryptCypher(char TextToEncrypt, int AmountToShift)
        {
            char CypherText = PerformCypherShiftEncryption(TextToEncrypt,AmountToShift);



            return CypherText;

        }



        /// <summary>
        /// Performs the Cypher Shift, note that returning anything below 36 is bad and we 
        /// should throw a note informing the user of the error.  Should limit to stock prices above $100
        /// </summary>
        /// <param name="AmountToShift"></param>
        /// <returns></returns>
        public char PerformCypherShiftEncryption(char StartingCharacter, int AmountToShift)
        {

               int StartingCharacterLocation = Array.IndexOf(CharacterLibrary, StartingCharacter);

               int CypherPointer = StartingCharacterLocation;
               
               for (int i = 0; i < AmountToShift; i++) 
                {
                    if (CypherPointer < CharacterLibrary.Length)
                    {
                        CypherPointer++;
                    }
                    else
                    {
                        CypherPointer = 0;
                    }
                }
            return CharacterLibrary[CypherPointer];
        }

        public char PerformCypherShiftDecryption(char StartingCharacter, int AmountToShift)
        {

            int StartingCharacterLocation = Array.IndexOf(CharacterLibrary, StartingCharacter);

            int CypherPointer = StartingCharacterLocation;

            for (int i = 0; i < AmountToShift; i++)
            {
                if (CypherPointer > 0)
                {
                    CypherPointer--;
                }
                else
                {
                    CypherPointer = MaxLength;
                }
            }
            return CharacterLibrary[CypherPointer];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaxCharacters">Optional Max Number of characters to generate. Defaults to 500</param>
        public int[] GenerateKey(int MaxCharacters = MaxNumberOfCharacters)
        {

            int[] Key = new int[MaxCharacters];

            for (int i= 0; i < MaxCharacters; i++)
            {

                //https://code-maze.com/csharp-random-double-range/
                using (var generator = RandomNumberGenerator.Create())
                {
                    var salt = new byte[4];
                    generator.GetBytes(salt);
                    Key[i] = Math.Abs(BitConverter.ToInt32(salt));
                }

            }

            return Key;

        }

    }
}