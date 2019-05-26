﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Cartas
{
    public enum numCarta
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
    }

    public enum tipoCarta
    {
        Club = 0, // ♣
        Diamond = 1, // ♦
        Heart = 2, // ♥
        Spade = 3 // ♠
    }
}
