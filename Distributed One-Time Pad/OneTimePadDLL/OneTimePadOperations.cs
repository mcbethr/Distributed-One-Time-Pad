using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.Json;
using System.Linq;
using System.Xml.Linq;
using static OneTimePadDLL.OneTimePadOperations;
using System.Threading.Channels;

namespace OneTimePadDLL
{
    public class OneTimePadOperations
    {
        public const int MaxNumberOfCharacters = 500;
        public DoublyLinkedList<string> ASCIILibrary { get; }

        /// <summary>
        /// In the future we should use Hexidecimal
        /// </summary>
        public OneTimePadOperations()
        {

            ///Create the linked list for the program
            ASCIILibrary = CreateAlphabetLinkedList();

        }


        public class DoublyLinkedListNode<T>
        {
            public T Value { get; set; }
            public DoublyLinkedListNode<T>? Previous { get; set; }
            public DoublyLinkedListNode<T>? Next { get; set; }

            public DoublyLinkedListNode(T value)
            {
                Value = value;
                Previous = null;
                Next = null;
            }


        }

        public class DoublyLinkedList<T>
        {
            public DoublyLinkedListNode<T>? Head { get; set; }
            public DoublyLinkedListNode<T>? Tail { get; set; }

            public void AddLast(T value)
            {
                DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(value);

                if (Head == null)
                {
                    Head = newNode;
                    Tail = newNode;
                }
                else
                {
                    Tail.Next = newNode;
                    newNode.Previous = Tail;
                    Tail = newNode;
                }
            }

            public void CloseLoop()
            {
                Tail.Next = Head;
            }

            private DoublyLinkedListNode<T> Find(char character)
            {
                DoublyLinkedListNode<T> currentNode = Head;
                while (currentNode != null)
                {
                    string nodeValue = currentNode.Value.ToString();
                    if (nodeValue.Contains(character))
                    {
                        return currentNode;
                    }
                    currentNode = currentNode.Next;
                }

                return null; // Character not found in the linked list
            }

            public char RetreatNodes(char Character, int n)
            {


                DoublyLinkedListNode<T> currentNode = Find(Character);
                for (int i = 0; i < n; i++)
                {



                    currentNode = currentNode.Previous;
                }

                Tail = currentNode;
                return Convert.ToChar(Head.Value);
            }

            public char AdvanceNodes(char Character, int n)
            {


                DoublyLinkedListNode<T> currentNode = Find(Character);
                for (int i = 0; i < n; i++)
                {



                    currentNode = currentNode.Next;
                }

                Head = currentNode;
                return Convert.ToChar(Head.Value);
            }


        }

        public DoublyLinkedList<string> CreateAlphabetLinkedList()
        {
            DoublyLinkedList<string> linkedList = new DoublyLinkedList<string>();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                linkedList.AddLast(c.ToString());
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                linkedList.AddLast(c.ToString());
            }

            for (char c = '0'; c <= '9'; c++)
            {
                linkedList.AddLast(c.ToString());
            }

            string punctuationMarks = "!@#$%^&*()-_=+[{]};:'\",<.>/?";
            foreach (char c in punctuationMarks)
            {
                linkedList.AddLast(c.ToString());
            }

            linkedList.CloseLoop();

            return linkedList;
        }

        public string EncryptMessage(string message, int[] ShiftArray)
        {
            string cypherText = string.Empty;
            int i = 0;
            foreach (char c in message)
            {
                char EncryptedText = EnryptCypher(c, ShiftArray[i]);
                cypherText = cypherText + EncryptedText;
                i++;
            }

            return cypherText;
        }

        public string DecryptMessage(string message, int[] ShiftArray)
        {
            string Message = string.Empty;
            int i = 0;
            foreach (char c in message)
            {
                char DecryptedText = DecryptCypher(c, ShiftArray[i]);
                Message = Message + DecryptedText;
                i++;
            }

            return Message;
        }


        public char EnryptCypher(char TextToEncrypt, int AmountToShift)
        {
            char CypherText = PerformCypherShiftEncryptionLL(TextToEncrypt,AmountToShift);



            return CypherText;

        }

        public char DecryptCypher(char TextToDecrypt, int AmountToShift)
        {
            char CypherText = PerformCypherShiftDecryptionLL(TextToDecrypt, AmountToShift);



            return CypherText;

        }

        public char PerformCypherShiftEncryptionLL(char StartingCharacter, int AmountToShift)
        {


            char FoundCharacter = ASCIILibrary.AdvanceNodes(StartingCharacter, AmountToShift);
            return FoundCharacter;

        }

        public char PerformCypherShiftDecryptionLL(char StartingCharacter, int AmountToShift)
        {


            char FoundCharacter = ASCIILibrary.RetreatNodes(StartingCharacter, AmountToShift);
            return FoundCharacter;

        }

 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaxCharacters">Optional Max Number of characters to generate. Defaults to 500</param>
        public int[] GenerateKey(int MaxCharacters = MaxNumberOfCharacters, byte[] ImageKey = null)
        {

            int[] Key = new int[MaxCharacters];

            for (int i= 0; i < MaxCharacters; i++)
            {

                //https://code-maze.com/csharp-random-double-range/
                using (var generator = RandomNumberGenerator.Create())
                {
                    var salt = new byte[4];
                    generator.GetBytes(salt);
                    int Value = Math.Abs(BitConverter.ToInt32(salt));

                    Key[i] = Value;
                }

            }

            return Key;

        }

        public OneTimePad GeneratePad(int SeriesNumber, int MaxCharacters, byte[]? ImageKey = null)
        {

            OneTimePad Pad = new OneTimePad();
            Pad.PadSeriesNumber = SeriesNumber;
            byte[] Key = new byte[MaxCharacters];

            if (ImageKey == null)
            {
                Pad.PadKey = GenerateKey(MaxCharacters);
            }
            else
            {
           
                Array.Copy(ImageKey, Key, MaxCharacters);
                Pad.PadKey = Array.ConvertAll(Key, c => (int)c);
            }

            return Pad;

        }

        public void SavePad(string filename,int series, int MaxCharacters)
        {
            OneTimePadOperations OTPO = new OneTimePadOperations();

            OneTimePad Pad = OTPO.GeneratePad(series, MaxCharacters);
            string jsonString = JsonSerializer.Serialize(Pad);
            File.WriteAllText(filename, jsonString);
        }
        
    }


}