using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePadDLL
{
    public class OneTimePad
    {

        public int PadSeriesNumber { get;}
        public int[] PadKey { get; }

        public OneTimePad(int _PadSeriesNumber, int[] _Padkey)
        {
            PadKey = _Padkey;
            PadSeriesNumber = _PadSeriesNumber;
        }


    }


}
