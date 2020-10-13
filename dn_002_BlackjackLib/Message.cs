using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dn_002_BlackjackLib
{
    public enum MessageType
    {
        TakeCard,
        OpenCards,
        GiveCard
    }


    [Serializable]
    public class Message
    {
        public Card[] Cards { get; set; }
        public MessageType MessageType { get; set; }


        public override string ToString()
        {
            string[] cards = null;
            if (Cards != null)
            {
                cards = Array.ConvertAll(Cards, c => c.ToString());
            }

            return $"Msg: {MessageType} Cards: {(Cards == null ? "" : String.Join(", ", cards))}";
        }
    }
}
