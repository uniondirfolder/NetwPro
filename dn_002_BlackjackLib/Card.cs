using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dn_002_BlackjackLib
{
    public enum Suite
    {
        Clubs, Diamonds, Spades, Harts
    }

    [Serializable]
    public class Card
    {
        public Suite Suite { get; set; }
        public string  Value { get; set; }

        public override string ToString()
        {
            return $"{Suite}-{Value}";
        }
    }
}
