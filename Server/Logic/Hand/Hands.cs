using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Cartas
{
    public sealed class Hands
    {
        public static readonly int CARDS = 5;
        public enum Type
        {
            HIGH_CARD,
            ONE_PAIR,
            TWO_PAIR,
            THREE_OF_A_KIND,
            STRAIGHT,
            FLUSH,
            FULL_HOUSE,
            FOUR_OF_A_KIND,
            STRAIGHT_FLUSH
        }

        private Hands()
        {

        }
    }
}
