using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePadDLL
{
    internal class OneTimePadGenerator
    {

        const int MaxNumberOfCharacters = 500;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaxCharacters">Optional Max Number of characters to generate. Defaults to 500</param>
        /// <param name="SeriesNumber">Required - needed to match pads </param>
        public OneTimePadGenerator(int SeriesNumber, int MaxCharacters = MaxNumberOfCharacters) {

            int[] Cypher = new int[MaxCharacters];

            //RandomNumberGenerator.Create();

        }


    }


}
